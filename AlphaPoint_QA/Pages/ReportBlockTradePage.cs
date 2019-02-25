﻿using System;
using System.Collections.Generic;
using System.Threading;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Pages
{
    public class ReportBlockTradePage
    {
        //ILog logger;
        Config data;
        ProgressLogger logger;

        private string placeBlockTradeDateTime;
        private string placeBlockTradeDateTimePlusOneMin;
        private string typeValue;
        public static IWebDriver driver;
        private string statusValue;
        private string placeBlockTradeDateTimeOfBlockTrade;
        private string placeBlockTradeDateTimePlusOneMinOfBlockTrade;
        private string cancelValue;

        public ReportBlockTradePage(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        By reportBlockTradeBtn = By.XPath("//div[text()='« Report Block Trade']");
        By boughtTab = By.XPath("//div[@class='report-block-trade-sidepane__tab-container']/div[1]");
        By soldTab = By.XPath("//div[@class='report-block-trade-sidepane__tab-container']/div[2]");
        By instrument = By.XPath("//select[@name='instrument']");
        By counterparty = By.XPath("//input[@data-test='Counterparty:']");
        By lockedIn = By.XPath("//div[@class='report-block-trade-form__checkbox-container']/div/label");
        By productBought = By.XPath("//input[@data-test='Product Bought:']");
        By productSold = By.XPath("//input[@data-test='Product Sold:']");
        By fees = By.XPath("//div[@class='ap-label-with-text report-block-trade-form__lwt-container']//span[contains(@data-test,'Fees')]");
        By received = By.XPath("//div[@class='ap-label-with-text report-block-trade-form__lwt-container']//span[contains(@data-test,'Received')]");
        By orderTotal = By.XPath("//div[@class='ap-label-with-text report-block-trade-form__lwt-container']//span[contains(@data-test,'Order Total')]");
        By btcBalance = By.XPath("//div[@class='ap-label-with-text user-balance__lwt-container']//span[contains(@data-test,'BTC Balance')]");
        By usdBalance = By.XPath("//div[@class='ap-label-with-text user-balance__lwt-container']//span[contains(@data-test,'USD Balance')]");
        By submitReport = By.XPath("//button[text()='Submit Report']");
        By reportBlockTradeWindowTitle = By.XPath("//h2[text()='Report Block Trade']");
        By dropdownInstrument = By.XPath("//select[@name='instrument']");
        By counterPartyID = By.XPath("//label[text()='Counterparty:']");
        By counterPartyTextField = By.XPath("//input[@data-test='Counterparty:']");
        By productBoughtTextField = By.XPath("//input[@data-test='Product Bought:']");
        By productSoldTextField = By.XPath("//input[@data-test='Product Sold:']");
        By lockedInCheckBox = By.XPath("//label[@for='locked-in']");
        By productBoughtText = By.XPath("//label[text()='Product Bought:']");
        By productSoldText = By.XPath("//label[text()='Product Sold:']");
        By feesText = By.XPath("//label[text()='Fees:']");
        By boughAndSoldBTCBalances = By.XPath("//label[text()='BTC Balance:']//following::span[@data-test='BTC Balance:']");
        By boughAndSoldUSDBalances = By.XPath("//label[text()='BTC Balance:']//following::span[@data-test='USD Balance:']");
        By orderTotalAmount = By.XPath("//div[@class='ap-label-with-text report-block-trade-form__lwt-container']//span[@data-test='Order Total:']");
        By receivedAmount = By.XPath("//div[@class='ap-label-with-text report-block-trade-form__lwt-container']//span[@data-test='Received:']");
        By feeAmount = By.XPath("//div[@class='ap-label-with-text report-block-trade-form__lwt-container']//span[@data-test='Fees:']");
        By submitReportBtn = By.XPath("//button[@type='submit' and text()='Submit Report']");
        By confirmBuyOrderBtn = By.XPath("//button[text()='Confirm Buy Order']");
        By buyTradeReportOrderMsg = By.XPath("//div[text()='Your order has been successfully added']");
        By counterPartyErrorMsg = By.XPath("//div[@class='snackbar__text snackbar__text--warning custom-snackbar__text custom-snackbar__text--warning']");
        By labelConfirmBlockTradeInstrumentText = By.XPath("//div[@class='ap-label-with-text confirm-block-trade-modal__lwt-container']//label[text()='Instrument']//following::span[@data-test='Instrument']");
        By labelConfirmBlockTradeCountryPartyValue = By.XPath("//div[@class='ap-label-with-text confirm-block-trade-modal__lwt-container']//label[text()='Instrument']//following::span[@data-test='Counterparty']");
        By labelConfirmBlockTradeLockedInStatus = By.XPath("//div[@class='ap-label-with-text confirm-block-trade-modal__lwt-container']//label[text()='Instrument']//following::span[@data-test='Locked in']");
        By labelConfirmBlockTradeProductBoughtValue = By.XPath("//div[@class='ap-label-with-text confirm-block-trade-modal__lwt-container']//label[text()='Instrument']//following::span[@data-test='Product Bought']");
        By labelConfirmBlockTradeProductSoldValue = By.XPath("//div[@class='ap-label-with-text confirm-block-trade-modal__lwt-container']//label[text()='Instrument']//following::span[@data-test='Product Sold']");
        By labelConfirmBlockTradeFeeValue = By.XPath("//div[@class='ap-label-with-text confirm-block-trade-modal__lwt-container']//label[text()='Instrument']//following::span[@data-test='Fee']");
        By labelConfirmBlockTradeFinalAMountValue = By.XPath("//div[@class='ap-label-with-text confirm-block-trade-modal__lwt-container']//label[text()='Instrument']//following::span[@data-test='Final Amount']");
        By labelConfirmBlockTradeFinalValue = By.XPath("//div[@class='ap-label-with-text confirm-block-trade-modal__lwt-container']//label[text()='Instrument']//following::span[@data-test='Final Value']");
        By labelConfirmBlockTradeDate = By.XPath("//div[@class='ap-label-with-text confirm-block-trade-modal__lwt-container']//label[text()='Instrument']//following::span[@data-test='Date:']");
        By closeReportBlockTradeWindowSection = By.XPath("//div[@class='ap-sidepane__close-button report-block-trade-sidepane__close-button']");
        By cancelButton = By.XPath("//button[@data-test='Cancel']");

        public IWebElement CancelButton()
        {
            return driver.FindElement(cancelButton);
        }

        public IWebElement CloseReportBlockTradeWindowSection()
        {
            Thread.Sleep(2000);
            return driver.FindElement(closeReportBlockTradeWindowSection);
        }
        public IWebElement CounterPartyErrorMsg()
        {
            return driver.FindElement(counterPartyErrorMsg);
        }
        public IWebElement BuyTradeReportOrderMsg()
        {
            return driver.FindElement(buyTradeReportOrderMsg);
        }

        public IWebElement SubmitReportBtn()
        {
            return driver.FindElement(submitReportBtn);
        }

        public IWebElement ConfirmBuyOrderBtn()
        {
            return driver.FindElement(confirmBuyOrderBtn);
        }

        public IWebElement OrderTotalAmount()
        {
            return driver.FindElement(orderTotalAmount);
        }

        public IWebElement ReceivedAmount()
        {
            return driver.FindElement(receivedAmount);
        }

        public IWebElement FeeAmount()
        {
            return driver.FindElement(feeAmount);
        }

        public IWebElement CounterPartyTextField()
        {
            return driver.FindElement(counterPartyTextField);
        }

        public IWebElement ProductBoughtTextField()
        {
            return driver.FindElement(productBoughtTextField);
        }

        public IWebElement ProductSoldTextField()
        {
            return driver.FindElement(productSoldTextField);
        }

        public IWebElement CounterPartyID()
        {
            return driver.FindElement(counterPartyID);
        }
        public IWebElement LockedInCheckBox()
        {
            return driver.FindElement(lockedInCheckBox);
        }
        public IWebElement ProductBoughtText()
        {
            return driver.FindElement(productBoughtText);
        }
        public IWebElement ProductSoldText()
        {
            return driver.FindElement(productSoldText);
        }
        public IWebElement FeesText()
        {
            return driver.FindElement(feesText);
        }
        public IWebElement BoughAndSoldBTCBalances()
        {
            return driver.FindElement(boughAndSoldBTCBalances);
        }

        public IWebElement BoughAndSoldUSDBalances()
        {
            return driver.FindElement(boughAndSoldUSDBalances);
        }

        public IWebElement DropdownInstrument()
        {
            return driver.FindElement(dropdownInstrument);
        }

        public IWebElement ReportBlockTradeWindowTitle()
        {
            return driver.FindElement(reportBlockTradeWindowTitle);
        }

        public IWebElement Received()
        {
            return driver.FindElement(received);
        }

        public IWebElement OrderTotal()
        {
            return driver.FindElement(orderTotal);
        }

        public IWebElement BtcBalance()
        {
            return driver.FindElement(btcBalance);
        }

        public IWebElement USDBalance()
        {
            return driver.FindElement(usdBalance);
        }

        public IWebElement SubmitReport()
        {
            return driver.FindElement(submitReport);
        }

        public IWebElement ReportBlockTradeBtn()
        {
            return driver.FindElement(reportBlockTradeBtn);
        }

        public IWebElement BoughtTab()
        {
            return driver.FindElement(boughtTab);
        }

        public IWebElement SoldTab()
        {
            return driver.FindElement(soldTab);
        }

        public IWebElement Instrument()
        {
            return driver.FindElement(instrument);
        }

        public IWebElement Counterparty()
        {
            return driver.FindElement(counterparty);
        }

        public IWebElement LockedIn()
        {
            return driver.FindElement(lockedIn);
        }

        public IWebElement ProductBought()
        {
            return driver.FindElement(productBought);
        }

        public IWebElement ProductSold()
        {
            return driver.FindElement(productSold);
        }

        public IWebElement Fees()
        {
            return driver.FindElement(fees);
        }

        public IWebElement LabelConfirmBlockTradeInstrumentText()
        {
            return driver.FindElement(labelConfirmBlockTradeInstrumentText);
        }

        public IWebElement LabelConfirmBlockTradeCountryPartyValue()
        {
            return driver.FindElement(labelConfirmBlockTradeCountryPartyValue);
        }

        public IWebElement LabelConfirmBlockTradeLockedInStatus()
        {
            return driver.FindElement(labelConfirmBlockTradeLockedInStatus);
        }

        public IWebElement LabelConfirmBlockTradeProductBoughtValue()
        {
            return driver.FindElement(labelConfirmBlockTradeProductBoughtValue);
        }

        public IWebElement LabelConfirmBlockTradeProductSoldValue()
        {
            return driver.FindElement(labelConfirmBlockTradeProductSoldValue);
        }

        public IWebElement LabelConfirmBlockTradeFeeValue()
        {
            Thread.Sleep(2000);
            return driver.FindElement(labelConfirmBlockTradeFeeValue);
        }

        public IWebElement LabelConfirmBlockTradeFinalAMountValue()
        {
            return driver.FindElement(labelConfirmBlockTradeFinalAMountValue);
        }

        public IWebElement LabelConfirmBlockTradeFinalValue()
        {
            return driver.FindElement(labelConfirmBlockTradeFinalValue);
        }

        public IWebElement LabelConfirmBlockTradeDate()
        {
            return driver.FindElement(labelConfirmBlockTradeDate);
        }

        public void ActionCancelButton()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(CancelButton());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on "Report Block Trade" button
        public void ReportBlockTradeButton()
        {
            UserSetFunctions.Click(ReportBlockTradeBtn());
        }

        public void LockedInCheckBoxButton()
        {
            try
            {
                Actions action = new Actions(driver);
                action.MoveToElement(LockedInCheckBox()).Build().Perform();
                Thread.Sleep(2000);
                UserSetFunctions.Click(LockedInCheckBox());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //This will verify the visibility Report Block Trade Window
        public void VerifyReportBlockTradeWindow()
        {
            string reportBlockTradeMsg = string.Format(LogMessage.ReportBlockTradeMsg);
            string msgFromReportTradeWindow = ReportBlockTradeWindowTitle().Text;
            Thread.Sleep(2000);
            try
            {
                try
                {
                    Assert.Equal(msgFromReportTradeWindow, reportBlockTradeMsg);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedReportBlockTradeWindowAppeared));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedReportBlockTradeWindowNotAppeared));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This will verify the visibility of Dropdown instrument
        public void VerifyDropdownInstrument()
        {
            try
            {
                try
                {
                    if (DropdownInstrument().Enabled)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.VerifiedDropdownInstrumentPassed));
                    }
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedDropdownInstrumentFailed));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This will verify the visibility of CounterParty
        public void VerifyCounterParty()
        {
            try
            {
                string counterPartyIDText = string.Format(LogMessage.CounterPartyIDTextMsg);
                string msgFromcounterPartyID = CounterPartyID().Text;
                Thread.Sleep(2000);

                try
                {
                    Assert.Equal(msgFromcounterPartyID, counterPartyIDText);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedCounterPartyTextPassed));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedCounterPartyTextFailed));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This will verify the visibility of LockedIn Checkbox
        public void VerifyLockedInCheckbox()
        {
            try
            {

                try
                {
                    if (LockedInCheckBox().Enabled)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.VerifiedLockedInCheckBoxPassed));
                    }
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedLockedInCheckBoxFailed));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This will verify the visibility of Product Bought
        public void VerifyProductBought()
        {
            try
            {
                string productBoughtText = string.Format(LogMessage.ProductBoughtTextMsg);
                string msgFromProductBought = ProductBoughtText().Text;
                Thread.Sleep(2000);

                try
                {
                    Assert.Equal(msgFromProductBought, productBoughtText);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedProductBoughtTextPassed));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedProductBoughtTextFailed));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This will verify the visibility of Product Sold
        public void VerifyProductSold()
        {
            try
            {
                string productSoldText = string.Format(LogMessage.ProductSoldTextMsg);
                string msgFromProductSold = ProductSoldText().Text;
                Thread.Sleep(2000);

                try
                {
                    Assert.Equal(msgFromProductSold, productSoldText);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedProductSoldTextPassed));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedProductSoldTextFailed));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This will verify the visibility of Fees
        public void VerifyFees()
        {
            try
            {
                string feeText = string.Format(LogMessage.FeeTextMsg);
                string msgFromFee = FeesText().Text;
                Thread.Sleep(2000);

                try
                {
                    Assert.Equal(msgFromFee, feeText);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedFeesTextPassed));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedFeesTextFailed));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //This will verify the visibility of Fees
        public void VerifyBalances()
        {
            try
            {
                string msgFromBtcBalances = BoughAndSoldBTCBalances().Text;
                string msgFromUsdBalances = BoughAndSoldUSDBalances().Text;
                Thread.Sleep(2000);
                try
                {
                    if (BoughAndSoldBTCBalances().Displayed)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.VerifiedBoughAndSoldBTCBalancesPassed));
                    }
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedBoughAndSoldBTCBalancesFailed));
                }

                try
                {
                    if (BoughAndSoldUSDBalances().Displayed)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.VerifiedBoughAndSoldUSDBalancesPassed));
                    }
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedBoughAndSoldUSDBalancesFailed));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SubmitReportButton()
        {
            try
            {
                UserSetFunctions.Click(SubmitReportBtn());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ConfirmSubmitReportButton()
        {
            try
            {
                UserSetFunctions.Click(ConfirmBuyOrderBtn());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method willpopulate the details of user in Trade Report tab(used TC_34)
        public Dictionary<string, string> ConfirmBlockTradeReport(string instrument, string buyTab, string counterPartyValue, string productBoughtPrice, string productSoldPrice, string status)
        {

            try
            {
                Dictionary<string, string> placeBlockTradeReportOrder = new Dictionary<string, string>();
                var blockTradePrice = GetBlockTradePrice(productBoughtPrice, productSoldPrice);
                //SubmitBlockTradeReport(counterPartyValue, productBoughtPrice, productSoldPrice, instrument, buyTab, counterPartyValue, status);
                string feeValueText = LabelConfirmBlockTradeFeeValue().Text;
                placeBlockTradeReportOrder = VerifyConfirmBlockTradeElements(counterPartyValue, instrument);
                VerifyFinalAmountAndFinalValue();
                string expectedBuyTradeReportOrderMsg = BuyTradeReportOrderMsg().Text;
                string actualCancelMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);

                string actualBuyTradeReportOrderMsg = string.Format(LogMessage.BuyTradeReportOrderMesgSuccess);
                string expectedCancelMsg = string.Format(LogMessage.BuyTradeReportOrderMesgCanceled);
                try
                {
                    Assert.Equal(expectedBuyTradeReportOrderMsg, actualBuyTradeReportOrderMsg);
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgSuccess));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgCanceled));
                }

                UserSetFunctions.Click(CloseReportBlockTradeWindowSection());
                try
                {
                    VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, logger);
                    objVerifyOrdersTab.VerifyTradeReportsTab(instrument, buyTab, productBoughtPrice, blockTradePrice, feeValueText, placeBlockTradeReportOrder["PlaceBlockTradeTime"], placeBlockTradeReportOrder["PlaceBlockTradeTimePlusOneMin"], status);
                    logger.LogCheckPoint(String.Format(LogMessage.BuyBlockTradeReportTestPassed, buyTab));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.BuyBlockTradeReportTestFailed, buyTab));
                }

                try
                {
                    Thread.Sleep(2000);
                    if (CancelButton().Displayed)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.VerifiedCancelButtonFailed));
                    }
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedCancelButtonPassed));
                }
                return placeBlockTradeReportOrder;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will very the details in Trade Report tab TC_33
        public Dictionary<string, string> SubmitBuyTradeReport(string instrument, string buyTab, string counterPartyValue, string productBoughtPrice, string productSoldPrice, string status)
        {

            try
            {
                Dictionary<string, string> placeBlockTradeReportOrder = new Dictionary<string, string>();
                var blockTradePrice = GetBlockTradePrice(productBoughtPrice, productSoldPrice);
                SubmitReportButton();
                string feeValueText = LabelConfirmBlockTradeFeeValue().Text;
                placeBlockTradeReportOrder = VerifyConfirmBlockTradeElements(counterPartyValue, instrument);
                VerifyFinalAmountAndFinalValue();
                ConfirmSubmitReportButton();
                string expectedBuyTradeReportOrderMsg = BuyTradeReportOrderMsg().Text;
                string actualCancelMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);

                string actualBuyTradeReportOrderMsg = string.Format(LogMessage.BuyTradeReportOrderMesgSuccess);
                string expectedCancelMsg = string.Format(LogMessage.BuyTradeReportOrderMesgCanceled);
                try
                {
                    Assert.Equal(expectedBuyTradeReportOrderMsg, actualBuyTradeReportOrderMsg);
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgSuccess));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgCanceled));
                }
                Thread.Sleep(2000);
                UserSetFunctions.Click(CloseReportBlockTradeWindowSection());
                try
                {
                    VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, logger);
                    objVerifyOrdersTab.VerifyTradeReportsTab(instrument, buyTab, productBoughtPrice, blockTradePrice, feeValueText, placeBlockTradeReportOrder["PlaceBlockTradeTime"], placeBlockTradeReportOrder["PlaceBlockTradeTimePlusOneMin"], status);
                    logger.LogCheckPoint(String.Format(LogMessage.BuyBlockTradeReportTestPassed, buyTab));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.BuyBlockTradeReportTestFailed, buyTab));
                }

                try
                {
                    Thread.Sleep(2000);
                    if (CancelButton().Enabled)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.VerifiedCancelButtonFailed));
                    }
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedCancelButtonPassed));
                }

                Thread.Sleep(2000);
                return placeBlockTradeReportOrder;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will verify the Block Trade Trade Report of Other Party 
        public void VerifyOtherPartyBlockTradeReportTab(string instrument, string sellTab, string counterPartyValue, string productBoughtPrice, string productSoldPrice, string status, Dictionary<string, string> otherPartyBlockTradeData)
        {
            OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
            string lastPrice = orderEntryPage.GetLastPrice();
            var blockTradePrice = GetBlockTradePrice(productBoughtPrice, productSoldPrice);
            double feeValue = (Double.Parse(productBoughtPrice) * Double.Parse(lastPrice)) / 25;
            string feeValueText = GenericUtils.ConvertToDoubleFormat(feeValue);

            try
            {
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, logger);
                objVerifyOrdersTab.VerifyTradeReportsTab(instrument, sellTab, productBoughtPrice, blockTradePrice, feeValueText, otherPartyBlockTradeData["PlaceBlockTradeTime"], otherPartyBlockTradeData["PlaceBlockTradeTimePlusOneMin"], status);
                logger.LogCheckPoint(String.Format(LogMessage.SellBlockTradeReportTestPassed, sellTab));
            }
            catch (Exception)
            {
                logger.LogCheckPoint(String.Format(LogMessage.SellBlockTradeReportTestFailed, sellTab));
            }
        }

        //This method will verify the details present in "Confirm Block Trade" window section
        public Dictionary<string, string> VerifyConfirmBlockTradeElements(string counterPartyPrice, string instrument)
        {
            string dateText;
            string expectedBlockTradeDate;
            string actualInstrumentText;
            string actualCounterPartyText;
            placeBlockTradeDateTime = TestData.GetData("TC33_PlaceBlockTradeTime");
            placeBlockTradeDateTimePlusOneMin = TestData.GetData("TC33_PlaceBlockTradeTimePlusOneMin");


            try
            {
                Dictionary<string, string> placeBlockTradeDict = new Dictionary<string, string>();
                dateText = LabelConfirmBlockTradeDate().Text;
                expectedBlockTradeDate = dateText;
                actualInstrumentText = LabelConfirmBlockTradeInstrumentText().Text;
                actualCounterPartyText = LabelConfirmBlockTradeCountryPartyValue().Text;
                string actualLockedInText = LabelConfirmBlockTradeLockedInStatus().Text;

                string exepctedCounterPartyText = counterPartyPrice;
                string exepctedInstrumentText = instrument;

                string LockedInCheckboxEnabled = string.Format(LogMessage.LockedInChecked);
                string LockedInCheckboxDisabled = string.Format(LogMessage.LockedInUnChecked);


                string actualBlockTradeDate = GenericUtils.GetCurrentTime();
                string actualBlockTradeDatePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();

                placeBlockTradeDict.Add(placeBlockTradeDateTime, actualBlockTradeDate);
                placeBlockTradeDict.Add(placeBlockTradeDateTimePlusOneMin, actualBlockTradeDatePlusOneMin);

                string productSoldText = LabelConfirmBlockTradeProductSoldValue().Text;

                string productSoldDigit = productSoldText.Split(" ")[0];
                double doubleSoldBoughtPrice = double.Parse(productSoldDigit);


                //This will verify the status of lockedIn checkbox in "Report Block Trade" window section
                try
                {
                    Assert.Equal(actualLockedInText, LockedInCheckboxEnabled);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedLockedInStatusInConfirmBlockTradePassed, actualLockedInText, LockedInCheckboxEnabled));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedLockedInStatusInConfirmBlockTradeFailed, actualLockedInText, LockedInCheckboxDisabled));
                }

                //This will verify the instrument 
                try
                {
                    Assert.Equal(actualInstrumentText, exepctedInstrumentText);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedInstrumentInConfirmBlockTradePassed, actualInstrumentText, exepctedInstrumentText));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedInstrumentInConfirmBlockTradeFailed, actualInstrumentText, exepctedInstrumentText));
                }

                //This will verify the counter party value in confirm block trade
                try
                {
                    Assert.Equal(actualCounterPartyText, exepctedCounterPartyText);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedCounterPartyValueInConfirmBlockTradePassed, actualCounterPartyText, exepctedCounterPartyText));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedCounterPartyValueInConfirmBlockTradeFailed, actualCounterPartyText, exepctedCounterPartyText));
                }

                //This will verify the Date of on confirm block trade
                try
                {
                    Assert.Equal(expectedBlockTradeDate.ToString(), actualBlockTradeDate);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedDateOnConfirmBlockTradePassed, expectedBlockTradeDate));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedDateOnConfirmBlockTradeFailed, actualBlockTradeDate));
                }
                return placeBlockTradeDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //This method will verify the final amount and final value in confirm block trade section
        public void VerifyFinalAmountAndFinalValue()
        {
            try
            {
                string finalAmountText = LabelConfirmBlockTradeFinalAMountValue().Text;
                string finalValueText = LabelConfirmBlockTradeFinalValue().Text;
                string productBoughtText = LabelConfirmBlockTradeProductBoughtValue().Text;
                string productSoldText = LabelConfirmBlockTradeProductSoldValue().Text;

                string feeValueText = LabelConfirmBlockTradeFeeValue().Text;
                string productSoldDigit = productSoldText.Split(" ")[0];
                double doubleSoldBoughtPrice = double.Parse(productSoldDigit);

                string productBoughtDigit = productBoughtText.Split(" ")[0];
                double doubleProductBoughtPrice = double.Parse(productBoughtDigit);

                double doubleFinalValuePrice = double.Parse(finalValueText);

                double doubleFeePrice = double.Parse(feeValueText);
                double doublefinalAmountPrice = doubleProductBoughtPrice - doubleFeePrice;

                //This will verify final value is equal to product sold
                try
                {
                    Assert.Equal(doubleSoldBoughtPrice, doubleFinalValuePrice);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedFinalValueEqualToProductSoldPassed, doubleSoldBoughtPrice, doubleFinalValuePrice));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedFinalValueEqualToProductSoldFailed, doubleSoldBoughtPrice, doubleFinalValuePrice));
                }

                //This will verify final amount is product bought minus fee
                try
                {
                    Assert.Equal(doublefinalAmountPrice, doubleProductBoughtPrice);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedFinalAmountAfterFeeDeductingPassed, doublefinalAmountPrice, doubleProductBoughtPrice));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedFinalAmountAfterFeeDeductingFailed, doublefinalAmountPrice, doubleFeePrice));
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //This method will perform buy trade report(Will be use by TC_35)
        public void SubmitBlockTradeReportForUser(string counterPartyPrice, string producBoughtPrice, string productSoldPrice)
        {
            try
            {
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
                //ReportBlockTradeButton();
                UserSetFunctions.EnterText(CounterPartyTextField(), counterPartyPrice);
                UserSetFunctions.EnterText(ProductBoughtTextField(), producBoughtPrice);
                UserSetFunctions.EnterText(ProductSoldTextField(), productSoldPrice);
                Thread.Sleep(2000);
                SubmitReportButton();
                Thread.Sleep(2000);
                ConfirmSubmitReportButton();
                Thread.Sleep(2000);
                string expectedBuyTradeReportOrderMsg = BuyTradeReportOrderMsg().Text;
                string actualCancelMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);

                string actualBuyTradeReportOrderMsg = string.Format(LogMessage.BuyTradeReportOrderMesgSuccess);
                string expectedCancelMsg = string.Format(LogMessage.BuyTradeReportOrderMesgCanceled);
                try
                {
                    Assert.Equal(expectedBuyTradeReportOrderMsg, actualBuyTradeReportOrderMsg);
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgSuccess));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgCanceled));
                }
                Thread.Sleep(2000);
                UserSetFunctions.Click(CloseReportBlockTradeWindowSection());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will perform a simple Block Buy trade operation(Used for TC_34)
        public void SubmitBlockTradeReportWithoutLockedInCheckBox(string instrument, string counterPartyValue, string producBoughtPrice, string productSoldPrice, string counterPartyPrice, string buyTab, string status)
        {
            placeBlockTradeDateTimeOfBlockTrade = TestData.GetData("TC34_PlaceBlockTradeTime");
            placeBlockTradeDateTimePlusOneMinOfBlockTrade = TestData.GetData("TC34_PlaceBlockTradeTimePlusOneMin");
            typeValue = TestData.GetData("TC34_Type");
            statusValue = TestData.GetData("TC34_Status");
            cancelValue = TestData.GetData("TC35_Cancel");
            try
            {
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
                UserSetFunctions.EnterText(CounterPartyTextField(), counterPartyPrice);
                UserSetFunctions.EnterText(ProductBoughtTextField(), producBoughtPrice);
                UserSetFunctions.EnterText(ProductSoldTextField(), productSoldPrice);
                Thread.Sleep(2000);
                string productBought = ProductBoughtTextField().GetAttribute(string.Format(LogMessage.ProductBoughtTextFieldAttributeValue));
                double actualProductBought = double.Parse(productBought);

                string productSold = ProductSoldTextField().GetAttribute(string.Format(LogMessage.ProductSoldTextFieldAttributeValue));
                double actualProductSold = double.Parse(productSold);

                string orderTotalAmountStringValue = OrderTotalAmount().Text;
                string orderTotalAmountdigits = orderTotalAmountStringValue.Split(" ")[1];
                double doubleOrderTotalPrice = Double.Parse(orderTotalAmountdigits);

                string receivedAmountStringValue = ReceivedAmount().Text;
                string receivedAmountdigits = receivedAmountStringValue.Split(" ")[1];
                double doubleReceivedPrice = Double.Parse(receivedAmountdigits);

                string feeAmountStringValue = FeeAmount().Text;
                string feeAmountdigits = feeAmountStringValue.Split(" ")[1];
                double doubleFeePrice = Double.Parse(feeAmountdigits);

                double receivedBTCAmount = actualProductBought - doubleFeePrice;

                //This will verify if product Sold is equal to Order Total amount.
                try
                {
                    Assert.Equal(actualProductSold, doubleOrderTotalPrice);
                    logger.LogCheckPoint(string.Format(LogMessage.ProductSoldEqualsToOrderTotal, actualProductSold, doubleOrderTotalPrice));

                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.ProductSoldNotEqualsToOrderTotal, actualProductBought, doubleOrderTotalPrice));
                }

                //This will verify if Receiver amount is deducted after fee.
                try
                {
                    Assert.Equal(receivedBTCAmount, doubleOrderTotalPrice);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedReceivedBTCAmountAfterFeeDeductingPassed, receivedBTCAmount, actualProductBought));

                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedReceivedBTCAmountAfterFeeDeductingFailed, receivedBTCAmount, doubleFeePrice));
                }
                Thread.Sleep(2000);
                SubmitReportButton();
                Thread.Sleep(2000);
                VerifyFinalAmountAndFinalValue();
                string feeValueText = LabelConfirmBlockTradeFeeValue().Text;
                var blockTradePrice = GetBlockTradePrice(producBoughtPrice, productSoldPrice);
                //string dateText;
                //string expectedBlockTradeDate;
                //Dictionary<string, string> placeBlockTradeReportOrder = new Dictionary<string, string>();
                //dateText = LabelConfirmBlockTradeDate().Text;
                //expectedBlockTradeDate = dateText;
                //string actualBlockTradeDate = GenericUtils.GetCurrentTime();
                //string actualBlockTradeDatePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                //placeBlockTradeReportOrder.Add(placeBlockTradeDateTimeOfBlockTrade, actualBlockTradeDate);
                //placeBlockTradeReportOrder.Add(placeBlockTradeDateTimePlusOneMinOfBlockTrade, actualBlockTradeDatePlusOneMin);
                Thread.Sleep(2000);
                ConfirmSubmitReportButton();
                Thread.Sleep(2000);
                string expectedBuyTradeReportOrderMsg = BuyTradeReportOrderMsg().Text;
                string actualCancelMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                string actualBuyTradeReportOrderMsg = String.Format(LogMessage.BuyTradeReportOrderMesgSuccess);
                string expectedCancelMsg = String.Format(LogMessage.BuyTradeReportOrderMesgCanceled);
                try
                {
                    Assert.Equal(expectedBuyTradeReportOrderMsg, actualBuyTradeReportOrderMsg);
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgSuccess));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgCanceled));
                }

                Thread.Sleep(2000);
                UserSetFunctions.Click(CloseReportBlockTradeWindowSection());
                try
                {
                    VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, logger);
                    //Need to create a logic to cehck if block trade oorder comings in open trade order
                    objVerifyOrdersTab.VerifyBlockTradeReportsInOpenOrderTab(instrument, buyTab, typeValue, producBoughtPrice, blockTradePrice, statusValue, cancelValue);
                    logger.LogCheckPoint(String.Format(LogMessage.BuyBlockTradeReportInOpenTabWithWorkingStatusPassed, buyTab));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.BuyBlockTradeReportInOpenTabWithWorkingStatusFailed, buyTab));
                }
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will perform a simple Block Buy trade operation and verify the fields(Used for TC_33)
        public void VerifyElementsAndSubmitBlockTradeReport(string counterPartyPrice, string wrongCounterPrice, string producBoughtPrice, string productSoldPrice)
        {
            string ExpectedCounterPartyMsg = string.Format(LogMessage.CounterPartyErrorMsgVerified, wrongCounterPrice);
            try
            {
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
                UserSetFunctions.EnterText(CounterPartyTextField(), wrongCounterPrice);
                UserSetFunctions.EnterText(ProductBoughtTextField(), producBoughtPrice);
                UserSetFunctions.EnterText(ProductSoldTextField(), productSoldPrice);
                Thread.Sleep(2000);
                SubmitReportButton();
                Thread.Sleep(2000);
                ConfirmSubmitReportButton();
                string AcceptedCountrPartyErrorMsg = CounterPartyErrorMsg().Text;

                //This will verify if the counterParty error message is coming
                try
                {
                    Assert.Equal(ExpectedCounterPartyMsg, AcceptedCountrPartyErrorMsg);
                    logger.LogCheckPoint(string.Format(LogMessage.CounterPartyErrorMsgVerified, wrongCounterPrice));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.CounterPartyErrorMsgVerified));
                }
                Thread.Sleep(2000);
                UserSetFunctions.Clear(CounterPartyTextField());
                UserSetFunctions.EnterText(CounterPartyTextField(), counterPartyPrice);
                Thread.Sleep(2000);
                LockedInCheckBoxButton();
                Thread.Sleep(2000);
                string productBought = ProductBoughtTextField().GetAttribute(string.Format(LogMessage.ProductBoughtTextFieldAttributeValue));
                double actualProductBought = double.Parse(productBought);

                string productSold = ProductSoldTextField().GetAttribute(string.Format(LogMessage.ProductSoldTextFieldAttributeValue));
                double actualProductSold = double.Parse(productSold);

                string orderTotalAmountStringValue = OrderTotalAmount().Text;
                string orderTotalAmountdigits = orderTotalAmountStringValue.Split(" ")[1];
                double doubleOrderTotalPrice = Double.Parse(orderTotalAmountdigits);

                string receivedAmountStringValue = ReceivedAmount().Text;
                string receivedAmountdigits = receivedAmountStringValue.Split(" ")[1];
                double doubleReceivedPrice = Double.Parse(receivedAmountdigits);

                string feeAmountStringValue = FeeAmount().Text;
                string feeAmountdigits = feeAmountStringValue.Split(" ")[1];
                double doubleFeePrice = Double.Parse(feeAmountdigits);

                double receivedBTCAmount = actualProductBought - doubleFeePrice;

                //This will verify if product Sold is equal to Order Total amount.
                try
                {
                    Assert.Equal(actualProductSold, doubleOrderTotalPrice);
                    logger.LogCheckPoint(string.Format(LogMessage.ProductSoldEqualsToOrderTotal, actualProductSold, doubleOrderTotalPrice));

                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.ProductSoldNotEqualsToOrderTotal, actualProductBought, doubleOrderTotalPrice));
                }

                //This will verify if Receiver amount is deducted after fee.
                try
                {
                    Assert.Equal(receivedBTCAmount, doubleOrderTotalPrice);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedReceivedBTCAmountAfterFeeDeductingPassed, receivedBTCAmount, actualProductBought));

                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedReceivedBTCAmountAfterFeeDeductingFailed, receivedBTCAmount, doubleFeePrice));
                }
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will verify the block trade in Admin UI
        public void VerifyBlockTradeInAdmin(string accountTypeID, string counterPartyID, string instrument, string originalQuantity, string quantityExecuted)
        {
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);
            try
            {
                objAdminCommonFunctions.SelectTradeMenu();
                objAdminCommonFunctions.BlockTradeBtn();
                objAdminCommonFunctions.BlockTradeInstrumentSelection(instrument);
                objAdminCommonFunctions.BuyBlockTradeList(accountTypeID, counterPartyID, instrument, originalQuantity, quantityExecuted);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string GetBlockTradePrice(string productBoughtPrice, string productSoldPrice)
        {
            string firstValuePrecision = Convert.ToDecimal(productBoughtPrice).ToString("#,##0.00000000");
            string secondValuePrecision = Convert.ToDecimal(productSoldPrice).ToString("#,##0.00000000");
            var firstValueToDouble = Double.Parse(firstValuePrecision);
            var secondValueToDouble = Double.Parse(secondValuePrecision);
            double price = Math.Abs(secondValueToDouble / firstValueToDouble);
            string blockTradePrice = String.Format("{0:0.00000000}", price);
            return blockTradePrice;
        }
    }
}