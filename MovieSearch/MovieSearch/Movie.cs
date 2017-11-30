using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch
{
    public class Movie
    {
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public List<string> Actors { get; set; }
        public string ImageUrl { get; set; }
     //   public string localImagePath { get; set; }

    }
}