using System;
using System.Collections.Generic;

namespace MovieApp.Domain.Models
{
    public class ListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public int UserId { get; set; }
        public IEnumerable<MovieDto> Movies { get; set; }
    }
}
