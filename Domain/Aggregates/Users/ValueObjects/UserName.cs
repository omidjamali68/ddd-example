using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.SeedWork;

namespace Domain.Aggregates.Users.ValueObjects
{
    public class UserName : ValueObject
    {
        public const byte MinLenght = 8;
        public const byte MaxLenght = 20;
        public const string Regex = "^[a-zA-Z0-9_-]{8,20}$";

        public string Value { get; }

        private UserName(string value)
        {
            Value = value;
        }

        public static FluentResults.Result<UserName> Create(string userName)
        {
            var result = new FluentResults.Result<UserName>();

            if (string.IsNullOrEmpty(userName))
            {
                var error = "UserName Is Null Exception";
                result.WithError(error);
            }

            if (userName.Length < MinLenght || userName.Length > MaxLenght)
            {
                var error = "UserName Lenght Exception";
                result.WithError(error);
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(userName, Regex))
            {
                var error = "UserName Regex Exception";
                result.WithError(error);
            }

            if (result.IsFailed)
                return result;

            result.WithValue(new UserName(userName));

            return result.Value;
        }
    }
}