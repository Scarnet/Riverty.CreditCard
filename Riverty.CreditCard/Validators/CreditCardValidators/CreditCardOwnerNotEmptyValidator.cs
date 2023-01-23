using Riverty.CreditCard.Commands;

namespace Riverty.CreditCard.Validators.CreditCardValidators
{
    public class CreditCardOwnerNotEmptyValidator : ICreditCardCommandValidator
    {
        public string ErrorMessage => "Card owner name can not be empty";

        public string Field => "CardOwner";

        public bool IsValid(ValidateCreditCardCommand value)
        {
           return new NotEmptyOrWhiteSpaceValidator().IsValid(value.CardOwner);
        }
    }
}
