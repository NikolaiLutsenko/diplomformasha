using DiplomaWork.Data;
using DiplomaWork.Data.Models;
using DiplomaWork.Interfaces;
using DiplomaWork.Models.Requests;
using DiplomaWork.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DiplomaWork.Services
{
    public class RequestService : IRequestService
    {
        private readonly DiplomaWorkContext _db;
        private readonly IRequestNotifier _requestNotifier;

        public RequestService(DiplomaWorkContext db, IRequestNotifier requestNotifier)
        {
            _db = db;
            _requestNotifier = requestNotifier;
        }

        public async Task<IEnumerable<RequestModel>> GetRequestsAsync(Guid? employeeId = null, RequestsFlag flags = RequestsFlag.None)
        {
            var expression = RequestExpresionBuilder
                    .Create(employeeId, flags)
                    .TryBuildForEmployeeId()
                    .TryBuildForUnnassigned()
                    .TryBuildNotWaitingQualityControl()
                    .Build();

            var requests = await _db.Requests
                .Include(x => x.CurrentEmployee)
                .Include(x => x.States)
                .ThenInclude(state => state.User)
                .Include(x => x.Service)
                .ThenInclude(x => x.Category)
                .Where(expression)
                .OrderByDescending(x => x.CreatedDate)
                .ToArrayAsync();

            return requests
                .Select(ToRequestModel);
        }

        public async Task SetStateAsync(SetStateModel model, Guid employeeId)
        {
            var request = await _db.Requests.FindAsync(model.RequestId);

            request.StateType = ToRequestStateType(model);

            _db.RequestStates.Add(new RequestState
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                Description = model.Description,
                State = model.State,
                RequestId = model.RequestId,
                UserId = employeeId
            });

            await _db.SaveChangesAsync();

            await _requestNotifier.NotifyAsync(request.Id, request.StateType);

            static RequestStateType ToRequestStateType(SetStateModel model)
            {
                if (model.IsCompleted)
                    return RequestStateType.Completed;
                else if (model.ToQualityControl)
                    return RequestStateType.WaitingQualityControl;
                else if (model.ToTechnicalSpecialist)
                    return RequestStateType.Returned;
                else
                    return RequestStateType.InProgress;
            }
        }

        private static RequestModel ToRequestModel(Request request)
        {
            return new RequestModel
            {
                Id = request.Id,
                ServiceName = request.Service.Name,
                CategoryName = request.Service.Category.Name,
                CreatedAt = request.CreatedDate,
                UserEmail = request.UserEmail,
                Badget = new ShowBadgeModel(request.StateType),
                CurrentEmployeeId = request.CurrentEmployeeId,
                States = request.States
                .OrderByDescending(x => x.CreatedDate)
                .Select(s => new RequestStateModel
                {
                    CreatedDate = s.CreatedDate,
                    State = s.State,
                    Description = s.Description,
                    EmployeeEmail = s.User.Email
                })
            };
        }

        public async Task<bool> TryCreateRequestAsync(CreateRequesModel model)
        {
            var service = await _db.Services
                    .Include(x => x.UserServices)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Requests)
                    .FirstOrDefaultAsync(x => x.Id == model.ServiceId);
            if (service == null)
                return false;
            Guid? employeeId = null;
            if (service.UserServices.Any(x => x.User != null))
                employeeId = service
                    .UserServices
                    .OrderBy(x => x.User.Requests.Count())
                    .First().UserId;
            var request = new Request
            {
                Id = Guid.NewGuid(),
                Description = model.Description,
                UserEmail = model.UserEmail,
                UserPhone = model.UserPhone,
                UserName = model.UserName,
                ServiceId = model.ServiceId,
                CurrentEmployeeId = employeeId,
                CreatedDate = DateTime.UtcNow,
                StateType = RequestStateType.InProgress
            };
            _db.Requests.Add(request);

            await _db.SaveChangesAsync();

            await _requestNotifier.NotifyAsync(request.Id, request.StateType);

            return true;
        }

        private class RequestExpresionBuilder
        {
            RequestExpresionBuilder(Guid? employeeId, RequestsFlag flags)
            {
                _employeeId = employeeId;
                _flags = flags;
            }

            public RequestExpresionBuilder TryBuildForEmployeeId()
            {
                if (_employeeId.HasValue)
                {
                    var employeeIdConstant = Expression.Constant(_employeeId.Value, typeof(Guid?));
                    var expression = Expression.Equal(currentEmployeeMember, employeeIdConstant);
                    prevExpression = prevExpression == null ? expression : Expression.OrElse(prevExpression, expression);
                }

                return this;
            }

            public RequestExpresionBuilder TryBuildForUnnassigned()
            {
                if (_flags.HasFlag(RequestsFlag.Unnassigned))
                {
                    var unnassignedEmployeeIdConstant = Expression.Constant(null, typeof(Guid?));
                    var expression = Expression.Equal(currentEmployeeMember, unnassignedEmployeeIdConstant);
                    prevExpression = prevExpression == null ? expression : Expression.OrElse(prevExpression, expression);
                }

                return this;
            }

            public RequestExpresionBuilder TryBuildNotWaitingQualityControl()
            {
                if (_flags.HasFlag(RequestsFlag.IsNotWaitingQualityControl))
                {
                    var isWaitingQualityControlMember = Expression.Property(pr, nameof(Request.StateType));
                    var waitingQualityControl = Expression.Constant(RequestStateType.WaitingQualityControl);
                    var expression = Expression.NotEqual(isWaitingQualityControlMember, waitingQualityControl);
                    prevExpression = prevExpression == null ? expression : Expression.OrElse(prevExpression, expression);
                }

                return this;
            }

            public Expression<Func<Request, bool>> Build()
            {
                prevExpression = prevExpression ?? Expression.Equal(Expression.Constant(1), Expression.Constant(1));
                return Expression.Lambda<Func<Request, bool>>(prevExpression, new[] { pr });
            }

            public static RequestExpresionBuilder Create(Guid? employeeId, RequestsFlag flags) => new RequestExpresionBuilder(employeeId, flags);

            private readonly Guid? _employeeId;
            private readonly RequestsFlag _flags;
            private BinaryExpression prevExpression;
            private static readonly ParameterExpression pr = Expression.Parameter(typeof(Request), "x");
            private static readonly MemberExpression currentEmployeeMember = Expression.Property(pr, nameof(Request.CurrentEmployeeId));
        }
    }
}
