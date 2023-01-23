using Riverty.CreditCard.BusinessRules;
using Riverty.CreditCard.Enums;

namespace Riverty.CreditCard
{
    public class CardTypeDetector
    {
        private CreditCardBusinessRule[] _creditCardTypeBusinessRules;

        public CardTypeDetector()
        {
            var masterCardRanges = Enumerable.Range(51, 5).ToList();
            masterCardRanges.AddRange(Enumerable.Range(2221, 500));

            _creditCardTypeBusinessRules = new CreditCardBusinessRule[]
            {
                new CreditCardBusinessRule()
                {
                    CardType = CardType.Visa,
                    CardNumberLengths = new int[] {13, 16, 19},
                    CardNumberPrefixs= new string[] {"4"},
                    CvcLength = 3
                },
                new CreditCardBusinessRule()
                {
                    CardType = CardType.MasterCard,
                    CardNumberLengths = new int[] {16},
                    CardNumberPrefixs = masterCardRanges.Select(num => num.ToString()).ToArray(),
                    CvcLength = 3
                },
                new CreditCardBusinessRule()
                {
                    CardType = CardType.AmericanExpress,
                    CardNumberLengths= new int[] {15},
                    CardNumberPrefixs = new string[] { "34", "37" },
                    CvcLength = 4
                }
            };
        }

        public CardType DetectCardType(long cardNumber, int cvc)
        {
            string sCardNumber = cardNumber.ToString();
            int cardNumberLength = sCardNumber.Length;
            string cardPrefix = sCardNumber.Substring(0, 4);
            int cvcLength = cvc.ToString().Length;

            var cardRule = _creditCardTypeBusinessRules
                        .FirstOrDefault(rule => rule.CardNumberLengths.Contains(cardNumberLength)
                        && rule.CardNumberPrefixs.Any(prefix => cardPrefix.StartsWith(prefix))
                        && rule.CvcLength == cvcLength);

            return cardRule?.CardType ?? CardType.Unknown;
        }
    }
}
