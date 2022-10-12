using App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App1.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
              
                new Item { Id = Guid.NewGuid().ToString(), Name = "First item",NameKitchen ="This is an item description.", Type = "TYPE", Category = "CAT", HTMLText="" },
               
            };
            items = MySql.MySql.GetDate();
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);
            await Task.Run(() => { MySql.MySql.AddDateAsync(item); }); 
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            await Task.Run(() => { MySql.MySql.UpdateDateAsync(oldItem.Id,item); });

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            await Task.Run(() => { MySql.MySql.DeleteDateAsync(oldItem.Id); });
            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}