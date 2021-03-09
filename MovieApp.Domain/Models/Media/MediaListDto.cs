using System.Collections.Generic;

namespace MovieApp.Domain.Models
{
    public class MediaListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public UserSlimDto User { get; set; }
        public IEnumerable<MediaDto> Media { get; set; }
    }
}
