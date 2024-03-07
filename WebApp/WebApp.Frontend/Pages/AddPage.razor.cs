using WebApp.Frontend.Models;

namespace WebApp.Frontend.Pages
{
    public partial class AddPage
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private decimal _price = 0m;
        private Category _category = new Category(0, string.Empty);
        private string _imageUrl = string.Empty;

        private async Task AddProductAsync()
        {
            await Catalogue.AddProductAsync(_name, _description, _price, _category, _imageUrl);
        }
    }
}
