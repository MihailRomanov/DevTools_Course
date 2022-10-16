using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Northwind.Model;
using Northwind.Web.Models;
using Northwind.Web.Models.Repositories;

namespace Northwind.Web.Controllers
{
    public class Categories2Controller : Controller
    {
        private readonly ICategoriesRepository categoriesRepository;

        public Categories2Controller(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IActionResult Index()
        {
              return View("/Views/Categories/Index.cshtml", 
                  categoriesRepository
                  .GetAll()
                  .Select(c => c.ToViewModel()));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoriesRepository.GetById(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View("/Views/Categories/Details.cshtml", category.ToViewModel());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoryId,CategoryName,Description,Picture")] CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                categoriesRepository.Add(categoryViewModel.ToModel());
                categoriesRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("/Views/Categories/Create.cshtml", categoryViewModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoriesRepository.GetById(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View("/Views/Categories/Edit.cshtml", category.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CategoryId,CategoryName,Description,Picture")] CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    categoriesRepository.UpdateOrAdd(categoryViewModel.ToModel());
                    categoriesRepository.SaveChanges();
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
            return View("/Views/Categories/Edit.cshtml", categoryViewModel);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoriesRepository.GetById(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View("/Views/Categories/Delete.cshtml", category.ToViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = categoriesRepository.GetById(id);
            if (category != null)
            {
                categoriesRepository.Delete(category);
            }
            
            categoriesRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Image(int id)
        {
            var category = categoriesRepository.GetById(id);

            if (category == null || category.Picture == null)
                return NotFound();

            return File(category.Picture, "image/jpeg");
        }

        private bool CategoryExists(int id)
        {
          return categoriesRepository.GetById(id) != null;
        }
    }
}
