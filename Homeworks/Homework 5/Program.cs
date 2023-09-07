//Usings
using Homework_5;
using Homework_5.Catalogue;
using Homework_5.Email;
using Homework_5.Week;
using Microsoft.AspNetCore.Http.Json;
//Builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(options => { options.SerializerOptions.WriteIndented = true; });
builder.Services.AddSingleton<ICatalogue, Catalogue>();
builder.Services.AddSingleton<IWeek, WeekDiscount>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddHostedService<EmailBackgroundService>();
//App
var app = builder.Build();
var catalogue = app.Services.GetService<ICatalogue>();
var weekDiscount = app.Services.GetService<IWeek>();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var sender = services.GetRequiredService<IEmailSender>();
}
var emailBackgroundService = app.Services.GetService<EmailBackgroundService>();
app.MapGet("/", () =>
"Asp.Net: ƒомашн€€ работа є5, —егодн€ " + weekDiscount.GetDateTime().ToShortDateString() + " " + weekDiscount.GetDateTime().DayOfWeek +
"\nREST:" +
"\n[Post]/catalogue - создать новый товар;" +
"\n[Get]/catalogue/{id} - получить товар с идентификатором {id};" +
"\n[Get]/catalogue - получить весь каталог;" +
"\n[Put]/catalogue/{id} - редактировать товар c идентификатором {id};" +
"\n[Delete]/catalogue/{id} - удалить товар с идентификатором {id};" +
"\n[Delete]/catalogue - очистить каталог." +
"\nRPC:" +
"\n[Post]/create - создать новый товар;" +
"\n[Get]/read?id={id} - получить товар с идентификатором {id};" +
"\n[Get]/read_all - получить весь каталог;" +
"\n[Post]/update?id={id} - редактировать товар c идентификатором {id};" +
"\n[Post]/delete?id={id} - удалить товар с идентификатором {id};" +
"\n[Post]/clear - очистить каталог.");
//REST
app.MapPost("/catalogue", (Item item) => { catalogue.Create(item); return Results.Created("/catalogue", item); });
app.MapGet("/catalogue/{id}", (Guid id) => catalogue.Read(weekDiscount, id));
app.MapGet("/catalogue", () => catalogue.ReadAll(weekDiscount));
app.MapPut("/catalogue/{id}", (Guid id, Item item) => catalogue.Update(id, item));
app.MapDelete("/catalogue/{id}", (Guid id) => catalogue.Delete(id));
app.MapDelete("/catalogue", () => catalogue.Clear());
//RPC
app.MapPost("/create", (Item item) => { catalogue.Create(item); return Results.Created("/create", item); });
app.MapGet("/read", (Guid id) => catalogue.Read(weekDiscount, id));
app.MapGet("/read_all", () => catalogue.ReadAll(weekDiscount));
app.MapPost("/update", (Guid id, Item item) => catalogue.Update(id, item));
app.MapPost("/delete", (Guid id) => catalogue.Delete(id));
app.MapPost("/clear", () => catalogue.Clear());
//
app.Run();