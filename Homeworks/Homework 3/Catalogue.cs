using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
//Реализация 1: BlockingCollection
//Операции по изменению и удалению не удалось реализовать в желаемом виде при использовании данной коллекции, т.к. методы .Take() и .TryTake() "берут" первый эл-т из коллекции
namespace Homework_3
{
    public class Catalogue
    {
        private BlockingCollection<Item> items = new BlockingCollection<Item>()
        {
            new Item (1, "Socks", 100),
            new Item (2, "Pants", 500),
            new Item (3, "Pants", 350)
        };
        public async Task Create(Item item)
        {
            await Task.Run(()=>items.Add(item));
        }
        public async Task<Item> Read(int id)
        {
            if (id <= 0 || id > items.Count)
                throw new ArgumentOutOfRangeException("id");
            List<Item> temp = new();
            await Task.Run(() => temp = items.ToList());
            return temp.Find(i => i.Id == id);
        }
        public async Task<Item[]> ReadAll()
        {
            await Task.Delay(0);
            return items.ToArray();
        }
        public async Task Update(int id, Item item)
        {
            if (id <= 0 || id > items.Count)
                throw new ArgumentOutOfRangeException("id");
            await Task.Run(() => items.Take());
            item.Id = id;
            await Task.Run(() => items.Add(item));

        }
        public async Task Delete(int id)
        {
            if (id <= 0 || id > items.Count)
                throw new ArgumentOutOfRangeException("id");
            await Task.Run(() => items.Take());
        }
        public async Task Clear()
        {
            for(int i = 0; i < items.Count; i++)
            {
                await Task.Run(()=>items.Take());
            }
        }
    }
}
