using MasterPOS.Functions.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterPOS.Functions.Cashier
{
    public class CashierHelper
    {

        private string _currency;

        private List<decimal> _currencies = new List<decimal>();

        public CashierHelper() {
            try
            {
                _currency = ConfigurationManager.AppSettings["currency"];
            }
            catch (Exception ex) {
                throw new CashierException("Error reading the currency global setting", ex);
            }

            var items = _currency.Split(new[] { ',' });

            try
            {
                foreach (var item in items)
                {
                    _currencies.Add(decimal.Parse(item));
                }
            }
            catch (Exception ex) {
                throw new CashierException("Error parsing currency settings", ex);
            }

            _currencies.Sort();
        }

        /// <summary>
        /// This fucntion returns the smallest number of bills and coins equal to the change due
        /// </summary>
        /// <param name="amount">Total price of items </param>
        /// <param name="inputCash">Bills and coins provided by the customer</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CashierException">Validation of input data</exception>
        public CashModel MakePayment(decimal amount, CashModel inputCash) {

            if (inputCash == null) {
                throw new ArgumentNullException("inputCash", "The items is null");
            }

            if (inputCash.Cash.Count == 0)
            {
                throw new CashierException("Input cash is empty");
            }

            if (amount <= 0) {
                throw new CashierException("Amount is not valid");
            }

            decimal totalGivenCash = inputCash.Cash.Sum(x => x.Key * x.Value);
            if (totalGivenCash < amount) {
                throw new CashierException("Cash is not enough");
            }

            var change = totalGivenCash - amount;

            var result = new CashModel();
            while (change > 0) {
                var max = _currencies.Where(x => x <= change).DefaultIfEmpty().Max();
                if (max == 0) { 
                    break;
                }
                int count = (int)(change / max);
                if (count > 0) {
                    result.Cash.Add(max, count);
                    change = change % max;
                }
            }

            //there is not specfication in case total is not exact
            result.Rounding = change;

            return result;
        }


        public CashModel MakePayment2(decimal amount, CashModel inputCash)
        {

            if (inputCash == null)
            {
                throw new ArgumentNullException("inputCash", "The items is null");
            }

            if (inputCash.Cash.Count == 0)
            {
                throw new CashierException("Input cash is empty");
            }

            if (amount <= 0)
            {
                throw new CashierException("Amount is not valid");
            }

            decimal totalGivenCash = inputCash.Cash.Sum(x => x.Key * x.Value);
            if (totalGivenCash < amount)
            {
                throw new CashierException("Cash is not enough");
            }

            var change = totalGivenCash - amount;

            var result = new CashModel();

            while (change > 0)
            {
                var max = _currencies.Where(x => x <= change).DefaultIfEmpty().Max();
                if (max == 0)
                {
                    break;
                }
                int count = (int)(change / max);
                if (count > 0)
                {
                    result.Cash.Add(max, count);
                    change = change % max;
                }
            }

            //there is not specfication in case total is not exact
            result.Rounding = change;

            return result;
        }
    }
}
