using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Data
{
    public class DataAccess
    {
        /*[Inject]
        public ILocalStorageService localdb { get; set; }*/
        ILocalStorageService localdb;

        public DataAccess(ILocalStorageService _localdb)
        {
            localdb = _localdb;
        }

        public async Task<List<T>> GetList<T>(string table)
        {
            if (!await localdb.ContainKeyAsync(table))
                await localdb.SetItemAsync(table, new List<T>());
            var list = await localdb.GetItemAsync<List<T>>(table);
            if (list == null)
                return default(List<T>);
            return list;
        }
        /*public async Task<T> GetRecord<T>(string table, string id)
        {
            var rs = await localdb.GetItemAsync<T>($"{table}{id}");
            return rs;
        }*/
        public async Task SaveRecord<T>(string table, T data)
        {
            await localdb.SetItemAsync(table, data);
        }
        /*public async Task DeleteRecord<T>(string table, string id)
        {
            await localdb.RemoveItemAsync($"{table}{id}");
        }*/
    }
}
