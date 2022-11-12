using HtmlElements.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Northwind.Web.Tests.SeleniumTests.Pages
{
    public class MainPage : HtmlPage
    {
        [FindsBy(How = How.CssSelector, Using = "a[href*='Categories'].nav-link")]
        private HtmlLink categoriesLink;

        public MainPage(ISearchContext webDriverOrWrapper) : base(webDriverOrWrapper)
        {
        }

        public CategoryListPage GoToCategoriesListPage()
        {
            categoriesLink.Click();
            return PageObjectFactory.Create<CategoryListPage>(this);
        }
    }
}
