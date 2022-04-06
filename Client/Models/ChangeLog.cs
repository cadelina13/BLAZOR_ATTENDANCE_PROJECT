using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class ChangeLog
    {
        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public ChangeLog()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
