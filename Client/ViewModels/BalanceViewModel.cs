using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class BalanceViewModel
    {
        public string type { get; set; }
        public double amount { get; set; }
        public double remaining_sms { get; set; }
    }
}
