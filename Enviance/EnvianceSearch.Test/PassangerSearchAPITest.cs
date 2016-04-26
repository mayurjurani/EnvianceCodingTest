using System;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using System.Threading;

namespace EnvianceSearch.Test
{
    [TestClass]
    public class PassangerSearchAPITest
    {
        private IWebDriver webDriver;
        string BaseUrl = "http://localhost:49302/api/v1/passanger/search/";
        [TestInitialize]
        public void Setup()
        {
            FirefoxProfile profile = new FirefoxProfile();
            profile.SetPreference("network.http.accept.default", "application/json;q=0.9");
            webDriver = new FirefoxDriver(profile);            
        }
        [TestMethod]
        public void TestName1()
        {
            const string nameString = "Mayur";
            ApiResponse apiResponse = new ApiResponse();
            string expectedResult = apiResponse.GetResponse(nameString);
            Thread.Sleep(1000);
            Console.WriteLine("-------------------- Web Page Displayed ------------------");
            webDriver.Navigate().GoToUrl(BaseUrl + nameString);
            IWebElement responseElement = webDriver.FindElement(By.TagName("pre"));
            string displayedResponse = responseElement.Text;
            Console.WriteLine(displayedResponse);
            Assert.IsTrue(expectedResult.Equals(displayedResponse));
        }
        [TestMethod]
        public void TestName2()
        {
            const string nameString = "Mayur                     ,,,,,,,,  Jurani  ";
            ApiResponse apiResponse = new ApiResponse();
            string expectedResult = apiResponse.GetResponse(nameString);
            Thread.Sleep(1000);
            Console.WriteLine("-------------------- Web Page Displayed ------------------");
            webDriver.Navigate().GoToUrl(BaseUrl + nameString);
            IWebElement responseElement = webDriver.FindElement(By.TagName("pre"));
            string displayedResponse = responseElement.Text;
            Console.WriteLine(displayedResponse);
            Assert.IsTrue(expectedResult.Equals(displayedResponse));
        }
       
        [TestCleanup]
        public void Teardown()
        {
            webDriver.Close();
            webDriver.Quit();
        }
    }
}
