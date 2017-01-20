using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessImplementation;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class SeoTests
    {
        private string correctStr;
        private string incorrectStr;

        [SetUp]       
        public void SetUp()
        {
            correctStr = "test-string-1";
            incorrectStr = "Test StRing @1";
        }

        [Test]
        [Category("SeoService")]
        public void Can_not_change_correct_string()
        {
            var str1 = SeoService.GetSeName(correctStr);
            str1.ShouldEqual(correctStr);
        }

        [Test]
        [Category("SeoService")]
        public void Can_change_incorrect_string_to_correct_string()
        {
            var str1 = SeoService.GetSeName(incorrectStr);
            str1.ShouldEqual(correctStr);
        }
    }
}
