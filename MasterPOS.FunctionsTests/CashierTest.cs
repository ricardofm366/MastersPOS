using MasterPOS.Functions.Cashier;
using MasterPOS.Functions.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MasterPOS.FunctionsTests
{
    [TestClass]
    public class CashierTest
    {
        [TestMethod]
        public void CheckCurrencySettings()
        {

        }


        [TestMethod]
        public void Rounding_Test()
        {
            CashierHelper helper = new CashierHelper();
            var totalToPay = 2.22M;

            var cash = new CashModel
            {
                Cash = new Dictionary<decimal, int>() { { 100, 4 }, { 5, 3 }, { 0.50M, 3 } }
            };
            var result = helper.MakePayment(totalToPay, cash);
            Assert.IsTrue(result.Rounding == 0.03M);
        }


        [TestMethod]
        [ExpectedException(typeof(CashierException), "The paymeny amount is not valid.")]
        public void Amount_Test()
        {
            CashierHelper helper = new CashierHelper();
            var cash = new CashModel
            {
                Cash = new Dictionary<decimal, int>() { { 100, 4 }, { 5, 3 }, { 0.50M, 3 } }
            };
            _ = helper.MakePayment(0, cash);
        }

        [TestMethod]
        [ExpectedException(typeof(CashierException), "The cash input is not valid")]
        public void Cash_Fill_Test()
        {
            CashierHelper helper = new CashierHelper();
            var cash = new CashModel
            {
            };
            _ = helper.MakePayment(10M, cash);
        }


        [TestMethod]
        [ExpectedException(typeof(CashierException), "The cash input is not enough")]
        public void Cash_Total_Test()
        {
            CashierHelper helper = new CashierHelper();
            var cash = new CashModel
            {
                Cash = new Dictionary<decimal, int>() { { 50, 4 }, { 10, 3 } }
            };
            _ = helper.MakePayment(600M, cash);
        }


        [TestMethod]
        public void Cash_Payment_Test()
        {
            CashierHelper helper = new CashierHelper();
            var cash = new CashModel
            {
                Cash = new Dictionary<decimal, int>() { { 100, 2 }, { 50, 2 }, { 20, 1 } ,{ .5M, 10} }
            };
            var result = helper.MakePayment(200M, cash);

            Assert.IsTrue(result.Cash[100M] == 1);
            Assert.IsTrue(result.Cash[20M] == 1);
            Assert.IsTrue(result.Cash[5M] == 1);

            Assert.IsTrue(result.Rounding == 0);
        }


    }
}
