using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Northwind.Model;
using Northwind.Web.Controllers;
using Northwind.Web.Models;
using Northwind.Web.Models.Repositories;

namespace Northwind.Web.Tests
{
    [TestClass]
    public class CategoriesControllerUnitTests
    {
        [TestMethod]
        public void Index_ReturnViewResult_WithAllCategories_RepositoryVersion()
        {
            var mockCategoryRepository = new Mock<ICategoriesRepository>();
            mockCategoryRepository.Setup(rep => rep.GetAll())
                .Returns(GetCategories(5));
            var controller = new Categories2Controller(mockCategoryRepository.Object);


            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var model = ((ViewResult)result).ViewData.Model;
            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(model, typeof(IEnumerable<CategoryViewModel>));
            var categories = ((IEnumerable<CategoryViewModel>)model).ToList();

            Assert.AreEqual(5, categories.Count);

        }


        private IEnumerable<Category> GetCategories(int count)
        {
            var categoryGenerator = new Faker<Category>()
                .StrictMode(false)
                .RuleFor(c => c.CategoryId, f => f.UniqueIndex)
                .RuleFor(c => c.CategoryName, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                .RuleFor(c => c.Picture, f => f.Image.Random.Bytes(10));

            for (var i = 0; i < count; i++)
                yield return categoryGenerator.Generate();
                
        }
    }
}