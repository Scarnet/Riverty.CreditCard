using Riverty.CreditCard.Enums;

namespace Riverty.CreditCard.Queries
{
    public record CardTypeQuery : BaseQuery
    {
        public string? CardType { get; init; }
    }
}
