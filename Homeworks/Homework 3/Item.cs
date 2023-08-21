using System.Xml.Linq;
//
namespace Homework_3
{
    public class Item
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public Item() { }
        public Item(int id, string name, decimal price)
        {
            if (string.IsNullOrEmpty(name))
            { throw new ArgumentNullException("name"); }
            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException("price");
            }
            Id = id;
            Name = name;
            Price = price;
        }
        public Item(Item that)
        {
            if (string.IsNullOrEmpty(that.Name))
            { throw new ArgumentNullException("name"); }
            if (that.Price <= 0)
            {
                throw new ArgumentOutOfRangeException("price");
            }
            this.Id = that.Id;
            this.Name = that.Name;
            this.Price = that.Price;
        }
    }
}
