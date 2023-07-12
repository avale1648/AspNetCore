var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "������� ���� � �����:\n" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() );
app.MapGet("/newYear", () => "�� ������ ���� �������� " + (new DateTime(DateTime.Now.Year + 1, 1, 1) - DateTime.Now).Days + " ����.");
app.Run();
