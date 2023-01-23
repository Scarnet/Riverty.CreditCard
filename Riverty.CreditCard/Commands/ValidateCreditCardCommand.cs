namespace Riverty.CreditCard.Commands
{
    /// <summary>
    /// Command model for credit card validation
    /// </summary>
    public record ValidateCreditCardCommand : BaseCommand
    {
        public string CardOwner { get; set; }

        public long CardNumber { get; set; }

        public int CVC { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }
    }
}
