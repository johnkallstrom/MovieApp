using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class Configuration
    {
        public Image Images { get; set; }

        public IEnumerable<string> Change_Keys { get; set; }
    }
}
