using System;
using System.Collections.Generic;
using System.Threading;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using Xunit;


namespace AlphaPoint_QA.pages
{
    public class OrderEntryPage
    {
        IWebDriver driver;
        ProgressLogger logger;

        public OrderEntryPage(IWebDriver driver, ProgressLogger logger)
        {
            this.driver = driver;
            this.logger = logger;
        }

        /// <summary>
        /// Locators for elements
        /// </summary>
        public By orderEntryButton = By.XPath("//div[@data-test='Order Entry']");
        public By buyOrderEntryButton = By.XPath("//label[@data-test='Buy Side']");
        public By marketOrderTypeButton = By.XPath("//label[@data-test='Market Order Type']");
        public By buyAmountTextField = By.XPath("//input[@data-test='Buy Amount']");
        public By placeBuyOrderButton = By.XPath("//button[text()='Place Buy Order']");
        public By lastPrice = By.XPath("//span[@class='instrument-table__value instrument-table__value--last-price']");
        public By placeSellOrderButton = By.XPath("//button[text()='Place Sell Order']");
        public By stopOrderTypeButton = By.XPath("//label[@data-test='Stop Order Type']");
        public By sellAmountTextField = By.XPath("//input[@data-test='Sell Amount']");
        public By limitOrderTypeButton = By.XPath("//label[@data-test='Limit Order Type']");
        public By sellOrderEntryButton = By.XPath("//label[@data-test='Sell Side']");
        public By limitPriceTextBox = By.XPath("//div[@class='ap-input__input-box order-entry__input-box']//input[@name='limitPrice']");
        public By timeInForce = By.XPath("//select[@name='timeInForce']");

        public By feesText = By.XPath("//div[contains(@class,'ap-label-with-text')]//label[contains(@class,'order-entry__lwt-label') and text()='Fees']");
        public By orderTotalText = By.XPath("//label[contains(@class,'ap--label ap-label-with-text__label') and text()='Order Total']//following::span[@class='ap-label-with-text__text order-entry__lwt-text']");
        public By netText = By.XPath("//div[contains(@class,'ap-label-with-text')]//label[contains(@class,'order-entry__lwt-label') and text()='Net']");
        public By marketPriceText = By.XPath("//label[contains(@class,'ap--label ap-label-with-text__label') and text()='Market Price']//following::span[@data-test='Market Price']");
        public By stopPriceTextField = By.XPath("//input[@data-test='Stop Price']");
        public By exchangeMenuText = By.XPath("//span[@class='page-header-nav__label' and text()='Exchange']");
        public By successfullymsg = By.XPath("//div[contains(@class,'snackbar snackbar')]/div");
        static By askPrice = By.XPath("//span[@class='instrument-table__value instrument-table__value--hideable instrument-table__value--ask']");

        public IWebElement PlaceBuyOrderButton()
        {
            return GenericUtils.WaitForElementClickable(driver, placeBuyOrderButton, 30);
        }

        public IWebElement TransactionMessage()
        {
            return driver.FindElement(successfullymsg);
        }

        public IWebElement PlaceSellOrderButton()
        {
            return GenericUtils.WaitForElementClickable(driver, placeSellOrderButton, 30);
        }

        public IWebElement StopOrderTypeButton()
        {
            return driver.FindElement(stopOrderTypeButton);
        }

        public IWebElement BuyAmountTextField()
        {
            return driver.FindElement(buyAmountTextField);
        }

        public IWebElement SellAmountTextField()
        {
            return driver.FindElement(sellAmountTextField);
        }
        public static IWebElement AskPrice(IWebDriver driver)
        {
            return driver.FindElement(askPrice);
        }

        public IWebElement MarketPriceText()
        {
            return driver.FindElement(marketPriceText);
        }

        public IWebElement MarketOrderTypeButton()
        {
            return driver.FindElement(marketOrderTypeButton);
        }
        public IWebElement StopPriceTextField()
        {
            return driver.FindElement(stopPriceTextField);
        }

        public IWebElement LimitPriceTextBox()
        {
            return driver.FindElement(limitPriceTextBox);
        }

        public IWebElement TimeInForce()
        {
            return driver.FindElement(timeInForce);
        }

        public IWebElement LimitOrderTypeButton()
        {
            return driver.FindElement(limitOrderTypeButton);
        }

        public IWebElement BuyOrderEntryButton()
        {
            return driver.FindElement(buyOrderEntryButton);
        }

        public IWebElement SellOrderEntryButton()
        {
            return driver.FindElement(sellOrderEntryButton);
        }

