using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Domain.Exceptions
{
    public class MovieExistsException : Exception
    {
        public MovieExistsException()
        {
        }

        public MovieExistsException(string message) : base(message)
        {
        }

        public MovieExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
