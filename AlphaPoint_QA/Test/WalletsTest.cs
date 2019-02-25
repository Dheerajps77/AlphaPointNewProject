﻿using System;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{

    [Collection("Alphapoint_QA_USER")]
    public class WalletsTest : TestBase
    {

        private string instrument;
        private string currencyName;
        private string comment;
        private string amountOfBtcToSend;
        private string withdrawStatus;
        private string user12_EmailAddress;
        private string user13_EmailAddress;

        private string amountOfUSDToWithdraw;
        private string fullName;
        private string language;
        private string bankAddress;
        private string bankAccountNumber;
        private string bankName;
        private string swiftCode;


        public WalletsTest(ITestOutputHelper output) : base(output)
        {

        }


        [Fact]
        public void TC36_SendExternalWallets()
        {
            try
            {
                string emailAddress;
                string gmailPassword;
                string successMsg;
                string currentBalanceOfUser3;
                string holdBalance;
                string availableBalance;
                string btcAmount;
                string minerFees;
                string btcTotalaAmount;
                string withdrawSuccessMsg;
                string increasedHoldAmount;
                string incresedHoldBalance;
                string TotalBalance;
                string reducedAvailableBalance;
                string hold;
                string expectedReducedAvailableBalance;
                string statusID;
                string mailSubject;
                string withdrawSuccess;
                string acceptedticketStatus;
                string totalBalance;
                string expectedReducedHoldBalance;
                string expectedReducedTotalBalance;
                string linkUrl;
                string ticketStatusNew;

                instrument = TestData.GetData("Instrument");
                currencyName = TestData.GetData("CurrencyName");
                comment = TestData.GetData("Comment");
                amountOfBtcToSend = TestData.GetData("AmountOfBtcToSend");
                withdrawStatus = TestData.GetData("WithdrawStatus");
                emailAddress = TestData.GetData("User_14EmailAddress");
                gmailPassword = TestData.GetData("GmailUser_Test1Password");
                mailSubject = TestData.GetData("GmailMailSubject_ConfirmYourWithdraw");
                acceptedticketStatus = TestData.GetData("AcceptedTicketStatus");
                ticketStatusNew = TestData.GetData("TicketStatus");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER12);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER12));

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                WalletPage walletpage = new WalletPage();
                walletpage.ClickOnInstrumentReceiveButton(driver, currencyName);
                walletpage.CopyAddressToReceiveBTC(driver);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.CopyAddressSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.RecievedAddressCopied, Const.USER12));
                walletpage.CloseSendOrReciveSection(driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER12));

                userFunctions.LogIn(TestProgressLogger, Const.USER14);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER14));
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                currentBalanceOfUser3 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                holdBalance = walletpage.HoldBalanceDetailsPage;
                availableBalance = walletpage.AvailableBalanceDetailsPage;
                walletpage.ClickSendButtonOnDetailsPage(driver);
                walletpage.SendBitCoinExternalWallet(driver, comment, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SendBitCoinSuccessfully, amountOfBtcToSend));
                btcAmount = walletpage.GetBtcAmountOnConfirmation(driver);
                minerFees = walletpage.GetMinerFeesOnConfirmation(driver);
                btcTotalaAmount = GenericUtils.GetSumFromStringAfterAddition(btcAmount, minerFees);
                walletpage.ClickConfirmButton(driver);
                withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.WithdrawSuccessMsg, withdrawSuccessMsg);

                increasedHoldAmount = GenericUtils.GetSumFromStringAfterAddition(holdBalance, btcTotalaAmount);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                incresedHoldBalance = walletpage.HoldBalanceDetailsPage;
                TotalBalance = walletpage.TotalBalanceDetailsPage;
                reducedAvailableBalance = walletpage.AvailableBalanceDetailsPage;
                hold = GenericUtils.ConvertToDoubleFormat(GenericUtils.ConvertStringToDouble(increasedHoldAmount));
                Assert.Equal(hold, incresedHoldBalance);
                TestProgressLogger.LogCheckPoint(LogMessage.HoldAmountIncreasedSuccessfully);

                expectedReducedAvailableBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(availableBalance, btcTotalaAmount);
                Assert.Equal(expectedReducedAvailableBalance, GenericUtils.RemoveCommaFromString(reducedAvailableBalance));
                TestProgressLogger.LogCheckPoint(LogMessage.AvailableAmountReducedSuccessfully);
                statusID = walletpage.GetStatusID(driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER14));

                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                admincommonfunctions.SelectTicketsMenu();
                admincommonfunctions.VerifyStatus(driver, statusID, withdrawStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CreatedTicketStatusVerified, statusID));
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminUserLogoutSuccessfully, Const.ADMIN1));

                GmailCommonFunctions gmailobj = new GmailCommonFunctions();
                linkUrl = gmailobj.Gmail(driver, emailAddress, gmailPassword, mailSubject);
                driver.Navigate().GoToUrl(linkUrl);
                withdrawSuccess = walletpage.GetWithdrawConfirmedMsg(driver);
                Assert.Equal(LogMessage.WithdrawSuccessfullyConfirmMsg, withdrawSuccess);
                walletpage.ClickOnGoToExchange(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.WithdrawConfirmedMassage);

                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);
                admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                admincommonfunctions.SelectTicketsMenu();
                admincommonfunctions.VerifyStatus(driver, statusID, ticketStatusNew);

                admincommonfunctions.DoubleClickOnCreatedDepositTicket(driver, statusID);
                admincommonfunctions.ClickOnAcceptButtonFromDepositsTicketModal();
                admincommonfunctions.VerifyStatus(driver, statusID, acceptedticketStatus);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedTicketStatus);
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminUserLogoutSuccessfully, Const.ADMIN1));

                userFunctions.LogIn(TestProgressLogger, Const.USER14);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER14));
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                currentBalanceOfUser3 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                holdBalance = walletpage.HoldBalanceDetailsPage;
                totalBalance = walletpage.TotalBalanceDetailsPage;

                expectedReducedHoldBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(incresedHoldBalance, btcTotalaAmount);
                Assert.Equal(expectedReducedHoldBalance, GenericUtils.RemoveCommaFromString(holdBalance));
                TestProgressLogger.LogCheckPoint(LogMessage.HoldBalanceVerified);

                expectedReducedTotalBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(TotalBalance, btcTotalaAmount);
                Assert.Equal(expectedReducedTotalBalance, GenericUtils.RemoveCommaFromString(totalBalance));
                TestProgressLogger.LogCheckPoint(LogMessage.TotalBalanceVerified);

                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER14));
                TestProgressLogger.EndTest();
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(LogMessage.SendExternalWalletsTestFailed, e);
                throw e;
            }
        }


        [Fact]
        public void TC37_WalletsSendToEmailAddress()
        {
            try
            {
                string username;
                string secondUsername;
                string currentBalance;
                string withdrawSuccessMsg;
                string updatedCurrentBalance;
                string expectedupdateBalance;
                string availableBalance;
                string totalBalance;
                string expectedTotalBalanceAfterSent;
                string totalBalanceAfterSent;
                string availableBalanceAfterSent;
                string expectedAvailableBalanceAfterSent;
                string availableBalanceOfFirstUser;
                string totalBalanceOfFirstUser;

                currencyName = TestData.GetData("CurrencyName");
                comment = TestData.GetData("Comment");
                amountOfBtcToSend = TestData.GetData("AmountOfBtcToSend");
                withdrawStatus = TestData.GetData("WithdrawStatus");
                user12_EmailAddress = TestData.GetData("User_12EmailAddress");
                user13_EmailAddress = TestData.GetData("User_13EmailAddress");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                username = userFunctions.LogIn(TestProgressLogger, Const.USER12);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER12));

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                WalletPage walletpage = new WalletPage();
                currentBalance = walletpage.GetInstrumentCurrentBalance(driver, currencyName);

                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                availableBalanceOfFirstUser = walletpage.AvailableBalanceDetailsPage;
                totalBalanceOfFirstUser = walletpage.TotalBalanceDetailsPage;

                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.StoreCurrentBalance, Const.USER12));
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER12));

                secondUsername = userFunctions.LogIn(TestProgressLogger, Const.USER13);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER13));
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                availableBalance = walletpage.AvailableBalanceDetailsPage;
                totalBalance = walletpage.TotalBalanceDetailsPage;
                walletpage.ClickSendButtonOnDetailsPage(driver);

                walletpage.ClickOnEmailAddressTab(driver);
                walletpage.SendBitCoinToEmailAddress(driver, comment, user12_EmailAddress, amountOfBtcToSend);
                walletpage.VerifySendDetailsBalances(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.RemainingBalanceVerified);
                walletpage.ClickOnSendBitCoin(driver);
                walletpage.VerifyConfirmationModal(driver, user12_EmailAddress, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedDetailsOnConfirmModal);
                walletpage.ClickConfirmButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.ConfirmationModalVerified);
                withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.TransferSuccessMsg, withdrawSuccessMsg);
                walletpage.CloseSendOrReciveSection(driver);


                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                totalBalanceAfterSent = walletpage.TotalBalanceDetailsPage;
                expectedTotalBalanceAfterSent = GenericUtils.GetDifferenceFromStringAfterSubstraction(totalBalance, amountOfBtcToSend);
                Assert.Equal(expectedTotalBalanceAfterSent, GenericUtils.RemoveCommaFromString(totalBalanceAfterSent));
                TestProgressLogger.LogCheckPoint(LogMessage.TotalBalanceVerified);

                availableBalanceAfterSent = walletpage.AvailableBalanceDetailsPage;
                expectedAvailableBalanceAfterSent = GenericUtils.GetDifferenceFromStringAfterSubstraction(totalBalance, amountOfBtcToSend);
                Assert.Equal(expectedAvailableBalanceAfterSent, GenericUtils.RemoveCommaFromString(availableBalanceAfterSent));
                TestProgressLogger.LogCheckPoint(LogMessage.RemainingBalanceVerified);
                walletpage.ClickRefreshTransfers(driver);

                walletpage.VerifyAmountInTransferSection(driver, username, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedRequestUnderSentRequest);

                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER13));
                userFunctions.LogIn(TestProgressLogger, Const.USER12);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER12));
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                updatedCurrentBalance = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                expectedupdateBalance = GenericUtils.GetSumFromStringAfterAddition(currentBalance, amountOfBtcToSend);
                Assert.Equal(expectedupdateBalance, GenericUtils.RemoveCommaFromString(updatedCurrentBalance));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BalanceUpdatedSuccessfully, Const.USER12));

                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);

                totalBalanceAfterSent = walletpage.TotalBalanceDetailsPage;
                expectedTotalBalanceAfterSent = GenericUtils.GetSumFromStringAfterAddition(availableBalanceOfFirstUser, amountOfBtcToSend);
                Assert.Equal(expectedTotalBalanceAfterSent, GenericUtils.RemoveCommaFromString(totalBalanceAfterSent));
                TestProgressLogger.LogCheckPoint(LogMessage.TotalBalanceVerified);

                availableBalanceAfterSent = walletpage.AvailableBalanceDetailsPage;
                expectedAvailableBalanceAfterSent = GenericUtils.GetSumFromStringAfterAddition(totalBalanceOfFirstUser, amountOfBtcToSend);
                Assert.Equal(expectedAvailableBalanceAfterSent, GenericUtils.RemoveCommaFromString(availableBalanceAfterSent));
                TestProgressLogger.LogCheckPoint(LogMessage.RemainingBalanceVerified);

                walletpage.ClickReceivedTransferOnDetailsPage(driver);
                walletpage.ClickRefreshTransfers(driver);
                walletpage.VerifyAmountInTransferSection(driver, secondUsername, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedTransactionUnderReceivedTransfer);

                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER12));
                TestProgressLogger.EndTest();
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(LogMessage.WalletsSendToEmailAddressTestFailed, e);
                throw e;
            }
        }

        [Fact]
        public void TC39_WalletsReceiveRequestbyEmail()
        {
            try
            {
                string username;
                string currentBalanceOfUser7;
                string currentBalanceOfUser8;
                string availablebalance;
                string totalbalance;
                string withdrawSuccessMsg;
                string updatedCurrentBalance;
                string expectedupdateBalance;
                string currentBalance;
                string expupdateBalance;
                string availablebalanceAfterApprove;


                instrument = TestData.GetData("Instrument");
                currencyName = TestData.GetData("CurrencyName");
                comment = TestData.GetData("Comment");
                amountOfBtcToSend = TestData.GetData("AmountOfBtcToSend");
                withdrawStatus = TestData.GetData("WithdrawStatus");
                user12_EmailAddress = TestData.GetData("User_12EmailAddress");
                user13_EmailAddress = TestData.GetData("User_13EmailAddress");

                TestProgressLogger.StartTest();

                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                username = userFunctions.LogIn(TestProgressLogger, Const.USER12);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER12));
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                WalletPage walletpage = new WalletPage();
                currentBalanceOfUser7 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.StoreCurrentBalance, Const.USER12));
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER12));

                userFunctions.LogIn(TestProgressLogger, Const.USER13);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER13));

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                currentBalanceOfUser8 = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.StoreCurrentBalance, Const.USER13));
                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                availablebalance = walletpage.AvailableBalanceDetailsPage;
                totalbalance = walletpage.AvailableBalanceDetailsPage;
                walletpage.ClickReceiveButtonOnDetailsPage(driver);
                walletpage.ClickOnReceiveRequestByEmail(driver);
                walletpage.SendBitCoinRequestByEmail(driver, comment, user12_EmailAddress, amountOfBtcToSend);
                walletpage.ClickOnSendBitCoin(driver);
                walletpage.VerifyConfirmationModal(driver, user12_EmailAddress, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedDetailsOnConfirmModal);
                walletpage.ClickConfirmButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.ConfirmationModalVerified);
                withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.RequestTransferSuccessMsg, withdrawSuccessMsg);
                walletpage.CloseSendOrReciveSection(driver);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                Assert.Equal(availablebalance, walletpage.AvailableBalanceDetailsPage);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedAvailableBalance);
                Assert.Equal(totalbalance, walletpage.AvailableBalanceDetailsPage);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedTotalBalance);
                walletpage.ClickRefreshTransfers(driver);
                walletpage.SelectSentRequests(driver);
                walletpage.VerifyAmountInTransferSentRequestsSection(driver, username, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedRequestUnderSentRequest);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER13));
                userFunctions.LogIn(TestProgressLogger, Const.USER12);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER12));

                walletpage.ClickApproveButton(driver);
                withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(LogMessage.TransferApproved, withdrawSuccessMsg);
                Assert.False(walletpage.VerifyApproveButton(driver));
                Assert.False(walletpage.VerifyRejectButton(driver));
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedApproveAndRejectButton);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                updatedCurrentBalance = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                expectedupdateBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(currentBalanceOfUser7, amountOfBtcToSend);
                Assert.Equal(expectedupdateBalance, GenericUtils.RemoveCommaFromString(updatedCurrentBalance));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BalanceReducedSuccessfully, Const.USER12));
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER12));

                userFunctions.LogIn(TestProgressLogger, Const.USER13);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER13));
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                currentBalance = walletpage.GetInstrumentCurrentBalance(driver, currencyName);
                expupdateBalance = GenericUtils.GetSumFromStringAfterAddition(currentBalanceOfUser8, amountOfBtcToSend);
                Assert.Equal(expupdateBalance, GenericUtils.RemoveCommaFromString(currentBalance));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BalanceIncreasedSuccessfully, Const.USER13));

                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                availablebalanceAfterApprove = walletpage.AvailableBalanceDetailsPage;
                expupdateBalance = GenericUtils.GetSumFromStringAfterAddition(availablebalance, amountOfBtcToSend);
                Assert.Equal(expupdateBalance, GenericUtils.RemoveCommaFromString(availablebalanceAfterApprove));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AvailableBalanceIncresedAfterApporve, Const.USER13));
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER13));
                TestProgressLogger.EndTest();
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(LogMessage.WalletsReceiveRequestByEmailTestFailed, e);
                throw e;
            }
        }


        [Fact]
        public void TC40_WalletsWithdrawFiatcurrency()
        {
            try
            {
                string emailAddress;
                string gmailPassword;
                string amounttowithdraw;
                string currentUSDBalance;
                string fee;
                string remainingBalance;
                string amountToWithdrawAndFees;
                string expectedRemainingBalance;
                string holdBalance;
                string totalBalance;
                string availableBalance;
                string withdrawSuccessMsg;
                string holdBalanceAfterDeposit;
                string availableBalanceAfterDeposit;
                string expectedAvailableBalanceAfterDeposit;
                string expectedHoldBalanceAfterDeposit;
                string statusID;
                string linkUrl;
                string ticketStatus;
                string withdrawSuccess;
                string mailSubject;
                string totalBalanceAfterDeposit;
                string expectedTotalBalanceAfterDeposit;


                currencyName = TestData.GetData("USDCurrency");
                comment = TestData.GetData("TC40_Comment");
                amountOfUSDToWithdraw = TestData.GetData("USDAmount");
                fullName = TestData.GetData("FullName");
                language = TestData.GetData("TC40_Language");
                bankAddress = TestData.GetData("TC40_BankAddress");
                bankAccountNumber = TestData.GetData("TC40_BankAccountNumber");
                bankName = TestData.GetData("TC40_BankName");
                swiftCode = TestData.GetData("TC40_SwiftCode");
                withdrawStatus = TestData.GetData("WithdrawStatus");
                emailAddress = TestData.GetData("User_14EmailAddress");
                gmailPassword = TestData.GetData("GmailUser_Test1Password");
                mailSubject = TestData.GetData("GmailMailSubject_ConfirmYourWithdraw");
                ticketStatus = TestData.GetData("FullyProcessedTicketStatus");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER15);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER15));

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                WalletPage walletpage = new WalletPage();
                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                holdBalance = walletpage.HoldBalanceDetailsPage;
                availableBalance = walletpage.AvailableBalanceDetailsPage;
                walletpage.ClickWithdrawButtonOnDetails(driver);

                walletpage.WithdrawUSD(driver, amountOfUSDToWithdraw, fullName, language, comment, bankAddress, bankAccountNumber, bankName, swiftCode);
                amounttowithdraw = walletpage.GetAmountToWithdraw(driver);
                currentUSDBalance = walletpage.GetCurrentUSDBalance(driver);
                fee = walletpage.GetFee(driver);
                remainingBalance = walletpage.GetRemainingBalance(driver);
                amountToWithdrawAndFees = GenericUtils.GetSumFromStringAfterAddition(amounttowithdraw, fee);
                expectedRemainingBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(currentUSDBalance, amountToWithdrawAndFees);
                Assert.Equal(expectedRemainingBalance, GenericUtils.RemoveCommaFromString(remainingBalance));
                TestProgressLogger.LogCheckPoint(LogMessage.RemainingBalanceVerifiedOnBalanceSection);

                walletpage.ClickOnWithdrawUSDButton(driver);
                walletpage.VerifyWithdrawUSDOnConfirmationModal(driver, amountOfUSDToWithdraw, fullName, language, comment, bankAddress, bankAccountNumber, bankName, swiftCode, fee);
                walletpage.ClickOnConfirmUSDModalButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.ConfirmationModalVerified);
                withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(LogMessage.USDWithdrawSuccessMsg, withdrawSuccessMsg);

                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                holdBalanceAfterDeposit = walletpage.HoldBalanceDetailsPage;
                availableBalanceAfterDeposit = walletpage.AvailableBalanceDetailsPage;
                totalBalanceAfterDeposit = walletpage.TotalBalanceDetailsPage;
                expectedAvailableBalanceAfterDeposit = GenericUtils.GetDifferenceFromStringAfterSubstraction(availableBalance, amountToWithdrawAndFees);
                Assert.Equal(expectedAvailableBalanceAfterDeposit, GenericUtils.RemoveCommaFromString(availableBalanceAfterDeposit));
                TestProgressLogger.LogCheckPoint(LogMessage.RemainingBalanceVerified);
                expectedHoldBalanceAfterDeposit = GenericUtils.GetSumFromStringAfterAddition(holdBalance, amountToWithdrawAndFees);
                Assert.Equal(expectedHoldBalanceAfterDeposit, holdBalanceAfterDeposit);
                TestProgressLogger.LogCheckPoint(LogMessage.HoldBalanceVerified);

                statusID = walletpage.GetStatusID(driver);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER15));

                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                admincommonfunctions.SelectTicketsMenu();
                admincommonfunctions.VerifyStatus(driver, statusID, withdrawStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CreatedTicketStatusVerified, statusID));
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminUserLogoutSuccessfully, Const.ADMIN1));

                GmailCommonFunctions gmailobj = new GmailCommonFunctions();
                linkUrl = gmailobj.Gmail(driver, emailAddress, gmailPassword, mailSubject);
                driver.Navigate().GoToUrl(linkUrl);
                withdrawSuccess = walletpage.GetWithdrawConfirmedMsg(driver);
                Assert.Equal(LogMessage.WithdrawSuccessfullyConfirmMsg, withdrawSuccess);
                walletpage.ClickOnGoToExchange(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.WithdrawConfirmedMassage);

                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);
                admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                admincommonfunctions.SelectTicketsMenu();
                admincommonfunctions.VerifyStatus(driver, statusID, ticketStatus);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedTicketStatus);
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminUserLogoutSuccessfully, Const.ADMIN1));

                userFunctions.LogIn(TestProgressLogger, Const.USER15);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER15));

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                holdBalance = walletpage.HoldBalanceDetailsPage;
                totalBalance = walletpage.TotalBalanceDetailsPage;

                expectedTotalBalanceAfterDeposit = GenericUtils.GetDifferenceFromStringAfterSubstraction(totalBalanceAfterDeposit, amountToWithdrawAndFees);
                Assert.Equal(expectedTotalBalanceAfterDeposit, GenericUtils.RemoveCommaFromString(totalBalance));
                TestProgressLogger.LogCheckPoint(LogMessage.TotalBalanceVerified);

                expectedHoldBalanceAfterDeposit = GenericUtils.GetDifferenceFromStringAfterSubstraction(holdBalanceAfterDeposit, amountToWithdrawAndFees);
                Assert.Equal(expectedHoldBalanceAfterDeposit, holdBalance);
                TestProgressLogger.LogCheckPoint(LogMessage.HoldBalanceVerified);

                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER15));

                TestProgressLogger.EndTest();
                TestProgressLogger.LogCheckPoint(LogMessage.WalletsWithdrawFiatcurrencyTestPassed);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(LogMessage.WalletsWithdrawFiatcurrencyTestFailed, e);
                throw e;
            }
        }


        [Fact]
        public void TC41_WalletsDepositFiatcurrency()
        {
            try
            {
                string ticketStatus;
                string AcceptedticketStatus;
                string amount;
                string availableBalanceAfterDeposit;
                string availableBalanceAfterAccept;
                string totalBalance;
                string pendingBalance;
                string withdrawSuccessMsg;
                string pendingBalanceAfterDeposit;
                string expectedPendingBalanceAfterDeposit;
                string expectedPendingBalanceAfterAccept;
                string ticketID;
                string expectedAvailableBalanceAfterAccept;

                currencyName = TestData.GetData("USDCurrency");
                comment = TestData.GetData("TC41_Comment");
                amount = TestData.GetData("USDAmount");
                fullName = TestData.GetData("FullName");
                language = TestData.GetData("TC40_Language");
                bankAddress = TestData.GetData("TC40_BankAddress");
                bankAccountNumber = TestData.GetData("TC40_BankAccountNumber");
                bankName = TestData.GetData("TC40_BankName");
                swiftCode = TestData.GetData("TC40_SwiftCode");
                withdrawStatus = TestData.GetData("WithdrawStatus");
                ticketStatus = TestData.GetData("TicketStatus");
                AcceptedticketStatus = TestData.GetData("AcceptedTicketStatus");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER15);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER15));

                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                WalletPage walletpage = new WalletPage();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.StoreCurrentBalance, Const.USER15));
                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                pendingBalance = walletpage.PendingDepositDetailsPage;
                totalBalance = walletpage.TotalBalanceDetailsPage;
                walletpage.ClickDepositButtonOnDetails(driver);
                walletpage.SendUSDDeposit(driver, fullName, amount, comment);
                walletpage.VerifyUSDDepositOnConfirmationModal(driver, fullName, amount, comment);
                walletpage.ClickOnConfirmUSDModalButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.ConfirmationModalVerified);
                withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(LogMessage.USDDepositSuccessMsg, withdrawSuccessMsg);
                ticketID = walletpage.GetDepositUSDTicketID(driver);
                GenericUtils.RefreshPage(driver);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                pendingBalanceAfterDeposit = walletpage.PendingDepositDetailsPage;
                availableBalanceAfterDeposit = walletpage.AvailableBalanceDetailsPage;
                expectedPendingBalanceAfterDeposit = GenericUtils.GetSumFromStringAfterAddition(pendingBalance, amount);
                Assert.Equal(expectedPendingBalanceAfterDeposit, GenericUtils.RemoveCommaFromString(pendingBalanceAfterDeposit));
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER15));

                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                admincommonfunctions.SelectTicketsMenu();
                admincommonfunctions.NavigateToDepositTicketsTab();
                admincommonfunctions.VerifyStatus(driver, ticketID, ticketStatus);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedTicketStatusAsNew);
                admincommonfunctions.DoubleClickOnCreatedDepositTicket(driver, ticketID);
                admincommonfunctions.ClickOnAcceptButtonFromDepositsTicketModal();
                admincommonfunctions.VerifyStatus(driver, ticketID, AcceptedticketStatus);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedTicketStatusAsAccepted);
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminUserLogoutSuccessfully, Const.ADMIN1));

                userFunctions.LogIn(TestProgressLogger, Const.USER15);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER15));
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                walletpage.ClickInstrumentDetails(driver, currencyName);
                walletpage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                pendingBalance = walletpage.PendingDepositDetailsPage;
                availableBalanceAfterAccept = walletpage.AvailableBalanceDetailsPage;

                expectedPendingBalanceAfterAccept = GenericUtils.GetDifferenceFromStringAfterSubstraction(pendingBalanceAfterDeposit, amount);
                Assert.Equal(expectedPendingBalanceAfterAccept, GenericUtils.RemoveCommaFromString(pendingBalance));
                TestProgressLogger.LogCheckPoint(LogMessage.PendingBalanceVerified);

                expectedAvailableBalanceAfterAccept = GenericUtils.GetSumFromStringAfterAddition(availableBalanceAfterDeposit, amount);
                Assert.Equal(expectedAvailableBalanceAfterAccept, GenericUtils.RemoveCommaFromString(availableBalanceAfterAccept));
                TestProgressLogger.LogCheckPoint(LogMessage.AvailableBalanceVerified);

                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER15));

                TestProgressLogger.EndTest();
                TestProgressLogger.LogCheckPoint(LogMessage.WalletsDepositFiatcurrencyTestPassed);
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error(LogMessage.WalletsDepositFiatcurrencyTestFailed, e);
                throw e;
            }
        }





    }
}