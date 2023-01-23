using AutoFixture;
using AutoFixture.Xunit;
using Riverty.CreditCard.Commands;
using Riverty.CreditCard.Enums;
using Riverty.CreditCard.Services;

namespace Riverty.CreditCard.Tests
{
    /// <summary>
    /// This class is made to test the functionality of the CreditCardValidationService service.
    /// Test cases picked using the boundary value analysis technique
    /// </summary>
    public class CreditCardValidationTests
    {
        private readonly IFixture _fixture;
        private readonly CreditCardValidationService _validationService;
        public CreditCardValidationTests() 
        {
            _fixture = new Fixture();
            _validationService = new CreditCardValidationService(new CardTypeDetector());
        }

        [Theory(DisplayName = "Validation service should return visa when valid visa card data is inserted")]
        [InlineData(4456535678976)]
        [InlineData(4564356789876543)]
        [InlineData(4567890987654323456)]
        public async Task ValidationService_ShouldReturnVisa_WhenValidVisaCardDataIsInserted(long cardNumber)
        {
            #region Arrange
            string cardOwner = "Kareem Said";
            int cvc = 123;
            int expiryMonth = 2;
            int expiryYear = DateTime.Now.AddYears(2).Year;

            var command = _fixture.Build<ValidateCreditCardCommand>()
                .With(c => c.CardNumber, cardNumber)
                .With(c => c.CardOwner, cardOwner)
                .With(c => c.CVC, cvc)
                .With(c => c.ExpiryMonth, expiryMonth)
                .With(c => c.ExpiryYear, expiryYear)
                .Create();
            #endregion

            #region Act
            var query = await _validationService.Execute(command);
            #endregion

            #region Assert
            Assert.True(query.IsSuccessful);

            Assert.Empty(query.Errors);

            Assert.Equal(Enum.GetName(typeof(CardType), CardType.Visa), query.CardType);
            #endregion
        }

        [Theory(DisplayName = "Vaidation service should return master card when valid master card data is inserted")]
        [InlineData(5143567898765432)]
        [InlineData(5243567898765432)]
        [InlineData(5443567898765432)]
        [InlineData(5543567898765432)]
        [InlineData(5343567898765432)]
        [InlineData(2221567898765432)]
        [InlineData(2222567898765432)]
        [InlineData(2245567898765432)]
        [InlineData(2269567898765432)]
        [InlineData(2270567898765432)]
        public async Task ValidationService_ShouldReturnMasterCard_WhenMasterCardDataIsInserted(long cardNumber)
        {
            #region Arrange
            string cardOwner = "Kareem Said";

            int cvc = 123;
            int expiryMonth = 2;
            int expiryYear = DateTime.Now.AddYears(2).Year;

            var command = _fixture.Build<ValidateCreditCardCommand>()
                .With(c => c.CardNumber, cardNumber)
                .With(c => c.CardOwner, cardOwner)
                .With(c => c.CVC, cvc)
                .With(c => c.ExpiryMonth, expiryMonth)
                .With(c => c.ExpiryYear, expiryYear)
                .Create();
            #endregion

            #region Act
            var query = await _validationService.Execute(command);
            #endregion

            #region Assert
            Assert.True(query.IsSuccessful);

            Assert.Empty(query.Errors);

            Assert.Equal(Enum.GetName(typeof(CardType), CardType.MasterCard), query.CardType);
            #endregion
        }

