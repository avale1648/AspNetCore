using System.Xml.Linq;
//
namespace Homework_5.Catalogue
{
    public class Item : ICloneable
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public Item() { }
        public Item(string name, decimal price)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException("price");
            }
            Name = name;
            Price = price;
        }
        public Item(Item that)
        {
            if (string.IsNullOrEmpty(that.Name))
            {
                throw new ArgumentNullException("name");
            }
            if (that.Price <= 0)
            {
                throw new ArgumentOutOfRangeException("price");
            }
            Name = that.Name;
            Price = that.Price;
        }

        public object Clone()
        {
            var clonedItem = new Item(Name, Price);
            return clonedItem;
        }
    }
}
