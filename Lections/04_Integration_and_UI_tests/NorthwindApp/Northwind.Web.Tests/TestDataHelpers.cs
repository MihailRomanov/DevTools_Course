using Bogus;
using Northwind.Model;

namespace Northwind.Web.Tests
{
    public static class TestDataHelpers
    {
        public static IEnumerable<Category> GenerateCategories(int count)
        {
            var categoryGenerator = new Faker<Category>()
                .StrictMode(false)
                .RuleFor(c => c.CategoryName, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                .RuleFor(c => c.Picture, f => f.Image.Random.Bytes(10));

            for (var i = 0; i < count; i++)
                yield return categoryGenerator.Generate();

        }
    }
}