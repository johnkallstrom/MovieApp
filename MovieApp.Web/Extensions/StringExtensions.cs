using System;
using System.Linq;

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

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string EnsureEndsWithDot(this string value)
        {
            if (!value.EndsWith(".")) return $"{value}...";
            return value;
        }
    }
}
