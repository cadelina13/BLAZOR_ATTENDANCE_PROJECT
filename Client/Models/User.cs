
using Client.Data;
using System;

namespace Client.Models
{
    public class User
    {
        public string Id { get; set; }
        public int SMS { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string PIN { get; set; }
        public bool IsVerified { get; set; }
        public DateTime DateRegistered { get; set; }
        public User()
        {
            Id = Guid.NewGuid().ToURLGuid();
            DateRegistered = DateTime.Now;
        }
    }
}
