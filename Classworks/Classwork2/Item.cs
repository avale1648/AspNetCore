namespace Classwork2
{
    public class Item
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;

        public override string ToString()
        {
            return $"{Name} {Price}";
        }
    }
}
