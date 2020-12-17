using ComicBookGalleryModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookGalleryModel
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context()) 
            {
                var series1 = new Series { Title = "The Amazing SpiderMan"};
                context.ComicBooks.Add(new ComicBook
                {
                    Series = series1,
                    IssueNumber = 1,
                    PublishedOn = DateTime.Today
                });

                var series2 = new Series { Title = "The Amazing SpiderMan2" };
                context.ComicBooks.Add(new ComicBook
                {
                    Series = series2,
                    IssueNumber = 2,
                    PublishedOn = DateTime.Today
                });
                context.SaveChanges();

                var comicBooks = context.ComicBooks.ToList();

                foreach (ComicBook book in comicBooks)
                {
                    Console.WriteLine(book.DisplayText);
                }
            }

            Console.ReadLine();
        }
    }
}
