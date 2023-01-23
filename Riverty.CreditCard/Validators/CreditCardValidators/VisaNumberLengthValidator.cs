using Riverty.CreditCard.Commands;

namespace Riverty.CreditCard.Validators.CreditCardValidators
{
    public class VisaNumberLengthValidator : ICreditCardCommandValidator
    {
        private readonly IntegerLengthValidator _13NumbersValidator;
        private readonly IntegerLengthValidator _16NumbersValidator;
        private readonly IntegerLengthValidator _19NumbersValidator;

        public VisaNumberLengthValidator()
        {
            _13NumbersValidator = new IntegerLengthValidator(13);
            _16NumbersValidator= new IntegerLengthValidator(16);
            _19NumbersValidator= new IntegerLengthValidator(19);
        }

        public string ErrorMessage => "Visa card number can only 13, 16 or 19 numbers length";

        public string Field => "CardNumber";

        public bool IsValid(ValidateCreditCardCommand value)
        {
            long cardNumber = value.CardNumber;

            return _13NumbersValidator.IsValid(cardNumber) || _16NumbersValidator.IsValid(cardNumber) || _19NumbersValidator.IsValid(cardNumber);
        }
    }
}
