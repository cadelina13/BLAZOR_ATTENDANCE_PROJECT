using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedClass.ViewModels
{
    public class PaymentResponseHeaderViewModel
    {
        public PaymentResponseViewModel data { get; set; }
        public int success { get; set; }
        public string elapsed_time { get; set; }
    }
    public class PaymentResponseViewModel
    {
        public string byapi { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string amount { get; set; }
        public string fee { get; set; }
        public string grossamount { get; set; }
        public string netamount { get; set; }
        public string currency { get; set; }
        public string hash { get; set; }
        public string expiry { get; set; }
        public string customername { get; set; }
        public string customeremail { get; set; }
        public string customermobile { get; set; }
        public string merchantname { get; set; }
        public string merchantlogourl { get; set; }
        public string webhooksuccessurl { get; set; }
        public string webhookfailurl { get; set; }
        public string redirectsuccessurl { get; set; }
        public string redirectfailurl { get; set; }
        public string dateadded { get; set; }
        public string checkouturl { get; set; }
        public string request_id { get; set; }
    }
}
