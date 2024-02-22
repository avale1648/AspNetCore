//Usings
using Microsoft.AspNetCore.Http.Json;
using Homework_6.Email;
using Homework_6.Email.HostedService;
//Builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Configuration.AddEnvironmentVariables().Build();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(options => { options.SerializerOptions.WriteIndented = true; });;
builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
builder.Services.AddHostedService<EmailHostedService>();
//App
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
var emailSender = app.Services.GetService<IEmailSender>();
var emailHostedService = app.Services.GetService<EmailHostedService>();
//
app.Run();