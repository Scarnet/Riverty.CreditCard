using Riverty.CreditCard.Commands;
using Riverty.CreditCard.Queries;

namespace Riverty.CreditCard.Services
{
    /// <summary>
    /// Card validation service.
    /// </summary>
    public interface ICreditCardValidationService : IService<ValidateCreditCardCommand, CardTypeQuery>
    {
    }
}
