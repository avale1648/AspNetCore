using Classwork2;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Catalogue.Init();
app.MapGet("/", () => "/list - перечень товаров;\n" +
                      "/add&name=[наименование]s&price=[цена]m - добавить товар");
app.MapGet("/list", () => Catalogue.GetItems());
app.MapPost("/add", (string? name, decimal? price) => Catalogue.AddItem(new Item { Name = (string)name, Price = (decimal)price }));
app.Run();
