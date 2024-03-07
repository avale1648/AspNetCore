namespace WebApp.Frontend.Models;

public interface ICatalogue
{
	Task AddProductAsync(string name, string description, decimal price, Category category, string imageUrl);

	IReadOnlyList<Product> GetProducts();

	Task<Product> GetProductsAsync(int id);
}
