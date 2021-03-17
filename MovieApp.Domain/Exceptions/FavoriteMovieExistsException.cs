using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Domain.Exceptions
{
    public class FavoriteMovieExistsException : Exception
    {
        public FavoriteMovieExistsException()
        {
        }

        public FavoriteMovieExistsException(string message) : base(message)
        {
        }

        public FavoriteMovieExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