        [Theory(DisplayName = "Vaidation service should return american express when valid american express data is inserted")]
        [InlineData(347056789876543)]
        [InlineData(377056789876543)]
        public async Task ValidationService_ShouldReturnMasterCard_WhenValidAmericanExpressCardDataIsInserted(long cardNumber)
        {
            #region Assert
            string cardOwner = "Kareem Said";

            int cvc = 1234;
            int expiryMonth = 2;
            int expiryYear = DateTime.Now.AddYears(2).Year;

            var command = _fixture.Build<ValidateCreditCardCommand>()
                .With(c => c.CardNumber, cardNumber)
                .With(c => c.CardOwner, cardOwner)
                .With(c => c.CVC, cvc)
                .With(c => c.ExpiryMonth, expiryMonth)
                .With(c => c.ExpiryYear, expiryYear)
                .Create();
            #endregion

            #region Act
            var query = await _validationService.Execute(command);
            #endregion

            #region Assert
            Assert.True(query.IsSuccessful);

            Assert.Empty(query.Errors);

            Assert.Equal(Enum.GetName(typeof(CardType), CardType.AmericanExpress), query.CardType);
            #endregion
        }

        [Fact]
        public async Task ValidationService_ShouldReturnAllErrors_WhenAllCardDataAreInValid()
        {
            #region Assert
            string cardOwner = "Kareem Said1";

            int cvc = 123;
            int expiryMonth = 13;
            int expiryYear = 777;

            var command = _fixture.Build<ValidateCreditCardCommand>()
                .With(c => c.CardNumber, 347056789876543)
                .With(c => c.CardOwner, cardOwner)
                .With(c => c.CVC, cvc)
                .With(c => c.ExpiryMonth, expiryMonth)
                .With(c => c.ExpiryYear, expiryYear)
                .Create();
            #endregion

            #region Act
            var query = await _validationService.Execute(command);
            #endregion

            #region Assert
            Assert.False(query.IsSuccessful);
            Assert.NotEmpty(query.Errors);
            Assert.Equal(4, query.Errors.Count);
            #endregion
        }

        [Fact(DisplayName = "Validation service should return error when card name is empty")]
        public async Task ValidationService_ShouldReturnError_WhenCardOwnerNameIsEmpty()
        {
            #region Assert
            string cardOwner = string.Empty;
            int cvc = 123;
            int expiryMonth = 2;
            int expiryYear = DateTime.Now.AddYears(2).Year;
            long cardNumber = 4456535678976;

            var command = _fixture.Build<ValidateCreditCardCommand>()
                .With(c => c.CardNumber, cardNumber)
                .With(c => c.CardOwner, cardOwner)
                .With(c => c.CVC, cvc)
                .With(c => c.ExpiryMonth, expiryMonth)
                .With(c => c.ExpiryYear, expiryYear)
                .Create();
            #endregion

            #region Act
            var query = await _validationService.Execute(command);
            #endregion

            #region Assert
            Assert.False(query.IsSuccessful);
            Assert.NotEmpty(query.Errors);
            Assert.True(query.Errors.ContainsKey("CardOwner"));
            Assert.Contains("Card owner name can not be empty", query.Errors["CardOwner"]);
            #endregion

        }

        [Theory(DisplayName = "Validation service should return error when card name does not contain only letters")]
        [InlineData("Kareem1")]
        [InlineData("Kareem@")]
        public async Task ValidationService_ShouldReturnError_WhenCardOwnerDoesNotContainOnlyLetters(string cardOwner)
        {
            #region Assert
            int cvc = 123;
            int expiryMonth = 2;
            int expiryYear = DateTime.Now.AddYears(2).Year;
            long cardNumber = 4456535678976;

            var command = _fixture.Build<ValidateCreditCardCommand>()
                .With(c => c.CardNumber, cardNumber)
                .With(c => c.CardOwner, cardOwner)
                .With(c => c.CVC, cvc)
                .With(c => c.ExpiryMonth, expiryMonth)
                .With(c => c.ExpiryYear, expiryYear)
                .Create();
            #endregion

            #region Act
            var query = await _validationService.Execute(command);
            #endregion

            #region Assert
            Assert.False(query.IsSuccessful);
            Assert.NotEmpty(query.Errors);
            Assert.True(query.Errors.ContainsKey("CardOwner"));
            Assert.Contains("Credit card owner name can contain letters only", query.Errors["CardOwner"]);
            #endregion
        }

