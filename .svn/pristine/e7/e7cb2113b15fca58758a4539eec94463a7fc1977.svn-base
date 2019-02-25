using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class AdvancedOrderTest : TestBase
    {
        private string instrument;
        private string orderType;
        private string menuTab;
        private string buyTab;
        private string sellTab;
        private string orderSize;
        private string limitPrice;
        private string marketOrder;
        private string buyOrderLimitPrice;
        private string sellOrderLimitPrice;
        private string timeInForce;
        private string feeComponent;
        private string buyOrderSize;
        private string sellOrderSize;
        private string reserveOrder;
        private string buyOrderDisplayQty;
        private string sellOrderDisplayQty;
        private string stopPrice;
        private string orderTypeDropdown;
        private string limitPriceEqualsStop;
        private string trailingAmount;
        private string pegPriceDropdown;
        private string buyOrderFeeValue;
        private string sellOrderFeeValue;
        private string incSellOrderSize;
        private string decSellOrderSize;
        private string incBuyOrderSize;
        private string decBuyOrderSize;
        private string stopLimitPrice;
        private string equalSellOrderSize;
        private string equalBuyOrderSize;
        private string stopTimeInForce;
        private string triggerTrailingPrice;
        private string finalTrailingPrice;
        private string limitOffset;
        private string pegPrice;
        private string setMarketPrice;
        private string buyLimitPrice1;
        private string buyLimitPrice2;
        private string buyLimitPrice3;
        private string buyLimitPrice4;
        private string buyLimitPrice5;
        private string sellLimitPrice1;
        private string sellLimitPrice2;
        private string sellLimitPrice3;
        private string sellLimitPrice4;
        private string sellLimitPrice5;
        private string iocTimeInForce;
        private string fokTimeInForce;

        public AdvancedOrderTest(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]
        public void TC9_VerifyMarketOrderTypeAdvanceBuyOrder()
        {
            try
            {
                string askPrice;
                string buyOrderSuccessMsg;
                string feeValue;
                instrument = TestData.GetData("Instrument");
                marketOrder = TestData.GetData("MarketOrder");
                menuTab = TestData.GetData("MenuTab");
                sellTab = TestData.GetData("SellTab");
                buyTab = TestData.GetData("BuyTab");
                buyOrderSize = TestData.GetData("TC9_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC9_SellOrderSize");
                limitPrice = TestData.GetData("TC9_LimitPrice");
                timeInForce = TestData.GetData("TC9_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");
                feeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);

                // Creating Buy and Sell Order to match to set price
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                // Place sell order to set up market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));

                // Place Advance Market Buy Order
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(TestProgressLogger);
                advanceorder.SelectBuyOrSellTab(buyTab);
                advanceorder.SelectInstrumentsAndOrderType(instrument, marketOrder);
                var placeMarketBuyOrder = advanceorder.PlaceMarketBuyOrder(buyOrderSize);
                buyOrderSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, buyOrderSuccessMsg);
                TestProgressLogger.LogCheckPoint(buyOrderSuccessMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceMarketOrderSuccessMsg, buyTab, buyOrderSize));
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Verify the order placed in Filled Orders Tab
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(buyOrderSize), feeValue, placeMarketBuyOrder["PlaceOrderTime"], placeMarketBuyOrder["PlaceOrderTimePlusOneMin"]));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceMarketOrderFailureMsg, buyTab, buyOrderSize));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceMarketOrderFailureMsg, buyTab, buyOrderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }


        [Fact]
        public void TC10_VerifyMarketOrderTypeAdvanceSellOrder()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                marketOrder = TestData.GetData("MarketOrder");
                menuTab = TestData.GetData("MenuTab");
                sellTab = TestData.GetData("SellTab");
                buyTab = TestData.GetData("BuyTab");
                buyOrderSize = TestData.GetData("TC10_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC10_SellOrderSize");
                limitPrice = TestData.GetData("TC10_LimitPrice");
                timeInForce = TestData.GetData("TC10_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                string feeValue = GenericUtils.SellFeeAmount(buyOrderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER5);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(TestProgressLogger);
                advanceorder.SelectBuyOrSellTab(sellTab);
                advanceorder.SelectInstrumentsAndOrderType(instrument, marketOrder);
                var placeMarketSellOrder = advanceorder.PlaceMarketSellOrder(sellOrderSize);
                string sellOrderSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, sellOrderSuccessMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                TestProgressLogger.Info(String.Format(LogMessage.AdvanceMarketOrderSuccessMsg, sellTab, sellOrderSize));
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(sellOrderSize), feeValue, placeMarketSellOrder["PlaceOrderTime"], placeMarketSellOrder["PlaceOrderTimePlusOneMin"]));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceMarketOrderFailureMsg, sellTab, sellOrderSize));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceMarketOrderFailureMsg, sellTab, sellOrderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]
        public void TC11_VerifyIOCAdvLimitBuyOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TC11_OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC11_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC11_SellOrderSize");
                incSellOrderSize = TestData.GetData("TC11_IncSellOrderSize");
                decSellOrderSize = TestData.GetData("TC11_DecSellOrderSize");
                limitPrice = TestData.GetData("TC11_LimitPrice");
                timeInForce = TestData.GetData("TC11_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Creating Buy and Sell Order to get the last price
                //userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                // Scenario 1: Place Buy IOC order with buy order size = sell order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeIOCBuyOrderTime = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(buyOrderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, buyOrderSize, limitPrice));

                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(buyOrderSize), buyOrderFeeValue, placeIOCBuyOrderTime["PlaceOrderTime"], placeIOCBuyOrderTime["PlaceOrderTimePlusOneMin"]);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, buyTab, buyOrderSize, placeIOCBuyOrderTime));

                // Scenario 2: Place Buy IOC order with buy order size < sell order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, incSellOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, incSellOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeIOCBuyOrderTime2 = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(buyOrderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, buyOrderSize, limitPrice));
                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(buyOrderSize), buyOrderFeeValue, placeIOCBuyOrderTime2["PlaceOrderTime"], placeIOCBuyOrderTime2["PlaceOrderTimePlusOneMin"]);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, buyTab, buyOrderSize, placeIOCBuyOrderTime2));

                // Scenario 3: Place Buy IOC order with buy order size > sell order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, decSellOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, decSellOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeIOCBuyOrderTime3 = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(buyOrderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderCancelledMsg, successMsg);
                buyOrderFeeValue = GenericUtils.FeeAmount(decSellOrderSize, feeComponent);
                string orderSizeDifference = GenericUtils.GetDifferenceFromStringAfterSubstraction(buyOrderSize, decSellOrderSize);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, buyOrderSize, limitPrice));
                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(decSellOrderSize), buyOrderFeeValue, placeIOCBuyOrderTime3["PlaceOrderTime"], placeIOCBuyOrderTime3["PlaceOrderTimePlusOneMin"]);
                objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.Limit, Double.Parse(buyOrderSize), limitPrice, placeIOCBuyOrderTime3["PlaceOrderTime"], placeIOCBuyOrderTime3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, buyTab, buyOrderSize, placeIOCBuyOrderTime3));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(Const.IOCOrderTypeFailedMsg);
                TestProgressLogger.Error(Const.IOCOrderTypeFailedMsg, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact] // In progress
        public void TC12_VerifyIOCAdvLimitSellOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TC12_OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC12_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC12_SellOrderSize");
                incBuyOrderSize = TestData.GetData("TC12_IncSellOrderSize");
                decBuyOrderSize = TestData.GetData("TC12_DecSellOrderSize");
                limitPrice = TestData.GetData("TC12_LimitPrice");
                timeInForce = TestData.GetData("TC12_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Creating Buy and Sell Order to get the last price
                //userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                // Scenario 1: Place Sell IOC order with sell order size = buy order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeIOCBuyOrderTime = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(sellOrderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, sellOrderSize, limitPrice));

                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(sellOrderSize), sellOrderFeeValue, placeIOCBuyOrderTime["PlaceOrderTime"], placeIOCBuyOrderTime["PlaceOrderTimePlusOneMin"]);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, sellTab, sellOrderSize, placeIOCBuyOrderTime));

                // Scenario 2: Place Sell IOC order with sell order size < buy order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, incBuyOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, incBuyOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeIOCBuyOrderTime2 = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(sellOrderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, sellOrderSize, limitPrice));
                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(sellOrderSize), sellOrderFeeValue, placeIOCBuyOrderTime2["PlaceOrderTime"], placeIOCBuyOrderTime2["PlaceOrderTimePlusOneMin"]);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, sellTab, sellOrderSize, placeIOCBuyOrderTime2));

                // Scenario 3: Place Sell IOC order with sell order size > buy order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, decBuyOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, decBuyOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeIOCBuyOrderTime3 = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(sellOrderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderCancelledMsg, successMsg);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(decBuyOrderSize, limitPrice, feeComponent);
                string orderSizeDifference = GenericUtils.GetDifferenceFromStringAfterSubstraction(sellOrderSize, decBuyOrderSize);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, sellOrderSize, limitPrice));
                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(decBuyOrderSize), sellOrderFeeValue, placeIOCBuyOrderTime3["PlaceOrderTime"], placeIOCBuyOrderTime3["PlaceOrderTimePlusOneMin"]);
                objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.Limit, Double.Parse(sellOrderSize), limitPrice, placeIOCBuyOrderTime3["PlaceOrderTime"], placeIOCBuyOrderTime3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, sellTab, sellOrderSize, placeIOCBuyOrderTime3));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(Const.IOCOrderTypeFailedMsg);
                TestProgressLogger.Error(Const.IOCOrderTypeFailedMsg, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact] // In progress
        public void TC15_VerifyFOKAdvStopLimitBuyOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("StopLimitOrder");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC15_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC15_SellOrderSize");
                incSellOrderSize = TestData.GetData("TC15_IncSellOrderSize");
                decSellOrderSize = TestData.GetData("TC15_DecSellOrderSize");
                limitPrice = TestData.GetData("TC15_LimitPrice");
                timeInForce = TestData.GetData("TC15_TimeInForce");
                stopPrice = TestData.GetData("TC15_StopPrice");
                limitPriceEqualsStop = TestData.GetData("TC15_LimitPriceEqualsStopPrice");
                feeComponent = TestData.GetData("FeeComponent");
                stopLimitPrice = TestData.GetData("TC15_StopLimitPrice");
                equalSellOrderSize = TestData.GetData("TC15_EqualSellOrderSize");
                stopTimeInForce = TestData.GetData("TC15_StopTimeInForce");

                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Creating Buy and Sell Order to get the last price
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                // Scenario 1: Place Buy Stop Limit order with buy order size = sell order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeStopLimitBuyOrderTime = advanceOrder.PlaceStopLimitBuyOrder(buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedStopLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));

                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.StopLimit, Double.Parse(buyOrderSize), stopPrice, placeStopLimitBuyOrderTime["PlaceOrderTime"], placeStopLimitBuyOrderTime["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                orderEntryPage.PlaceMarketBuyOrder(instrument, buyTab, Double.Parse(buyOrderSize));

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, equalSellOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(buyOrderSize), buyOrderFeeValue, placeStopLimitBuyOrderTime["PlaceOrderTime"], placeStopLimitBuyOrderTime["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitFilledOrder, buyTab, buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));


                // Scenario 2: Place Buy Stop Limit order with buy order size < sell order size

                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeStopLimitBuyOrderTime2 = advanceOrder.PlaceStopLimitBuyOrder(buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedStopLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));

                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.StopLimit, Double.Parse(buyOrderSize), stopPrice, placeStopLimitBuyOrderTime2["PlaceOrderTime"], placeStopLimitBuyOrderTime2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                orderEntryPage.PlaceMarketBuyOrder(instrument, buyTab, Double.Parse(buyOrderSize));

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, incSellOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(buyOrderSize), buyOrderFeeValue, placeStopLimitBuyOrderTime2["PlaceOrderTime"], placeStopLimitBuyOrderTime2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitFilledOrder, buyTab, buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));


                // Scenario 3: Place Buy Stop Limit order with buy order size > sell order size
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeStopLimitBuyOrderTime3 = advanceOrder.PlaceStopLimitBuyOrder(buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedStopLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));

                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.StopLimit, Double.Parse(buyOrderSize), stopPrice, placeStopLimitBuyOrderTime3["PlaceOrderTime"], placeStopLimitBuyOrderTime3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                orderEntryPage.PlaceMarketBuyOrder(instrument, buyTab, Double.Parse(buyOrderSize));

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, decSellOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.StopLimit, Double.Parse(buyOrderSize), stopPrice, placeStopLimitBuyOrderTime3["PlaceOrderTime"], placeStopLimitBuyOrderTime3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitInactiveOrder, buyTab, Const.StopLimit, buyOrderSize, stopPrice));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(Const.IOCOrderTypeFailedMsg);
                TestProgressLogger.Error(Const.IOCOrderTypeFailedMsg, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]
        public void TC16_VerifyFOKAdvStopLimitBuyOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("StopLimitOrder");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC16_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC16_SellOrderSize");
                incBuyOrderSize = TestData.GetData("TC16_IncBuyOrderSize");
                decBuyOrderSize = TestData.GetData("TC16_DecBuyOrderSize");
                limitPrice = TestData.GetData("TC16_LimitPrice");
                timeInForce = TestData.GetData("TC16_TimeInForce");
                stopPrice = TestData.GetData("TC16_StopPrice");
                limitPriceEqualsStop = TestData.GetData("TC16_LimitPriceEqualsStopPrice");
                feeComponent = TestData.GetData("FeeComponent");
                stopLimitPrice = TestData.GetData("TC16_StopLimitPrice");
                equalBuyOrderSize = TestData.GetData("TC16_EqualBuyOrderSize");
                stopTimeInForce = TestData.GetData("TC16_StopTimeInForce");

                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, stopPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Creating Buy and Sell Order to get the last price
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                // Scenario 1: Place Sell Stop Limit order with sell order size = buy order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeStopLimitBuyOrderTime = advanceOrder.PlaceStopLimitSellOrder(sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedStopLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));

                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.StopLimit, Double.Parse(sellOrderSize), stopPrice, placeStopLimitBuyOrderTime["PlaceOrderTime"], placeStopLimitBuyOrderTime["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                orderEntryPage.PlaceMarketSellOrder(instrument, buyTab, Double.Parse(sellOrderSize), Double.Parse(feeComponent));

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, equalBuyOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(sellOrderSize), sellOrderFeeValue, placeStopLimitBuyOrderTime["PlaceOrderTime"], placeStopLimitBuyOrderTime["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitFilledOrder, sellTab, sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));


                // Scenario 2: Place Sell Stop Limit order with sell order size < buy order size

                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeStopLimitBuyOrderTime2 = advanceOrder.PlaceStopLimitSellOrder(sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedStopLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));

                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.StopLimit, Double.Parse(sellOrderSize), stopPrice, placeStopLimitBuyOrderTime2["PlaceOrderTime"], placeStopLimitBuyOrderTime2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                orderEntryPage.PlaceMarketSellOrder(instrument, buyTab, Double.Parse(sellOrderSize), Double.Parse(feeComponent));

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, incBuyOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(sellOrderSize), sellOrderFeeValue, placeStopLimitBuyOrderTime2["PlaceOrderTime"], placeStopLimitBuyOrderTime2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitFilledOrder, sellTab, sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));

                // Scenario 3: Place Sell Stop Limit order with sell order size > buy order size
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeStopLimitBuyOrderTime3 = advanceOrder.PlaceStopLimitSellOrder(sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedStopLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));

                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.StopLimit, Double.Parse(sellOrderSize), stopPrice, placeStopLimitBuyOrderTime3["PlaceOrderTime"], placeStopLimitBuyOrderTime3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                orderEntryPage.PlaceMarketSellOrder(instrument, buyTab, Double.Parse(sellOrderSize), Double.Parse(feeComponent));

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, decBuyOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.StopLimit, Double.Parse(sellOrderSize), stopPrice, placeStopLimitBuyOrderTime3["PlaceOrderTime"], placeStopLimitBuyOrderTime3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitInactiveOrder, buyTab, Const.StopLimit, buyOrderSize, stopPrice));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(Const.IOCOrderTypeFailedMsg);
                TestProgressLogger.Error(Const.IOCOrderTypeFailedMsg, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact] // Check messages
        public void TC19_VerifyAdvTrailingStopLimitBuyOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string initialTrailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TrailingStopLimit");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                setMarketPrice = TestData.GetData("TC19_SetMarketPrice");
                buyOrderSize = TestData.GetData("TC19_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC19_SellOrderSize");
                incSellOrderSize = TestData.GetData("TC19_IncSellOrderSize");
                decSellOrderSize = TestData.GetData("TC19_DecSellOrderSize");
                equalSellOrderSize = TestData.GetData("TC19_EqualSellOrderSize");
                timeInForce = TestData.GetData("TC19_TimeInForce");
                trailingAmount = TestData.GetData("TC19_TrailingAmount");
                triggerTrailingPrice = TestData.GetData("TC19_TrailingPrice");
                limitOffset = TestData.GetData("TC19_LimitOffset");
                pegPrice = TestData.GetData("TC19_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                buyLimitPrice1 = TestData.GetData("TC19_BuyLimitPrice_1");
                buyLimitPrice2 = TestData.GetData("TC19_BuyLimitPrice_2");
                buyLimitPrice3 = TestData.GetData("TC19_BuyLimitPrice_3");
                buyLimitPrice4 = TestData.GetData("TC19_BuyLimitPrice_4");
                buyLimitPrice5 = TestData.GetData("TC19_BuyLimitPrice_5");
                sellLimitPrice1 = TestData.GetData("TC19_SellLimitPrice_1");
                sellLimitPrice2 = TestData.GetData("TC19_SellLimitPrice_2");
                sellLimitPrice3 = TestData.GetData("TC19_SellLimitPrice_3");
                sellLimitPrice4 = TestData.GetData("TC19_SellLimitPrice_4");
                sellLimitPrice5 = TestData.GetData("TC19_SellLimitPrice_5");

                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, setMarketPrice, feeComponent);
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                finalTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(triggerTrailingPrice, limitOffset);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Creating Buy and Sell Order to set the last price
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);

                // Scenario 1: Place Buy Trailing Stop Limit order with buy order size = sell order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));
                
                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice5, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, equalSellOrderSize, triggerTrailingPrice, timeInForce);
                
                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
               
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, double.Parse(buyOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, buyOrderSize, finalTrailingPrice));


                // Scenario 2: Place Buy Trailing Stop Limit order with buy order size < sell order size

                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder2 = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice5, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, incSellOrderSize, triggerTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, double.Parse(buyOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder2["PlaceOrderTime"], placeTrailingStopLimitBuyOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, buyOrderSize, finalTrailingPrice));

                // Scenario 3: Place Buy Trailing Stop Limit order with buy order size > sell order size

                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER7);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder3 = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice5, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, decSellOrderSize, triggerTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                var filledOrderSize = GenericUtils.GetDifferenceFromStringAfterSubstraction(decBuyOrderSize, sellOrderSize);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(filledOrderSize, finalTrailingPrice, feeComponent);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, double.Parse(decSellOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, decSellOrderSize, finalTrailingPrice));
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(decSellOrderSize), triggerTrailingPrice, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitOpenOrder, buyTab, Const.TrailingStopLimit, decSellOrderSize, triggerTrailingPrice));
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact] // Check messages
        public void TC20_VerifyAdvTrailingStopLimitSellOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string initialTrailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TrailingStopLimit");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                setMarketPrice = TestData.GetData("TC20_SetMarketPrice");
                buyOrderSize = TestData.GetData("TC20_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC20_SellOrderSize");
                incBuyOrderSize = TestData.GetData("TC20_IncBuyOrderSize");
                decBuyOrderSize = TestData.GetData("TC20_DecBuyOrderSize");
                equalBuyOrderSize = TestData.GetData("TC20_EqualBuyOrderSize");
                timeInForce = TestData.GetData("TC20_TimeInForce");
                trailingAmount = TestData.GetData("TC20_TrailingAmount");
                triggerTrailingPrice = TestData.GetData("TC20_TrailingPrice");
                limitOffset = TestData.GetData("TC20_LimitOffset");
                pegPrice = TestData.GetData("TC20_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                buyLimitPrice1 = TestData.GetData("TC20_BuyLimitPrice_1");
                buyLimitPrice2 = TestData.GetData("TC20_BuyLimitPrice_2");
                buyLimitPrice3 = TestData.GetData("TC20_BuyLimitPrice_3");
                buyLimitPrice4 = TestData.GetData("TC20_BuyLimitPrice_4");
                buyLimitPrice5 = TestData.GetData("TC20_BuyLimitPrice_5");
                sellLimitPrice1 = TestData.GetData("TC20_SellLimitPrice_1");
                sellLimitPrice2 = TestData.GetData("TC20_SellLimitPrice_2");
                sellLimitPrice3 = TestData.GetData("TC20_SellLimitPrice_3");
                sellLimitPrice4 = TestData.GetData("TC20_SellLimitPrice_4");
                sellLimitPrice5 = TestData.GetData("TC20_SellLimitPrice_5");

                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, setMarketPrice, feeComponent);
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                finalTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(triggerTrailingPrice, limitOffset);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Creating Buy and Sell Order to get the last price
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
               
                // Scenario 1: Place Sell Trailing Stop Limit order with sell order size = buy order size
                // Setting up market with 5 limit (buy and sell) orders
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, sellTab));
                
                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, equalBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);   

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, double.Parse(sellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, sellOrderSize, finalTrailingPrice));

                // Scenario 2: Place Sell Trailing Stop Limit order with sell order size < buy order size

                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder2 = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, sellTab));

                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, incBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, double.Parse(sellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, sellOrderSize, finalTrailingPrice));

                // Scenario 3: Place Sell Trailing Stop Limit order with sell order size > buy order size

                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER11);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder3 = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, sellTab));

                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, decBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);                
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, double.Parse(decSellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, decSellOrderSize, finalTrailingPrice));
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(decSellOrderSize), triggerTrailingPrice, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitOpenOrder, sellTab, Const.TrailingStopLimit, decSellOrderSize, triggerTrailingPrice));
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact] // Check messages
        public void TC21_VerifyAdvTrailingStopLimitBuyOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string initialTrailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TrailingStopLimit");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                setMarketPrice = TestData.GetData("TC21_SetMarketPrice");
                buyOrderSize = TestData.GetData("TC21_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC21_SellOrderSize");
                incSellOrderSize = TestData.GetData("TC21_IncSellOrderSize");
                decSellOrderSize = TestData.GetData("TC21_DecSellOrderSize");
                equalSellOrderSize = TestData.GetData("TC21_EqualSellOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                iocTimeInForce = TestData.GetData("TC21_TimeInForce");
                trailingAmount = TestData.GetData("TC21_TrailingAmount");
                triggerTrailingPrice = TestData.GetData("TC21_TrailingPrice");
                limitOffset = TestData.GetData("TC21_LimitOffset");
                pegPrice = TestData.GetData("TC21_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                buyLimitPrice1 = TestData.GetData("TC21_BuyLimitPrice_1");
                buyLimitPrice2 = TestData.GetData("TC21_BuyLimitPrice_2");
                buyLimitPrice3 = TestData.GetData("TC21_BuyLimitPrice_3");
                buyLimitPrice4 = TestData.GetData("TC21_BuyLimitPrice_4");
                buyLimitPrice5 = TestData.GetData("TC21_BuyLimitPrice_5");
                sellLimitPrice1 = TestData.GetData("TC21_SellLimitPrice_1");
                sellLimitPrice2 = TestData.GetData("TC21_SellLimitPrice_2");
                sellLimitPrice3 = TestData.GetData("TC21_SellLimitPrice_3");
                sellLimitPrice4 = TestData.GetData("TC21_SellLimitPrice_4");
                sellLimitPrice5 = TestData.GetData("TC21_SellLimitPrice_5");

                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, setMarketPrice, feeComponent);
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                finalTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(triggerTrailingPrice, limitOffset);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Creating Buy and Sell Order to get the last price
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);

                // Scenario 1: Place Buy Trailing Stop Limit order with buy order size = sell order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce));
                // Verify the order is present in Open orders tab
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));
                
                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, equalSellOrderSize, finalTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                                
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, double.Parse(buyOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, buyOrderSize, finalTrailingPrice));

                // Scenario 2: Place Buy Trailing Stop Limit order with buy order size < sell order size

                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder2 = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, incSellOrderSize, finalTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, Double.Parse(buyOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder2["PlaceOrderTime"], placeTrailingStopLimitBuyOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, buyOrderSize, finalTrailingPrice));

                // Scenario 3: Place Buy Trailing Stop Limit order with buy order size > sell order size

                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER7);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder3 = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, decSellOrderSize, finalTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, Double.Parse(decSellOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, decSellOrderSize, finalTrailingPrice));
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), triggerTrailingPrice, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitInactiveOrder, buyTab, Const.TrailingStopLimit, buyOrderSize, triggerTrailingPrice));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact] // Check messages
        public void TC22_VerifyAdvTrailingStopLimitSellOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string initialTrailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TrailingStopLimit");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                setMarketPrice = TestData.GetData("TC22_SetMarketPrice");
                buyOrderSize = TestData.GetData("TC22_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC22_SellOrderSize");
                incBuyOrderSize = TestData.GetData("TC22_IncBuyOrderSize");
                decBuyOrderSize = TestData.GetData("TC22_DecBuyOrderSize");
                equalBuyOrderSize = TestData.GetData("TC22_EqualBuyOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                iocTimeInForce = TestData.GetData("TC21_TimeInForce");
                trailingAmount = TestData.GetData("TC22_TrailingAmount");
                triggerTrailingPrice = TestData.GetData("TC22_TrailingPrice");
                limitOffset = TestData.GetData("TC22_LimitOffset");
                pegPrice = TestData.GetData("TC22_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                buyLimitPrice1 = TestData.GetData("TC22_BuyLimitPrice_1");
                buyLimitPrice2 = TestData.GetData("TC22_BuyLimitPrice_2");
                buyLimitPrice3 = TestData.GetData("TC22_BuyLimitPrice_3");
                buyLimitPrice4 = TestData.GetData("TC22_BuyLimitPrice_4");
                buyLimitPrice5 = TestData.GetData("TC22_BuyLimitPrice_5");
                sellLimitPrice1 = TestData.GetData("TC22_SellLimitPrice_1");
                sellLimitPrice2 = TestData.GetData("TC22_SellLimitPrice_2");
                sellLimitPrice3 = TestData.GetData("TC22_SellLimitPrice_3");
                sellLimitPrice4 = TestData.GetData("TC22_SellLimitPrice_4");
                sellLimitPrice5 = TestData.GetData("TC22_SellLimitPrice_5");

                initialTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(setMarketPrice, trailingAmount);
                finalTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(triggerTrailingPrice, limitOffset);
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, setMarketPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Creating Buy and Sell Order to get the last price
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                
                // Scenario 1: Place Sell Trailing Stop Limit order with sell order size = buy order size
                // Setting up market with 5 limit (buy and sell) orders
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab                
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, sellTab));

                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);
                
                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, equalBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, Double.Parse(sellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, sellOrderSize, finalTrailingPrice));

                // Scenario 2: Place Sell Trailing Stop Limit order with sell order size < buy order size

                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder2 = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder2["PlaceOrderTime"], placeTrailingStopLimitSellOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, sellTab));
                
                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);                

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, incBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, Double.Parse(sellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder2["PlaceOrderTime"], placeTrailingStopLimitSellOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, sellOrderSize, finalTrailingPrice));

                // Scenario 3: Place Sell Trailing Stop Limit order with sell order size > buy order size

                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER11);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder3 = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, sellTab));
                
                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, decBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);

                finalTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(triggerTrailingPrice, limitOffset);
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                var filledOrderSize = GenericUtils.GetDifferenceFromStringAfterSubstraction(decBuyOrderSize, sellOrderSize);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(filledOrderSize, finalTrailingPrice, feeComponent);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, double.Parse(filledOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, decSellOrderSize, finalTrailingPrice));
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), triggerTrailingPrice, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitInactiveOrder, sellTab, Const.TrailingStopLimit, sellOrderSize, triggerTrailingPrice));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact] // Check messages
        public void TC23_VerifyAdvTrailingStopLimitBuyOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string initialTrailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TrailingStopLimit");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                setMarketPrice = TestData.GetData("TC23_SetMarketPrice");
                buyOrderSize = TestData.GetData("TC23_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC23_SellOrderSize");
                incSellOrderSize = TestData.GetData("TC23_IncSellOrderSize");
                decSellOrderSize = TestData.GetData("TC23_DecSellOrderSize");
                equalSellOrderSize = TestData.GetData("TC23_EqualSellOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                fokTimeInForce = TestData.GetData("TC23_TimeInForce");
                trailingAmount = TestData.GetData("TC23_TrailingAmount");
                triggerTrailingPrice = TestData.GetData("TC23_TrailingPrice");
                limitOffset = TestData.GetData("TC23_LimitOffset");
                pegPrice = TestData.GetData("TC23_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                buyLimitPrice1 = TestData.GetData("TC23_BuyLimitPrice_1");
                buyLimitPrice2 = TestData.GetData("TC23_BuyLimitPrice_2");
                buyLimitPrice3 = TestData.GetData("TC23_BuyLimitPrice_3");
                buyLimitPrice4 = TestData.GetData("TC23_BuyLimitPrice_4");
                buyLimitPrice5 = TestData.GetData("TC23_BuyLimitPrice_5");
                sellLimitPrice1 = TestData.GetData("TC23_SellLimitPrice_1");
                sellLimitPrice2 = TestData.GetData("TC23_SellLimitPrice_2");
                sellLimitPrice3 = TestData.GetData("TC23_SellLimitPrice_3");
                sellLimitPrice4 = TestData.GetData("TC23_SellLimitPrice_4");
                sellLimitPrice5 = TestData.GetData("TC23_SellLimitPrice_5");

                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, setMarketPrice, feeComponent);
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                finalTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(triggerTrailingPrice, limitOffset);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Creating Buy and Sell Order to get the last price
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);

                // Scenario 1: Place Buy Trailing Stop Limit order with buy order size = sell order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, fokTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab                
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, equalSellOrderSize, finalTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, Double.Parse(buyOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, buyOrderSize, finalTrailingPrice));

                // Scenario 2: Place Buy Trailing Stop Limit order with buy order size < sell order size

                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder2 = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, fokTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder2["PlaceOrderTime"], placeTrailingStopLimitBuyOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, incSellOrderSize, finalTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, Double.Parse(buyOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder2["PlaceOrderTime"], placeTrailingStopLimitBuyOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, buyOrderSize, finalTrailingPrice));

                // Scenario 3: Place Buy Trailing Stop Limit order with buy order size > sell order size


                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER7);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder3 = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, fokTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, buyTab));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, decSellOrderSize, finalTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                var filledOrderSize = GenericUtils.GetDifferenceFromStringAfterSubstraction(decSellOrderSize, buyOrderSize);
                buyOrderFeeValue = GenericUtils.FeeAmount(filledOrderSize, feeComponent);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, Double.Parse(decSellOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, decSellOrderSize, finalTrailingPrice));
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), triggerTrailingPrice, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitInactiveOrder, buyTab, Const.TrailingStopLimit, buyOrderSize, triggerTrailingPrice));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact] // Check messages
        public void TC24_VerifyAdvTrailingStopLimitSellOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string initialTrailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TrailingStopLimit");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                setMarketPrice = TestData.GetData("TC24_SetMarketPrice");
                buyOrderSize = TestData.GetData("TC24_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC24_SellOrderSize");
                incBuyOrderSize = TestData.GetData("TC24_IncBuyOrderSize");
                decBuyOrderSize = TestData.GetData("TC24_DecBuyOrderSize");
                equalBuyOrderSize = TestData.GetData("TC24_EqualBuyOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                fokTimeInForce = TestData.GetData("TC21_TimeInForce");
                trailingAmount = TestData.GetData("TC24_TrailingAmount");
                triggerTrailingPrice = TestData.GetData("TC24_TrailingPrice");
                limitOffset = TestData.GetData("TC24_LimitOffset");
                pegPrice = TestData.GetData("TC24_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                buyLimitPrice1 = TestData.GetData("TC24_BuyLimitPrice_1");
                buyLimitPrice2 = TestData.GetData("TC24_BuyLimitPrice_2");
                buyLimitPrice3 = TestData.GetData("TC24_BuyLimitPrice_3");
                buyLimitPrice4 = TestData.GetData("TC24_BuyLimitPrice_4");
                buyLimitPrice5 = TestData.GetData("TC24_BuyLimitPrice_5");
                sellLimitPrice1 = TestData.GetData("TC24_SellLimitPrice_1");
                sellLimitPrice2 = TestData.GetData("TC24_SellLimitPrice_2");
                sellLimitPrice3 = TestData.GetData("TC24_SellLimitPrice_3");
                sellLimitPrice4 = TestData.GetData("TC24_SellLimitPrice_4");
                sellLimitPrice5 = TestData.GetData("TC24_SellLimitPrice_5");

                initialTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(setMarketPrice, trailingAmount);
                finalTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(triggerTrailingPrice, limitOffset);
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, setMarketPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Creating Buy and Sell Order to get the last price
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);

                // Scenario 1: Place Sell Trailing Stop Limit order with sell order size = buy order size
                // Setting up market with 5 limit (buy and sell) orders
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, fokTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab                
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, sellTab));

                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, equalBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, Double.Parse(sellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, sellOrderSize, finalTrailingPrice));

                // Scenario 2: Place Sell Trailing Stop Limit order with sell order size < buy order size

                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder2 = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, fokTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder2["PlaceOrderTime"], placeTrailingStopLimitSellOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, sellTab));

                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);
                
                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, incBuyOrderSize, finalTrailingPrice, timeInForce);
                
                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, Double.Parse(sellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder2["PlaceOrderTime"], placeTrailingStopLimitSellOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, sellOrderSize, finalTrailingPrice));

                // Scenario 3: Place Sell Trailing Stop Limit order with sell order size > buy order size
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER11);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder3 = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, fokTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.OpenOrdersVerifiedSuccessfully, sellTab));
                
                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, decBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);

                finalTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(triggerTrailingPrice, limitOffset);
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                var filledOrderSize = GenericUtils.GetDifferenceFromStringAfterSubstraction(decBuyOrderSize, sellOrderSize);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(filledOrderSize, finalTrailingPrice, feeComponent);
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), triggerTrailingPrice, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitInactiveOrder, sellTab, Const.TrailingStopLimit, sellOrderSize, triggerTrailingPrice));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]
        public void TC25_VerifyIOCAdvanceBuyOrder()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("OrderSize");
                limitPrice = TestData.GetData("LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                string feeValue = GenericUtils.FeeAmount(orderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER10);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER3);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);

                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));

                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCBuyOrderTime = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(orderSize, limitPrice);

                string successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, orderSize, limitPrice));
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(orderSize), feeValue, placeIOCBuyOrderTime["PlaceOrderTime"], placeIOCBuyOrderTime["PlaceOrderTimePlusOneMin"]);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, buyTab, orderSize, placeIOCBuyOrderTime));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(Const.IOCOrderTypeFailedMsg);
                TestProgressLogger.Error(Const.IOCOrderTypeFailedMsg, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]
        public void TC26_VerifyIOCAdvanceBuyOrderLimitAskPrice()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("OrderSize");
                buyOrderLimitPrice = TestData.GetData("LimitPrice");
                sellOrderLimitPrice = TestData.GetData("TC26_SellOrderLimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                string feeValue = GenericUtils.FeeAmount(orderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER10);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                string askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, sellOrderLimitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, sellOrderLimitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, sellOrderLimitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER3);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);

                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));

                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCBuyOrder = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(orderSize, buyOrderLimitPrice);

                string cancelledMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderCancelledMsg, cancelledMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, orderSize, buyOrderLimitPrice));
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.Limit, Double.Parse(orderSize), buyOrderLimitPrice, placeIOCBuyOrder["PlaceOrderTime"], placeIOCBuyOrder["PlaceOrderTimePlusOneMin"], Const.CancelledStatus);

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, buyTab));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }

        }

        [Fact]
        public void TC27_VerifyPartiallyIOCAdvanceBuyOrderLimitAskPrice()
        {
            try
            {
                string feeValue;
                string askPrice;
                string cancelledMsg;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderLimitPrice = TestData.GetData("TC27_BuyOrderLimitPrice");
                sellOrderLimitPrice = TestData.GetData("TC27_SellOrderLimitPrice");
                buyOrderSize = TestData.GetData("TC27_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC27_SellOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                feeValue = GenericUtils.FeeAmount(sellOrderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER10);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, sellOrderLimitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, sellOrderLimitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, sellOrderLimitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER5);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);

                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));

                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCBuyOrder = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(buyOrderSize, buyOrderLimitPrice);

                cancelledMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderCancelledMsg, cancelledMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, buyOrderSize, buyOrderLimitPrice));
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(sellOrderSize), feeValue, placeIOCBuyOrder["PlaceOrderTime"], placeIOCBuyOrder["PlaceOrderTimePlusOneMin"]);
                objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.Limit, Double.Parse(buyOrderSize), buyOrderLimitPrice, placeIOCBuyOrder["PlaceOrderTime"], placeIOCBuyOrder["PlaceOrderTimePlusOneMin"], Const.CancelledStatus);

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, buyTab));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();

            }

        }

        [Fact]
        public void TC28_VerifyIOCAdvanceSellOrder()
        {
            try
            {
                string feeValue;
                string askPrice;
                string successMsg;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("OrderSize");
                limitPrice = TestData.GetData("LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");
                feeValue = GenericUtils.SellFeeAmount(orderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER10);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, limitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER3);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);

                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));

                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCSellOrderTime = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(orderSize, limitPrice);

                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, orderSize, limitPrice));
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(orderSize), feeValue, placeIOCSellOrderTime["PlaceOrderTime"], placeIOCSellOrderTime["PlaceOrderTimePlusOneMin"]);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();

            }

        }

        [Fact]
        public void TC29_VerifyIOCAdvanceSellOrderMoreThanBidPrice()
        {
            try
            {
                string feeValue;
                string askPrice;
                string cancelledMsg;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC29_OrderSize");
                buyOrderLimitPrice = TestData.GetData("TC29_BuyOrderLimitPrice");
                sellOrderLimitPrice = TestData.GetData("TC29_SellOrderLimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                feeValue = GenericUtils.SellFeeAmount(orderSize, sellOrderLimitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER10);

                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, buyOrderLimitPrice, timeInForce);

                UserCommonFunctions.ConfirmWindowOrder(askPrice, buyOrderLimitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, buyOrderLimitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER3);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCSellOrder = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(orderSize, sellOrderLimitPrice);
                cancelledMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderCancelledMsg, cancelledMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, orderSize, sellOrderLimitPrice));
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.Limit, Double.Parse(orderSize), sellOrderLimitPrice, placeIOCSellOrder["PlaceOrderTime"], placeIOCSellOrder["PlaceOrderTimePlusOneMin"], Const.CancelledStatus);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab, orderSize));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }

        }

        [Fact]
        public void TC30_VerifyPartiallyIOCAdvanceSellOrderMoreThanBidPrice()
        {
            try
            {
                string feeValue;
                string askPrice;
                string cancelledMsg;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderLimitPrice = TestData.GetData("TC30_BuyOrderLimitPrice");
                sellOrderLimitPrice = TestData.GetData("TC30_SellOrderLimitPrice");
                buyOrderSize = TestData.GetData("TC30_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC30_SellOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");
                feeValue = GenericUtils.SellFeeAmount(buyOrderSize, buyOrderLimitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyOrderLimitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, buyOrderLimitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, buyOrderLimitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER5);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCSellOrder = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(sellOrderSize, sellOrderLimitPrice);
                cancelledMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderCancelledMsg, cancelledMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, sellOrderSize, sellOrderLimitPrice));
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(buyOrderSize), feeValue, placeIOCSellOrder["PlaceOrderTime"], placeIOCSellOrder["PlaceOrderTimePlusOneMin"]);
                objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.Limit, Double.Parse(sellOrderSize), sellOrderLimitPrice, placeIOCSellOrder["PlaceOrderTime"], placeIOCSellOrder["PlaceOrderTimePlusOneMin"], Const.CancelledStatus);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }

        }

        [Fact]
        public void TC31_VerifyPlaceBuyOrderWithReservOrderType()
        {
            try
            {
                string successMsg;
                string type = Const.Limit;
                instrument = TestData.GetData("Instrument");
                reserveOrder = TestData.GetData("ReserveOrder");
                buyTab = TestData.GetData("BuyTab");
                buyOrderLimitPrice = TestData.GetData("TC31_BuyOrderLimitPrice");
                buyOrderSize = TestData.GetData("TC31_BuyOrderSize");
                buyOrderDisplayQty = TestData.GetData("TC31_BuyOrderDisplayQty");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER10);

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);

                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));

                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(TestProgressLogger);
                advanceorder.SelectBuyOrSellTab(buyTab);
                advanceorder.SelectInstrumentsAndOrderType(instrument, reserveOrder);
                var placeReserveBuyOrder = advanceorder.PlaceBuyOrderWithReserveOrderType(buyOrderSize, buyOrderLimitPrice, buyOrderDisplayQty);

                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceReserveOrderSuccessMsg, buyTab, buyOrderSize, buyOrderLimitPrice, buyOrderDisplayQty));
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(buyOrderSize), buyOrderLimitPrice, placeReserveBuyOrder["PlaceOrderTime"], placeReserveBuyOrder["PlaceOrderTimePlusOneMin"]));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceReserveOrderFailureMsg, buyTab));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceReserveOrderFailureMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]
        public void TC32_VerifyPlaceSellOrderWithReserveOrderType()
        {
            try
            {
                string successMsg;
                string type = Const.Limit;
                instrument = TestData.GetData("Instrument");
                reserveOrder = TestData.GetData("ReserveOrder");
                sellTab = TestData.GetData("SellTab");
                sellOrderLimitPrice = TestData.GetData("TC32_SellOrderLimitPrice");
                sellOrderSize = TestData.GetData("TC32_SellOrderSize");
                sellOrderDisplayQty = TestData.GetData("TC32_SellOrderDisplayQty");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER10);

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);

                UserCommonFunctions.CancelAllOrders(driver);

                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));



                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(TestProgressLogger);
                advanceorder.SelectBuyOrSellTab(sellTab);
                advanceorder.SelectInstrumentsAndOrderType(instrument, reserveOrder);
                var placeReserveSellOrder = advanceorder.PlaceSellOrderWithReserveOrderType(sellOrderSize, sellOrderLimitPrice, sellOrderDisplayQty);

                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceReserveOrderSuccessMsg, sellTab, sellOrderSize, sellOrderLimitPrice, sellOrderDisplayQty));
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(sellOrderSize), sellOrderLimitPrice, placeReserveSellOrder["PlaceOrderTime"], placeReserveSellOrder["PlaceOrderTimePlusOneMin"]));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceReserveOrderFailureMsg, sellTab));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceReserveOrderFailureMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]
        public void TC13_VerifyStopMarketBuyOrder()
        {
            try
            {
                string placeBuyOrderTime;
                string placeBuyOrderTimePlusOneMin;
                string placeSellOrderTime;
                string placeSellOrderTimePlusOneMin;
                string successMsg;
                string feeValue;
                string askPrice;
                string sellOrderPrice;
                string type = Const.StopMarket;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC13_SellOrderSize");
                limitPrice = TestData.GetData("TC13_LimitPrice");
                timeInForce = TestData.GetData("TC13_TimeInForce");
                stopPrice = TestData.GetData("TC13_StopPrice");
                feeComponent = TestData.GetData("FeeComponent");
                orderTypeDropdown = TestData.GetData("StopMarketOrder");
                limitPriceEqualsStop = TestData.GetData("TC13_LimitPriceEqualsStop");
                feeValue = GenericUtils.FeeAmount(orderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);

                // Creating Buy and Sell Order to match to set price
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                // Setting up market with User 8 - Sell Order
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));

                // Creating Advance Stop Market Order with User 9 - Buy Order
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderTypeDropdown);
                var placeStopMarketBuyOrder = advanceOrder.PlaceStopMarketBuyOrder(orderSize, stopPrice);
                placeBuyOrderTime = placeStopMarketBuyOrder["PlaceOrderTime"];
                placeBuyOrderTimePlusOneMin = placeStopMarketBuyOrder["PlaceOrderTimePlusOneMin"];
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Verify the order in Open orders tab
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(orderSize), stopPrice, placeStopMarketBuyOrder["PlaceOrderTime"], placeStopMarketBuyOrder["PlaceOrderTimePlusOneMin"]));

                // Creating Buy and Sell Order to match Stop Price
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPriceEqualsStop, timeInForce, Const.USER10, Const.USER11);

                // Create a seller to execute the Stop order
                userFunctions.LogIn(TestProgressLogger, Const.USER1);
                sellOrderPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPriceEqualsStop, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(sellOrderPrice, limitPrice, driver);
                placeSellOrderTime = GenericUtils.GetCurrentTime();
                placeSellOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPriceEqualsStop));

                // Verify that the Stop order is executed
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(orderSize), feeValue, placeSellOrderTime, placeSellOrderTimePlusOneMin));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceStopMarketOrderSuccessMsg, buyTab, orderSize));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceStopMarketOrderFailureMsg, buyTab, orderSize));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceStopMarketOrderFailureMsg, buyTab, orderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]
        public void TC14_VerifyStopMarketSellOrder()
        {
            try
            {
                string type;
                string feeValue;
                string placeBuyOrderTime;
                string placeBuyOrderTimePlusOneMin;
                string placeSellOrderTime;
                string placeSellOrderTimePlusOneMin;
                string successMsg;
                string askPrice;
                string buyOrderPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC14_SellOrderSize");
                limitPrice = TestData.GetData("TC14_LimitPrice");
                timeInForce = TestData.GetData("TC14_TimeInForce");
                stopPrice = TestData.GetData("TC14_StopPrice");
                feeComponent = TestData.GetData("FeeComponent");
                orderTypeDropdown = TestData.GetData("StopMarketOrder");
                limitPriceEqualsStop = TestData.GetData("TC14_LimitPriceEqualsStop");

                type = Const.StopMarket;
                feeValue = GenericUtils.SellFeeAmount(orderSize, stopPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);

                // Creating Buy and Sell Order to match to set price
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                // Setting up market with User 8 - Buy Order
                userFunctions.LogIn(TestProgressLogger, Const.USER8);

                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, limitPrice));

                // Creating Advance Stop Market Order with User 9 - Sell Order
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderTypeDropdown);
                var placeStopMarketSellOrder = advanceOrder.PlaceStopMarketSellOrder(orderSize, stopPrice);
                placeSellOrderTime = placeStopMarketSellOrder["PlaceOrderTime"];
                placeSellOrderTimePlusOneMin = placeStopMarketSellOrder["PlaceOrderTimePlusOneMin"];
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Verify the order in Open orders tab
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(orderSize), stopPrice, placeStopMarketSellOrder["PlaceOrderTime"], placeStopMarketSellOrder["PlaceOrderTimePlusOneMin"]));

                // Creating Buy and Sell Order to match Stop Price
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPriceEqualsStop, timeInForce, Const.USER10, Const.USER11);

                // Create a buyer to execute the Stop order
                userFunctions.LogIn(TestProgressLogger, Const.USER1);
                buyOrderPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPriceEqualsStop, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(buyOrderPrice, limitPrice, driver);
                placeBuyOrderTime = GenericUtils.GetCurrentTime();
                placeBuyOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPriceEqualsStop));

                // Verify that the Stop order is executed

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(orderSize), feeValue, placeBuyOrderTime, placeBuyOrderTimePlusOneMin));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceStopMarketOrderSuccessMsg, sellTab, orderSize));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceStopMarketOrderFailureMsg, sellTab, orderSize));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceStopMarketOrderFailureMsg, sellTab, orderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]
        public void TC17_VerifyTrailingStopMarketBuyOrder()
        {
            try
            {
                string type;
                string feeValue;
                string successMsg;
                string askPrice;
                string trailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC17_BuyOrderSize");
                limitPrice = TestData.GetData("TC17_LimitPrice");
                timeInForce = TestData.GetData("TC17_TimeInForce");
                trailingAmount = TestData.GetData("TC17_TrailingAmount");
                pegPriceDropdown = TestData.GetData("TC17_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                orderTypeDropdown = TestData.GetData("TrailingStopMarket");

                type = Const.TrailingStopMarket;
                feeValue = GenericUtils.FeeAmount(orderSize, feeComponent);

                // Creating Buy and Sell Order to get the last price
                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                // Setting up market with User 8 - Sell Order
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));

                // Place Trailing Stop Market BuyOrder
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderTypeDropdown);
                var placeTrailingStopMarketBuyOrder = advanceOrder.PlaceTrailingStopMarketBuyOrder(driver, orderSize, trailingAmount, pegPriceDropdown);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Verify the order in Open orders tab
                trailingPrice = UserCommonFunctions.GetBuyOrderTrailingPrice(limitPrice, trailingAmount);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(orderSize), trailingPrice, placeTrailingStopMarketBuyOrder["PlaceOrderTime"], placeTrailingStopMarketBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceTrailingStopStopMarketOrderSuccessMsg, buyTab, orderSize));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceTrailingStopStopMarketOrderFailureMsg, buyTab, orderSize));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceTrailingStopStopMarketOrderFailureMsg, buyTab, orderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();
            }
        }

        [Fact]
        public void TC18_VerifyTrailingStopMarketSellOrder()
        {
            try
            {
                string type;
                string successMsg;
                string askPrice;
                string trailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC18_BuyOrderSize");
                limitPrice = TestData.GetData("TC18_LimitPrice");
                timeInForce = TestData.GetData("TC18_TimeInForce");
                trailingAmount = TestData.GetData("TC18_TrailingAmount");
                pegPriceDropdown = TestData.GetData("TC18_PegPrice");
                orderTypeDropdown = TestData.GetData("TrailingStopMarket");

                type = Const.TrailingStopMarket;

                // Creating Buy and Sell Order to get the last price
                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);

                // Setting up market with User 8 - Buy Order
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPrice, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(askPrice, limitPrice, driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, limitPrice));

                // Place Trailing Stop Market Sell Order
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                UserCommonFunctions.CancelAllOrders(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderTypeDropdown);
                var placeTrailingStopMarketSellOrder = advanceOrder.PlaceTrailingStopMarketSellOrder(orderSize, trailingAmount, pegPriceDropdown);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Verify the order in Open orders tab
                trailingPrice = UserCommonFunctions.GetSellOrderTrailingPrice(limitPrice, trailingAmount);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(orderSize), trailingPrice, placeTrailingStopMarketSellOrder["PlaceOrderTime"], placeTrailingStopMarketSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceTrailingStopStopMarketOrderSuccessMsg, sellTab, orderSize));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceTrailingStopStopMarketOrderFailureMsg, sellTab, orderSize));
                TestProgressLogger.Error(String.Format(LogMessage.AdvanceTrailingStopStopMarketOrderFailureMsg, sellTab, orderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
                UserFunctions userFunctionality = new UserFunctions(TestProgressLogger);
                userFunctionality.LogOut();

            }
        }
    }
}
