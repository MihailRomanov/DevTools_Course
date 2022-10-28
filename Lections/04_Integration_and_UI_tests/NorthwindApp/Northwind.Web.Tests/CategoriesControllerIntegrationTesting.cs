using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Model;
using Microsoft.Extensions.DependencyInjection;
using AngleSharp;
using AngleSharp.Dom;
using FluentAssertions;
using Northwind.Web.Tests.TestDataGenerators;

namespace Northwind.Web.Tests
{
    [TestClass]
    public class CategoriesControllerIntegrationTesting
    {
        [TestMethod]
        public async Task Index_ReturnViewResult_WithAllCategories_InMemoryEF()
        {
            var context = NorthwindContextHelpers.GetInMemoryContext();
            var categoryGenerator = new CategoryGenerator(context);
            var categories = categoryGenerator.Generate(10).ToList();

            context.SaveChanges();

            var client = GetTestHttpClient(context);
            var response = await client.GetStringAsync("/categories");

            var result = GetResultCategories(response).ToList();
            result.Should().BeEquivalentTo(categories, 
                options => options
                    .Excluding(c => c.Products)
                    .Excluding(c => c.Picture));
        }

        [TestMethod]
        public async Task Index_ReturnViewResult_WithAllCategories_InMemorySQLite()
        {
            var context = NorthwindContextHelpers.GetSqliteContext();
            var categoryGenerator = new CategoryGenerator(context);
            var categories = categoryGenerator.Generate(10).ToList();

            context.SaveChanges();

            HttpClient client = GetTestHttpClient(context);
            var response = await client.GetStringAsync("/categories");

            var result = GetResultCategories(response).ToList();
            result.Should().BeEquivalentTo(categories,
                options => options
                    .Excluding(c => c.Products)
                    .Excluding(c => c.Picture));
        }

        [TestMethod]
        public async Task Index_ReturnViewResult_WithAllCategories_RealDb()
        {
            var client = GetTestHttpClient();
            var response = await client.GetStringAsync("/categories");

            var result = GetResultCategories(response).ToList();
            var names = new[] {
                "Beverages", "Condiments", "Confections", "Dairy Products",
                "Grains/Cereals", "Meat/Poultry", "Produce", "Seafood" };
            var categories = names
                .Select((n, i) => new Category { CategoryId = i + 1, CategoryName = n });

            result.Should().BeEquivalentTo(categories,
                options => options
                    .Excluding(c => c.Products)
                    .Excluding(c => c.Picture)
                    .Excluding(c=> c.Description));
        }

        private static HttpClient GetTestHttpClient(NorthwindContext? context = null)
        {
            var factory = new WebApplicationFactory<Program>();
            if (context != null)
            {
                factory = factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddScoped<NorthwindContext>(services => context);
                    });
                });
            }

            var client = factory.CreateClient();
            return client;
        }

        private static IEnumerable<Category> GetResultCategories(string htmlSource)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var document = context.OpenAsync(req => req.Content(htmlSource)).Result;

            foreach (var categoryRow in document.QuerySelectorAll("tr[data-tid|='category-row']"))
            {
                var id = categoryRow.GetAttribute("data-tid")?.Split("-").Last();
                var name = categoryRow.QuerySelector("td[data-tid='category-name']")?.Text().Trim();
                var description = categoryRow.QuerySelector("td[data-tid='category-description']")?.Text().Trim();

                yield return new Category
                {
                    CategoryId = int.Parse(id ?? "-1"),
                    CategoryName = name ?? "",
                    Description = description,
                    Picture = null,
                };
            }
        }
    }
}
