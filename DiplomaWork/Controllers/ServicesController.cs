using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomaWork1.Data;
using DiplomaWork1.Data.Models;
using DiplomaWork1.Models.Category;
using DiplomaWork1.Models.Constants;
using DiplomaWork1.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DiplomaWork1.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    public class ServicesController : Controller
    {
        private readonly DiplomaWorkContext _db;

        public ServicesController(DiplomaWorkContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Guid? categoryId = null)
        {
            var services = await _db.Services
                .Include(x => x.Category)
                .Where(x => !categoryId.HasValue || x.CategoryId == categoryId.Value)
                .ToListAsync();
            return View(new ServicesModel
            {
                CategoryId = categoryId,
                Services = services.Select(x => new ServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = new CategoryModel
                    {
                        Id = x.CategoryId,
                        Name = x.Category.Name
                    }
                }).ToArray() 
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create([FromQuery] Guid? categoryId)
        {
            var categories = await _db.Categories.ToListAsync();
            if (categoryId.HasValue && !categories.Any(x => x.Id == categoryId.Value))
            {
                return RedirectToAction(nameof(Index));
            }
            ;
            return View(new ServiceModel
            {
                CategoryId = categoryId,
                Categories = categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var service = await _db.Services.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (service == null)
                return RedirectToAction(nameof(Index));

            return View(nameof(Create), new ServiceModel
            {
                Id = id,
                Categories = new List<SelectListItem> {
                    new SelectListItem { Value = service.CategoryId.ToString(), Text = service.Category.Name }
                },
                CategoryId = service.CategoryId,
                Name = service.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ServiceModel serviceModel)
        {
            var service = await _db.Services.FirstOrDefaultAsync(x => serviceModel.Id.HasValue ? x.Id == serviceModel.Id.Value : false);
            if (service == null)
            {
                if (await _db.Services.AnyAsync(x => string.Equals(x.Name, serviceModel.Name, StringComparison.InvariantCultureIgnoreCase) && x.CategoryId == serviceModel.CategoryId))
                {
                    ModelState.AddModelError(string.Empty, "Такая услуга уже существует");
                    return View(serviceModel);
                }

                service = new Service
                {
                    Id = Guid.NewGuid(),
                    CategoryId = serviceModel.CategoryId.Value,
                    Name = serviceModel.Name
                };
                _db.Services.Add(service);
                await _db.SaveChangesAsync();
            }
            else
            {
                service.Name = serviceModel.Name;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] Guid id, [FromForm] Guid? categoryId)
        {
            var service = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (service != null)
            {
                _db.Remove(service);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { categoryId = categoryId });
        }
    }
}
