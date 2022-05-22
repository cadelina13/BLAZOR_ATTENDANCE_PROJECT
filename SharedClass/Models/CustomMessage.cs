using SharedClass.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedClass.Models
{
    public class CustomMessage
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public CustomMessage()
        {
            Id = Guid.NewGuid().ToURLGuid();
        }
    }
}
