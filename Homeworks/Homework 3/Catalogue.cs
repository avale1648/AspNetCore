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
        private static int key = 1;
        private ConcurrentDictionary<int, Item> items = new ConcurrentDictionary<int, Item>();
        public Catalogue()
        {
            items.TryAdd(key, new Item(key, "Штаны", 1000));
            key++;
            items.TryAdd(key, new Item(key, "Рубашка", 750));
            key++;
            items.TryAdd(key, new Item(key, "Футболка", 500));
            key++;
        }
        public async Task Create(Item item)
        {
            item.Id = key;
            await Task.Run(() => { items.TryAdd(key, item); key++; });
        }
        public async Task<Item> Read(int id)
        {
            if (id <= 0 || id > items.Count)
                throw new ArgumentOutOfRangeException("id");
            await Task.Delay(0);
            return items[id];
        }
        public async Task<KeyValuePair<int, Item>[]> ReadAll()
        {
            List<Item> temp = new List<Item>();
            await Task.Delay(0);
            return items.ToArray();
        }
        public async Task Update(int id, Item item)
        {
            if (id <= 0 || id > items.Count)
                throw new ArgumentOutOfRangeException("id");
            await Task.Run(() => items.TryUpdate(id, item, items[id]));

        }
        public async Task Delete(int id)
        {
            if (id <= 0 || id > items.Count)
                throw new ArgumentOutOfRangeException("id");
            await Task.Run(() => items.TryRemove(new KeyValuePair<int, Item>(id, items[id])));
        }
        public async Task Clear()
        {
            await Task.Run(() => items.Clear());
        }
    }
}
