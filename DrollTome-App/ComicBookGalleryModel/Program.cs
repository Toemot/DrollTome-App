using ComicBookGalleryModel.ConsoleHelpers;
using ComicBookGalleryModel.DAL;
using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookGalleryModel
{
    class Program
    {
        const string CommandListComicBooks = "l";
        const string CommandListComicBook = "i";
        const string CommandListComicBookProperties = "p";
        const string CommandAddComicBook = "a";
        const string CommandUpdateComicBook = "u";
        const string CommandDeleteComicBook = "d";
        const string CommandSave = "s";
        const string CommandCancel = "c";
        const string CommandQuit = "q";

        readonly static IList<string> EditableProperties = new List<string>
        {
            "SeriesId",
            "IssueNumber",
            "Description",
            "PublishedOn",
            "AverageRating"
        };

        static void Main(string[] args)
        {
            string command = CommandListComicBooks;
            IList<int> comicBookIds = null;

            while (command != CommandQuit)
            {
                switch (command)
                {
                    case CommandListComicBooks:
                        comicBookIds = ListComicBooks();
                        break;
                    case CommandAddComicBook:
                        AddComicBook();
                        command = CommandListComicBooks;
                        continue;
                    default:
                        if (AttemptDisplayComicBook(command, comicBookIds))
                        {
                            command = CommandListComicBooks;
                            continue;
                        }
                        else
                        {
                            Helper.Output("This is an error");
                        }
                        break;
                }
                Helper.OutputBlankLine();
                Helper.Output("Commands: ");
                int comicBookCount = Repository.GetComicBookCount();
                if (comicBookCount > 0)
                {
                    Helper.Output("1 - {0}: ", comicBookCount);
                }
                Helper.Output("Add- A, Quit- Q", false);
                Helper.OutputBlankLine();
                command = Helper.ReadInput("Enter a command: ", true);
            }
            //Console.ReadLine();
        }

        private static int GetSeriesId()
        {
            int? seriesId = null;
            IList<Series> series = Repository.GetSeries();

            while (seriesId == null)
            {
                Helper.OutputBlankLine();

                foreach (Series s in series)
                {
                    Helper.OutputLine("{0} {1} ", series.IndexOf(s) + 1, s.Title);
                }

                string lineNumberInput = Helper.ReadInput("Enter a Series num: ");
                int lineNumber = 0;
                if (int.TryParse(lineNumberInput, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber <= series.Count)
                    {
                        seriesId = series[lineNumber - 1].Id;
                    }
                }
                if (seriesId == null)
                {
                    Helper.OutputLine("Error processing your request");
                }
            }
            return seriesId.Value;
        }

        private static int GetArtistId()
        {
            int? artistId = null;
            IList<Artist> artists = Repository.GetArtists();

            while (artistId == null)
            {
                Helper.OutputBlankLine();

                foreach (Artist a in artists)
                {
                    Helper.OutputLine("{0} {1} ", artists.IndexOf(a) + 1, a.Name);
                }

                string lineInputNumber = Helper.ReadInput("Enter an artist num: ");
                int lineNumber = 0;

                if (int.TryParse(lineInputNumber, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber <= artists.Count)
                    {
                        artistId = artists[lineNumber - 1].Id;
                    }
                }
                if (artistId == null)
                {
                    Helper.OutputLine("Error in communcation");
                }
            }
            return artistId.Value;
        }

        private static int GetRoleId()
        {
            int? roleId = null;
            IList<Role> roles = Repository.GetRoles();

            while (roleId == null)
            {
                Helper.OutputBlankLine();

                foreach (Role r in roles)
                {
                    Helper.OutputLine("{0} {1} ", roles.IndexOf(r) + 1, r.Name);
                }

                string lineInpurNumber = Helper.ReadInput("Enter the role number: ");
                int lineNumber = 0;

                if (int.TryParse(lineInpurNumber, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber <= roles.Count)
                    {
                        roleId = roles[lineNumber - 1].Id;
                    }
                }
                if (roleId == null)
                {
                    Helper.OutputLine("Error inn communication");
                }
            }
            return roleId.Value;
        }

        private static int GetIssueNumber()
        {
            int issueNumber = 0;

            while (issueNumber <= 0)
            {
                string lineInputNumber = Helper.ReadInput("Enter an issue number: ");

                int.TryParse(lineInputNumber, out issueNumber);

                if (issueNumber <= 0)
                {
                    Helper.OutputLine("This is an error");
                }
            }
            return issueNumber;
        }

        private static string GetDescription()
        {
            return Helper.ReadInput("Enter a description: ");
        }

        private static DateTime GetPublishedDate()
        {
            DateTime publishedDate = DateTime.MinValue;

            while (publishedDate == DateTime.MinValue)
            {
                string dateTimeInput = Helper.ReadInput("Enter a date line num: ");

                DateTime.TryParse(dateTimeInput, out publishedDate);

                if (publishedDate == DateTime.MinValue)
                {
                    Helper.Output("Enter a correct date");
                }
            }
            return publishedDate;
        }

        private static decimal GetAverageRating()
        {
            decimal? averageRating = null;
            var promptUser = true;

            while (promptUser)
            {
                string averageRatingValue = Helper.ReadInput("Enter an average rating: ");

                if (!string.IsNullOrWhiteSpace(averageRatingValue))
                {
                    decimal rating = 0;
                    if (decimal.TryParse(averageRatingValue, out rating))
                    {
                        averageRating = rating;
                        promptUser = false;
                    } 
                    else
                    {
                        Helper.Output("Erronious average rating");
                    }
                }
                else
                {
                    promptUser = false;
                }
            }
            return averageRating.Value;
        }

        private static IList<int> ListComicBooks()
        {
            IList<int> comicBookIds = new List<int>();
            IList<ComicBook> comicBooks = Repository.GetComicBooks();

            Helper.ClearOutput();
            Helper.OutputLine("COMIC BOOKS");
            Helper.OutputBlankLine();

            foreach (ComicBook comicBook in comicBooks)
            {
                comicBookIds.Add(comicBook.Id);
                Helper.OutputLine("{0} {1}: ", comicBooks.IndexOf(comicBook) + 1,
                    comicBook.DisplayText);
            }
            return comicBookIds;
        }

        private static bool DeleteComicBook(int comicBookId)
        {
            var successful = false;

            string input = Helper.ReadInput("Are you sure that you want to delete Y/N: ");

            if (input == "y")
            {
                Repository.DeleteComicBook(comicBookId);
                successful = true;
            }
            return successful;
        }

        private static void ListComicBook(int comicBookId)
        {
            ComicBook comicBook = Repository.GetComicBook(comicBookId);

            Helper.ClearOutput();
            Helper.OutputBlankLine();
            Helper.OutputLine("COMIC BOOK DETAILS");
            Helper.OutputLine(comicBook.DisplayText);

            if (!string.IsNullOrWhiteSpace(comicBook.Description))
            {
                Helper.OutputLine(comicBook.Description);
            }
            Helper.OutputBlankLine();
            Helper.OutputLine("Published On: {0} ", comicBook.PublishedOn.ToShortDateString());
            Helper.OutputLine("Average Rating: {0} ",
                comicBook.AverageRating != null ? 
                comicBook.AverageRating.Value.ToString("N2") : "N/A");

            Helper.OutputLine("Artist");
            foreach (ComicBookArtist cba in comicBook.Artists)
            {
                Helper.OutputLine("{0} {1} ", cba.Artist.Name, cba.Role.Name);
            }
        }

        private static bool AttemptUpdateComicBookProperty
            (string command, ComicBook comicBook)
        {
            var successful = false;
            int lineNumber = 0;

            int.TryParse(command, out lineNumber);
            
                if (lineNumber > 0 && lineNumber <= EditableProperties.Count)
                {
                    string propertyName = EditableProperties[lineNumber - 1];

                    switch (propertyName)
                    {
                        case "SeriesId":
                            comicBook.SeriesId = GetSeriesId();
                            comicBook.Series = Repository.GetSeries(comicBook.SeriesId);
                            break;
                        case "IssueNumber":
                            comicBook.IssueNumber = GetIssueNumber();
                            break;
                        case "Description":
                            comicBook.Description = GetDescription();
                            break;
                        case "PublishedOn":
                            comicBook.PublishedOn = GetPublishedDate();
                            break;
                        case "AverageRating":
                            comicBook.AverageRating = GetAverageRating();
                            break;
                        default:
                            break;
                    }
                    successful = true;
                }
            
            return successful;
        }

        private static void ListComicBookProperties(ComicBook comicBook)
        {
            Helper.ClearOutput(); 
            Helper.OutputBlankLine();
            
            Helper.OutputLine("UPDATE COMIC BOOK");
            Helper.OutputBlankLine();
            Helper.OutputLine("1) Series: {0} ", comicBook.Series.Title);
            Helper.OutputLine("2) Issue Number: {0} ", comicBook.IssueNumber);
            Helper.OutputLine("3) Description: {0} ", comicBook.Description);
            Helper.OutputLine("4) Published Date: {0} ", comicBook.PublishedOn);
            Helper.OutputLine("5) Average Rating: {0} ", comicBook.AverageRating);
        }

        private static void UpdateComicBook(int comicBookId)
        {
            ComicBook comicBook = Repository.GetComicBook(comicBookId);
            string command = CommandListComicBookProperties;

            while (command != CommandCancel)
            {
                switch (command)
                {
                    case CommandListComicBookProperties:
                        ListComicBookProperties(comicBook);
                        break;
                    case CommandSave:
                        Repository.UpdateComicBook(comicBook);
                        command = CommandCancel;
                        continue;
                    default:
                        if (AttemptUpdateComicBookProperty(command, comicBook))
                        {
                            command = CommandListComicBookProperties;
                            continue;
                        }
                        else
                        {
                            Helper.Output("Invalid details entered");
                        }
                        break;
                }
                Helper.OutputBlankLine();
                Helper.Output("Commands: ");
                if (EditableProperties.Count > 0)
                {
                    Helper.Output("Enter a Num 1-{0}, ", EditableProperties.Count);
                }
                Helper.Output("S- save, C- Cancel", false);
                command = Helper.ReadInput("Enter a command: ", true);
            }
            Helper.ClearOutput();
        }

        private static void DisplayComicBook(int comicBookId)
        {
            string command = CommandListComicBook;

            while (command != CommandCancel)
            {
                switch (command)
                {
                    case CommandListComicBook:
                        ListComicBook(comicBookId);
                        break;
                    case CommandUpdateComicBook:
                        UpdateComicBook(comicBookId);
                        command = CommandListComicBook;
                        continue;
                    case CommandDeleteComicBook:
                        if (DeleteComicBook(comicBookId))
                        {
                            command = CommandCancel;
                        }
                        else
                        {
                            command = CommandListComicBook;
                        }
                        continue;
                    default:
                        Helper.Output("This is an error");
                        break;
                }
                Helper.OutputBlankLine();
                Helper.Output("Commands: ");
                Helper.OutputLine("Update - U, Delete - D, Cancel - C", false);
                command = Helper.ReadInput("Enter the next command: ", true);
            }
        }

        private static void AddComicBook()
        {
            Helper.ClearOutput();
            Helper.Output("ADD COMIC BOOK");

            var comicBook = new ComicBook();
            comicBook.SeriesId = GetSeriesId();
            comicBook.IssueNumber = GetIssueNumber();
            comicBook.Description = GetDescription();
            comicBook.PublishedOn = GetPublishedDate();
            comicBook.AverageRating = GetAverageRating();

            var comicBookArtist = new ComicBookArtist();
            comicBookArtist.ArtistId = GetArtistId();
            comicBookArtist.RoleId = GetRoleId();
            comicBook.Artists.Add(comicBookArtist);

            Repository.AddComicBook(comicBook);
        }

        private static bool AttemptDisplayComicBook
            (string command, IList<int> comicBookIds)
        {
            var successful = false;
            int? comicBookId = null;

            if (comicBookIds != null)
            {
                int lineNumber;
                int.TryParse(command, out lineNumber);
                if (lineNumber > 0 && lineNumber <= comicBookIds.Count)
                {
                    comicBookId = comicBookIds[lineNumber - 1];
                    successful = true;
                }
                
            }
            if (comicBookId != null)
            {
                DisplayComicBook(comicBookId.Value);
            }
            return successful;
        }
    }
}
