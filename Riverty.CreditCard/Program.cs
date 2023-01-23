using Riverty.CreditCard.Features;
using Riverty.CreditCard.Modules;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using Module = Riverty.CreditCard.Modules.Module;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add all the minimal API modules you need to use here
builder.Services.AddModules(builder.Configuration, typeof(CreditCardModule).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseModules();

app.Run();
