namespace WebApp.Frontend.Models;

public class Product
{
	public int Id { get; set; }
	public string Name { get; set; }

	public string Description { get; set; }
	public decimal Price { get; set; }
	public Category Category { get; set; }
	public string ImageUrl { get; set; }

	public Product(int id, string name, string description, decimal price, Category category, string imageUrl)
	{
		Id = id;
		Name = name ?? throw new ArgumentNullException(nameof(name));
		Description = description ?? throw new ArgumentNullException();
		if (price <= 0) throw new ArgumentException(nameof(price));
		Price = price;
		Category = category ?? throw new ArgumentNullException(nameof(category));
		ImageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
	}
}
