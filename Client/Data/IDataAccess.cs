using SharedClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Data
{
    public interface IDataAccess
    {
        public Task AddUser(User user);
    }
}
