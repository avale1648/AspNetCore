using Homework_3;
using Microsoft.AspNetCore.Http.Json;
//
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>
(options => { options.SerializerOptions.WriteIndented = true; });
var app = builder.Build();
var catalogue = new Catalogue();
app.MapGet("/", () => 
"Asp.Net: Домашняя работа №2" +
"\nREST:" +
"\n[Post]/catalogue - создать новый товар;" +
"\n[Get]/catalogue/{id} - получить товар с идентификатором {id};" +
"\n[Get]/catalogue - получить весь каталог;" +
"\n[Put]/catalogue/{id} - редактировать товар c идентификатором {id};" +
"\n[Delete]/catalogue/{id} - удалить товар с идентификатором {id};" +
"\n[Delete]/catalogue - очистить каталог." +
"\nRPC:" +
"\n[Post]/create - создать новый товар;" +
"\n[Get]/read?id={id} - получить товар с идентификатором {id};"+
"\n[Get]/read_all - получить весь каталог;" +
"\n[Post]/update?id={id} - редактировать товар c идентификатором {id};" +
"\n[Post]/delete?id={id} - удалить товар с идентификатором {id};" +
"\n[Post]/clear - очистить каталог.");
//REST
app.MapPost("/catalogue", async (Item item) => {await catalogue.Create(item); return Results.Created("/catalogue", item); });
app.MapGet("/catalogue/{id}", async (int id) => await catalogue.Read(id));
app.MapGet("/catalogue", async () => await catalogue.ReadAll());
app.MapPut("/catalogue/{id}", async (int id, Item item) => await catalogue.Update(id, item)) ;
app.MapDelete("/catalogue/{id}", async (int id) => await catalogue.Delete(id));
app.MapDelete("/catalogue", async () => await catalogue.Clear());
//RPC
app.MapPost("/create", async (Item item) => { await catalogue.Create(item); return Results.Created("/create", item); });
app.MapGet("/read", async (int id) => await catalogue.Read(id));
app.MapGet("/read_all", async () => await catalogue.ReadAll());
app.MapPost("/update", async (int id, Item item) => await catalogue.Update(id, item));
app.MapPost("/delete", async (int id) => await catalogue.Delete(id));
app.MapPost("/clear", async () => await catalogue.Clear());
//
app.Run();