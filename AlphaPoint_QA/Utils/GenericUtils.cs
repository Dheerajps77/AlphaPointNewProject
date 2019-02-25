using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Utils
{
    public class GenericUtils
    {
        private readonly ITestOutputHelper output;
        static ILog logger;
        private static string dayFormat = TestData.GetData("DayFormat");
        private static string dateFormat = TestData.GetData("DateFormat");
        private static string dateTimeFormat = TestData.GetData("DateTimeFormat");

        public GenericUtils(ITestOutputHelper output)
        {
            this.output = output;
            logger = APLogger.GetLog();    
        }

        public static string GetCurrentTime()
        {
            DateTime date = DateTime.Now;
            string formattedDate = date.ToString(dateTimeFormat).ToLower();
            return formattedDate;
        }
        
        public static string GetCurrentTimePlusOneMinute()
        {
            DateTime date = DateTime.Now.AddMinutes(1);
            string formattedDate = date.ToString(dateTimeFormat).ToLower();
            return formattedDate;
        }

        public static string GetCurrentDate()
        {
            DateTime date = DateTime.Today;
            string formattedDate = date.ToString(dateFormat).ToLower();
            return formattedDate;
        }

        public static string GetCurrentDateMinusOne()
        {
            DateTime date = DateTime.Now.AddDays(-1);
            return date.ToString(dateFormat);
        }

        public static string GetCurrentDatePlusOne()
        {
            DateTime date = DateTime.Now.AddDays(1);
            return date.ToString(dateFormat);
        }

        public static int GetOnlyCurrentDate()
        {
            DateTime date = DateTime.Now;
            string formattedDate = date.ToString(dayFormat);
            return Int32.Parse(formattedDate);
        }

        // dateString should be in MM/DD/YYYY format 
        public static int GetOnlyDateFromDateString(string dateString)
        {
            string[] formats = { dateFormat };
            var dateTime = DateTime.ParseExact(dateString, formats, new CultureInfo("en-US"), DateTimeStyles.None);
            string formattedDate = dateTime.ToString(dayFormat);
            return Int32.Parse(formattedDate);
        }

        public static string GetDifferenceFromStringAfterSubstraction(string firstValue, string secondValue)
        {
            string firstValuePrecision = Convert.ToDecimal(firstValue).ToString("#,##0.00000000");
            string secondValuePrecision = Convert.ToDecimal(secondValue).ToString("#,##0.00000000");
            var firstValueToDouble = Double.Parse(firstValue);
            var secondValueToDouble = Double.Parse(secondValue);
            double difference = Math.Abs(firstValueToDouble - secondValueToDouble);
            string differenceInString = String.Format("{0:0.00000000}", difference);
            return differenceInString;
        }


        public static string GetSumFromStringAfterAddition(string firstValue, string secondValue)
        {
            string firstValuePrecision = Convert.ToDecimal(firstValue).ToString("#,##0.00000000");
            string secondValuePrecision = Convert.ToDecimal(secondValue).ToString("#,##0.00000000");
            var firstValueToDouble = Double.Parse(firstValue);
            var secondValueToDouble = Double.Parse(secondValue);
            double sum = Math.Abs(firstValueToDouble + secondValueToDouble);
            string sumInString = String.Format("{0:0.00000000}", sum);
            return sumInString;
        }

        public static String ConvertToDoubleFormat(double amount)
        {
            return Convert.ToDecimal(amount).ToString("#,##0.00000000");
        }

        public static double ConvertStringToDouble(string amount)
        {
            return Convert.ToDouble(amount);
        }

        public static string ConvertDoubleToString(double amount)
        {
            return Convert.ToString(amount);
        }

        public static string ConvertTo8DigitAfterDecimal(double amount)
        {
            var amt = Math.Floor(amount * 100000000) / 100000000;
            return Convert.ToString(amt);
        }

        public static string RemoveCommaFromString(string str)
        {
            return str.Replace(@",", string.Empty);
        }

        public static String FilledOrdersTotalAmount(double size, double price)
        {
            double totalAmount = size * price;
            return Convert.ToDecimal(totalAmount).ToString("#,##0.00000000");
        }

        public static void ActionClick(IWebDriver driver,IWebElement we)
        {
            Actions actionobj=new Actions(driver);
            actionobj.Click(we).Build().Perform();
        }


        public static void DoubleClick(IWebDriver driver, IWebElement we)
        {
            Actions actionobj = new Actions(driver);
            actionobj.DoubleClick(we).Build().Perform();
        }

        public static void TurnOffImplicitWaits(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        public static void TurnOnImplicitWaits(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public static IWebElement WaitForElementVisibility(IWebDriver driver, By findByCondition, int waitInSeconds)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSeconds));
                wait.Until(ExpectedConditions.ElementExists(findByCondition));
                return driver.FindElement(findByCondition);
            }
            catch (Exception e)
            {
                logger.Error("Element not visible");
                throw e;
            }
        }

        public static IWebElement WaitForElementPresence(IWebDriver driver, By findByCondition, int waitInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSeconds));
            wait.Until(ExpectedConditions.ElementIsVisible(findByCondition));
            return driver.FindElement(findByCondition);
        }

        public static IWebElement WaitForElementClickable(IWebDriver driver, By findByCondition, int waitInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSeconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(findByCondition));
            return driver.FindElement(findByCondition);
        }

        public static void CloseCurrentBrowserTab(IWebDriver driver)
        {
            driver.Close();
        }

        public static bool IsElementPresent(IWebDriver driver, By locator)
        {
            TurnOffImplicitWaits(driver);
            bool result = false;
            try
            {
                result = driver.FindElement(locator).Displayed;
            }
            catch (Exception e)
            {
                TurnOnImplicitWaits(driver);
            }
            finally
            {
                TurnOnImplicitWaits(driver);
            }
            return result;
        }

        public static IWebElement ScrollToViewElement(IWebDriver driver, By findByCondition)
        {
            IWebElement element = driver.FindElement(findByCondition);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            return driver.FindElement(findByCondition);
        }

        public static void ScrollUp(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,-250)", "");
        }

        public static void HoverElement(IWebDriver driver, By locator)
        {
            Actions action = new Actions(driver);
            IWebElement we = driver.FindElement(locator);
            action.MoveToElement(we).Build().Perform();
        }

        public static string GetScreenshot(IWebDriver driver, string screenshotName)
        {
            try
            {
                // Get the current TimeStamp
                string timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff");
                string screenShotPath = Directory.GetCurrentDirectory() + "\\Screenshot\\";
                if (!Directory.Exists(screenShotPath))
                {
                    Directory.CreateDirectory(screenShotPath);
                }
                string fileName = screenShotPath + screenshotName + "_" + timeStamp + ".png";
                // Take a screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
                // Return the filename of the screenshot
                return fileName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void RefreshPage(IWebDriver driver)
        {
            driver.Navigate().Refresh();
        }

        public static void SelectDropDownByValue(IWebDriver driver, By locator, String value)
        {
            SelectElement dropdown = new SelectElement(driver.FindElement(locator));
            dropdown.SelectByValue(value);
        }

        public static void OpenNewBrowserWindow(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static string ReducedAmount(string amount)
        {
            return (ConvertStringToDouble(amount) - 1).ToString();
        }

        public static string IncreaseAmount(string amount)
        {
            return (ConvertStringToDouble(amount) + 1).ToString();
        }

        public static string FeeAmount(string amount, string feeComponent)
        {
            double orderSizeInDouble = Double.Parse(amount);
            double feeComponentInDouble = Double.Parse(feeComponent);
            double feeValue = orderSizeInDouble * feeComponentInDouble;
            return feeValue.ToString();
        }

        public static string SellFeeAmount(string orderSize, string limitPrice, string feeComponent)
        {
            double orderSizeInDouble = Double.Parse(orderSize);
            double limitPriceInDouble = Double.Parse(limitPrice);
            double feeComponentInDouble = Double.Parse(feeComponent);
            double feeValue = (orderSizeInDouble * limitPriceInDouble * feeComponentInDouble);
            return feeValue.ToString("0.00000000");
        }

        public string GetDifferenceFromString(string firstValue, string secondValue)
        {
            string firstValuePrecision = Convert.ToDecimal(firstValue).ToString("#,##0.00000000");
            string secondValuePrecision = Convert.ToDecimal(secondValue).ToString("#,##0.00000000");
            var firstValueToDouble = Double.Parse(firstValue);
            var secondValueToDouble = Double.Parse(secondValue);
            double difference = Math.Abs(firstValueToDouble - secondValueToDouble);
            string differenceInString = String.Format("{0:0.00000000}", difference);
            return differenceInString;
        }
        // Generates random string
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