        public IWebElement OrderEntryButton()
        {
            return driver.FindElement(orderEntryButton);
        }

        public IWebElement FeesText()
        {
            return driver.FindElement(feesText);
        }

        public IWebElement OrderTotalText()
        {
            return driver.FindElement(orderTotalText);
        }

        public IWebElement NetText()
        {
            return driver.FindElement(netText);
        }

        public IWebElement ExchangeMenuText()
        {
            return driver.FindElement(exchangeMenuText);
        }

        public void ClickPlaceBuyOrder()
        {
            UserSetFunctions.Click(PlaceBuyOrderButton());
        }

        public void ClickPlaceSellOrder()
        {
            UserSetFunctions.Click(PlaceSellOrderButton());
        }

        // This method enters the Buy Amount value
        public void EnterBuyAmount(string amountEntered)
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.EnterText(BuyAmountTextField(), amountEntered);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // This method enters the Sell Amount value
        public void EnterSellAmount(string amountEntered)
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.EnterText(SellAmountTextField(), amountEntered);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // This method click on market button in order entry page
        public void MarketOrderTypeBtn()
        {
            try
            {
                UserSetFunctions.Click(MarketOrderTypeButton());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // This method click on sell button tab in order entry page
        public void SellOrderEntryBtn()
        {
            try
            {
                UserSetFunctions.Click(SellOrderEntryButton());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void NavigateToHomePage(string instrument)
        {
            string exchangeStringValueFromSite;
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            Thread.Sleep(2000);
            exchangeStringValueFromSite = ExchangeMenuText().Text;
            Thread.Sleep(2000);
            if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Thread.Sleep(2000);
            }
        }

        // This method fetches the Last Price value
        public string GetLastPrice()
        {
            return driver.FindElement(lastPrice).Text;
        }

        // This method selects Limit button for Buy side
        public void SelectBuyLimitButton()
        {
            UserSetFunctions.Click(BuyOrderEntryButton());
            UserSetFunctions.Click(LimitOrderTypeButton());
        }

        // This method selects Limit button for Sell side
        public void SelectSellLimitButton()
        {
            UserSetFunctions.Click(SellOrderEntryButton());
            UserSetFunctions.Click(LimitOrderTypeButton());
        }

        // This method verifies the persistence of Amount entered in the Order Size field
        public bool VerifyOrderEntryAmountPersistence(string instrument, string amountEntered)
        {
            bool flag = false;
            string exchangeStringValueFromSite;
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            Thread.Sleep(2000);
            exchangeStringValueFromSite = ExchangeMenuText().Text;
            if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserSetFunctions.Click(MarketOrderTypeButton());
                UserSetFunctions.EnterText(BuyAmountTextField(), amountEntered);
                UserSetFunctions.Click(LimitOrderTypeButton());
                UserSetFunctions.Click(StopOrderTypeButton());
                UserSetFunctions.Click(MarketOrderTypeButton());
                Thread.Sleep(2000);
                string amountPersisted = BuyAmountTextField().GetAttribute("value");
                if (amountEntered.Equals(amountPersisted))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.AmountPersistenceCheckSuccessMsg, TestData.GetData("BuyTab")));
                    flag = true;
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.AmountPersistenceCheckFailureMsg, TestData.GetData("BuyTab")));
                    flag = false;
                }

                UserSetFunctions.Click(SellOrderEntryButton());
                UserSetFunctions.EnterText(SellAmountTextField(), amountEntered);
                UserSetFunctions.Click(MarketOrderTypeButton());
                UserSetFunctions.Click(LimitOrderTypeButton());
                UserSetFunctions.Click(StopOrderTypeButton());

