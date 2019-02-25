using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace AlphaPoint_QA.Pages
{
    class LoyaltyTokenPage
    {
        ProgressLogger logger;
        static Config data;
        public static IWebDriver driver;

        public LoyaltyTokenPage(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        By loyaltyTokenLink = By.XPath("//li[@data-test='Loyalty Token']");
        By tradingFee = By.XPath("//label[@for='loyaltyToken5']");
        By userSettingMenu = By.XPath("//div[@class='page-header-nav__menu-toggle']");
        By marketOrderFees= By.XPath("//span[@data-test='Fees']");

        public IWebElement MarketOrderFees()
        {
            return driver.FindElement(marketOrderFees);
        }

        public IWebElement LoyaltyTokenLink()
        {
            return driver.FindElement(loyaltyTokenLink);
        }

        public IWebElement UserSettingMenu()
        {
            return driver.FindElement(userSettingMenu);
        }

        public IWebElement TradingFee()
        {
            return driver.FindElement(tradingFee);
        }

        //This method will click on "LoyaltyToken" Button in User settings page
        public void LoyaltyTokenButton(IWebDriver driver)
        {
            try
            {
                Thread.Sleep(2000);
                GenericUtils.WaitForElementVisibility(driver, loyaltyTokenLink, 30).Click();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //This method will click on "TradingFee" Radio Button in User settings page
        public void TradingFeeRadioButton(IWebDriver driver)
        {
            try
            {
                GenericUtils.WaitForElementVisibility(driver, tradingFee, 30).Click();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on "LoyaltyToken" Button in User settings page
        public void UserSettingMenuButton(IWebDriver driver)
        {
            try
            {
                GenericUtils.WaitForElementVisibility(driver, userSettingMenu, 30).Click();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on "LoyaltyToken" Button in User settings page
        public bool GetFeeText()
        {
            try
            {
                bool flag = false;
                Thread.Sleep(2000);
                string feeText = MarketOrderFees().Text;
                if (feeText.Contains(Const.LoyaltyToken))
                {
                    flag = true;
                    logger.LogCheckPoint(String.Format(LogMessage.AppliedFeeIsLTC, feeText));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.AppliedFeeIsNotLTC, feeText));
                }
                return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
