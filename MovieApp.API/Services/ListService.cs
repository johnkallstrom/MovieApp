using Microsoft.EntityFrameworkCore;
using MovieApp.API.Data;
using MovieApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public class ListService : IListService
    {
        private readonly MovieAppContext _context;

        public ListService(
            MovieAppContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(List list)
        {
            if (list is null) throw new ArgumentNullException(nameof(list));

            await _context.Lists.AddAsync(list);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<List>> GetListsByUserAsync(int userId)
        {
            return await _context.Lists
                .Where(x => x.UserId == userId)
                .Include(x => x.Movies)
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<List>> GetAllListsAsync()
        {
            return await _context.Lists
                .Include(x => x.Movies)
                .Include(x => x.User)
                .ToListAsync();
        }
    }
}
