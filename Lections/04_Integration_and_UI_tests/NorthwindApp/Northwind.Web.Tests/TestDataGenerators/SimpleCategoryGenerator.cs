using Northwind.Model;

namespace Northwind.Web.Tests.TestDataGenerators
{
    public class SimpleCategoryGenerator
    {
        private int id = 1;
        private string? name = null;
        private string? description = null;
        private byte[]? picture = null;

        public SimpleCategoryGenerator StartFromId(int id)
        {
            this.id = id;
            return this;
        }

        public SimpleCategoryGenerator WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name),
                    "Явно указанное имя категории не должно быть пустым");

            this.name = name;
            return this;
        }

        public SimpleCategoryGenerator WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public SimpleCategoryGenerator WithPicture(byte[] picture)
        {
            this.picture = picture;
            return this;
        }

        public Category Generate()
        {
            return new Category
            {
                CategoryId = id++,
                CategoryName = name ?? $"Категория {id}",
                Description = description,
                Picture = picture
            };
        }
    }
}
