using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProject
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver("C:\\Users\\rartu\\source\\repos\\NUnitTestProject\\Drivers");
        }

        [Test]
        public void TestHomepage()
        {
            driver.Navigate().GoToUrl("http://qa1magento.dev.evozon.com/");
            driver.FindElement(By.CssSelector("#header > div > a")).Click();

            Assert.That(driver.Url, Is.EqualTo("http://qa1magento.dev.evozon.com/"));
        }

        [Test]
        public void TestAccount()
        {
            driver.Navigate().GoToUrl("http://qa1magento.dev.evozon.com/");
            IWebElement account = driver.FindElement(By.CssSelector("#header > div > div.skip-links > div > a"));
            account.Click();

            Assert.IsTrue(driver.FindElement(By.CssSelector(".skip-content.skip-active")).Displayed);
        }

        [Test]
        public void TestLanguage()
        {
            driver.Navigate().GoToUrl("http://qa1magento.dev.evozon.com/");
            IWebElement lang = driver.FindElement(By.CssSelector("#select-language"));
            SelectElement languages = new SelectElement(lang);
            languages.SelectByIndex(2);

            SelectElement selectLanguages = new SelectElement(driver.FindElement(By.CssSelector("#select-language")));

            Assert.That(selectLanguages.SelectedOption.Text, Is.EqualTo("German"));
        }

        [Test]
        public void TestSearch()
        {
            driver.Navigate().GoToUrl("http://qa1magento.dev.evozon.com/");
            IWebElement searchElement = driver.FindElement(By.CssSelector("div.input-box input"));
            searchElement.SendKeys("woman");
            searchElement.Submit();

            Assert.That(driver.Url, Is.EqualTo("http://qa1magento.dev.evozon.com/catalogsearch/result/?q=woman"));
        }

        [Test]
        public void TestNewProducts()
        {
            driver.Navigate().GoToUrl("http://qa1magento.dev.evozon.com/");

            driver.FindElement(By.CssSelector("#nav > .nav-primary > .level0.nav-1")).Click();
            WebElement elem = (WebElement)driver.FindElement(By.CssSelector("body > div > div > div.main-container.col1-layout > div > div.col-main > ul > li"));
            elem.Click();
            string element = driver.FindElement(By.CssSelector("body > div > div > div.main-container.col3-layout > div > div.col-wrapper > div.col-main > div.category-products > div.toolbar > div.pager > div > p")).Text;
            Assert.True(element.Contains("4"));
        }

        [Test]
        public void TestNavigation()
        {
            driver.Navigate().GoToUrl("http://qa1magento.dev.evozon.com/");
            IWebElement element = driver.FindElement(By.CssSelector(".level0.nav-5.parent"));
            element.Click();

            Assert.That(driver.Url, Is.EqualTo("http://qa1magento.dev.evozon.com/sale.html"));
        }

        [Test]
        public void TestAddToCart()
        {
            driver.Navigate().GoToUrl("http://qa1magento.dev.evozon.com/sale.html");

            driver.FindElement(By.CssSelector("#nav > ol > li.level0.nav-5.parent > a")).Click();
            driver.FindElement(By.CssSelector("div.col-main > div.category-products > ul > li:nth-child(2)")).Click();
            driver.FindElement(By.CssSelector("#swatch18")).Click();
            driver.FindElement(By.CssSelector("#swatch81")).Click();
            WebElement elem = (WebElement)driver.FindElement(By.CssSelector("#qty"));
            elem.Clear();
            elem.SendKeys("1");
            driver.FindElement(By.CssSelector("#product_addtocart_form > div.product-shop > div.product-options-bottom > div.add-to-cart > div.add-to-cart-buttons > button")).Click();

            Assert.IsTrue(driver.FindElement(By.CssSelector(".product-cart-image")).Displayed);
        }

        [Test]
        public void TestRemoveFromCart()
        {
            driver.Navigate().GoToUrl("http://qa1magento.dev.evozon.com");
            driver.FindElement(By.CssSelector("#nav > ol > li.level0.nav-5.parent > a")).Click();
            driver.FindElement(By.CssSelector("div.col-main > div.category-products > ul > li:nth-child(2)")).Click();
            driver.FindElement(By.CssSelector("#swatch18")).Click();
            driver.FindElement(By.CssSelector("#swatch81")).Click();
            driver.FindElement(By.CssSelector("#product_addtocart_form > div.product-shop > div.product-options-bottom > div.add-to-cart > div.add-to-cart-buttons > button")).Click();
            driver.Navigate().Back();
            driver.Navigate().Back();
            driver.FindElement(By.CssSelector("div.col-main > div.category-products > ul > li:nth-child(3) > a")).Click();
            driver.FindElement(By.CssSelector("#swatch27")).Click();
            driver.FindElement(By.CssSelector("#swatch81")).Click();
            driver.FindElement(By.CssSelector("#product_addtocart_form > div.product-shop > div.product-options-bottom > div.add-to-cart > div.add-to-cart-buttons > button")).Click();
            driver.FindElement(By.CssSelector("#shopping-cart-table > tbody > tr.first.odd > td.a-center.product-cart-remove.last > a")).Click();

            Assert.IsTrue(driver.FindElements(By.CssSelector(".product-cart-image")).Count == 1);
        }

        [Test]
        public void TestReview()
        {
            driver.Navigate().GoToUrl("http://qa2magento.dev.evozon.com/");

            driver.FindElement(By.CssSelector("#nav > ol > li.level0.nav-5.parent > a")).Click();
            driver.FindElement(By.CssSelector("div.col-main > div.category-products > ul > li:nth-child(2)")).Click();
            driver.FindElement(By.CssSelector("ul.toggle-tabs > li.last")).Click();
            driver.FindElement(By.CssSelector("p.no-rating > a")).Click();

            driver.FindElement(By.CssSelector("#Quality_1")).Click();
            driver.FindElement(By.CssSelector("#Price_2")).Click();
            driver.FindElement(By.CssSelector("#Value_3")).Click();

            IWebElement review = driver.FindElement(By.CssSelector("#review_field"));
            review.SendKeys("Review content");

            IWebElement summary = driver.FindElement(By.CssSelector("#summary_field"));
            summary.SendKeys("Summary review");

            IWebElement name = driver.FindElement(By.CssSelector("#nickname_field"));
            name.SendKeys("Name");

            driver.FindElement(By.CssSelector("div.buttons-set button[type=submit]")).Click();

            Assert.IsTrue(driver.FindElement(By.CssSelector("li.success-msg span")).Displayed);
        }

        [Test]
        public void TestRegister()
        {
            driver.Navigate().GoToUrl("http://qa1magento.dev.evozon.com/");
            driver.FindElement(By.CssSelector(".skip-link.skip-account")).Click();
            driver.FindElement(By.CssSelector("#header-account > div > ul > li:nth-child(5) > a")).Click();

            IWebElement firstName = driver.FindElement(By.CssSelector("#firstname"));
            firstName.SendKeys("asd");

            IWebElement midName = driver.FindElement(By.CssSelector("#middlename"));
            midName.SendKeys("asddas");

            IWebElement lastName = driver.FindElement(By.CssSelector("#lastname"));
            lastName.SendKeys("das");

            IWebElement email = driver.FindElement(By.CssSelector("#email_address"));
            email.SendKeys("asbbbbb@yahoo.com");

            IWebElement password = driver.FindElement(By.CssSelector("#password"));
            password.SendKeys("asdasd");

            IWebElement confirmPassword = driver.FindElement(By.CssSelector("#confirmation"));
            confirmPassword.SendKeys("asdasd");

            IWebElement registerButton = driver.FindElement(By.CssSelector("#form-validate > div.buttons-set > button"));
            registerButton.Click();

            Assert.That(driver.Url, Is.EqualTo("http://qa1magento.dev.evozon.com/customer/account/index/"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}