using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedClass.ViewModels
{
    public class GcashApiViewModel
    {
        public string success { get; set; }
        public string request_id { get; set; }
        public double amount { get; set; }
        public string reference { get; set; }
        public string response_message { get; set; }
        public string response_advise { get; set; }
        public DateTime timestamp { get; set; }

    }
}
