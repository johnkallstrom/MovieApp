namespace MovieApp.Web.Enums
{
    public static class SortingMovieType
    {
        public const string TitleAscending = "original_title.asc";
        public const string TitleDescending = "original_title.desc";
        public const string PopularityAscending = "popularity.asc";
        public const string PopularityDescending = "popularity.desc";
        public const string ReleaseAscending = "primary_release_date.asc";
        public const string ReleaseDescending = "primary_release_date.desc";
        public const string RatingAscending = "vote_average.asc";
        public const string RatingDescending = "vote_average.desc";
    }
}
