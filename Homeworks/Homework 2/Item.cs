using System.Xml.Linq;
//
namespace Homework_2
{
    public class Item
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public Item() { }
        public Item(int id, string name, decimal price)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException("id");
            }
            if(string.IsNullOrEmpty(name))
            { throw new ArgumentNullException("name"); }
            if(price <= 0)
            {
                throw new ArgumentOutOfRangeException("price");
            }
            Id = id;
            Name = name;
            Price = price;
        }
        public Item(Item other)
        {
            if(other.Id <= 0)
            {
                throw new ArgumentOutOfRangeException("id");
            }
            if (string.IsNullOrEmpty(other.Name))
            { throw new ArgumentNullException("name"); }
            if (other.Price <= 0)
            {
                throw new ArgumentOutOfRangeException("price");
            }
            this.Id = other.Id;
            this.Name = other.Name;
            this.Price = other.Price;
        }
    }
}
