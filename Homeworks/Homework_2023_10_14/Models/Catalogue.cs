using System.Collections.Concurrent;

namespace Homework_2023_10_14.Models
{
    public class Catalogue: ICatalogue
    {
        private ConcurrentDictionary<Guid, Item> items = new ConcurrentDictionary<Guid, Item>();
        public Catalogue()
        {
            var guid = Guid.NewGuid();
            items.TryAdd(guid, new Item("Штаны", 1000, "https://www.google.com/url?sa=i&url=https%3A%2F%2Flcls.ru%2Fcollection%2Fbryuki%2Fproduct%2Fsportivnye-shtany-element&psig=AOvVaw2H-PDdmrKbeL4O1UTmv3mm&ust=1697638039497000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCNCVl9Sg_YEDFQAAAAAdAAAAABAD"));
            guid = Guid.NewGuid();
            items.TryAdd(guid, new Item("Рубашка", 750, "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.gulliver.ru%2Fproducts%2Fbelaya-rubashka-s-dlinnym-rukavom-gulliver-219gsbc2319%2F&psig=AOvVaw34NqbDm5_GixcDTnmCym9x&ust=1697637807227000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCNjJvOWf_YEDFQAAAAAdAAAAABAD"));
            guid = Guid.NewGuid();
            items.TryAdd(guid, new Item("Футболка", 500, "https://www.google.com/url?sa=i&url=https%3A%2F%2Fmos-poshiv.ru%2Fproduct%2Ffutbolka-chernaya-oversayz-100-hlopok-plotnost-190-g&psig=AOvVaw1aqMEO7rOAFCfQ23FUmZon&ust=1697638072708000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCLjRrOOg_YEDFQAAAAAdAAAAABAD"));
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
