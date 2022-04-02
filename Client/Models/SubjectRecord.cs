using Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class SubjectRecord
    {
        public string Id { get; set; }
        public string SubjectId { get; set; }
        public string StudentId { get; set; }
        public SubjectRecord()
        {
            Id = Guid.NewGuid().ToURLGuid();
        }
    }
}
