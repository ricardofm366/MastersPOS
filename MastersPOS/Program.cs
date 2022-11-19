using MasterPOS.Functions.Cashier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastersPOS
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                CashierHelper helper = new CashierHelper();

                var totalToPay = 2.22M;

                var cash = new CashModel
                {
                    Cash = new Dictionary<decimal, int>() { { 100, 4 }, { 5, 3 }, { 0.50M, 3 } }
                };
                var result = helper.MakePayment(totalToPay, cash);


                Console.WriteLine($"TOTAL :{totalToPay}");
                Console.WriteLine($"PAYMENTS :{cash.Total}");
                Console.WriteLine($"AMOUNT DUE :{result.Total}");
                foreach (var item in result.Cash)
                {
                    Console.WriteLine($"{item.Value} - {item.Key} = {item.Key * item.Value}");
                }

                if (result.Rounding > 0)
                {
                    Console.WriteLine($"Do you want to donate {result.Rounding} ?");
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }
    }
}
