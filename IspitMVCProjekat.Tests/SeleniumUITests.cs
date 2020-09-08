using Microsoft.AspNetCore.Mvc.Testing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace IspitMVCProjekat.Tests
{

    public class SeleniumUITests
    {
        private string _websiteURL = "https://localhost:44303/";

        private RemoteWebDriver _browserDriver;

        public ITestOutputHelper _output { get; set; }

        public SeleniumUITests(ITestOutputHelper output)
        {
            _output = output;
        }

        #region Student UI tests
        [Theory]
        [InlineData("S20", "Petar", "Peric", "Karadjordjeva 20", "Beograd")]
        //[InlineData("S21", "Marija", "Maric", "Kralja Petra 120", "Beograd")]
        public void CreateStudent(string brIndexa, string ime, string prezime, string adresa, string grad)
        {
            //Arrange
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + "Studenti/Create");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(5);

            _browserDriver.FindElementByName("BrojIndexa").SendKeys(brIndexa);
            _browserDriver.FindElementByName("Ime").SendKeys(ime);
            _browserDriver.FindElementByName("Prezime").SendKeys(prezime);
            _browserDriver.FindElementByName("Adresa").SendKeys(adresa);
            _browserDriver.FindElementByName("Grad").SendKeys(grad);

            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{brIndexa}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);


            //Act
            _browserDriver.FindElement(By.Id("success")).Click();
            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(3);

            //Assert

            Assert.Contains(brIndexa, _browserDriver.PageSource);
            Assert.Contains(ime, _browserDriver.PageSource);
            Assert.Contains(prezime, _browserDriver.PageSource);
            Assert.Contains(adresa, _browserDriver.PageSource);
            Assert.Contains(grad, _browserDriver.PageSource);



            if (_browserDriver.PageSource.Contains(brIndexa)
                && _browserDriver.PageSource.Contains(ime)
                && _browserDriver.PageSource.Contains(prezime)
                && _browserDriver.PageSource.Contains(adresa)
                && _browserDriver.PageSource.Contains(grad))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();




        }

        [Theory]
        [InlineData("S19")]
        public void Edit_FindStudent(string brIndexa)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Studenti/Edit?brIndexa={brIndexa}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(3);

            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{brIndexa}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(5);
            _browserDriver.FindElement(By.Id("secondary")).Click();


            Assert.Contains(brIndexa, _browserDriver.PageSource);

            if (_browserDriver.PageSource.Contains(brIndexa))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }


        [Theory]
        [InlineData("Petar", "Markovic", "Adresa 123", "Beograd")]
        public void EditStudent(string ime, string prezime, string adresa, string grad)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + "Studenti/Edit/?brIndexa=S20");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(5);


            _browserDriver.FindElementByName("Ime").Clear();
            _browserDriver.FindElementByName("Prezime").Clear();
            _browserDriver.FindElementByName("Adresa").Clear();
            _browserDriver.FindElementByName("Grad").Clear();


            _browserDriver.FindElementByName("Ime").SendKeys(ime);
            _browserDriver.FindElementByName("Prezime").SendKeys(prezime);
            _browserDriver.FindElementByName("Adresa").SendKeys(adresa);
            _browserDriver.FindElementByName("Grad").SendKeys(grad);


            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{ime + prezime}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);


            _browserDriver.FindElement(By.Id("primary")).Click();
            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(3);


            Assert.Contains(ime, _browserDriver.PageSource);
            Assert.Contains(prezime, _browserDriver.PageSource);
            Assert.Contains(adresa, _browserDriver.PageSource);
            Assert.Contains(grad, _browserDriver.PageSource);

            if (_browserDriver.PageSource.Contains(ime)
                && _browserDriver.PageSource.Contains(prezime)
                && _browserDriver.PageSource.Contains(adresa)
                && _browserDriver.PageSource.Contains(grad))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }


            _browserDriver.Quit();
        }


        [Theory]
        [InlineData("S19")]
        public void Delete_FindStudent(string brIndexa)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Studenti/Delete?brIndexa={brIndexa}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(3);

            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{brIndexa}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(5);
            _browserDriver.FindElement(By.Id("secondary")).Click();

            Assert.Contains(brIndexa, _browserDriver.PageSource);

            if (_browserDriver.PageSource.Contains(brIndexa))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }


        [Theory]
        [InlineData("S20")]
        public void DeleteStudent(string brIndexa)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Studenti/Delete?brIndexa={brIndexa}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(3);

            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{brIndexa}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.FindElement(By.Id("danger")).Click();
            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(3);



            Assert.DoesNotContain(brIndexa, _browserDriver.PageSource);

            if (!_browserDriver.PageSource.Contains(brIndexa))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }


            _browserDriver.Quit();
        }



        

        #endregion


        #region Predmet UI tests

        [Theory]
        [InlineData("C# programiranje")]
        public void CreatePredmet(string predmet)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + "Predmeti/Create");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(5);

            _browserDriver.FindElementByName("ImePredmeta").SendKeys(predmet);

            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{predmet}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);


            _browserDriver.FindElement(By.Id("success")).Click();
            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(5);

            Assert.Contains(predmet, _browserDriver.PageSource);

            if (_browserDriver.PageSource.Contains(predmet))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }


        [Theory]
        [InlineData(5)]
        public void Edit_FindPredmet(int PredmetID)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Predmeti/Edit?PredmetID={PredmetID}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(3);

            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{"Find " + PredmetID + "Edit" }.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(5);
            _browserDriver.FindElement(By.Id("secondary")).Click();


            Assert.Contains(PredmetID.ToString(), _browserDriver.PageSource);

            if (_browserDriver.PageSource.Contains(PredmetID.ToString()))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }


        [Theory]
        [InlineData("SQL")]
        public void EditPredmet(string predmet)
        {
            var predmetID = 13;

            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Predmeti/Edit?PredmetID={predmetID}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(5);

            _browserDriver.FindElementByName("ImePredmeta").Clear();
            _browserDriver.FindElementByName("ImePredmeta").SendKeys(predmet);


            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{predmet}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.FindElement(By.Id("primary")).Click();
            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(3);

            Assert.Contains(predmet, _browserDriver.PageSource);

            if (_browserDriver.PageSource.Contains(predmet))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }


        [Theory]
        [InlineData(5)]
        public void Delete_FindPredmet(int PredmetID)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Predmeti/Delete?PredmetID={PredmetID}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(3);

            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{"PredmetID " + PredmetID}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(5);
            _browserDriver.FindElement(By.Id("secondary")).Click();

            Assert.Contains(PredmetID.ToString(), _browserDriver.PageSource);

            if (_browserDriver.PageSource.Contains(PredmetID.ToString()))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }


        [Theory]
        [InlineData(13)]
        public void DeletePredmet(int PredmetID)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Predmeti/Delete?PredmetID={PredmetID}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(3);

            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{"Predmet " + PredmetID + "deleted"}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.FindElement(By.Id("danger")).Click();
            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(3);


            Assert.DoesNotContain(PredmetID.ToString(), _browserDriver.PageSource);

            if (!_browserDriver.PageSource.Contains(PredmetID.ToString()))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }


            _browserDriver.Quit();
        }


        [Theory]
        [InlineData(5)]
        public void Details_FindPredmet(int PredmetID)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Predmeti/Details?PredmetId={PredmetID}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(3);

            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{"Details predmet" + PredmetID}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(5);
            _browserDriver.FindElement(By.Id("primary")).Click();


            Assert.Contains(PredmetID.ToString(), _browserDriver.PageSource);

            if (_browserDriver.PageSource.Contains(PredmetID.ToString()))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }


        #endregion


        #region Ispit UI tests

        [Theory]
        [InlineData("S10", "Fizika", 5)]
        public void CreateIspit(string brIndexa, string predmet, int ocena)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + "Ispiti/Create");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(5);

            _browserDriver.FindElementByName("Student").SendKeys(brIndexa);
            _browserDriver.FindElementByName("Predmet").SendKeys(predmet);
            _browserDriver.FindElementByName("Ocena").SendKeys(ocena.ToString());

            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{predmet}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);


            _browserDriver.FindElement(By.Id("success")).Click();
            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(5);

            Assert.Contains(brIndexa, _browserDriver.PageSource);
            Assert.Contains(predmet, _browserDriver.PageSource);
            Assert.Contains(ocena.ToString(), _browserDriver.PageSource);


            if (_browserDriver.PageSource.Contains(brIndexa)
                && _browserDriver.PageSource.Contains(predmet)
                && _browserDriver.PageSource.Contains(ocena.ToString()))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }


        [Theory]
        [InlineData(5)]
        public void Edit_FindIspit(int IspitID)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Ispiti/Edit?IspitId={IspitID}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(3);

            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{"Find ispit " + IspitID + "Edit" }.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(5);
            _browserDriver.FindElement(By.Id("secondary")).Click();


            Assert.Contains(IspitID.ToString(), _browserDriver.PageSource);

            if (_browserDriver.PageSource.Contains(IspitID.ToString()))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }

        [Theory]
        [InlineData("S18", "Hemija", 5)]
        public void EditIspit(string student, string predmet, int ocena)
        {
            var ispitID = 49;

            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Ispiti/Edit?IspitId={ispitID}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(5);

            _browserDriver.FindElementByName("Student").Clear();
            _browserDriver.FindElementByName("Student").SendKeys(student);

            _browserDriver.FindElementByName("Predmet").Clear();
            _browserDriver.FindElementByName("Predmet").SendKeys(predmet);

            _browserDriver.FindElementByName("Ocena").Clear();
            _browserDriver.FindElementByName("Ocena").SendKeys(ocena.ToString());


            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{ispitID + "Izmena podataka"}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.FindElement(By.Id("primary")).Click();
            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(3);

            Assert.Contains(student, _browserDriver.PageSource);
            Assert.Contains(predmet, _browserDriver.PageSource);
            Assert.Contains(ocena.ToString(), _browserDriver.PageSource);


            if (_browserDriver.PageSource.Contains(student)
                && _browserDriver.PageSource.Contains(predmet)
                && _browserDriver.PageSource.Contains(ocena.ToString()))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }


        [Theory]
        [InlineData(30)]
        public void Delete_FindIspit(int IspitID)
        {

            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Ispiti/Delete?IspitId={IspitID}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(3);

            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{"IspitID " + IspitID}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(5);
            _browserDriver.FindElement(By.Id("secondary")).Click();

            Assert.Contains(IspitID.ToString(), _browserDriver.PageSource);

            if (_browserDriver.PageSource.Contains(IspitID.ToString()))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }


        [Theory]
        [InlineData(49)]
        public void DeleteIspit(int IspitID)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Ispiti/Delete?IspitId={IspitID}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(3);

            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{"Ispit " + IspitID + "deleted"}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.FindElement(By.Id("danger")).Click();
            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(3);


            Assert.DoesNotContain(IspitID.ToString(), _browserDriver.PageSource);

            if (!_browserDriver.PageSource.Contains(IspitID.ToString()))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }



        [Theory]
        [InlineData(5)]
        public void Details_FindIspit(int IspitID)
        {
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + $"Ispiti/Details?IspitId={IspitID}");
            _browserDriver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(3);

            //create screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var fileName = $"{"Details ispit" + IspitID}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            _browserDriver.Manage().Timeouts().ImplicitWait.Seconds.Equals(5);
            _browserDriver.FindElement(By.Id("primary")).Click();


            Assert.Contains(IspitID.ToString(), _browserDriver.PageSource);

            if (_browserDriver.PageSource.Contains(IspitID.ToString()))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("Test failed");
            }

            _browserDriver.Quit();
        }



        #endregion

    }


    public class ControllerTests : IClassFixture<WebApplicationFactory<IspitMVCProjekat.Startup>>
    {
        private readonly WebApplicationFactory<IspitMVCProjekat.Startup> _factory;

        public ControllerTests(WebApplicationFactory<IspitMVCProjekat.Startup> factory)
        {
            _factory = factory;
        }


        [Theory]
        [InlineData("/")]
        [InlineData("/Predmeti")]
        [InlineData("/Studenti")]
        [InlineData("/Ispiti")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }


}
