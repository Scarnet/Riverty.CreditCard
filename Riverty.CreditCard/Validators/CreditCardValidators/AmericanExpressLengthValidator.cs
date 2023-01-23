using Riverty.CreditCard.Commands;

namespace Riverty.CreditCard.Validators.CreditCardValidators
{
    public class AmericanExpressLengthValidator : ICreditCardCommandValidator
    {
        private readonly IntegerLengthValidator _integerLengthValidator;

        public AmericanExpressLengthValidator()
        {
            _integerLengthValidator = new IntegerLengthValidator(15);
        }
        public string ErrorMessage => "American Express card number can only 15 numbers length";

        public string Field => "CardNumber";

        public bool IsValid(ValidateCreditCardCommand value)
        {
            return _integerLengthValidator.IsValid(value.CardNumber);
        }
    }
}
