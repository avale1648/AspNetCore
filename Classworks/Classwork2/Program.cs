using Classwork2;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Catalogue.Init();
app.MapGet("/", () => "/list - �������� �������;\n" +
                      "/add&name=[������������]s&price=[����]m - �������� �����");
app.MapGet("/list", () => Catalogue.GetItems());
app.MapPost("/add", (string? name, decimal? price) => Catalogue.AddItem(new Item { Name = (string)name, Price = (decimal)price }));
app.Run();
