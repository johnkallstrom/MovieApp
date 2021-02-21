using System;

namespace MovieApp.API.Exceptions
{
    public class EmailExistsException : Exception
    {
        public EmailExistsException()
        {
        }

        public EmailExistsException(string message) : base(message)
        {
        }

        public EmailExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
