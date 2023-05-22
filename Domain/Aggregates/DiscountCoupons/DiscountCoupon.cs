using Domain.Aggregates.DiscountCoupons.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.DiscountCoupons
{
    public class DiscountCoupon : Entity
    {
        public DiscountPercent Percent { get; private set; }
        public ValidDateFrom ValidDateFrom { get; private set; }
        public ValidDateTo ValidDateTo { get; private set; }        

        private DiscountCoupon(
            DiscountPercent percent,
            ValidDateFrom validDateFrom,
            ValidDateTo validDateTo)
        {
            Percent = percent;
            ValidDateFrom = validDateFrom;
            ValidDateTo = validDateTo;
        }

        public static FluentResults.Result<DiscountCoupon> Create(
            int percent,
            DateTime validDateFrom,
            DateTime validDateTo)
        {
            var result = new FluentResults.Result<DiscountCoupon>();

            var percentResult = DiscountPercent.Create(percent);

            result.WithErrors(percentResult.Errors);

            var validDateFromResult = ValidDateFrom.Create(validDateFrom);

            result.WithErrors(validDateFromResult.Errors);

            var validDateToResult = ValidDateTo.Create(validDateTo);

            result.WithErrors(validDateToResult.Errors);

            if (validDateFromResult.Value.Value.Date > validDateToResult.Value.Value.Date)
            {
                var error = "ValidDateFromGreaterThanValidDateToException";
                result.WithError(error);
            }

            if (result.IsFailed)
                return result;

            result.WithValue(new DiscountCoupon(
                percentResult.Value,
                validDateFromResult.Value,
                validDateToResult.Value));

            return result.Value;
        }

        public FluentResults.Result Update(
            int percent,
            DateTime validDateFrom,
            DateTime validDateTo)
        {
            var result = Create(percent, validDateFrom, validDateTo);

            if (result.IsFailed)
                return result.ToResult();

            Percent = result.Value.Percent;
            ValidDateFrom = result.Value.ValidDateFrom;
            ValidDateTo = result.Value.ValidDateTo;

            return result.ToResult();
        }

    }
}