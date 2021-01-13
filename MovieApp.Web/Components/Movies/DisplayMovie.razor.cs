﻿using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Movies
{
    public partial class DisplayMovie
    {
        [Parameter]
        public Movie Movie { get; set; }

        [Parameter]
        public bool ShowTitle { get; set; }
    }
}