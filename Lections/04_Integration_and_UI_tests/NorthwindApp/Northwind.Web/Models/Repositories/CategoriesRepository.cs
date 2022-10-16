using Northwind.Model;

namespace Northwind.Web.Models.Repositories
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> GetAll();
        Category? GetById(int id);
        Category? SearchByName(string name);
        void Add(Category category);
        void UpdateOrAdd(Category category);
        void Delete(Category category);
        void SaveChanges();
    }

    public class CategoriesRepository : ICategoriesRepository
    {
        NorthwindContext northwindContext;

        public CategoriesRepository(NorthwindContext northwindContext) 
            => this.northwindContext = northwindContext;

        public void Add(Category category)
            => northwindContext.Categories.Add(category);

        public IEnumerable<Category> GetAll() =>
            northwindContext.Categories;

        public Category? GetById(int id)
            => northwindContext.Categories.SingleOrDefault(c => c.CategoryId == id);

        public Category? SearchByName(string name)
            => northwindContext.Categories.FirstOrDefault(c => c.CategoryName == name);

        public void UpdateOrAdd(Category category)
        {
            var categoryForUpdate = GetById(category.CategoryId);
            if (categoryForUpdate == null)
            {
                categoryForUpdate = new Category();
                Add(categoryForUpdate);
            }
            categoryForUpdate.CategoryName = category.CategoryName;
            categoryForUpdate.Description = category.Description;
            categoryForUpdate.Picture = category.Picture;
        }

        public void SaveChanges()
            => northwindContext.SaveChanges();

        public void Delete(Category category)
            => northwindContext.Categories.Remove(category);
    }
}
