using Domain.Aggregates.Users.ValueObjects;
using Domain.SeedWork;
using Domain.SharedKernel;

namespace Domain.Aggregates.Users
{
    public class User : Entity
    {
        public FullName FullName { get; private set; }
        public UserName UserName { get; private set; }

        private User(FullName fullName, UserName userName)
        {
            FullName = fullName;
            UserName = userName;
        }

        public static FluentResults.Result<User> Create(
            string firstName, string lastName, string userName)
        {
            var result = new FluentResults.Result<User>();

            var fullNameResult = FullName.Create(firstName, lastName);
            result.WithErrors(fullNameResult.Errors);

            var userNameResult = UserName.Create(userName);
            result.WithErrors(userNameResult.Errors);

            if (result.IsFailed)
                return result;

            result.WithValue(new User(fullNameResult.Value, userNameResult.Value));

            return result.Value;
        }
    }
}