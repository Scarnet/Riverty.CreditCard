using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riverty.CreditCard.Commands;
using Riverty.CreditCard.Modules;
using Riverty.CreditCard.Queries;
using Riverty.CreditCard.Services;

namespace Riverty.CreditCard.Features
{
    public class CreditCardModule : BaseModule, IModule
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapPost("/api/validate-credit-card", ValidateCreditCard).Produces<CardTypeQuery>();
        }

        public void DefineServices(IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<CreditCardValidationService>();
            services.TryAddSingleton<CardTypeDetector>();
        }

        private async Task<IResult> ValidateCreditCard([FromBody] ValidateCreditCardCommand command, CreditCardValidationService service)
        {
            return MapResponse(await service.Execute(command));
        }

    }
}
