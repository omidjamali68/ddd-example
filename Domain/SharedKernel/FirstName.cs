using Domain.SeedWork;

namespace Domain.SharedKernel
{
    public class FirstName : ValueObject
    {
        public const byte MinLenght = 10;
        public const byte MaxLenght = 50;

        public string Value { get; }

        private FirstName(string name)
        {
            Value = name;
        }

        public static FluentResults.Result<FirstName> Create(string name)
        {
            var result = new FluentResults.Result<FirstName>();

            if (string.IsNullOrEmpty(name))
            {
                var error = "FirstName is null exception";
                result.WithError(error);
            }

            if (name.Length < MinLenght || name.Length > MaxLenght)
            {
                var error = "FirstName Lenght Error";
                result.WithError(error);
            }

            if (result.IsFailed)
                return result;

            result.WithValue(new FirstName(name));

            return result.Value;
        }
    }
}