using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
//
namespace Homework_2
{
    public class Catalogue
    {
        private List<Item> items = new List<Item>()
        {
            new Item (1, "Socks", 100),
            new Item (2, "Pants", 500),
            new Item (3, "Pants", 350)
        };
        public void Create(Item item)
        {
            items.Add(item);
        }
        public Item Read(int id)
        {
            if (id <= 0 || id > items.Count)
                throw new ArgumentOutOfRangeException("id");
            return items.Find(i => i.Id == id);
        }
        public Item[] ReadAll()
        {
            return items.ToArray();
        }
        public void Update(int id, Item item)
        {
            if (id <= 0 || id > items.Count)
                throw new ArgumentOutOfRangeException("id");
            int index = items.FindIndex(i => i.Id == id);
            items[index].Name = item.Name;
            items[index].Price = item.Price;
        }
        public void Delete(int id)
        {
            if (id <= 0 || id > items.Count)
                throw new ArgumentOutOfRangeException("id");
            items.Remove(items.Find(i => i.Id == id));
        }
        public void Clear()
        {
            items.Clear();
        }
    }
}
