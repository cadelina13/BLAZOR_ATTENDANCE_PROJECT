using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class AttendanceRecord
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string SubjectId { get; set; }
        public bool IsPresent { get; set; }
        public DateTime? DateAttended { get; set; }
        public AttendanceRecord()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
