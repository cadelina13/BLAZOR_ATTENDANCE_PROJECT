using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class AttendanceViewModel
    {
        public AttendanceRecord AttendanceRecord { get; set; }
        public Student Student { get; set; }

        public string userId { get; set; }
        public string subjectId { get; set; }
        public DateTime selectedDate { get; set; }
    }
}
