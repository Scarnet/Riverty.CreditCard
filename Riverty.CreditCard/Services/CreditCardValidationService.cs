using Riverty.CreditCard.Commands;
using Riverty.CreditCard.Enums;
using Riverty.CreditCard.Queries;
using Riverty.CreditCard.Validators;
using Riverty.CreditCard.Validators.CreditCardValidators;

namespace Riverty.CreditCard.Services
{
    /// <inherit />
    public class CreditCardValidationService : BaseService<ValidateCreditCardCommand, CardTypeQuery>, ICreditCardValidationService
    {
        private CardTypeDetector _cardTypeDetector;

        public CreditCardValidationService(CardTypeDetector cardTypeDetector)
        {
            Validators.AddRange(new IValidator<ValidateCreditCardCommand>[]
            {
                new CreditCardOwnerNotEmptyValidator(),
                new CreditCardOwnerOnlyLettersValidator(),
                new ExpiryMonthValidator(),
                new ExpiryYearValidator(),
                new CreditCardExpiryValidator(),
            });

            _cardTypeDetector = cardTypeDetector;
        }

        /// <Inherit />
        public override Task<CardTypeQuery> Execute(ValidateCreditCardCommand command)
        {
            base.Execute(command);

            if(Errors.Any()) 
            {
                var failedQuery = new CardTypeQuery
                {
                    IsSuccessful = false,
                    Errors = Errors
                };

                return Task.FromResult(failedQuery);
            }

            var cardType = _cardTypeDetector.DetectCardType(command.CardNumber, command.CVC);

            var query = new CardTypeQuery
            {
                IsSuccessful = cardType == CardType.Unknown ? false : true,
                CardType = Enum.GetName(typeof(CardType), cardType),
                Errors = cardType == CardType.Unknown ?
                new Dictionary<string, List<string>> { { "N/A", new List<string> { "Card type could not detected because of inconsistent card data" } } }
                    : new Dictionary<string, List<string>>()
            };

            return Task.FromResult(query);
        }
    }
}
