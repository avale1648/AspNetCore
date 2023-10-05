//Usings
using Microsoft.AspNetCore.Http.Json;
using Homework_5.Week;
using Homework_5.Catalogue;
using Homework_5.Email;
using Homework_5.Email.HostedService;
//Builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Configuration.AddEnvironmentVariables().Build();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(options => { options.SerializerOptions.WriteIndented = true; });;
builder.Services.AddSingleton<ICatalogue, Catalogue>();
builder.Services.AddSingleton<IWeek, WeekDiscount>();
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
builder.Services.AddHostedService<EmailHostedService>();
//App
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
var catalogue = app.Services.GetService<ICatalogue>();
var weekDiscount = app.Services.GetService<IWeek>();
var emailSender = app.Services.GetService<IEmailSender>();
var emailHostedService = app.Services.GetService<EmailHostedService>();
app.MapGet("/", () =>
"Asp.Net: �������� ������ �4, ������� " + weekDiscount.GetDateTime().ToShortDateString() + " " + weekDiscount.GetDateTime().DayOfWeek +
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