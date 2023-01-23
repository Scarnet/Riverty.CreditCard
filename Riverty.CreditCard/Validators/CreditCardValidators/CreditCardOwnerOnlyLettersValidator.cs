using Riverty.CreditCard.Commands;

namespace Riverty.CreditCard.Validators.CreditCardValidators
{
    public class CreditCardOwnerOnlyLettersValidator : ICreditCardCommandValidator
    {
        public string ErrorMessage => "Credit card owner name can contain letters only";

        public string Field => "CardOwner";

        public bool IsValid(ValidateCreditCardCommand value)
        {
            return new OnlyLettersValidator().IsValid(value.CardOwner);
        }
    }
}
