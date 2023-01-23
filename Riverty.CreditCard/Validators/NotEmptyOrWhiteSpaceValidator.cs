namespace Riverty.CreditCard.Validators
{
    public class NotEmptyOrWhiteSpaceValidator : IValidator<string>
    {
        public string ErrorMessage => "String can not be empty";

        public string Field => throw new NotImplementedException();

        public bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
