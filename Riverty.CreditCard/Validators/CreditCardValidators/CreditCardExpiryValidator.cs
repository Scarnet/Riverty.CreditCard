using Riverty.CreditCard.Commands;

namespace Riverty.CreditCard.Validators.CreditCardValidators
{
    public class CreditCardExpiryValidator : ICreditCardCommandValidator
    {
        private readonly DateIsNotExpiredValidator _dateIsNotExpiredValidator;

        public CreditCardExpiryValidator()
        {
            _dateIsNotExpiredValidator= new DateIsNotExpiredValidator();
        }

        public string ErrorMessage { get; private set; } = "Credit card has expired";

        public string Field => "ExpiryMonth, ExpiryYear";

        public bool IsValid(ValidateCreditCardCommand value)
        {
            try
            {
                DateTime expirationDate = new DateTime(value.ExpiryYear, value.ExpiryMonth, 1);
                return _dateIsNotExpiredValidator.IsValid(expirationDate);
            }
            catch
            {
                ErrorMessage = "Could not parse month/year provided";
                return false;
            }
        }
    }
}
