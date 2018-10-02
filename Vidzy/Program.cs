
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
                .Where(c => c.Genre.Name.Equals("Drama"))
                .Where(c=> c.Classification == Classification.Gold)
                .OrderBy(c => c.ReleaseDate)
                .Select(c => new {VideoName = c.Name});

            foreach (var c in videos2)
            {
                Console.WriteLine("{0}", c.VideoName);
            }

            Console.WriteLine("-------------------------------------------");

            var videos3 = context.Videos
                .Select(c => new {MovieName = c.Name, Genre1 = c.Genre});

            foreach (var c in videos3)
            {
                Console.WriteLine("{0} - {1}", c.MovieName, c.Genre1);
            }
        }
    }
}
