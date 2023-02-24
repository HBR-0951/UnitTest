using NUnit.Framework;
using SystemUnderTest;

namespace UnitTestCourse
{
    [TestFixture]
    public class Md5HelperTests
    {
        private Md5Helper _md5Helper;

        private string _input;
        private object _actual;

        // 1. write a test for Md5Helper
        [SetUp]
        public void SetUp()
        {
            _md5Helper = new Md5Helper();
            
        }
        
        // online md5 hash generator: https://www.md5hashgenerator.com/
        [Test]
        public void Hash_InputString_ReturnsMd5HashedString()
        {
            GivenInputString("David");
            WhenDoMd5Hash();
            StringShouldBe("464e07afc9e46359fb480839150595c5");
        }

        private void StringShouldBe(string expected)
        {
            Assert.AreEqual(expected, _actual);
        }

        private void WhenDoMd5Hash()
        {
            _actual = _md5Helper.Hash(_input);
        }


        private void GivenInputString(string input)
        {
            _input = input;
        }
    }
}