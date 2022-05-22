using SharedClass.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClass.Models
{
    public class SectionRecord
    {
        public string Id { get; set; }
        public string SectionId { get; set; }
        public string StudentId { get; set; }
        public SectionRecord()
        {
            Id = Guid.NewGuid().ToURLGuid();
        }
    }
}
