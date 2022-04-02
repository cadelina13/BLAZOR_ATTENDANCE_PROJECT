using Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Subject
    {
        public string Id { get; set; }
        public string SectionId { get; set; }
        public string SubjectName { get; set; }
        public TimeSpan? Time { get; set; }
        public Subject()
        {
            Id = Guid.NewGuid().ToURLGuid();
        }
    }
}
