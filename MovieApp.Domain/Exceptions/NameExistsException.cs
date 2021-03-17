using System;

namespace MovieApp.Domain.Exceptions
{
    public class NameExistsException : Exception
    {
        public NameExistsException()
        {
        }

        public NameExistsException(string message) : base(message)
        {
        }

        public NameExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