        [Theory(DisplayName = "Validation service should return error when expiry month is not a valid month")]
        [InlineData(0)]
        [InlineData(13)]
        [InlineData(123)]
        public async Task ValidationService_ShouldReturnError_WhenExpiryMonthIsNotValidMonth(int expiryMonth)
        {
            #region Assert
            string cardOwner = "Kareem Mohamed";
            int cvc = 123;
            int expiryYear = DateTime.Now.AddYears(2).Year;
            long cardNumber = 4456535678976;

            var command = _fixture.Build<ValidateCreditCardCommand>()
                .With(c => c.CardNumber, cardNumber)
                .With(c => c.CardOwner, cardOwner)
                .With(c => c.CVC, cvc)
                .With(c => c.ExpiryMonth, expiryMonth)
                .With(c => c.ExpiryYear, expiryYear)
                .Create();
            #endregion

            #region Act
            var query = await _validationService.Execute(command);
            #endregion

            #region Assert
            Assert.False(query.IsSuccessful);
            Assert.NotEmpty(query.Errors);
            Assert.True(query.Errors.ContainsKey("ExpiryMonth"));
            Assert.Contains("Expiry month should be between 1 and 12", query.Errors["ExpiryMonth"]);
            #endregion
        }

        [Fact(DisplayName = "Validation service should return error when expiry year is not a valid year")]
        public async Task ValidationService_ShouldReturnError_WhenExpiryYearIsNotValidYear()
        {
            #region Assert
            string cardOwner = "Kareem Mohamed";
            int expiryMonth = 2;
            int expiryYear = 12345;
            int cvc = 123;
            long cardNumber = 4456535678976;

            var command = _fixture.Build<ValidateCreditCardCommand>()
                .With(c => c.CardNumber, cardNumber)
                .With(c => c.CardOwner, cardOwner)
                .With(c => c.CVC, cvc)
                .With(c => c.ExpiryMonth, expiryMonth)
                .With(c => c.ExpiryYear, expiryYear)
                .Create();
            #endregion

            #region Act
            var query = await _validationService.Execute(command);
            #endregion

            #region Assert
            Assert.False(query.IsSuccessful);
            Assert.NotEmpty(query.Errors);
            Assert.True(query.Errors.ContainsKey("ExpiryYear"));
            Assert.Contains("Credit card expiry year should be maximum of 4 digits", query.Errors["ExpiryYear"]);
            #endregion
        }

        [Fact(DisplayName = "Validation service should return error when card has expired")]
        public async Task ValidationService_ShouldReturnError_WhenCardHasExpired()
        {
            #region Assert
            string cardOwner = "Kareem Mohamed";
            int expiryMonth = 2;
            int expiryYear = DateTime.Now.AddYears(-2).Year;
            int cvc = 123;
            long cardNumber = 4456535678976;

            var command = _fixture.Build<ValidateCreditCardCommand>()
                .With(c => c.CardNumber, cardNumber)
                .With(c => c.CardOwner, cardOwner)
                .With(c => c.CVC, cvc)
                .With(c => c.ExpiryMonth, expiryMonth)
                .With(c => c.ExpiryYear, expiryYear)
                .Create();
            #endregion

            #region Act
            var query = await _validationService.Execute(command);
            #endregion

            #region Assert
            Assert.False(query.IsSuccessful);
            Assert.NotEmpty(query.Errors);
            Assert.True(query.Errors.ContainsKey("ExpiryMonth, ExpiryYear"));
            Assert.Contains("Credit card has expired", query.Errors["ExpiryMonth, ExpiryYear"]);
            #endregion
        }

