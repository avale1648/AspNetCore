namespace WebApp.Frontend.Models;

public class Category
{
	public int Id { get; set; } = 0;
	public string Name { get; set; } = string.Empty;

	public Category(int id, string name)
	{
		Id = id;
		Name = name ?? throw new ArgumentNullException(nameof(name));
	}
}