using HtmlElements.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Northwind.Web.Tests.SeleniumTests.Pages
{
    public class CreateCategoryPage : HtmlPage
    {
        [FindsBy(How = How.Id, Using = "CategoryName")]
        private HtmlInput categoryName;

        [FindsBy(How = How.Id, Using = "Description")]
        private HtmlInput description;

        [FindsBy(How = How.Id, Using = "Picture")]
        private HtmlInput picture;

        [FindsBy(How = How.CssSelector, Using = "input[type='submit']")]
        private HtmlInput createButton;

        [FindsBy(How = How.LinkText, Using = "Вернуться к списку")]
        private HtmlLink backLink;

        public CreateCategoryPage(ISearchContext webDriverOrWrapper) : base(webDriverOrWrapper)
        {
        }

        public string CategoryName
        {
            get { return categoryName.Value; }
            set { categoryName.SendKeys(value); }
        }

        public string Description
        {
            get { return description.Value; }
            set { description.SendKeys(value); }
        }

        public void AddPictureFile(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException("File not exist", nameof(path));
            picture.SendKeys(path);
        }

        public CategoryListPage CreateAndGoToList()
        {
            createButton.Click();
            return PageObjectFactory.Create<CategoryListPage>(this);
        }

        public CategoryListPage ReturnToList()
        {
            backLink.Click();
            return PageObjectFactory.Create<CategoryListPage>(this);
        }
    }
}
