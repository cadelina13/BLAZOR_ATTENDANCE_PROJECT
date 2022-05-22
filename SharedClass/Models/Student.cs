using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedClass.Data;

namespace SharedClass.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public int GradeLevel { get; set; }
        public string ParentsPhoneNumber { get; set; }
        public Student()
        {
            Id = Guid.NewGuid().ToURLGuid();
        }
    }
}
