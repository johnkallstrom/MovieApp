using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string value)
        {
            switch (value)
            {
                case null: throw new ArgumentNullException(nameof(value));
                case "": throw new ArgumentNullException(nameof(value));
                default: return $"{value.First().ToString().ToUpper()}{value.Substring(1)}";
            }
        }
    }
}
