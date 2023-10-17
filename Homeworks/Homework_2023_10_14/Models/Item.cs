namespace Homework_2023_10_14.Models
{
    public class Item
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public string ImageLink { get; set; } = string.Empty;
        public Item() { }
        public Item(string name, decimal price, string imageLink)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException("price");
            }
            if(string.IsNullOrEmpty(imageLink))
            {
                throw new ArgumentNullException("image link");
            }
            Name = name;
            Price = price;
            ImageLink = imageLink;
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
            if(string.IsNullOrEmpty(that.ImageLink))
            {
                throw new ArgumentNullException("image link");
            }
            Name = that.Name;
            Price = that.Price;
            ImageLink = that.ImageLink;
        }
        public object Clone()
        {
            var clonedItem = new Item(Name, Price, ImageLink);
            return clonedItem;
        }
    }
}
