using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Homework_5.Week;
using System.Collections.Concurrent;
//
namespace Homework_5.Catalogue
{
    public class Catalogue : ICatalogue
    {
        private ConcurrentDictionary<Guid, Item> items = new ConcurrentDictionary<Guid, Item>();
        public Catalogue()
        {
            var guid = Guid.NewGuid();
            items.TryAdd(guid, new Item("Штаны", 1000));
            guid = Guid.NewGuid();
            items.TryAdd(guid, new Item("Рубашка", 750));
            guid = Guid.NewGuid();
            items.TryAdd(guid, new Item("Футболка", 500));
        }
        public bool Create(Item value)
        {
            var key = Guid.NewGuid();
            return items.TryAdd(key, value);
        }
        public KeyValuePair<Guid, Item> Read(Guid key)
        {
            var keyValue = new KeyValuePair<Guid, Item>(key, items[key]);
            return keyValue;
        }
        public KeyValuePair<Guid, Item>[] ReadAll()
        {
            return items.ToArray();
        }
        public KeyValuePair<Guid, Item> Read(IWeek weekDiscount, Guid key)
        {
            var day = weekDiscount.GetDateTime().DayOfWeek;
            var copy = new KeyValuePair<Guid, Item>(key, (Item)items[key].Clone());
            if (day == DayOfWeek.Monday)
            {
                copy.Value.Price = copy.Value.Price - 0.3m * copy.Value.Price;
            }
            return copy;
        }
        public KeyValuePair<Guid, Item>[] ReadAll(IWeek weekDiscount)
        {
            //Копирование
            var copies = new ConcurrentDictionary<Guid, Item>();
            foreach (var item in items)
            {
                copies.TryAdd(item.Key, (Item)item.Value.Clone());
            }
            //Обработка дня
            var day = weekDiscount.GetDateTime().DayOfWeek;
            if (day == DayOfWeek.Monday)
            {
                foreach (var copy in copies.Values)
                {
                    copy.Price = copy.Price - (decimal)0.3 * copy.Price;
                }
            }
            return copies.ToArray();
        }
        public bool Update(Guid key, Item value)
        {
            return items.TryUpdate(key, value, items[key]);
        }
        public bool Delete(Guid key)
        {
            return items.TryRemove(new KeyValuePair<Guid, Item>(key, items[key]));
        }
        public void Clear()
        {
            items.Clear();
        }
    }
}
