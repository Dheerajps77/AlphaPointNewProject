﻿using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using System;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class LoyaltyTokenTest : TestBase
    {
        private string instrument;
        private string accountID;
        private string buyMarketOrderAmount;
        private string sellMarketOrderAmount;
        private string buyTab;
        private string sellTab;

        public LoyaltyTokenTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void TC49_VerifyLoyalityFeeBuyMarketOrder()
        {
            instrument = TestData.GetData("Instrument");
            buyTab = TestData.GetData("BuyTab");          
            accountID = TestData.GetData("TC49_AccountID");
            buyMarketOrderAmount = TestData.GetData("TC49_BuyMarketOrderAmount");
           
            UserFunctions userFuntions = new UserFunctions(TestProgressLogger);
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            UserCommonFunctions userCommonFunctions = new UserCommonFunctions(TestProgressLogger);
            LoyaltyTokenPage loyaltyTokenPage = new LoyaltyTokenPage(TestProgressLogger);
            OrderEntryPage orderEnteryPage = new OrderEntryPage(driver, TestProgressLogger);
            try
            {
                
                TestProgressLogger.StartTest();
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminLoyaltyFeeCheckStart, accountID));
                objAdminCommonFunctions.SelectAccountsMenu();
                objAdminCommonFunctions.OpenAccountByIDText(accountID);
                objAdminCommonFunctions.OpenAccountBtn();
                objAdminCommonFunctions.EditInformationButton();
                objAdminCommonFunctions.LoyaltyFeeCheckedOrUnchecked();
                objAdminCommonFunctions.SaveButton();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminLoyaltyFeeCheckCompleted, accountID));
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();
                
                userFuntions.LogIn(TestProgressLogger, Const.USER13);
                UserCommonFunctions.DashBoardMenuButton(driver); 
                UserCommonFunctions.NavigateToUserSetting(driver);
                loyaltyTokenPage.LoyaltyTokenButton(driver);
                loyaltyTokenPage.TradingFeeRadioButton(driver);
                loyaltyTokenPage.UserSettingMenuButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.SelectLoyaltyTokenSuccess);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifyAppliedFeeIsLTC, buyTab));
                orderEnteryPage.MarketOrderTypeBtn();
                orderEnteryPage.EnterBuyAmount(buyMarketOrderAmount);
                Assert.True(loyaltyTokenPage.GetFeeText());
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.LoyaltyTokenSuccessMsg, buyTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.LoyaltyTokenFailureMsg, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.LoyaltyTokenFailureMsg, buyTab), e);
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
        public void TC50_VerifyLoyalityFeeSellMarketOrder()
        {
            instrument = TestData.GetData("Instrument");
            sellTab = TestData.GetData("SellTab");
            accountID = TestData.GetData("TC50_AccountID");           
            sellMarketOrderAmount = TestData.GetData("TC50_SellMarketOrderAmount");


            UserFunctions userFuntions = new UserFunctions(TestProgressLogger);
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            UserCommonFunctions userCommonFunctions = new UserCommonFunctions(TestProgressLogger);
            LoyaltyTokenPage loyaltyTokenPage = new LoyaltyTokenPage(TestProgressLogger);
            OrderEntryPage orderEnteryPage = new OrderEntryPage(driver, TestProgressLogger);
            try
            {

                TestProgressLogger.StartTest();
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminLoyaltyFeeCheckStart, accountID));
                objAdminCommonFunctions.SelectAccountsMenu();
                objAdminCommonFunctions.OpenAccountByIDText(accountID);
                objAdminCommonFunctions.OpenAccountBtn();
                objAdminCommonFunctions.EditInformationButton();
                objAdminCommonFunctions.LoyaltyFeeCheckedOrUnchecked();
                objAdminCommonFunctions.SaveButton();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminLoyaltyFeeCheckCompleted, accountID));
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();

                userFuntions.LogIn(TestProgressLogger, Const.USER13);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToUserSetting(driver);
                loyaltyTokenPage.LoyaltyTokenButton(driver);
                loyaltyTokenPage.TradingFeeRadioButton(driver);
                loyaltyTokenPage.UserSettingMenuButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.SelectLoyaltyTokenSuccess);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifyAppliedFeeIsLTC, sellTab));
                orderEnteryPage.SellOrderEntryBtn();
                orderEnteryPage.MarketOrderTypeBtn();
                orderEnteryPage.EnterSellAmount(sellMarketOrderAmount);
                Assert.True(loyaltyTokenPage.GetFeeText());
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.LoyaltyTokenSuccessMsg, sellTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.LoyaltyTokenFailureMsg, sellTab), ex);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.LoyaltyTokenFailureMsg, sellTab), e);
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