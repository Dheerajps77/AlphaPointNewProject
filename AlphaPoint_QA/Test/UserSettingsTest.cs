﻿using System;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class UserSettingsTest : TestBase
    {

        public UserSettingsTest(ITestOutputHelper output) : base(output)
        {

        }
      
        [Fact]
        public void TC44_VerifyCreateAPIKey()
        {
            try
            {
                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER1);
                UserSettingPage userSettingsPage = new UserSettingPage(driver, TestProgressLogger);
                Assert.True((userSettingsPage.SelectAPIKey()), LogMessage.CreateAPIKeyBtnIsPresent);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CreateAPIKeyBtnIsPresent));
                Assert.True((userSettingsPage.VerifyAPIKeyCheckboxesArePresent()), LogMessage.APIKeyCheckboxesArePresent);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.APIKeyCheckboxesArePresent));
                Assert.True((userSettingsPage.CreateAndVerifyAPIKey()), LogMessage.APIKeyCreatedSuccessMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.APIKeyCreatedSuccessMsg));
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(LogMessage.CreateAPIKeyFailed, e);
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
        public void TC45_VerifyAffiliateProgram()
        {
            string userByID = TestData.GetData("TC45_UserByID");
            string affiliateTagID = TestData.GetData("TC45_AffiliateTagID");
            string verificationLevel = TestData.GetData("TC45_VerificationLevel");
            TestProgressLogger.StartTest();
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            UserSettingPage userSettingsPage = new UserSettingPage(driver, TestProgressLogger);
            try
            {
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.ClickOnUsersMenuLink();
                objAdminCommonFunctions.UserByIDText(userByID);
                objAdminCommonFunctions.OpenUserButton();
                objAdminCommonFunctions.AffiliateTagCreation(affiliateTagID);
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();            
                userFunctions.LogIn(TestProgressLogger, Const.USER12);
                Assert.True(userSettingsPage.VerifyAffiliateProgramFunctionality(driver, verificationLevel));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AffiliateProgramSuccessMsg));
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(LogMessage.AffiliateProgramFailureMsg, e);
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
        public void TC46_VerifyDeleteAPIKey()
        {
            UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
            UserSettingPage userSettingsPage = new UserSettingPage(driver, TestProgressLogger);
            try
            {
                TestProgressLogger.StartTest();                
                userFunctions.LogIn(TestProgressLogger, Const.USER2);                
                Assert.True(userSettingsPage.DeleteAPIKey(driver), LogMessage.DeleteAPIKeySuccessMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.DeleteAPIKeySuccessMsg));
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(LogMessage.DeleteAPIKeyFailureMsg , e);
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