namespace Riverty.CreditCard.Validators
{
    public class IntegerLengthValidator : IValidator<long>
    {
        private readonly int _length;
        public string ErrorMessage => throw new NotImplementedException();

        public string Field => throw new NotImplementedException();

        public IntegerLengthValidator(int length) 
        {
            _length = length;
        }

        public bool IsValid(long value)
        {
            return value.ToString().Length == _length;
        }
    }
}