                Thread.Sleep(2000);
                if (amountEntered.Equals(amountPersisted))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.AmountPersistenceCheckSuccessMsg, TestData.GetData("SellTab")));
                    flag = true;
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.AmountPersistenceCheckFailureMsg, TestData.GetData("SellTab")));
                    flag = false;
                }
            }
            else
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerificationFailed);
                flag = false;
            }
            return flag;
        }

        public bool VerifyOrderTotal(string orderTotalValue, string buyAmountValue, string marketPriceValue)
        {
            double orderTotalCalculated;
            string orderTotalFromUI;
            string orderTotalInDouble;
            string feeComponent;
            string marketPriceFromUI;
            string orderTotalToCompare;
            string orderTotalExpected;
            feeComponent = TestData.GetData("OrderTotalFeeComponent");
            marketPriceFromUI = UserCommonFunctions.GetSplitValue(marketPriceValue);
            orderTotalFromUI = UserCommonFunctions.GetSplitValue(orderTotalValue);
            orderTotalCalculated = Double.Parse(marketPriceFromUI) * Double.Parse(buyAmountValue) * Double.Parse(feeComponent);
            orderTotalInDouble = GenericUtils.ConvertToDoubleFormat(orderTotalCalculated);
            orderTotalExpected = GenericUtils.RemoveCommaFromString(orderTotalInDouble);
            orderTotalToCompare = GenericUtils.RemoveCommaFromString(orderTotalFromUI);
            if (orderTotalExpected.Equals(orderTotalToCompare))
            {
                return true;
            }
            else
            {
                logger.LogCheckPoint(LogMessage.OrderTotalVerificationFailed);
                return false;
            }
        }

        // This method calculates order total in case of limit buy order
        public bool VerifyLimitOrderTotal(string orderTotalValue, string amountValue, string limitPriceValue)
        {
            double orderTotalCalculated;
            string orderTotalFromUI;
            string orderTotalInDouble;
            string orderTotalToCompare;
            string orderTotalExpected;
            orderTotalFromUI = UserCommonFunctions.GetSplitValue(orderTotalValue);
            orderTotalCalculated = Double.Parse(limitPriceValue) * Double.Parse(amountValue);
            orderTotalInDouble = GenericUtils.ConvertToDoubleFormat(orderTotalCalculated);
            orderTotalExpected = GenericUtils.RemoveCommaFromString(orderTotalInDouble);
            orderTotalToCompare = GenericUtils.RemoveCommaFromString(orderTotalFromUI);
            if (orderTotalExpected.Equals(orderTotalToCompare))
            {
                return true;
            }
            else
            {
                logger.LogCheckPoint(LogMessage.OrderTotalVerificationFailed);
                return false;
            }
        }

        public bool VerifyFees(string feeValue, string buyAmountValue)
        {
            string feeCalculated;
            string feeFromUI;
            string feeInDouble;
            string feeComponent;
            string feeToCompare;
            string feeExpected;
            feeComponent = TestData.GetData("FeeComponent");
            feeCalculated = GenericUtils.FeeAmount(buyAmountValue, feeComponent);
            feeInDouble = GenericUtils.ConvertToDoubleFormat(Double.Parse(feeCalculated));
            feeExpected = GenericUtils.RemoveCommaFromString(feeInDouble);
            feeFromUI = UserCommonFunctions.GetSplitValue(feeValue);
            feeToCompare = GenericUtils.RemoveCommaFromString(feeFromUI);
            if (feeExpected.Equals(feeToCompare))
            {
                return true;
            }
            else
            {
                logger.LogCheckPoint(LogMessage.FeesVerificationFailed);
                return false;
            }
        }

        public bool VerifySellMarketFees(string feeValue, string sellAmount, string marketPrice)
        {
            string feeCalculated;
            string feeFromUI;
            string feeInDouble;
            string feeComponent;
            string feeToCompare;
            string feeExpected;
            string marketPriceValue;
            feeComponent = TestData.GetData("SellFeeComponent");
            marketPriceValue = UserCommonFunctions.GetSplitValue(marketPrice);
            feeCalculated = GenericUtils.SellFeeAmount(sellAmount, marketPriceValue, feeComponent);
            feeInDouble = GenericUtils.ConvertToDoubleFormat(Double.Parse(feeCalculated));
            feeExpected = GenericUtils.RemoveCommaFromString(feeInDouble);
            feeFromUI = UserCommonFunctions.GetSplitValue(feeValue);
            feeToCompare = GenericUtils.RemoveCommaFromString(feeFromUI);
            if (feeExpected.Equals(feeToCompare))
            {
                return true;
            }
            else
            {
                logger.LogCheckPoint(LogMessage.FeesVerificationFailed);
                return false;
            }
        }

        public bool VerifySellLimitFees(string feeValue, string sellAmount, string limitPrice)
        {
            string feeCalculated;
            string feeFromUI;
            string feeInDouble;
            string feeComponent;
            string feeToCompare;
            string feeExpected;
            feeComponent = TestData.GetData("FeeComponent");
            feeCalculated = GenericUtils.SellFeeAmount(sellAmount, limitPrice, feeComponent);
            feeInDouble = GenericUtils.ConvertToDoubleFormat(Double.Parse(feeCalculated));
            feeExpected = GenericUtils.RemoveCommaFromString(feeInDouble);
            feeFromUI = UserCommonFunctions.GetSplitValue(feeValue);
            feeToCompare = GenericUtils.RemoveCommaFromString(feeFromUI);
            if (feeExpected.Equals(feeToCompare))
            {
                return true;
            }
            else
            {
                logger.LogCheckPoint(LogMessage.FeesVerificationFailed);
                return false;
            }
        }

        public bool VerifyNet(string netValue, string amountValue)
        {
            string feeCalculated;
            string netFromUI;
            string feeInDouble;
            string feeComponent;
            string netDifference;
            string netComponent;
            string feeExpected;
            feeComponent = TestData.GetData("FeeComponent");
            feeCalculated = GenericUtils.FeeAmount(amountValue, feeComponent);
            feeInDouble = GenericUtils.ConvertToDoubleFormat(Double.Parse(feeCalculated));
            feeExpected = GenericUtils.RemoveCommaFromString(feeInDouble);
            netFromUI = UserCommonFunctions.GetSplitValue(netValue);
            netComponent = GenericUtils.RemoveCommaFromString(netFromUI);
            netDifference = GenericUtils.GetDifferenceFromStringAfterSubstraction(amountValue, feeExpected);
            if (netDifference.Equals(netComponent))
            {
                return true;
            }
            else
            {
                logger.LogCheckPoint(LogMessage.NetVerificationFailed);
                return false;
            }
        }


        public bool VerifySellOrderNet(string netValue, string feeValue, string orderTotalValue)
        {
            string feeFromUI;
            string netFromUI;
            string orderTotalFromUI;
            string netComponent;
            string netDifference;
            feeFromUI = UserCommonFunctions.GetSplitValue(feeValue);
            orderTotalFromUI = UserCommonFunctions.GetSplitValue(orderTotalValue);
            netFromUI = UserCommonFunctions.GetSplitValue(netValue);
            netComponent = GenericUtils.RemoveCommaFromString(netFromUI);
            netDifference = GenericUtils.GetDifferenceFromStringAfterSubstraction(orderTotalFromUI, feeFromUI);
            if (netDifference.Equals(netComponent))
            {
                return true;
            }
            else
            {
                logger.LogCheckPoint(LogMessage.NetVerificationFailed);
                return false;
            }
        }


        public Dictionary<string, string> PlaceMarketBuyOrder(string instrument, string side, double buyAmount)
        {
            try
            {
                string exchangeStringValueFromSite;
                string buyAmountValue = buyAmount.ToString();
                string successMsg;
                string placeOrderTime;
                string placeOrderTimePlusOneMin;

                Dictionary<string, string> marketBuyOrderData = new Dictionary<string, string>();
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                Thread.Sleep(2000);
                exchangeStringValueFromSite = ExchangeMenuText().Text;
                Thread.Sleep(2000);

                if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
                {
                    logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                    UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                    UserCommonFunctions.CancelAllOrders(driver);
                    UserSetFunctions.Click(OrderEntryButton());
                    UserSetFunctions.Click(BuyOrderEntryButton());
                    UserSetFunctions.Click(MarketOrderTypeButton());
                    UserSetFunctions.EnterText(BuyAmountTextField(), buyAmountValue);
                    Thread.Sleep(2000);
                    Dictionary<string, string> balances = new Dictionary<string, string>();
                    if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                    {
                        // Storing balances in Dictionary
                        Thread.Sleep(1000);
                        balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                        Assert.True(VerifyFees(balances[Const.Fees], buyAmountValue));
                        Assert.True(VerifyOrderTotal(balances[Const.OrderTotal], buyAmountValue, balances[Const.MarketPrice]));
                        Assert.True(VerifyNet(balances[Const.Net], buyAmountValue));
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerifiedSuccessfully, side, balances[Const.Fees], balances[Const.OrderTotal], balances[Const.Net]));
                    }
                    else
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerificationFailed, side));
                    }
                    UserSetFunctions.Click(PlaceBuyOrderButton());
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.MarketOrderPlacedSuccessfully, side, buyAmount));
                    marketBuyOrderData.Add("Instrument", instrument);
                    marketBuyOrderData.Add("Side", side);
                    marketBuyOrderData.Add("BuyAmount", buyAmountValue);
                    marketBuyOrderData.Add("PlaceOrderTime", placeOrderTime);
                    marketBuyOrderData.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                    Thread.Sleep(2000);
                }
                else
                {
                    logger.LogCheckPoint(LogMessage.ExchangeMenuVerificationFailed);
                }
                UserCommonFunctions.ScrollingDownVertical(driver);
                Thread.Sleep(3000);
                return marketBuyOrderData;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<string, string> PlaceMarketSellOrder(string instrument, string side, double sellAmount, double feeComponent)
        {
            try
            {
                string exchangeStringValueFromSite;
                string fee = null;
                string sellAmountValue = sellAmount.ToString();
                string successMsg;
                string placeOrderTime;
                string placeOrderTimePlusOneMin;

                Dictionary<string, string> marketSellOrderData = new Dictionary<string, string>();
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                Thread.Sleep(2000);
                exchangeStringValueFromSite = ExchangeMenuText().Text;
                Thread.Sleep(2000);

                if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
                {
                    logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                    UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                    UserCommonFunctions.CancelAllOrders(driver);
                    UserSetFunctions.Click(driver.FindElement(orderEntryButton));
                    UserSetFunctions.Click(driver.FindElement(sellOrderEntryButton));
                    UserSetFunctions.Click(driver.FindElement(marketOrderTypeButton));
                    UserSetFunctions.EnterText(SellAmountTextField(), sellAmount.ToString());
                    Thread.Sleep(2000);
                    Dictionary<string, string> balances = new Dictionary<string, string>();
                    if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                    {
                        // Storing balances in Dictionary
                        Thread.Sleep(1000);
                        balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                        fee = UserCommonFunctions.GetSplitValue(balances[Const.Fees]);
                        Assert.True(VerifySellMarketFees(balances[Const.Fees], sellAmountValue, balances[Const.MarketPrice]));
                        Assert.True(VerifyOrderTotal(balances[Const.OrderTotal], sellAmountValue, balances[Const.MarketPrice]));
                        Assert.True(VerifySellOrderNet(balances[Const.Net], balances[Const.Fees], balances[Const.OrderTotal]));
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerifiedSuccessfully, side, balances[Const.Fees], balances[Const.OrderTotal], balances[Const.Net]));
                    }
                    else
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerificationFailed, side));
                    }

                    UserSetFunctions.Click(PlaceSellOrderButton());
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.MarketOrderPlacedSuccessfully, side, sellAmount));
                    marketSellOrderData.Add("Instrument", instrument);
                    marketSellOrderData.Add("Side", side);
                    marketSellOrderData.Add("SellAmount", sellAmountValue);
                    marketSellOrderData.Add("Fee", fee);
                    marketSellOrderData.Add("PlaceOrderTime", placeOrderTime);
                    marketSellOrderData.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                    Thread.Sleep(2000);
                }
                else
                {
                    logger.LogCheckPoint(LogMessage.ExchangeMenuVerificationFailed);
                }
                UserCommonFunctions.ScrollingDownVertical(driver);
                Thread.Sleep(3000);
                return marketSellOrderData;
            }
            catch (Exception e)
            {
                logger.Error(Const.MarketSellOrderFailureMsg);
                throw e;
            }
        }

        public Dictionary<string, string> PlaceStopBuyOrder(string instrument, string side, double buyAmount, double feeComponent, double stopPrice)
        {
            string exchangeStringValueFromSite;
            string fee = (buyAmount * feeComponent).ToString();
            string buyAmountValue = buyAmount.ToString();
            string successMsg;
            string placeOrderTime;
            string placeOrderTimePlusOneMin;
            string stopPriceValue = stopPrice.ToString();

            Dictionary<string, string> stopBuyOrderData = new Dictionary<string, string>();
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            Thread.Sleep(2000);
            exchangeStringValueFromSite = ExchangeMenuText().Text;
            Thread.Sleep(2000);

            if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                if (BuyOrderEntryButton().Displayed && StopOrderTypeButton().Displayed)
                {
                    UserSetFunctions.Click(OrderEntryButton());
                    UserSetFunctions.Click(BuyOrderEntryButton());
                    UserSetFunctions.Click(StopOrderTypeButton());
                    UserSetFunctions.EnterText(BuyAmountTextField(), buyAmount.ToString());
                    UserSetFunctions.EnterText(StopPriceTextField(), stopPrice.ToString());
                    Thread.Sleep(2000);

                    // Verify Market Price, Fees and Order Total
                    Dictionary<string, string> balances = new Dictionary<string, string>();
                    if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                    {
                        // Storing balances in Dictionary
                        Thread.Sleep(1000);
                        balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                        Assert.True(VerifyFees(balances[Const.Fees], buyAmountValue));
                        Assert.True(VerifyLimitOrderTotal(balances[Const.OrderTotal], buyAmountValue, stopPriceValue));
                        Assert.True(VerifyNet(balances[Const.Net], buyAmountValue));
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerifiedSuccessfully, side, balances[Const.Fees], balances[Const.OrderTotal], balances[Const.Net]));
                    }
                    else
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerificationFailed, side));
                    }
                    UserSetFunctions.Click(PlaceBuyOrderButton());
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.MarketOrderPlacedSuccessfully, side, buyAmount));
                    stopBuyOrderData.Add("Instrument", instrument);
                    stopBuyOrderData.Add("Side", side);
                    stopBuyOrderData.Add("BuyAmount", buyAmountValue);
                    stopBuyOrderData.Add("StopPrice", stopPriceValue);
                    stopBuyOrderData.Add("PlaceOrderTime", placeOrderTime);
                    stopBuyOrderData.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                    Thread.Sleep(2000);
                    UserCommonFunctions.ScrollingDownVertical(driver);
                    Thread.Sleep(2000);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifyStopSellOrderSuccess, side, buyAmountValue, stopPriceValue));
                }
                else
                {
                    logger.LogCheckPoint(LogMessage.ExchangeMenuVerificationFailed);
                }
            }
            UserCommonFunctions.ScrollingDownVertical(driver);
            Thread.Sleep(3000);
            return stopBuyOrderData;
        }

        public Dictionary<string, string> PlaceStopSellOrder(string instrument, string side, double sellAmount, double feeComponent, double stopPrice)
        {
            string exchangeStringValueFromSite;
            string fee = (sellAmount * feeComponent).ToString();
            string sellAmountValue = sellAmount.ToString();
            string successMsg;
            string placeOrderTime;
            string placeOrderTimePlusOneMin;
            string stopPriceValue = stopPrice.ToString();

            Dictionary<string, string> stopSellOrderData = new Dictionary<string, string>();
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            Thread.Sleep(2000);
            exchangeStringValueFromSite = ExchangeMenuText().Text;
            Thread.Sleep(2000);

            if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                if (SellOrderEntryButton().Displayed && StopOrderTypeButton().Displayed)
                {
                    UserSetFunctions.Click(OrderEntryButton());
                    UserSetFunctions.Click(SellOrderEntryButton());
                    UserSetFunctions.Click(StopOrderTypeButton());
                    UserSetFunctions.EnterText(SellAmountTextField(), sellAmount.ToString());
                    UserSetFunctions.EnterText(StopPriceTextField(), stopPrice.ToString());
                    Thread.Sleep(2000);

                    // Verify Market Price, Fees and Order Total
                    Dictionary<string, string> balances = new Dictionary<string, string>();
                    if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                    {
                        // Storing balances in Dictionary
                        Thread.Sleep(1000);
                        balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                        Assert.True(VerifySellLimitFees(balances[Const.Fees], sellAmountValue, stopPriceValue));
                        Assert.True(VerifyLimitOrderTotal(balances[Const.OrderTotal], sellAmountValue, stopPriceValue));
                        Assert.True(VerifySellOrderNet(balances[Const.Net], balances[Const.Fees], balances[Const.OrderTotal]));
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerifiedSuccessfully, side, balances[Const.Fees], balances[Const.OrderTotal], balances[Const.Net]));
                    }
                    else
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerificationFailed, side));
                    }
                    UserSetFunctions.Click(PlaceSellOrderButton());
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.MarketOrderPlacedSuccessfully, side, sellAmount));
                    stopSellOrderData.Add("Instrument", instrument);
                    stopSellOrderData.Add("Side", side);
                    stopSellOrderData.Add("SellAmount", sellAmountValue);
                    stopSellOrderData.Add("StopPrice", stopPriceValue);
                    stopSellOrderData.Add("PlaceOrderTime", placeOrderTime);
                    stopSellOrderData.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                    Thread.Sleep(2000);
                    UserCommonFunctions.ScrollingDownVertical(driver);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifyStopSellOrderSuccess, side, sellAmountValue, stopPriceValue));
                }
                else
                {
                    logger.LogCheckPoint(LogMessage.ExchangeMenuVerificationFailed);
                }
            }
            return stopSellOrderData;
        }

        public void PlaceMultipleLimitBuyOrder(string instrument, string side, string buyAmount, string limitPrice, string timeinforce)
        {
            string successMsg;
            string placeOrderTime;
            string placeOrderTimePlusOneMin;

            Dictionary<string, string> limitBuyOrderData = new Dictionary<string, string>();
                if (BuyOrderEntryButton().Displayed && StopOrderTypeButton().Displayed)
                {
                    UserSetFunctions.Click(OrderEntryButton());
                    UserSetFunctions.Click(BuyOrderEntryButton());
                    UserSetFunctions.Click(MarketOrderTypeButton());
                    Thread.Sleep(1000);
                    UserSetFunctions.Click(LimitOrderTypeButton());
                    UserSetFunctions.EnterText(BuyAmountTextField(), buyAmount);
                    UserSetFunctions.EnterText(LimitPriceTextBox(), limitPrice);
                    UserSetFunctions.SelectDropdown(TimeInForce(), timeinforce);
                    // Verify Market Price, Fees and Order Total
                    Dictionary<string, string> balances = new Dictionary<string, string>();
                    if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                    {
                        // Storing balances in Dictionary
                        Thread.Sleep(1000);
                        balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                        Assert.True(VerifyFees(balances[Const.Fees], buyAmount));
                        Assert.True(VerifyLimitOrderTotal(balances[Const.OrderTotal], buyAmount, limitPrice));
                        Assert.True(VerifyNet(balances[Const.Net], buyAmount));
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerifiedSuccessfully, side, balances[Const.Fees], balances[Const.OrderTotal], balances[Const.Net]));
                    }
                    else
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerificationFailed, side));
                    }
                    UserSetFunctions.Click(PlaceBuyOrderButton());
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.MarketOrderPlacedSuccessfully, side, buyAmount));
                    Thread.Sleep(2000);
                    UserCommonFunctions.ScrollingDownVertical(driver);
                    logger.LogCheckPoint(String.Format(LogMessage.LimitOrderSuccessMsg, side, buyAmount, limitPrice));
                }                      
        }

        public void PlaceMultipleLimitSellOrder(string instrument, string side, string sellAmount, string limitPrice, string timeinforce)
        {
            string successMsg;
            string placeOrderTime;
            string placeOrderTimePlusOneMin;

            Dictionary<string, string> limitSellOrderData = new Dictionary<string, string>();
            
                string askPrice = AskPrice(driver).Text;
                if (BuyOrderEntryButton().Displayed && StopOrderTypeButton().Displayed)
                {
                    UserSetFunctions.Click(OrderEntryButton());
                    UserSetFunctions.Click(SellOrderEntryButton());
                    UserSetFunctions.Click(MarketOrderTypeButton());
                    Thread.Sleep(1000);
                    UserSetFunctions.Click(LimitOrderTypeButton());
                    UserSetFunctions.EnterText(SellAmountTextField(), sellAmount);
                    UserSetFunctions.EnterText(LimitPriceTextBox(), limitPrice);
                    UserSetFunctions.SelectDropdown(TimeInForce(), timeinforce);
                    // Verify Market Price, Fees and Order Total
                    Dictionary<string, string> balances = new Dictionary<string, string>();
                    if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                    {
                        // Storing balances in Dictionary
                        Thread.Sleep(1000);
                        balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                        Assert.True(VerifySellLimitFees(balances[Const.Fees], sellAmount, limitPrice));
                        Assert.True(VerifyLimitOrderTotal(balances[Const.OrderTotal], sellAmount, limitPrice));
                        Assert.True(VerifySellOrderNet(balances[Const.Net], balances[Const.Fees], balances[Const.OrderTotal]));
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerifiedSuccessfully, side, balances[Const.Fees], balances[Const.OrderTotal], balances[Const.Net]));
                    }
                    else
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerificationFailed, side));
                    }
                    UserSetFunctions.Click(PlaceSellOrderButton());
                    UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.MarketOrderPlacedSuccessfully, side, sellAmount));
                    Thread.Sleep(2000);
                    UserCommonFunctions.ScrollingDownVertical(driver);
                    logger.LogCheckPoint(String.Format(LogMessage.LimitOrderSuccessMsg, side, sellAmount, limitPrice));
                }          
        }

        //Limit buy Order
        public Dictionary<string, string> PlaceLimitBuyOrder(string instrument, string side, string buyAmount, string limitPrice, string timeinforce)
        {
            string exchangeStringValueFromSite;
            string successMsg;
            string placeOrderTime;
            string placeOrderTimePlusOneMin;

            Dictionary<string, string> limitBuyOrderData = new Dictionary<string, string>();
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            Thread.Sleep(2000);
            exchangeStringValueFromSite = ExchangeMenuText().Text;
            Thread.Sleep(2000);
            if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                if (BuyOrderEntryButton().Displayed && StopOrderTypeButton().Displayed)
                {
                    UserSetFunctions.Click(OrderEntryButton());
                    UserSetFunctions.Click(BuyOrderEntryButton());
                    UserSetFunctions.Click(LimitOrderTypeButton());
                    UserSetFunctions.EnterText(BuyAmountTextField(), buyAmount);
                    UserSetFunctions.EnterText(LimitPriceTextBox(), limitPrice);
                    UserSetFunctions.SelectDropdown(TimeInForce(), timeinforce);

                    // Verify Market Price, Fees and Order Total
                    Dictionary<string, string> balances = new Dictionary<string, string>();
                    if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                    {
                        // Storing balances in Dictionary
                        Thread.Sleep(1000);
                        balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                        Assert.True(VerifyFees(balances[Const.Fees], buyAmount));
                        Assert.True(VerifyLimitOrderTotal(balances[Const.OrderTotal], buyAmount, limitPrice));
                        Assert.True(VerifyNet(balances[Const.Net], buyAmount));
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerifiedSuccessfully, side, balances[Const.Fees], balances[Const.OrderTotal], balances[Const.Net]));
                    }
                    else
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerificationFailed, side));
                    }
                    UserSetFunctions.Click(PlaceBuyOrderButton());
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    limitBuyOrderData.Add("PlaceOrderTime", placeOrderTime);
                    limitBuyOrderData.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                    Thread.Sleep(2000);
                    UserCommonFunctions.ScrollingDownVertical(driver);
                    logger.LogCheckPoint(String.Format(LogMessage.LimitOrderSuccessMsg, side, buyAmount, limitPrice));
                }
            }
            else
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerificationFailed);
            }
            return limitBuyOrderData;
        }

        //Limit sell Order
        public Dictionary<string, string> PlaceLimitSellOrder(string instrument, string side, string sellAmount, string limitPrice, string timeinforce)
        {
            string exchangeStringValueFromSite;
            string successMsg;
            string placeOrderTime;
            string placeOrderTimePlusOneMin;

            Dictionary<string, string> limitSellOrderData = new Dictionary<string, string>();
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            Thread.Sleep(2000);
            exchangeStringValueFromSite = ExchangeMenuText().Text;
            Thread.Sleep(2000);
            if (exchangeStringValueFromSite.Equals(TestData.GetData("MenuTab")))
            {
                logger.LogCheckPoint(LogMessage.ExchangeMenuVerifiedSuccessfully);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                string askPrice = AskPrice(driver).Text;
                if (BuyOrderEntryButton().Displayed && StopOrderTypeButton().Displayed)
                {
                    Thread.Sleep(1000);
                    UserSetFunctions.Click(OrderEntryButton());
                    UserSetFunctions.Click(SellOrderEntryButton());
                    UserSetFunctions.Click(LimitOrderTypeButton());
                    UserSetFunctions.EnterText(SellAmountTextField(), sellAmount);
                    UserSetFunctions.EnterText(LimitPriceTextBox(), limitPrice);
                    UserSetFunctions.SelectDropdown(TimeInForce(), timeinforce);
                    // Verify Market Price, Fees and Order Total
                    Dictionary<string, string> balances = new Dictionary<string, string>();
                    if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                    {
                        // Storing balances in Dictionary
                        Thread.Sleep(1000);
                        balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                        Assert.True(VerifySellLimitFees(balances[Const.Fees], sellAmount, limitPrice));
                        Assert.True(VerifyLimitOrderTotal(balances[Const.OrderTotal], sellAmount, limitPrice));
                        Assert.True(VerifySellOrderNet(balances[Const.Net], balances[Const.Fees], balances[Const.OrderTotal]));
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerifiedSuccessfully, side, balances[Const.Fees], balances[Const.OrderTotal], balances[Const.Net]));
                    }
                    else
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.BalancesVerificationFailed, side));
                    }
                    Thread.Sleep(1000);
                    UserSetFunctions.Click(PlaceSellOrderButton());
                    UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                    successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                    placeOrderTime = GenericUtils.GetCurrentTime();
                    placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                    Thread.Sleep(1000);
                    Assert.Equal(Const.OrderSuccessMsg, successMsg);
                    limitSellOrderData.Add("PlaceOrderTime", placeOrderTime);
                    limitSellOrderData.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                    Thread.Sleep(2000);
                    UserCommonFunctions.ScrollingDownVertical(driver);
                    logger.LogCheckPoint(String.Format(LogMessage.LimitOrderSuccessMsg, side, sellAmount, limitPrice));
                }
            }
            return limitSellOrderData;
        }
    }
}