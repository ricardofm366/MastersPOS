using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterPOS.Functions.Exceptions
{
    public class CashierException : Exception
    {
        public CashierException(string message, Exception ex):base(message, ex) {
        }

        public CashierException(string message) : base(message)
        {
        }
    }
}
