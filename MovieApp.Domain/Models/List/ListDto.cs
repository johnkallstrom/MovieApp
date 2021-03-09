using System.Collections.Generic;

namespace MovieApp.Domain.Models.List
{
    public class ListDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public UserDto User { get; set; }
        public IEnumerable<MediaDto> Medias { get; set; }
    }
}
