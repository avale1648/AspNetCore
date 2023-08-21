using Homework_3;
using Microsoft.AspNetCore.Http.Json;
//
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>
(options => { options.SerializerOptions.WriteIndented = true; });
var app = builder.Build();
var catalogue = new Catalogue();
app.MapGet("/", () =>
"Asp.Net: �������� ������ �3" +
"\nREST:" +
"\n[Post]/catalogue - ������� ����� �����;" +
"\n[Get]/catalogue/{id} - �������� ����� � ��������������� {id};" +
"\n[Get]/catalogue - �������� ���� �������;" +
"\n[Put]/catalogue/{id} - ������������� ����� c ��������������� {id};" +
"\n[Delete]/catalogue/{id} - ������� ����� � ��������������� {id};" +
"\n[Delete]/catalogue - �������� �������." +
"\nRPC:" +
"\n[Post]/create - ������� ����� �����;" +
"\n[Get]/read?id={id} - �������� ����� � ��������������� {id};" +
"\n[Get]/read_all - �������� ���� �������;" +
"\n[Post]/update?id={id} - ������������� ����� c ��������������� {id};" +
"\n[Post]/delete?id={id} - ������� ����� � ��������������� {id};" +
"\n[Post]/clear - �������� �������.");
//REST
app.MapPost("/catalogue", (Item item) => { catalogue.Create(item); return Results.Created("/catalogue", item); });
app.MapGet("/catalogue/{id}", (Guid id) => catalogue.Read(id));
app.MapGet("/catalogue", () => catalogue.ReadAll());
app.MapPut("/catalogue/{id}", (Guid id, Item item) => catalogue.Update(id, item));
app.MapDelete("/catalogue/{id}", (Guid id) => catalogue.Delete(id));
app.MapDelete("/catalogue", () => catalogue.Clear());
//RPC
app.MapPost("/create", (Item item) => { catalogue.Create(item); return Results.Created("/create", item); });
app.MapGet("/read", (Guid id) => catalogue.Read(id));
app.MapGet("/read_all", () => catalogue.ReadAll());
app.MapPost("/update", (Guid id, Item item) => catalogue.Update(id, item));
app.MapPost("/delete", (Guid id) => catalogue.Delete(id));
app.MapPost("/clear", () => catalogue.Clear());
//
app.Run();