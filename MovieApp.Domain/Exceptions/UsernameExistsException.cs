using System;

namespace MovieApp.Domain.Exceptions
{
    public class UsernameExistsException : Exception
    {
        public UsernameExistsException()
        {
        }

        public UsernameExistsException(string message) : base(message)
        {
        }

        public UsernameExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
