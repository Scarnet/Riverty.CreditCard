using Riverty.CreditCard.Commands;

namespace Riverty.CreditCard.Validators.CreditCardValidators
{
    public class ExpiryMonthValidator : ICreditCardCommandValidator
    {
        private readonly IntegerRangeValidator _integerRangeValidator;

        public ExpiryMonthValidator()
        {
            _integerRangeValidator = new IntegerRangeValidator(1, 12);
        }
        public string ErrorMessage => "Expiry month should be between 1 and 12";

        public string Field => "ExpiryMonth";

        public bool IsValid(ValidateCreditCardCommand value)
        {
            return _integerRangeValidator.IsValid(value.ExpiryMonth);
        }
    }
}
