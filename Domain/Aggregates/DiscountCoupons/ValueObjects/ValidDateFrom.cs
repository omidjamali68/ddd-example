using Domain.SeedWork;

namespace Domain.Aggregates.DiscountCoupons.ValueObjects
{
    public class ValidDateFrom : ValueObject
    {
        private ValidDateFrom(DateTime pValue)
        {
            this.Value = pValue;
        }

        public static FluentResults.Result<ValidDateFrom> Create(DateTime value)
        {
            var result = new FluentResults.Result<ValidDateFrom>();
            if (value.Date < DateTime.UtcNow.Date)
            {
                result.WithError("ValidDateFrom Greater Than Now");
            }

            if (result.IsFailed)
                return result;

            result.WithValue(new ValidDateFrom(value));

            return result.Value;
        }

        public DateTime Value { get; }
    }
}