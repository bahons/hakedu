using System.Collections.Generic;

namespace image.recognit.Models
{
    public class Search
    {
        public string Name { get; set; }
        public string Otvet { get; set; }
    }


    public class SearchResult
    {
        public IEnumerable<Search> Searches { get; set; }
        public string searchtext { get; set; }
    }
}
