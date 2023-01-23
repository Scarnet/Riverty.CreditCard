using Riverty.CreditCard.Commands;

namespace Riverty.CreditCard.Validators.CreditCardValidators
{
    public class CvcValidator : ICreditCardCommandValidator
    {
        private readonly IntegerLengthValidator _integerLengthValidator;

        public CvcValidator()
        {
            _integerLengthValidator = new IntegerLengthValidator(3);
        }
        public string ErrorMessage => "CVC Should be 3 digits";

        public string Field => "CVC";

        public bool IsValid(ValidateCreditCardCommand value)
        {
            return _integerLengthValidator.IsValid(value.CVC);
        }
    }
}
