using FluentAssertions;

namespace Tests.Unit.DiscountCoupons
{
    public class DiscountCouponTests
    {
        [Fact]
        public void Create_properly()
        {
            var fromDate = DateTime.Now.AddDays(1);
            var toDate = DateTime.Now.AddDays(5);

            var coupon = Domain.Aggregates.DiscountCoupons.DiscountCoupon.Create(10, fromDate, toDate);

            coupon.IsFailed.Should().BeFalse();
            coupon.IsSuccess.Should().BeTrue();
            coupon.Errors.Should().BeNullOrEmpty();
            coupon.Value.Percent.Value.Should().Be(10);
            coupon.Value.ValidDateFrom.Value.Date.Should().Be(fromDate.Date);
            coupon.Value.ValidDateTo.Value.Date.Should().Be(toDate.Date);
        }

        [Fact]
        public void Fail_when_validDateFrom_greater_than_validDateTo()
        {
            var fromDate = DateTime.Now.AddDays(5);
            var toDate = DateTime.Now.AddDays(1);

            var coupon = Domain.Aggregates.DiscountCoupons.DiscountCoupon.Create(10, fromDate, toDate);

            coupon.Errors.Should().HaveCount(1);
            coupon.IsFailed.Should().BeTrue();
            coupon.IsSuccess.Should().BeFalse();
            coupon.Errors.Should().Contain(_ =>
                _.Message == "ValidDateFromGreaterThanValidDateToException");
        }

        [Fact]
        public void Update_properly()
        {
            var fromDate = DateTime.Now.AddDays(1);
            var toDate = DateTime.Now.AddDays(5);
            var coupon = Domain.Aggregates.DiscountCoupons.DiscountCoupon.Create(10, fromDate, toDate);
            var newFromDate = DateTime.Now.AddDays(2);
            var newToDate = DateTime.Now.AddDays(3);

            coupon.Value.Update(50, newFromDate, newToDate);

            coupon.IsFailed.Should().BeFalse();
            coupon.IsSuccess.Should().BeTrue();
            coupon.Errors.Should().BeNullOrEmpty();
            coupon.Value.Percent.Value.Should().Be(50);
            coupon.Value.ValidDateFrom.Value.Date.Should().Be(newFromDate.Date);
            coupon.Value.ValidDateTo.Value.Date.Should().Be(newToDate.Date);

        }
    }
}