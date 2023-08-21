using Homework_3;
using Microsoft.AspNetCore.Http.Json;
//
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>
(options => { options.SerializerOptions.WriteIndented = true; });
var app = builder.Build();
var catalogue = new Catalogue();
app.MapGet("/", () => 
"Asp.Net: �������� ������ �2" +
"\nREST:" +
"\n[Post]/catalogue - ������� ����� �����;" +
"\n[Get]/catalogue/{id} - �������� ����� � ��������������� {id};" +
"\n[Get]/catalogue - �������� ���� �������;" +
"\n[Put]/catalogue/{id} - ������������� ����� c ��������������� {id};" +
"\n[Delete]/catalogue/{id} - ������� ����� � ��������������� {id};" +
"\n[Delete]/catalogue - �������� �������." +
"\nRPC:" +
"\n[Post]/create - ������� ����� �����;" +
"\n[Get]/read?id={id} - �������� ����� � ��������������� {id};"+
"\n[Get]/read_all - �������� ���� �������;" +
"\n[Post]/update?id={id} - ������������� ����� c ��������������� {id};" +
"\n[Post]/delete?id={id} - ������� ����� � ��������������� {id};" +
"\n[Post]/clear - �������� �������.");
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