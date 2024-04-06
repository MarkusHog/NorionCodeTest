using Domain;
using Application;
using TollFeeCalculator;

namespace UnitTests
{
    public class TollCalculatorTest
    {
        private ITollCalculator _tollCalculator;
              
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
        public void Two_passes_during_weekday_then_returns_21()
        {
            var car = new Car();
            var fee = _tollCalculator.GetTollFee(car, DateShouldReturn21());
            var expectedFee = 21;
            Assert.That(fee, Is.EqualTo(expectedFee));
        }
        [Test]
        public void Three_passes_during_weekday_then_returns_29()
        {
            var car = new Car();
            var fee = _tollCalculator.GetTollFee(car, DateShouldReturn29());
            var expectedFee = 29;
            Assert.That(fee, Is.EqualTo(expectedFee));
        }

        [Test]
        public void Two_passes_during_weekday_within_60_min_then_returns_8()
        {
            var car = new Car();
            var fee = _tollCalculator.GetTollFee(car, TwoPassesInOneHour());
            var expectedFee = 8;
            Assert.That(fee, Is.EqualTo(expectedFee));
        }

        [Test]
        public void Three_passes_during_weekday_then_returns_8()
        {
            var car = new Car();
            var fee = _tollCalculator.GetTollFee(car, ThreePassesInOneHour());
            var expectedFee = 8;
            Assert.That(fee, Is.EqualTo(expectedFee));
        }

        [Test]
        public void Three_passes_with_bike_during_weekday_then_returns_0()
        {
            var motorBike = new Motorbike();
            var fee = _tollCalculator.GetTollFee(motorBike, ThreePasses());
            var expectedFee = 0;
            Assert.That(fee, Is.EqualTo(expectedFee));
        }

        [Test]
        public void Three_passes_then_add_first_and_last_to_totalfee()
        {
            var car = new Car();
            var fee = _tollCalculator.GetTollFee(car, ThreePasses());
            var expectedFee = 16;
            Assert.That(fee, Is.EqualTo(expectedFee));
        }

        [Test]
        public void Three_passes_on_weekend_then_return_0()
        {
            var car = new Car();
            var fee = _tollCalculator.GetTollFee(car, ThreeWeekendPasses());
            var expectedFee = 0;
            Assert.That(fee, Is.EqualTo(expectedFee));
        }

        [Test]
        public void Passes_on_holiday_then_return_0()
        {
            var car = new Car();
            var fee = _tollCalculator.GetTollFee(car, Holiday());
            var expectedFee = 0;
            Assert.That(fee, Is.EqualTo(expectedFee));
        }

        [Test]
        public void Six_passes_then_return_max()
        {
            var car = new Car();
            var fee = _tollCalculator.GetTollFee(car, SixPasses());
            var expectedFee = 60;
            Assert.That(fee, Is.EqualTo(expectedFee));
        }

        [Test]
        public void Passing_in_july_then_returns_0()
        {
            var car = new Car();
            var fee = _tollCalculator.GetTollFee(car, JulyPassing());
            var expectedFee = 0;
            Assert.That(fee, Is.EqualTo(expectedFee));
        }

        private static DateTime[] DateShouldReturn8()
        {
            return [new DateTime(2024, 4, 5, 11, 0, 0, 0)];
        }
        private static DateTime[] DateShouldReturn21()
        {
            return [new DateTime(2024, 4, 4, 08, 20, 0, 0), new DateTime(2024, 4, 4, 11, 0, 0, 0)];
        }
        private static DateTime[] DateShouldReturn29()
        {
            return [new DateTime(2024, 4, 4, 08, 20, 0, 0), new DateTime(2024, 4, 4, 11, 0, 0, 0), new DateTime(2024, 4, 5, 18, 0, 0, 0)];
        }
        private static DateTime[] TwoPassesInOneHour()
        {
            return [new DateTime(2024, 4, 5, 11, 01, 0, 0), new DateTime(2024, 4, 5, 11, 10, 0, 0)];
        }
        private static DateTime[] ThreePassesInOneHour()
        {
            return [new DateTime(2024, 4, 5, 11, 01, 0, 0), new DateTime(2024, 4, 5, 11, 10, 0, 0), new DateTime(2024, 4, 5, 11, 15, 0, 0)];
        }
        private static DateTime[] ThreePasses()
        {
            return [new DateTime(2024, 4, 5, 11, 01, 0, 0), new DateTime(2024, 4, 5, 11, 10, 0, 0), new DateTime(2024, 4, 5, 12, 09, 0, 0)];
        }

        private static DateTime[] ThreeWeekendPasses()
        {
            return [new DateTime(2024, 4, 6, 11, 01, 0, 0), new DateTime(2024, 4, 6, 11, 10, 0, 0), new DateTime(2024, 4, 6, 12, 09, 0, 0)];
        }

        private static DateTime[] Holiday()
        {
            return [new DateTime(2024, 6, 6, 11, 01, 0, 0), new DateTime(2024, 6, 6, 11, 10, 0, 0), new DateTime(2024, 6, 6, 12, 09, 0, 0)];
        }
        private static DateTime[] SixPasses()
        {
            return [new DateTime(2024, 4, 5, 06, 31, 0, 0), new DateTime(2024, 4, 5, 07, 40, 0, 0), new DateTime(2024, 4, 5, 08, 50, 0, 0), new DateTime(2024, 4, 5, 15, 33, 0, 0), new DateTime(2024, 4, 5, 16, 40, 0, 0), new DateTime(2024, 4, 5, 17, 55, 0, 0)];
        }

        private static DateTime[] JulyPassing()
        {
            return [new DateTime(2023, 7, 13, 11, 01, 0, 0), new DateTime(2023, 7, 13, 11, 10, 0, 0)];
        }
    }
}
