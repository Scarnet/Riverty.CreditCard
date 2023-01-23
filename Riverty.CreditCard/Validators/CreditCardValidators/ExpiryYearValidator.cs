using Riverty.CreditCard.Commands;

namespace Riverty.CreditCard.Validators.CreditCardValidators
{
    public class ExpiryYearValidator : ICreditCardCommandValidator
    {
        private readonly IntegerLengthValidator _integerLengthValidator;

        public ExpiryYearValidator()
        {
            _integerLengthValidator = new IntegerLengthValidator(4);
        }

        public string ErrorMessage => "Credit card expiry year should be maximum of 4 digits";

        public string Field => "ExpiryYear";

        public bool IsValid(ValidateCreditCardCommand value)
        {
            return _integerLengthValidator.IsValid(value.ExpiryYear);
        }
    }
}
