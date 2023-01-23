using Riverty.CreditCard.Commands;

namespace Riverty.CreditCard.Validators.CreditCardValidators
{
    public class MasterCardNumberValidator : ICreditCardCommandValidator
    {
        private readonly IntegerLengthValidator _integerLengthValidator;

        public MasterCardNumberValidator()
        {
            _integerLengthValidator = new IntegerLengthValidator(16);
        }
        public string ErrorMessage => "MasterCard card number can only 16 numbers length";

        public string Field => "CardNumber";

        public bool IsValid(ValidateCreditCardCommand value)
        {
            return _integerLengthValidator.IsValid(value.CardNumber);
        }
    }
}