        [Theory(DisplayName = "Validation service should return unknown when card number length Is Not Valid")]
        [InlineData(445653567897, 123)]
        [InlineData(44565356789711, 123)]
        [InlineData(445653567897111, 123)]
        [InlineData(44565356789711111, 123)]
        [InlineData(51435678987654322, 123)]
        [InlineData(514356789876543, 123)]
        [InlineData(3470567898765433, 1234)]
        [InlineData(34705678987654, 1234)]
        public async Task ValidationService_ShouldReturnError_WhenCardNumberLengthIsNotValid(long cardNumber, int cvc)
        {
            #region Assert
            string cardOwner = "Kareem Said";
            int expiryMonth = 2;
            int expiryYear = DateTime.Now.AddYears(2).Year;

            var command = _fixture.Build<ValidateCreditCardCommand>()
                .With(c => c.CardNumber, cardNumber)
                .With(c => c.CardOwner, cardOwner)
                .With(c => c.CVC, cvc)
                .With(c => c.ExpiryMonth, expiryMonth)
                .With(c => c.ExpiryYear, expiryYear)
                .Create();
            #endregion

            #region Act
            var query = await _validationService.Execute(command);
            #endregion

            #region Assert
            Assert.False(query.IsSuccessful);
            Assert.NotEmpty(query.Errors);
            Assert.Equal(Enum.GetName(typeof(CardType), CardType.Unknown), query.CardType);
            #endregion
        }

        [Theory(DisplayName = "Validation service should return unknown when card prefix Is Not Valid")]
        [InlineData(3456535678976, 123)]
        [InlineData(5456535678976, 123)]
        [InlineData(5043567898765432, 123)]
        [InlineData(5643567898765432, 123)]
        [InlineData(2220567898765432, 123)]
        [InlineData(2721567898765432, 123)]
        [InlineData(337056789876543 , 1234)]
        [InlineData(357056789876543, 1234)]
        [InlineData(367056789876543, 1234)]
        [InlineData(368056789876543, 1234)]
        public async Task ValidationService_ShouldReturnError_WhenCardPrefixIsNotValid(long cardNumber, int cvc)
        {
            #region Assert
            string cardOwner = "Kareem Said";
            int expiryMonth = 2;
            int expiryYear = DateTime.Now.AddYears(2).Year;

            var command = _fixture.Build<ValidateCreditCardCommand>()
                .With(c => c.CardNumber, cardNumber)
                .With(c => c.CardOwner, cardOwner)
                .With(c => c.CVC, cvc)
                .With(c => c.ExpiryMonth, expiryMonth)
                .With(c => c.ExpiryYear, expiryYear)
                .Create();
            #endregion

            #region Act
            var query = await _validationService.Execute(command);
            #endregion

            #region Assert
            Assert.False(query.IsSuccessful);
            Assert.NotEmpty(query.Errors);
            Assert.Equal(Enum.GetName(typeof(CardType), CardType.Unknown), query.CardType);
            #endregion
        }

        [Theory(DisplayName = "Validation service should return unknown when card CVC Is Not Valid")]
        [InlineData(4456535678976, 1234)]
        [InlineData(4456535678976, 12)]
        [InlineData(5143567898765432, 1234)]
        [InlineData(5143567898765432, 12)]
        [InlineData(347056789876543, 12345)]
        [InlineData(347056789876543, 123)]
        public async Task ValidationService_ShouldReturnError_WhenCardCvcIsNotValid(long cardNumber, int cvc)
        {
            #region Assert
            string cardOwner = "Kareem Said";
            int expiryMonth = 2;
            int expiryYear = DateTime.Now.AddYears(2).Year;

            var command = _fixture.Build<ValidateCreditCardCommand>()
                .With(c => c.CardNumber, cardNumber)
                .With(c => c.CardOwner, cardOwner)
                .With(c => c.CVC, cvc)
                .With(c => c.ExpiryMonth, expiryMonth)
                .With(c => c.ExpiryYear, expiryYear)
                .Create();
            #endregion

            #region Act
            var query = await _validationService.Execute(command);
            #endregion

            #region Assert
            Assert.False(query.IsSuccessful);
            Assert.NotEmpty(query.Errors);
            Assert.Equal(Enum.GetName(typeof(CardType), CardType.Unknown), query.CardType);
            #endregion
        }

    }
}