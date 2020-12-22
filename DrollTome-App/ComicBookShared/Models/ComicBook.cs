using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.Models
{
    public class ComicBook
    {
        public ComicBook()
        {
            Artists = new List<ComicBookArtist>();
        }
        public int Id { get; set; }

        [Display(Name ="Issue ID")]
        public int SeriesId { get; set; }

        [Display(Name = "Issue Number")]
        public int IssueNumber { get; set; }

        public string Description { get; set; }

        [Display(Name = "Published Date")]
        public DateTime PublishedOn { get; set; }

        [Display(Name = "Average Rating")]
        public decimal? AverageRating { get; set; }

        public Series Series { get; set; }

        public ICollection<ComicBookArtist> Artists { get; set; }

        public string DisplayText
        {
            get
            {
                return $"{Series?.Title} # {IssueNumber}";
            }
        }

        public void AddArtist(Artist artist, Role role)
        {
            Artists.Add(new ComicBookArtist
            {
                Artist = artist,
                Role = role
            });
        }
        /// <summary>
        /// Adds an artist to the comic book.
        /// </summary>
        /// <param name="artistId">The artist ID to add.</param>
        /// <param name="roleId">The role ID that the artist had on this comic book.</param>
        public void AddArtist(int artistId, int roleId)
        {
            Artists.Add(new ComicBookArtist()
            {
                ArtistId = artistId,
                RoleId = roleId
            });
        }

    }
}
