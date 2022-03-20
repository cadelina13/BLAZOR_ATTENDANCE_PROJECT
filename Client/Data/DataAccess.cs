using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using SharedClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Data
{
    public class DataAccess : IDataAccess
    {
        [Inject]
        public ILocalStorageService db { get; set; }
        public async Task AddUser(User user)
        {
            //await db.SetItemAsync("user", user);   
        }
    }
}
