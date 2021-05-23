using DiplomaWork1.Data;
using DiplomaWork1.Data.Models;
using DiplomaWork1.Interfaces;
using DiplomaWork1.Models.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DiplomaWork1.Services
{
    public class RequestService : IRequestService
    {
        private readonly DiplomaWorkContext _db;

        public RequestService(DiplomaWorkContext db)
        {
            _db = db;
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

        private static RequestModel ToRequestModel(Request request)
        {
            return new RequestModel
            {
                Id = request.Id,
                ServiceName = request.Service.Name,
                CategoryName = request.Service.Category.Name,
                CreatedAt = request.CreatedDate,
                UserEmail = request.UserEmail,
                Badget = new ShowBadgeModel(request.IsCompleted, request.IsReturned, request.IsWaitingQualityControl),
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
                    var isWaitingQualityControlMember = Expression.Property(pr, nameof(Request.IsWaitingQualityControl));
                    var falseConstant = Expression.Constant(false);
                    var expression = Expression.Equal(isWaitingQualityControlMember, falseConstant);
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
