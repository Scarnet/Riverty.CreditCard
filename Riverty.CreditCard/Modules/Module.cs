using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Riverty.CreditCard.Modules
{
    /// <summary>
    /// Initial module that executes common service and end point registeration
    /// </summary>
    public class Module : IModule
    {
        private string? _apiName;

        public virtual void DefineEndpoints(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_apiName} v1"));
            }

            if(app.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        public virtual void DefineServices(IServiceCollection services, IConfiguration configuration)
        {
            _apiName = Assembly.GetEntryAssembly()?.GetName()?.Name;

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Description = _apiName,
                    Title = _apiName,
                    Version = "v1"
                });
            });
        }

    }
}
