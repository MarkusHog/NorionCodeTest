using TollFeeCalculator;

namespace UnitTests
{
    public class TollCalculatorTest
    {
        private TollCalculator _tollCalculator;
              
        [SetUp]
        public void Setup()
        {
            _tollCalculator = new TollCalculator();
        }

        [Test]
        public void One_pass_during_weekday_at_eleven_then_returns_eight()
        {
            var car = new Car();
            var fee = _tollCalculator.GetTollFee(car, DateShouldReturn8());
            var expectedFee = 8;
            Assert.That(fee, Is.EqualTo(expectedFee));
        }

        [Test]
        public void Two_passes_during_weekday_at_eleven_then_returns_21()
        {
            var car = new Car();
            var fee = _tollCalculator.GetTollFee(car, DateShouldReturn21());
            var expectedFee = 21;
            Assert.That(fee, Is.EqualTo(expectedFee));
        }
        private static DateTime[] DateShouldReturn8()
        {
            return [new DateTime(2024, 4, 5, 11, 0, 0, 0)];
        }
        private static DateTime[] DateShouldReturn21()
        {
            return [new DateTime(2024, 4, 4, 08, 30, 0, 0), new DateTime(2024, 4, 5, 11, 0, 0, 0)];
        }
    }
}
