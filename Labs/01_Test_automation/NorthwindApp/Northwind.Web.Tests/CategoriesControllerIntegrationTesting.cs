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
        private const string AspNetVerificationTokenName = "__RequestVerificationToken";

        [TestMethod]
        public async Task Index_ReturnViewResult_WithAllCategories()
        {
            // Создаем пустой контекст EF в памяти и заносим 10 категорий
            var context = NorthwindContextHelpers.GetInMemoryContext();
            var categoryGenerator = new CategoryGenerator(context);
            var categories = categoryGenerator.Generate(10).ToList();
            context.SaveChanges();

            // Запускаем наше приложение на основе созданного и заполненного 
            // контекста и получаем HTTP клиент, который будет к этому приложению
            // обращаться
            var client = GetTestHttpClient(context);

            // Делаем GET запрос к списку категорий
            var response = await client.GetStringAsync("/categories");

            // Парсим полученную HTML, достаем данные о категориях
            var result = GetResultCategories(response).ToList();

            // Сверяем полученные в запросе и созданные ранее категории
            result.Should().BeEquivalentTo(categories, 
                options => options
                    .Excluding(c => c.Products)
                    .Excluding(c => c.Picture));
        }

        [TestMethod]
        public async Task Create_AddNewCategory_WithoutPicture_AsUrlEncodedForm_And_RedirectToList()
        {
            // Создаем пустой контекст и 1 категорию, но в базу её не сохраняем
            var context = NorthwindContextHelpers.GetInMemoryContext();
            var category = new CategoryGenerator().Generate();

            // Запускаем приложение и получаем клиент, но с опцией, что он
            // не будет автоматически выполнять Redirect (чтобы мы могли проверить реальный
            // ответ на наше запрос)
            var client = GetTestHttpClient(context, 
                new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

            // Обращаемся к форме создания новой категории, только чтобы получить
            // верификационный токен
            var createForm = await client.GetStringAsync("/categories/create");
            var verificationToken = GetRequestVerificationToken(createForm);

            // Формируем запрс, как если бы отправлялась ранее полученная форма
            var formContent = new FormUrlEncodedContent(
                new Dictionary<string, string> { 
                    [nameof(Category.CategoryName)] = category.CategoryName,
                    [nameof(Category.Description)] = category.Description,
                    [AspNetVerificationTokenName] = verificationToken
                });

            // Получаем ответ и достаем из базы только что созданную категорию
            var response = await client.PostAsync("/categories/create", formContent);
            var newCategory = context.Categories.First();

            // Проверяем, что в качестве ответа нам пришел редирект на список категорий
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Redirect);
            response.Headers.Location.Should().Be("/Categories");

            // Проверяем, что категория из базы совпадает с тестовой
            newCategory.Should().BeEquivalentTo(category,
                options => options
                    .Including(c => c.CategoryName)
                    .Including(c => c.Description));
        }

        [TestMethod]
        public async Task Create_AddNewCategory_WithPicture_AsMultipartForm_And_RedirectToList()
        {
            // Всё делаем аналогично тесту выше, кроме формирования запроса
            var context = NorthwindContextHelpers.GetInMemoryContext();
            var category = new CategoryGenerator().Generate();

            var client = GetTestHttpClient(context,
                new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            var createForm = await client.GetStringAsync("/categories/create");
            var verificationToken = GetRequestVerificationToken(createForm);

            // Чтобы можно было отправить сразу тело файла картинки исползуем
            // multipart/form-data запрос
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(category.CategoryName), nameof(Category.CategoryName));
            multipartContent.Add(new StringContent(category.Description), nameof(Category.Description));
            multipartContent.Add(new StringContent(verificationToken), AspNetVerificationTokenName);
            multipartContent.Add(new ByteArrayContent(category.Picture), "Picture", "picture.jpg");

            // Получаем и счверяем результат как в предыдущем тесте, только сверяем еще и картинку 
            var response = await client.PostAsync("/categories/create", multipartContent);
            var newCategory = context.Categories.First();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Redirect);
            response.Headers.Location.Should().Be("/Categories");
            newCategory.Should().BeEquivalentTo(category,
                options => options
                    .Including(c => c.CategoryName)
                    .Including(c => c.Description)
                    .Including(c => c.Picture));
        }

        private static HttpClient GetTestHttpClient(
            NorthwindContext? context = null,
            WebApplicationFactoryClientOptions? clientOptions = null
            )
        {
            var factory = new WebApplicationFactory<Program>();
            if (context != null)
            {
                factory = factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddSingleton<NorthwindContext>(services => context);
                    });
                });
            }

            var client = clientOptions != null
                ? factory.CreateClient(clientOptions)
                : factory.CreateClient();

            return client;
        }
        
        private static string GetRequestVerificationToken(string htmlSource)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var document = context.OpenAsync(req => req.Content(htmlSource)).Result;
            
            return document?
                .QuerySelector($"input[name='{AspNetVerificationTokenName}']")?
                .GetAttribute("value") ?? "";
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
