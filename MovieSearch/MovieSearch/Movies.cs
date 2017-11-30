using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch
{
    public class Movies
    {
        private readonly List<Movie> _movies;

        public Movies()
        {
            this._movies = new List<Movie>();
        }

        public void AddMovie(List<Movie> movieList, string title, DateTime year)
        {
            var movie = new Movie()
            {
                Title = title,
                Year = year
            };
            movieList.Add(movie);

        }
    }
}
