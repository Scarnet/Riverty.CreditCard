namespace Riverty.CreditCard.Validators
{
    public interface IValidator<T>
    {
        string ErrorMessage { get; }
        string Field { get; }
        bool IsValid(T value);
    }
}
