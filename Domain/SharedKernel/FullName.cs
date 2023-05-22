using Domain.SeedWork;

namespace Domain.SharedKernel
{
    public class FullName : ValueObject
    {
        public FirstName FirstName { get; }
        public LastName LastName { get; }

        private FullName(FirstName firstName, LastName lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static FluentResults.Result<FullName> Create(string firstName, string lastName)
        {
            var result = new FluentResults.Result<FullName>();

            var firstNameResult = FirstName.Create(firstName);
            result.WithErrors(firstNameResult.Errors);

            var lastNameResult = LastName.Create(lastName);
            result.WithErrors(lastNameResult.Errors);

            if (result.IsFailed)
                return result;

            result.WithValue(new FullName(firstNameResult.Value, lastNameResult.Value));

            return result.Value;
        }
    }
}