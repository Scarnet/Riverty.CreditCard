using Riverty.CreditCard.Enums;

namespace Riverty.CreditCard.BusinessRules
{
    /// <summary>
    /// Credit card business rule model.
    /// </summary>
    public record CreditCardBusinessRule
    {
        public CardType CardType { get; init; }
        public int[] CardNumberLengths { get; init; }
        public string[] CardNumberPrefixs { get; init; }
        public int CvcLength { get; init; }
    }
}
