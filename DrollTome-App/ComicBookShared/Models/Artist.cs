using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.Models
{
    public class Artist
    {
        public Artist()
        {
            ComicBooks = new List<ComicBookArtist>();
        }
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public ICollection<ComicBookArtist> ComicBooks { get; set; }
    }
}
