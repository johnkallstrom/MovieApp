using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using System;

namespace MovieApp.Web.Components.Search
{
    public partial class DisplaySearchMedia
    {
		[Parameter]
		public Media Media { get; set; }

		protected string GetImagePath()
		{
			string path = "";

			switch (Media.Media_Type)
			{
				case MediaType.Movie:
					path = Media.Poster_Path;
					break;
				case MediaType.TV:
					path = Media.Poster_Path;
					break;
				case MediaType.Person:
					path = Media.Profile_Path;
					break;
			}

			return path;
		}

		protected string DisplayFirstAirOrReleaseYear()
		{
			string year = "";

			if (!string.IsNullOrWhiteSpace(Media.Release_Date))
			{
				var date = DateTime.Parse(Media.Release_Date);

				year = date.Year.ToString();
			}

			if (!string.IsNullOrWhiteSpace(Media.First_Air_Date))
			{
				var date = DateTime.Parse(Media.First_Air_Date);

				year = date.Year.ToString();
			}

			return year;
		}
	}
}
