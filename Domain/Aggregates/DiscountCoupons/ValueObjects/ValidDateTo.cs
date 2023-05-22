namespace Domain.Aggregates.DiscountCoupons.ValueObjects
{
    public class ValidDateTo
    {
        private ValidDateTo(DateTime validTo)
        {
            this.Value = validTo;
        }

        public static FluentResults.Result<ValidDateTo> Create(DateTime validTo)
        {
            var result = new FluentResults.Result<ValidDateTo>();

            if (validTo.Date < DateTime.UtcNow.Date)
            {
                var errorMessage = "ValidDateFrom Greater Than Now";
                result.WithError(errorMessage);
                return result;
            }

            result.WithValue(new ValidDateTo(validTo));

            return result.Value;
        }

        public DateTime Value { get; }
    }
}