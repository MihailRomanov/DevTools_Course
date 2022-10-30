using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Model;
using Northwind.Web.Models;

namespace Northwind.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly NorthwindContext context;

        public CategoriesController(NorthwindContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
              return View(await context
                  .Categories
                  .Select(c => c.ToViewModel())
                  .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Categories == null)
            {
                return NotFound();
            }

            var category = await context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category.ToViewModel());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Description,Picture")] CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(categoryViewModel.ToModel());
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.Categories == null)
            {
                return NotFound();
            }

            var category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Description,Picture")] CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(categoryViewModel.ToModel());
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Categories == null)
            {
                return NotFound();
            }

            var category = await context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category.ToViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Categories == null)
            {
                return Problem("Entity set 'NorthwindContext.Categories'  is null.");
            }
            var category = await context.Categories.FindAsync(id);
            if (category != null)
            {
                var haveProducts = context.Products.Where(p => p.Category == category).Any();
                
                if (haveProducts) 
                {
                    ModelState.AddModelError("", "Нельзя удалять категории с привязанными товарами!");
                    return View(category.ToViewModel());
                }
                context.Categories.Remove(category);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Image(int id)
        {
            var category = await context.Categories.FindAsync(id);

            if (category == null || category.Picture == null)
                return NotFound();

            return File(category.Picture, "image/jpeg");
        }

        private bool CategoryExists(int id)
        {
          return context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
