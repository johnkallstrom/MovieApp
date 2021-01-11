﻿using MovieApp.Web.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IPeopleService
    {
        Task<PersonDetails> GetPersonAsync(int personId);
    }
}
