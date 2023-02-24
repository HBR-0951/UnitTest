using System;
using NSubstitute;
using NUnit.Framework;
using SystemUnderTest;
using UnitTestCourse.Defaults;

namespace UnitTestCourse
{
    [TestFixture]
    public class TiPayRequestValidatorTests
    {
        private TiPayRequestValidator _tiPayRequestValidator;
        private IMerchantRepository _merchantRepository;
        private TiPayRequest _tiPayRequest;
        private TestDelegate _action;

        [SetUp]
        public void SetUp()
        {
            _merchantRepository = Substitute.For<IMerchantRepository>();
            _tiPayRequestValidator = new TiPayRequestValidator(_merchantRepository);
        }
        // 2. write unit tests for TiPayRequestValidator
        [Test]
        public void Validate_HashStringIsEqualToSignature_NotThrowException()
        {
            GivenMerchanKey("1234");
            
            GivenTiPayRequest(
                amount: 1000,
                merchantCode: "David",
                signature: "883152413201cb28ee2aa6c148d9204b"
            );

            WhenValidate();
            ShouldNotThrowException();
        }

        private void ShouldNotThrowException()
        {
            Assert.DoesNotThrow(_action);
        }

        [Test]
        public void Validate_HashStringIsEqualToSignature_ThrowException()
        {
            GivenMerchanKey("123");

            GivenTiPayRequest(
                amount: 1000,
                merchantCode: "David",
                signature: "883152413201cb28ee2aa6c148d9204b"
            );

            WhenValidate();
            ShouldThrowException();
        }

        private void ShouldThrowException()
        {
            Assert.Throws<Exception>(_action);
        }

        private void WhenValidate()
        {
            _action = () => _tiPayRequestValidator.Validate(_tiPayRequest);
        }

        private void GivenMerchanKey(string key)
        {
            _merchantRepository.GetMerchantKey(Arg.Any<string>()).Returns(key);
        }

        private void GivenTiPayRequest(int amount, string signature, string merchantCode)
        {
            _tiPayRequest = TiPayRequestDefault.DefaultTiPayRequest(x =>
            {
                x.Amount = amount;
                x.Signature = signature;
                x.MerchantCode = merchantCode;
            });
        }
    }
}