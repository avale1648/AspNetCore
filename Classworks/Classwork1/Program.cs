var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Текущая дата и время:\n" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() );
app.MapGet("/newYear", () => "До нового года осталось " + (new DateTime(DateTime.Now.Year + 1, 1, 1) - DateTime.Now).Days + " дней.");
app.Run();
