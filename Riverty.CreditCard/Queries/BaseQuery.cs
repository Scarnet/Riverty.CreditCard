namespace Riverty.CreditCard.Queries
{
    public abstract record BaseQuery
    {
        public bool IsSuccessful { get; init; }
        public Dictionary<string, List<string>> Errors { get; init; }
    }
}
