using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
//Реализация 2: ConcurrentDictionary
//Операции по изменению и удалению удалось реализовать в желаемом виде при использовании данной коллекции.
//Первоначальным минусом было то, что мне пришлось изменить структуру классa Item, убрав поле Id,
//однако позже Id был возвращен. Id "синхронизирован" с полем Catalogue.key.
namespace Homework_3
{
    public class Catalogue
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
        public KeyValuePair<Guid,Item> Read(Guid key)
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
