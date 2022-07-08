using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

namespace AppiumMobileContactBook
{
    public class ContactBookTests
    {
        private const string AppiumServerUri = "http://[::1]:4723/wd/hub";
        private const string ContactBookPath = @"C:\Users\Vistauser\Desktop\contactbook-androidclient.apk";
        private const string ApiServiceUri = "https://contactbook.nakov.repl.co/api";
        private AndroidDriver<AndroidElement> driver;
        private WebDriverWait wait;


        [SetUp]
        public void Setup()
        {
            var options = new AppiumOptions() { PlatformName = "Android" };
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumServerUri), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.CloseApp();
        }



        [Test]
        public void Vialid_Contact()
        {
            var ApiUrl = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
            ApiUrl.Clear();
            ApiUrl.SendKeys(ApiServiceUri);

            var conectionApiButton = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            conectionApiButton.Clear();
            conectionApiButton.Click();

            var surchfield = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            surchfield.Clear();
            surchfield.SendKeys("Steve");

            var surchbutton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            surchbutton.Click();

            var FirstName = driver.FindElementById("contactbook.androidclient:id/textViewFirstName");

            var LastName = driver.FindElementById("contactbook.androidclient:id/textViewLastName");

            Assert.That(FirstName.Text, Is.EqualTo("Steve"));
            Assert.That(LastName.Text, Is.EqualTo("Jobs"));

            var contactNum = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            Assert.That(contactNum.Text, Is.EqualTo("Contacts found: 1"));




        }
        [Test]
        public void Vialid_Multiple_Contacts()
        {
            var ApiUrl = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
            ApiUrl.Clear();
            ApiUrl.SendKeys(ApiServiceUri);

            var conectionApiButton = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            conectionApiButton.Clear();
            conectionApiButton.Click();

            var surchfield = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            surchfield.Clear();
            surchfield.SendKeys("e");

            var surchbutton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            surchbutton.Click();
            Thread.Sleep(3000);

            var contactNum = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            Assert.That(contactNum.Text, Is.EqualTo("Contacts found: 4"));

            var FirstName = driver.FindElementById("contactbook.androidclient:id/textViewFirstName");
            
            Assert.That(FirstName.Text, Is.EqualTo("Steve"));
            Assert.That(FirstName.Text, Is.EqualTo("Michael"));
            Assert.That(FirstName.Text, Is.EqualTo("Albert"));
        }
            [Test]
        public void Invialid_Contact()
        {
            var ApiUrl = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
            ApiUrl.Clear();
            ApiUrl.SendKeys(ApiServiceUri);

            var conectionApiButton = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            conectionApiButton.Clear();
            conectionApiButton.Click();

            var surchfield = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            surchfield.Clear();
            surchfield.SendKeys("Ivo");

            var surchbutton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            surchbutton.Click();
            Thread.Sleep(1000);
            var contactNum = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            Assert.That(contactNum.Text, Is.EqualTo("Contacts found: 0"));
        }
    }
}