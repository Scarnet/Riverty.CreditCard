using Riverty.CreditCard.Commands;

namespace Riverty.CreditCard.Validators.CreditCardValidators
{
    public class AmericanExpressCvcValidator : ICreditCardCommandValidator
    {
        private readonly IntegerLengthValidator _integerLengthValidator;

        public AmericanExpressCvcValidator()
        {
            _integerLengthValidator = new IntegerLengthValidator(4);
        }
        public string ErrorMessage => "American express card CVC value should be 4 digits";

        public string Field => "CVC";

        public bool IsValid(ValidateCreditCardCommand value)
        {
            return _integerLengthValidator.IsValid(value.CVC);
        }
    }
}
