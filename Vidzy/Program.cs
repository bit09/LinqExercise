
using System;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;

namespace Vidzy
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new VidzyContext();

            var videos = context.Videos
                .Where(c => c.Genre.Name.Equals("Action"))
                .OrderBy(c => c.Name)
                .Select(c => new {VideoName = c.Name});

            foreach (var c in videos)
            {
                Console.WriteLine("{0}", c.VideoName);
            }

            Console.WriteLine("-------------------------------------------");

            var videos2 = context.Videos
                .Where(c => c.Genre.Name.Equals("Drama") && c.Classification == Classification.Gold)
                .OrderByDescending(c => c.ReleaseDate)
                .Select(c => new {VideoName = c.Name});

            foreach (var c in videos2)
            {
                Console.WriteLine("{0}", c.VideoName);
            }

            Console.WriteLine("-------------------------------------------");

            var videos3 = context.Videos
                .Select(c => new {MovieName = c.Name, Genre = c.Genre.Name});

            foreach (var c in videos3)
            {
                Console.WriteLine("{0} - {1}", c.MovieName, c.Genre);
            }

            Console.WriteLine("-------------------------------------------");

            var videos4 = context.Videos
                .GroupBy(v => v.Classification)
                .Select(g => new
                {
                    Classification = g.Key.ToString(),
                    Videos = g.OrderBy(v => v.Name)
                });

            foreach (var g in videos4)
            {
                Console.WriteLine("Classification: " + g.Classification);

                foreach (var v in g.Videos)
                    Console.WriteLine("\t" + v.Name);
            }

            Console.WriteLine("-------------------------------------------");

            var videos5 = context.Videos
                .GroupBy(v => v.Classification)
                .Select(g => new
                {
                    Name = g.Key.ToString(),
                    VideosCount = g.Count()
                })
                .OrderBy(c => c.Name);

            foreach (var c in videos5)
                Console.WriteLine("{0} ({1})", c.Name, c.VideosCount);

            Console.WriteLine("-------------------------------------------");

            var videos6 = context.Genres
                .GroupJoin(context.Videos, g => g.Id, v => v.GenreId, (genre, videos1) => new
                {
                    Name = genre.Name,
                    VideosCount = videos1.Count()
                })
                .OrderByDescending(g => g.VideosCount);

            foreach (var g in videos6)
                Console.WriteLine("{0} ({1})", g.Name, g.VideosCount);
        }
    }
}
