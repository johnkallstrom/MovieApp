using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using System;

namespace MovieApp.Web.Components
{
    public partial class DisplayMedia
    {
		[Parameter]
		public Media Media { get; set; }

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
