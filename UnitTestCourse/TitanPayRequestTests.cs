using System;
using NSubstitute;
using NUnit.Framework;
using SystemUnderTest;

namespace UnitTestCourse
{
    [TestFixture]
    public class TitanPayRequestTests
    {
        private IMerchantRepository _merchantRepository;
        private TitanPayRequest _titanPayRequest;
        private ITxtMerchantKeyReader _txtMerchantKeyReader;
        private IDateTimeProvider _dateTimeProvider;
        private string _signature;

        [SetUp]
        public void SetUp()
        {
            _txtMerchantKeyReader = Substitute.For<ITxtMerchantKeyReader>();
            _dateTimeProvider = Substitute.For<IDateTimeProvider>();
            _titanPayRequest = new TitanPayRequest(_txtMerchantKeyReader, _dateTimeProvider);
            _titanPayRequest.Amount = 100;
        }
        // 3. write tests for TitanPayRequest.Sign()
        [Test]
        public void Validate_HashStringIsEqualToSignature()
        {
            WhenSign();
            ShouldBe("fd98262a120ec2f1c7612f7fa0a5cb29");
        }

        private void WhenSign()
        {
            _titanPayRequest.Sign();
        }


        // 4. write unit test for Sign2
        [Test]
        public void Validate_InputStringIsEqualToSignature()
        {
            GetMerchantKey("David");
            WhenSign2();
           ShouldBe("ba9fcde1412306b38226ad3a7aa69f4f");
           
        }

        private void WhenSign2()
        {
            _titanPayRequest.Sign2();
        }


        // 5. write unit test for Sign3
        [Test]
        public void Validate_InputDateToimeIsEqualToSignature()
        {
            GetMerchantKey(new DateTime(2023, 2, 23, 18, 44, 23));
            WhenSign3();
            ShouldBe("7e59bff89a0d00b47c1b1672ac0c8783");
        }

        private void WhenSign3()
        {
            _titanPayRequest.Sign3();
        }

        private void GetMerchantKey(string key)
        {
            _txtMerchantKeyReader.Get().Returns(key);
        }
        private void ShouldBe(string actual)
        {
            _signature = _titanPayRequest.Signature;
            Assert.AreEqual(_signature, actual);
        }

        private void GetMerchantKey(DateTime datetime)
        {
            _dateTimeProvider.Now().Returns(datetime);
        }
    }
    
    
}