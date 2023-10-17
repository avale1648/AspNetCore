using System.Collections.Concurrent;

namespace Homework_2023_10_14.Models
{
    public class Catalogue: ICatalogue
    {
        private ConcurrentDictionary<Guid, Item> items = new ConcurrentDictionary<Guid, Item>();
        public Catalogue()
        {
            var guid = Guid.NewGuid();
            items.TryAdd(guid, new Item("Штаны", 1000, "https://cdn.lcls.ru/r/Ok0TDTN-0uw/rs:fit:1000:0:1/plain/images/products/1/5597/450647517/empty_10.jpg"));
            guid = Guid.NewGuid();
            items.TryAdd(guid, new Item("Рубашка", 750, "https://cdn.lcls.ru/r/kN2hlwyTWms/rs:fit:1000:0:1/plain/images/products/1/2405/765307237/rugby1.jpg"));
            guid = Guid.NewGuid();
            items.TryAdd(guid, new Item("Футболка", 500, "https://cdn.lcls.ru/r/tJwXKeYjs4M/rs:fit:1000:0:1/plain/images/products/1/547/749617699/86993472.jpg"));
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
