using WebApp.Frontend.Models;

namespace WebApp.Frontend.Pages;

public partial class CataloguePage
{
	private IReadOnlyList<Product> _products;

	protected override void OnInitialized()
	{
		base.OnInitialized();
		_products = Catalogue.GetProducts();
	}

	private void NavigateToProductPage(int id)
	{
		string url = $"/catalogue/{id}";
		NavigationManager.NavigateTo(url);
	}

	private void NavigateToAddPage()
	{
		NavigationManager.NavigateTo("/add");
	}
}
