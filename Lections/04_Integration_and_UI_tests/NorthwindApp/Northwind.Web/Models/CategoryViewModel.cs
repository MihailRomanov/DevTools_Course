using Northwind.Model;

namespace Northwind.Web.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public string? Description { get; set; }
        public IFormFile? Picture { get; set; }
    }

    public static class CategoryViewModelExtensions 
    {
        public static CategoryViewModel ToViewModel(this Category category)
        {
            return new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description,
                Picture = null,
            };
        }

        public static Category ToModel(this CategoryViewModel model)
        {
            var category = new Category
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                Description = model.Description,
                Picture = null
            };

            if (model.Picture != null && model.Picture.Length != 0)
            {
                using var memoryStream = new MemoryStream();
                model.Picture.CopyTo(memoryStream);
                category.Picture = memoryStream.ToArray();
            }

            return category;
        }

    }
}
