using Domain.SeedWork;

namespace Domain.Aggregates.DiscountCoupons.ValueObjects
{
    public class DiscountPercent : ValueObject
    {
        private const byte Minimum = 0;
        private const byte Maximum = 100;
        private DiscountPercent()
        {
        }

        private DiscountPercent(int percent)
        {
            this.Value = percent;
        }

        public static FluentResults.Result<DiscountPercent> Create(int percent)
        {
            var result = new FluentResults.Result<DiscountPercent>();

            if (percent < Minimum || percent > Maximum)
                result.WithError("Percent Less Than Or Greater Error");

            if (result.IsFailed)
                return result;

            result.WithValue(new DiscountPercent(percent));

            return result.Value;
        }

        public int Value { get; }
    }
}