using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookShared.DAL
{
    public class ArtistRepository : BaseRepository<Artist>
    {
        public ArtistRepository(Context context)
            : base(context)
        {

        }

        public override Artist Get(int id, bool includeRelatedEntities = true)
        {
            var artists = Context.Artists.AsQueryable();

            if (includeRelatedEntities)
            {
                artists = artists
                    .Include(cb => cb.ComicBooks.Select(c => c.ComicBook.Series))
                    .Include(cb => cb.ComicBooks.Select(c => c.Role));
            }
            return artists.Where(a => a.Id == id)
                .SingleOrDefault();
        }

        public override IList<Artist> GetList()
        {
            return Context.Artists
                .OrderBy(a => a.Name)
                .ToList();
        }

        public bool ArtistNameExists(int artistId, string name)
        {
            return Context.Artists
                .Any(a => a.Id != artistId && a.Name == name);
        }
    }
}
