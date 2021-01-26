using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Original_Name { get; set; }
        public string Media_Type { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string Release_Date { get; set; }
        public string Original_Title { get; set; }
        public string Original_Language { get; set; }
        public string First_Air_Date { get; set; }
        public string Backdrop_Path { get; set; }
        public string Poster_Path { get; set; }
        public string Profile_Path { get; set; }
        public decimal Popularity { get; set; }
        public int Vote_Count { get; set; }
        public decimal Vote_Average { get; set; }
        public IEnumerable<int> Genre_Ids { get; set; }

        public string DisplayNameOrTitle()
        {
            string output = "";

            if (!string.IsNullOrWhiteSpace(Title))
            {
                output = Title;

                return output;
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                output = Name;

                return output;
            }

            return output;
        }
    }
}
