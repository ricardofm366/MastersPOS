using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterPOS.Functions.Cashier
{
    public class CashModel
    {
        public Dictionary<decimal, int> Cash { get; set; }

        public decimal Rounding { get; set; }

        public CashModel() {
            Cash = new Dictionary<decimal, int>();
        }

        public decimal Total {
            get { return Cash.Sum(x => x.Key * x.Value); }
        }
    }
}
