using FluentAssertions;

namespace Tests.Unit.DiscountCoupons
{
    public class DiscountPercentTests
    {
        [Fact]
        public void Fail_when_value_is_greater()
        {
            var percent = Domain.Aggregates.DiscountCoupons.ValueObjects.DiscountPercent.Create(110);

            percent.Errors.Should().HaveCount(1);
            percent.IsFailed.Should().Be(true);
            percent.IsSuccess.Should().Be(false);
            percent.Errors.Should().Contain(_ => _.Message == "Percent Less Than Or Greater Error");
        }

        [Fact]
        public void Fail_when_value_is_less()
        {
            var percent = Domain.Aggregates.DiscountCoupons.ValueObjects.DiscountPercent.Create(-10);

            percent.Errors.Should().HaveCount(1);
            percent.IsFailed.Should().Be(true);
            percent.IsSuccess.Should().Be(false);
            percent.Errors.Should().Contain(_ => _.Message == "Percent Less Than Or Greater Error");
        }

        [Fact]
        public void Create_properly()
        {
            var percent = Domain.Aggregates.DiscountCoupons.ValueObjects.DiscountPercent.Create(15);

            percent.Errors.Should().BeNullOrEmpty();
            percent.IsFailed.Should().Be(false);
            percent.IsSuccess.Should().Be(true);
            percent.Value.Value.Should().Be(15);
        }
    }
}