using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.DAL
{
    public class ComicBookArtistRepository
    {
        private Context _context;

        public ComicBookArtistRepository(Context context)
        {
            _context = context;
        }

        public void Add(ComicBookArtist comicBookArtist)
        {
            _context.ComicBookArtists.Add(comicBookArtist);
            _context.SaveChanges();
        }

        public ComicBookArtist Get(int id)
        {
            return _context.ComicBookArtists
                .Include(cba => cba.ComicBook.Series)
                .Include(cba => cba.Artist)
                .Include(cba => cba.Role)
                .Where(cba => cba.Id == id)
                .SingleOrDefault();
        }

        public void Delete(int? id)
        {
            var comicBookArtist = new ComicBookArtist { Id = id.Value };
            _context.Entry(comicBookArtist).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
