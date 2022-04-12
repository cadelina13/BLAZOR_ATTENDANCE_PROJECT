using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Reference { get; set; }
        public double Amount { get; set; }
    }
}
