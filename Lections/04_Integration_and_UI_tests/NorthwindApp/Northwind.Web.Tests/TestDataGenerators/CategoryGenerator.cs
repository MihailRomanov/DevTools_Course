using Bogus;
using Northwind.Model;

namespace Northwind.Web.Tests.TestDataGenerators
{
    public class CategoryGenerator: ITestDataGenerator<Category>
    {
        private readonly NorthwindContext northwindContext;
        private Faker<Category> faker;

        public CategoryGenerator(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;
            faker = new Faker<Category>()
                .StrictMode(false)
                .RuleFor(c => c.CategoryName, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                .RuleFor(c => c.Picture, f => f.Image.Random.Bytes(10));
        }

        public CategoryGenerator WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name),
                    "Явно указанное имя категории не должно быть пустым");

            faker.RuleFor(c => c.CategoryName, () => name);
            return this;
        }

        public CategoryGenerator WithDescription(string description)
        {
            faker.RuleFor(c => c.Description, () => description);
            return this;
        }

        public CategoryGenerator WithPicture(byte[] picture)
        {
            faker.RuleFor(c => c.Picture, () => picture);
            return this;
        }

        public CategoryGenerator WithProducts(params Product[] products)
        {
            faker.RuleFor(c => c.Products, () => products);
            return this;
        }

        public CategoryGenerator WithProduct(ITestDataGenerator<Product> generator, 
            int count = 1)
        {
            faker.RuleFor(c => c.Products, () => generator.Generate(count));
            return this;
        }


        public Category Generate()
        {
            var category = faker.Generate();
            return northwindContext.Categories.Add(category).Entity;
        }

        public IEnumerable<Category> Generate(int count)
        {
            var categories = faker.Generate(count);
            northwindContext.Categories.AddRange(categories);
            return categories;
        }
    }
}
