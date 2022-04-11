using Client.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class SendSMSViewModel
    {
        public List<AttendanceRecord> listabsent { get; set; }
        public Subject sub { get; set; }
        public DateTime selectedDate { get; set; }
        public string selectedTime { get; set; }
        public CustomMessage msg { get; set; }
    }
}
