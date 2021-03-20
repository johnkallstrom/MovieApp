using System.Collections.Generic;

namespace MovieApp.Domain.Models
{
    public class UpdateMovieListRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<MovieListItemDto> Items { get; set; } = new List<MovieListItemDto>();
    }
}
