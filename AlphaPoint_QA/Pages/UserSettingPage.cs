﻿using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace AlphaPoint_QA.Pages
{
    class UserSettingPage
    {
        IWebDriver driver;
        ProgressLogger logger;

        By apiKeysLink = By.XPath("//li[@data-test='API Keys']");
        By apiKeyButton = By.XPath("//button[@class='ap-button__btn ap-button__btn--general settings-api-keys-form__btn settings-api-keys-form__btn--general']");
        By tradingCheckbox = By.XPath("//label[@for='trading']");
        By depositsCheckbox = By.XPath("//label[@for='deposit']");
        By withdrawlsCheckbox = By.XPath("//label[@for='withdraw']");
        By tradingCheckboxSelected = By.XPath("//label[@for='trading']/span[@class='isvg loaded ap-icon ap-icon--checkbox ap-checkbox__icon create-api-key__icon ap-checkbox__icon create-api-key__icon--checkbox']");
        By depositsCheckboxSelected = By.XPath("//label[@for='deposit']/span[@class='isvg loaded ap-icon ap-icon--checkbox ap-checkbox__icon create-api-key__icon ap-checkbox__icon create-api-key__icon--checkbox']");
        By withdrawlsCheckboxSelected = By.XPath("//label[@for='withdraw']/span[@class='isvg loaded ap-icon ap-icon--checkbox ap-checkbox__icon create-api-key__icon ap-checkbox__icon create-api-key__icon--checkbox']");
        By createAPIKeyButton = By.XPath("//button[@class='ap-button__btn ap-button__btn--additive create-api-key__btn create-api-key__btn--additive']");
        By apiConfirmation = By.XPath("/html/body/div[5]/div/div");
        By apiConfirmationPermissions = By.XPath("//span[@data-test='Permissions']");
        By apiConfirmationKey = By.XPath("//span[@data-test='Key']");
        By apiConfirmationSecret = By.XPath("//span[@data-test='Secret']");
        By apiConfirmationButton = By.XPath("//button[@class='ap-button__btn ap-button__btn--additive ap-modal retail-api-keys-modal__btn ap-modal retail-api-keys-modal__btn--additive']");
        By deleteAPIKeyModalButton = By.XPath("//button[@class='ap-button__btn ap-button__btn--subtractive ap-modal retail-api-keys-modal-delete__btn ap-modal retail-api-keys-modal-delete__btn--subtractive']");
        By affliateProgramLink = By.XPath("//li[@data-test='Affiliate Program']");
        By noOfAffiliates = By.XPath("//section[@class='affiliate__body']/section[2]/section/p");
        By copyButtonLocator = By.XPath("//div[@class='affiliate__btn-container']/button[2]");
        By editButtonLocator = By.XPath("//div[@class='affiliate__btn-container']/button[1]");
        By affiliateTagDesc = By.XPath("//p[@class='affiliate__tag-description']");
        By selectServer = By.CssSelector("select[name = tradingServer");

        public UserSettingPage(IWebDriver driver, ProgressLogger logger)
        {
            this.driver = driver;
            this.logger = logger;
        }

        public IWebElement TradingCheckbox()
        {
            return driver.FindElement(tradingCheckbox);
        }

        public IWebElement DepositsCheckbox()
        {
            return driver.FindElement(depositsCheckbox);
        }

        public IWebElement WithdrawlsCheckbox()
        {
            return driver.FindElement(withdrawlsCheckbox);
        }

        public IWebElement TradingCheckboxSelected()
        {
            return driver.FindElement(tradingCheckboxSelected);
        }

        public IWebElement AffiliateTagDesc()
        {
            return driver.FindElement(affiliateTagDesc);
        }
        
        public IWebElement DepositsCheckboxSelected()
        {
            return driver.FindElement(depositsCheckboxSelected);
        }

        public IWebElement WithdrawlsCheckboxSelected()
        {
            return driver.FindElement(withdrawlsCheckboxSelected);
        }

        public IWebElement APIKeysLink()
        {
            return GenericUtils.WaitForElementVisibility(driver, apiKeysLink, 30);      
        }

        public IWebElement AffiliateProgramLink()
        {
            return driver.FindElement(affliateProgramLink);
        }

        public IWebElement APIKeyButton()
        {
            return driver.FindElement(apiKeyButton);
        }

        public IWebElement CreateAPIKeyButton()
        {
            return driver.FindElement(createAPIKeyButton);
        }

        public IWebElement APIConfirmationModal()
        {
            return driver.FindElement(apiConfirmation);
        }

        public IWebElement APIConfirmationButton()
        {
            return driver.FindElement(apiConfirmationButton);
        }

        public IWebElement APIConfirmationPermissions()
        {
            return driver.FindElement(apiConfirmationPermissions);
        }

        public IWebElement APIConfirmationKey()
        {
            return driver.FindElement(apiConfirmationKey);
        }

        public IWebElement APIConfirmationSecret()
        {
            return driver.FindElement(apiConfirmationSecret);
        }

        public IWebElement DeleteAPIKeymodalButton()
        {
            return GenericUtils.WaitForElementVisibility(driver, deleteAPIKeyModalButton, 30);
        }

        public IWebElement CopyAffiliateTagButton()
        {
            return driver.FindElement(copyButtonLocator);
        }

        public bool VerifyAPIKeyButtonIsPresent()
        {
            Thread.Sleep(1000);
            bool flag = false;
            if (APIKeyButton().Enabled)
            {
                flag = true;
            }
            return flag;
        }

        public bool VerifyAPIKeyConfirmationModalIsPresent()
        {
            Thread.Sleep(1000);
            bool flag = false;
            if (APIConfirmationModal().Displayed)
            {
                flag = true;
            }
            return flag;
        }

        // This method stores the data present in the API Confirmation Modal
        public Dictionary<string, string> StoreAPIConfirmationModalData()
        {
            Dictionary<string, string> apiConfirmationModalData = new Dictionary<string, string>();
            apiConfirmationModalData.Add(Const.APIPermissions, APIConfirmationPermissions().Text);
            apiConfirmationModalData.Add(Const.APIKey, APIConfirmationKey().Text);
            apiConfirmationModalData.Add(Const.APISecret, APIConfirmationSecret().Text);
            return apiConfirmationModalData;
        }

        // This method stores the list of activities checked
        public Dictionary<string, bool> StoreAPICheckedActivitiesData()
        {
            Dictionary<string, bool> checkedActivitiesData = new Dictionary<string, bool>();
            checkedActivitiesData.Add(Const.APITradingPermission, TradingCheckboxSelected().Displayed);
            checkedActivitiesData.Add(Const.APIDepositsPermission, DepositsCheckboxSelected().Displayed);
            checkedActivitiesData.Add(Const.APIWithdrawlsPermission, WithdrawlsCheckboxSelected().Displayed);
            return checkedActivitiesData;
        }

        // This method Deletes the API key, takes bool deleteFlag true
        // Returns true if the API Key is successfully deleted
        public bool VerifyDeleteButtonIsPresent(Dictionary<string, string> apiKeyData, bool deleteFlag)
        {
            Thread.Sleep(2000);
            ArrayList apiKeysList = new ArrayList();
            string apiKeyAdded = apiKeyData[Const.APIKey];
            var flag = false;
            int countOfAPIKeys = driver.FindElements(By.XPath("//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div")).Count;
            for (int i = 1; i <= countOfAPIKeys; i++)
            {
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= countItems; j++)
                {
                    string apiKey = driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[1]")).Text;
                    if (apiKey.Equals(apiKeyAdded))
                    {
                        IWebElement deleteButton = driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[5]"));
                        if (deleteButton.Text.Equals(Const.APIDeleteButton))
                        {
                            if (deleteFlag)
                            {
                                deleteButton.Click();
                                UserSetFunctions.Click(DeleteAPIKeymodalButton());
                                flag = true;
                            }
                        }
                    }
                }
            }
            if (flag)
            {
                logger.LogCheckPoint(Const.DeleteAPIKeyIsPresent);
            }
            else
            {
                logger.LogCheckPoint(Const.DeleteAPIKeyIsNotPresent);
            }
            return flag;
        }

        // This method verifies if the Secret key is present in the List
        // Secret key should not be displayed
        // Returns True if Secret Key is not displayed
        public bool VerifySecretkeyIsPresent(IWebDriver driver, Dictionary<string, string> apiKeyData)
        {
            var flag = false;
            string secretKeyAdded = apiKeyData[Const.APISecret];
            int countOfAPIKeys = driver.FindElements(By.XPath("//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div")).Count;

            for (int i = 1; i <= countOfAPIKeys; i++)
            {
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= countItems; j++)
                {
                    string verifySecretKey = driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[1]")).Text;
                    if (verifySecretKey.Equals(secretKeyAdded))
                    {
                        flag = true;

                    }

                }
            }
            if (flag)
            {
                logger.LogCheckPoint(Const.SecretKeyVerificationFailed);
            }
            return flag;
        }

        // This method verifies if the API key added is present in the List
        // This method matches the API key activities on List versus the activities selected while creating an API Key 
        // Returns True if success
        public bool VerifyAddedAPIKey(IWebDriver driver, Dictionary<string, string> apiKeyData, Dictionary<string, bool> checkedActivitiesAdded)
        {
            Thread.Sleep(1000);
            ArrayList apiKeysList = new ArrayList();
            string apiKeyAdded = apiKeyData[Const.APIKey];
            var flag = false;
            bool allowDeposits = checkedActivitiesAdded[Const.APIDepositsPermission];
            bool allowTradings = checkedActivitiesAdded[Const.APITradingPermission];
            bool allowWithdrawls = checkedActivitiesAdded[Const.APIWithdrawlsPermission];
            int countOfAPIKeys = driver.FindElements(By.XPath("//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div")).Count;
            for (int i = 1; i <= countOfAPIKeys; i++)
            {
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= countItems; j++)
                {
                    string apiKey = driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[1]")).Text;
                    string deleteButton = driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[5]")).Text;

                    if (apiKey.Equals(apiKeyAdded))
                    {
                        // This matches the state of the items checked
                        bool allowDepositsState = allowDeposits.Equals(driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[2]")).Enabled);
                        bool allowWithdrawlsState = allowWithdrawls.Equals(driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[3]")).Enabled);
                        bool allowTradingsState = allowTradings.Equals(driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[4]")).Enabled);

                        if (allowDepositsState && allowWithdrawlsState && allowTradingsState)
                        {
                            if (deleteButton.Equals(Const.APIDeleteButton))
                            {
                                if (!VerifySecretkeyIsPresent(driver, apiKeyData))
                                {
                                    flag = true;
                                }

                            }
                        }
                    }
                }
            }
            if (flag)
            {
                logger.LogCheckPoint(Const.APIKeyAddedIsPresentInTheList);
                logger.LogCheckPoint(Const.VerifiedSelectedPermissions);
                logger.LogCheckPoint(Const.SecretKeyVerificationPassed);
            }
            else
            {
                logger.LogCheckPoint(Const.APIKeyAddedIsNotPresentInTheList);
            }

            return flag;
        }

        public void SelectAPIKeyCheckboxes()
        {
            Thread.Sleep(2000);
            UserSetFunctions.Click(TradingCheckbox());
            UserSetFunctions.Click(DepositsCheckbox());
            UserSetFunctions.Click(WithdrawlsCheckbox());
            logger.LogCheckPoint(Const.SelectedAPIKeyCheckboxes);
            Thread.Sleep(2000);
        }

        public bool SelectAPIKey()
        {
            bool flag = false;
            Dictionary<string, string> apiKeyData = new Dictionary<string, string>();
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.NavigateToUserSetting(driver);
            Thread.Sleep(2000);
            UserSetFunctions.Click(APIKeysLink());
            Thread.Sleep(2000);
            if (VerifyAPIKeyButtonIsPresent())
            {
                UserSetFunctions.Click(APIKeyButton());
                flag = true;
                logger.LogCheckPoint(Const.ClickedOnAPIKeyButton);
            }
            else
            {
                logger.LogCheckPoint(Const.APIKeyButtonDisabled);
            }
            return flag;
        }

        // This method Verifies API Key Checkboxes Are Present
        public bool VerifyAPIKeyCheckboxesArePresent()
        {
            Thread.Sleep(2000);
            bool flag = false;
            if (TradingCheckbox().Displayed && DepositsCheckbox().Displayed && WithdrawlsCheckbox().Displayed)
            {
                flag = true;
            }
            else
            {
                logger.LogCheckPoint(Const.APIKeyCheckboxesAreNotPresent);
            }
            return flag;
        }

        public bool CreateAndVerifyAPIKey()
        {
            bool flag = false;
            SelectAPIKeyCheckboxes();
            Dictionary<string, bool> selectedActivities = StoreAPICheckedActivitiesData();
            UserSetFunctions.Click(CreateAPIKeyButton());
            if (VerifyAPIKeyConfirmationModalIsPresent())
            {
                Dictionary<string, string> apiKeyData = new Dictionary<string, string>();
                apiKeyData = StoreAPIConfirmationModalData();
                logger.LogCheckPoint(String.Format(LogMessage.VerifiedConfirmationModal, apiKeyData["Permissions"]));
                UserSetFunctions.Click(APIConfirmationButton());
                if (VerifyAddedAPIKey(driver, apiKeyData, selectedActivities))
                {
                    flag = true;
                }
                else
                {
                    logger.LogCheckPoint(Const.APIKeyCreationFailureMsg);
                }
            }
            else
            {
                logger.LogCheckPoint(Const.ConfirmationModalFailureMsg);
            }
            return flag;
        }

        public void SelectAffiliateProgram()
        {       
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.NavigateToUserSetting(driver);
            Thread.Sleep(2000);
            UserSetFunctions.Click(AffiliateProgramLink());
        }

        // This method returns the number of affiliates
        public int GetnumberOfAffiliates(IWebDriver driver)
        {
            //UserSetFunctions.Click(AffiliateProgramLink());
            string affiliates = driver.FindElement(noOfAffiliates).Text;
            string[] affiliatesList = affiliates.Split(" ");
            affiliates = affiliatesList[0];
            return Int32.Parse(affiliates);
        }

        // This method verifies affiliates program
        public bool VerifyAffiliateProgramFunctionality(IWebDriver driver, string verificationLevel)
        {
            var flag = false;
            int initialAffiliates;
            string registeredUser;
            string affiliateTagURL;
            
            UserFunctions userFunctions = new UserFunctions(logger);
            AdminFunctions objAdminFunctions = new AdminFunctions(logger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);

            try
            {
                SelectAffiliateProgram();
                initialAffiliates = GetnumberOfAffiliates(driver);
                logger.LogCheckPoint(String.Format(LogMessage.AffiliateProgramInitialAffiliates, initialAffiliates));
                UserSetFunctions.Click(CopyAffiliateTagButton());
                affiliateTagURL = AffiliateTagDesc().Text;
                logger.LogCheckPoint(String.Format(LogMessage.AffiliateProgramURLCopied, affiliateTagURL));
                userFunctions.LogOut();
                driver.Navigate().GoToUrl(affiliateTagURL);
                userFunctions.LogIn(logger, changeServerOnly: true);

                // Register a new user
                registeredUser = userFunctions.RegisterNewUser(driver);

                // Login as Admin and complete KYC of the registered user
                try
                {
                    objAdminFunctions.AdminLogIn(logger);
                    logger.LogCheckPoint(String.Format(LogMessage.KYCStarted, registeredUser));
                    objAdminCommonFunctions.ClickOnUsersMenuLink();
                    objAdminCommonFunctions.SelectUserFromUserList(driver, registeredUser);
                    objAdminCommonFunctions.EditUserEmailStatus(registeredUser);
                    UserCommonFunctions.ScrollingDownVertical(driver);
                    objAdminCommonFunctions.OpenAccountFromUserPage();
                    objAdminCommonFunctions.EditUserVerificationLevel(verificationLevel);
                    logger.LogCheckPoint(String.Format(LogMessage.KYCSuccess, registeredUser));
                    objAdminCommonFunctions.UserMenuBtn();
                    objAdminFunctions.AdminLogOut();
                }
                catch (Exception e)
                {
                    logger.TakeScreenshot();
                    logger.LogCheckPoint(String.Format(LogMessage.KYCFailure, registeredUser));
                    throw e;
                }

                // Login as the user and verify the number of affiliates
                userFunctions.LogIn(logger, Const.USER12);
                SelectAffiliateProgram();
                int affiliatesAfterUserRegistration = GetnumberOfAffiliates(driver);
                if ((affiliatesAfterUserRegistration - initialAffiliates) == 1)
                {
                    flag = true;
                    logger.LogCheckPoint(String.Format(LogMessage.AffiliateProgramFinalAffiliates, affiliatesAfterUserRegistration));
                }
            }
            catch (NoSuchElementException ex)
            {
                logger.TakeScreenshot();
                throw ex;
            }
            catch (Exception e)
            {
                logger.TakeScreenshot();
                throw e;
            }
            return flag;
        }       

        // This method Creates an API Key, returns API KEY DATA Stored in API Confirmation Modal
        public Dictionary<string, string> CreateAPIkey(IWebDriver driver)
        {
            Dictionary<string, string> apiKeyData = new Dictionary<string, string>();
            SelectAPIKeyCheckboxes();
            Dictionary<string, bool> selectedActivities = StoreAPICheckedActivitiesData();
            UserSetFunctions.Click(CreateAPIKeyButton());
            if (VerifyAPIKeyConfirmationModalIsPresent())
            {
                apiKeyData = StoreAPIConfirmationModalData();
                logger.LogCheckPoint(String.Format(LogMessage.VerifiedConfirmationModal, apiKeyData["Permissions"]));
                UserSetFunctions.Click(APIConfirmationButton());
                if (VerifyAddedAPIKey(driver, apiKeyData, selectedActivities))
                {
                    logger.LogCheckPoint(Const.APIKeyCreationSuccessMsg);
                }
                else
                {
                    logger.LogCheckPoint(Const.APIKeyCreationFailureMsg);
                }
            }
            else
            {
                logger.LogCheckPoint(Const.ConfirmationModalFailureMsg);
            } 
            return apiKeyData;
        }

        // This method deletes the API Key
        // Returns true if Delete Button is clicked and API Key is deleted
        public bool DeleteAPIKey(IWebDriver driver)
        {
            var flag = false;
            var deleteFlag = true;
            Dictionary<string, string> apiKeyData;
            SelectAPIKey();
            VerifyAPIKeyCheckboxesArePresent();            
            apiKeyData = CreateAPIkey(driver);
            bool verifyDeleteButton = VerifyDeleteButtonIsPresent(apiKeyData, deleteFlag);
            bool verifyAPIKeyIsDeleted = VerifyAPIKeyIsDeleted(driver, apiKeyData);

            if (verifyDeleteButton && verifyAPIKeyIsDeleted)
            {
                flag = true;
            }
            return flag;
        }

        // This method verifies the Deleted Key is not present in the List
        // Returns True if Deleted Key is displayed
        public bool VerifyAPIKeyIsDeleted(IWebDriver driver, Dictionary<string, string> apiKeyData)
        {
            Thread.Sleep(2000);
            ArrayList apiKeysList = new ArrayList();
            string apiKeyAdded = apiKeyData["Key"];
            var flag = true;
            int countOfAPIKeys = driver.FindElements(By.XPath("//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div")).Count;
            for (int i = 1; i <= countOfAPIKeys; i++)
            {
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= countItems; j++)
                {
                    string apiKey = driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[1]")).Text;
                    if (apiKey.Equals(apiKeyAdded))
                    {
                        flag = false;
                    }
                }
            }
            if (flag)
            {
                logger.LogCheckPoint(LogMessage.DeleteAPIKeySuccessMsg);
            }
            else
            {
                logger.LogCheckPoint(LogMessage.DeleteAPIKeyFailureMsg);
            }
            return flag;
        } 
    }
}
