using Classwork_7;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.Run();
var logger = new ServerStartLogger(Lo);