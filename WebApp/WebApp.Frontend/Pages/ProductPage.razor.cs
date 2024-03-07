using Microsoft.AspNetCore.Components;
using WebApp.Frontend.Models;

namespace WebApp.Frontend.Pages;

public partial class ProductPage
{
	[Parameter] public int ProductId { get; set; }
	private Product? _product;

	protected override async Task OnInitializedAsync()
	{
		await base.OnInitializedAsync();
		_product = await Catalogue.GetProductsAsync(ProductId) ?? throw new ArgumentOutOfRangeException(nameof(ProductId));
	}
}
