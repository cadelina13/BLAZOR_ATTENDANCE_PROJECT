using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Section
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public Section()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
