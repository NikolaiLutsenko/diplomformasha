using System;
using System.Threading.Tasks;
using DiplomaWork.Data;
using DiplomaWork.Data.Models;
using DiplomaWork.Models.Category;
using DiplomaWork.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomaWork.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    public class CategoryController : Controller
    {
        private readonly DiplomaWorkContext _db;

        public CategoryController(DiplomaWorkContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _db.Categories.Include(x => x.Services).ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateModel createCategory)
        {
            if (ModelState.IsValid)
            {
                if (await _db.Categories.AnyAsync(x => !createCategory.Id.HasValue && string.Equals(x.Name, createCategory.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    ModelState.AddModelError(string.Empty, "Такая категория уже существует");

                    return View(createCategory);
                }
                else if (!createCategory.Id.HasValue)
                {
                    _db.Categories.Add(new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = createCategory.Name
                    });

                    await _db.SaveChangesAsync();
                }
                else if (createCategory.Id.HasValue)
                {
                    var existingCategory = await _db.Categories.FirstAsync(x => x.Id == createCategory.Id);
                    existingCategory.Name = createCategory.Name;

                    await _db.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(createCategory);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return View(nameof(Create), new CreateModel { Id = category.Id, Name = category?.Name });
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] Guid id)
        {
            var categoryToDelete = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (categoryToDelete != null)
            {
                _db.Remove(categoryToDelete);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
