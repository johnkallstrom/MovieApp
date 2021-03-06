﻿using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
        public IEnumerable<int> Genre_Ids { get; set; } = new List<int>();
        public string Original_Language { get; set; }
        public string Original_Title { get; set; }
        public string Overview { get; set; }
        public decimal Popularity { get; set; }
        public string Poster_Path { get; set; }
        public string Backdrop_Path { get; set; }
        public string Release_Date { get; set; }
        public decimal Vote_Average { get; set; }
        public int Vote_Count { get; set; }
        public int Order { get; set; }
    }
}
