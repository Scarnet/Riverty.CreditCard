namespace Riverty.CreditCard.Validators
{
    public class IntegerRangeValidator : IValidator<int>
    {
        private int _from;
        private int _to;
        public string ErrorMessage => $"Integer should be between {_from} and {_to}";

        public string Field => throw new NotImplementedException();

        public IntegerRangeValidator(int from, int to)
        {
            _from = from;
            _to = to;
        }

        public bool IsValid(int value)
        {
            return _from <= value && _to >= value;
        }
    }
}
