using Domain.SeedWork;

namespace Domain.SharedKernel
{
    public class LastName : ValueObject
    {
        public const byte MinLenght = 10;
        public const byte MaxLenght = 50;

        public string Value { get; }

        private LastName(string name)
        {
            Value = name;
        }

        public static FluentResults.Result<LastName> Create(string name)
        {
            var result = new FluentResults.Result<LastName>();

            if (string.IsNullOrEmpty(name))
            {
                var error = "LastName is null exception";
                result.WithError(error);
            }

            if (name.Length < MinLenght || name.Length > MaxLenght)
            {
                var error = "LastName Lenght Error";
                result.WithError(error);
            }

            if (result.IsFailed)
                return result;

            result.WithValue(new LastName(name));

            return result.Value;
        }
    }
}