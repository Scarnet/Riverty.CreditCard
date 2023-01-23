using System.Text.RegularExpressions;

namespace Riverty.CreditCard.Validators
{
    public class OnlyLettersValidator : IValidator<string>
    {
        public string ErrorMessage => "String can contain only letters";
        public string Field => throw new NotImplementedException();

        public bool IsValid(string value)
        {
            return Regex.IsMatch(value, @"^[a-zA-Z]+(\s[a-zA-Z]+)?$");
        }
    }
}
