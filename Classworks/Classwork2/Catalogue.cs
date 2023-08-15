namespace Classwork2
{
    public static class Catalogue
    {
        private static List<Item> items = new List<Item> ();
        public static void Init()
        {
            items.Add (new Item { Name = "Носки", Price=150});
            items.Add(new Item { Name = "Штаны", Price = 500 });
            items.Add(new Item { Name = "Футболка", Price = 350 });
        }
        public static Item[] GetItems()
        {
            return items.ToArray();
        }
        public static string AddItem(Item item)
        {
            try
            {
                items.Add(item);
                return $"Добавлен товар:\n{item}";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
    
}
