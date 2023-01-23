namespace Riverty.CreditCard.Validators
{
    public class DateIsNotExpiredValidator : IValidator<DateTime>
    {
        public string ErrorMessage => "Date is expired";

        public string Field => throw new NotImplementedException();

        public bool IsValid(DateTime value)
        {
            return value.ToUniversalTime() > DateTime.UtcNow;
        }
    }
}
