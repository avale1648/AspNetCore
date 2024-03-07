namespace WebApp.Frontend.Models;

public class Catalogue : ICatalogue
{
	private SemaphoreSlim _semaphore = new SemaphoreSlim(1);
	private readonly List<Product> _products = new List<Product>()
	{
		new Product(1, "Ф. Энгельс, Принципы коммунизма", "", 250m, new Category(1, "Литература"), ""),
		new Product(2, "Ф. Энгельс, К. Маркс, Манифест коммунистической партии", "", 250m, new Category(1, "Литература"), ""),
		new Product(3, "В.И. Ленин, Государство и революция", "", 250m, new Category(1, "Литература"), ""),
	};
	private readonly List<Category> _categories = new List<Category>()
	{
		new Category(1, "Литература"),
		new Category(1, "Канц. товары")
	};

	public async Task AddProductAsync(string name, string description, decimal price, Category category, string imageUrl)
	{
		await _semaphore.WaitAsync();

		try
		{
			var id = _products.Max(p => p.Id) + 1;
			var product = new Product(id, name, description, price, category, imageUrl);
			_products.Add(product);
		}
		finally
		{
			_semaphore.Release();
		}
	}

	public IReadOnlyList<Product> GetProducts()
	{
		return _products.AsReadOnly();
	}

	public IReadOnlyList<Category> GetCategories()
	{
		return _categories.AsReadOnly();
	}

	public async Task<Product> GetProductsAsync(int id)
	{
		await _semaphore.WaitAsync();
		try
		{
			var product = _products.FirstOrDefault(p => p.Id == id);
			if(product is null)
			{
				throw new ArgumentNullException(nameof(product));
			}
			return product;
		}
		finally
		{
			_semaphore.Release();
		}
	}
}
