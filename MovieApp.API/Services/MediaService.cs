using Microsoft.EntityFrameworkCore;
using MovieApp.API.Data;
using MovieApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public class MediaService : IMediaService
    {
        private readonly MovieAppContext _context;

        public MediaService(
            MovieAppContext context)
        {
            _context = context;
        }

        public async Task CreateMediaListAsync(MediaList mediaList)
        {
            if (mediaList is null) throw new ArgumentNullException(nameof(mediaList));

            await _context.MediaLists.AddAsync(mediaList);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MediaList>> GetMediaListsAsync(int userId)
        {
            var mediaLists = await _context.MediaLists
                .Where(x => x.UserId == userId)
                .Include(x => x.Media)
                .Include(x => x.User)
                .ToListAsync();

            return mediaLists;
        }
    }
}
