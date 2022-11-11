using HtmlElements.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Northwind.Web.Tests.SeleniumTests.Pages
{
    public class CategoryListPage: HtmlPage
    {
        [FindsBy(How = How.LinkText, Using = "Create New")]
        private HtmlLink createNewLink;

        public CategoryListPage(ISearchContext webDriverOrWrapper) : base(webDriverOrWrapper)
        {
        }

        public IList<CategoryRowItem> Categories { get; private set; }

        public CreateCategoryPage GoToCreateNewCategoryPage()
        {
            createNewLink.Click();
            return PageObjectFactory.Create<CreateCategoryPage>(this);
        }
    }

    [ElementLocator(How = How.CssSelector, Using = "tr[data-tid|='category-row']")]
    public class CategoryRowItem : HtmlElement
    {
        [FindsBy(How = How.CssSelector, Using = "td[data-tid='category-name']")]
        private HtmlLabel categoryName;

        [FindsBy(How = How.CssSelector, Using = "td[data-tid='category-description']")]
        private HtmlLabel description;

        [FindsBy(How = How.CssSelector, Using = "td[data-tid='category-picture'] > img")]
        private HtmlImage picture;

        public CategoryRowItem(IWebElement webElement) : base(webElement) { }

        public int CategoryId => int.Parse(GetDomAttribute("data-tid")?.Split("-").Last() ?? "-1");
        public string CategoryName => categoryName.Text;
        public string Description => description.Text;      
    }
}
