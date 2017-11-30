using DM.MovieApi;
using System;

namespace MovieSearch
{
    
    public class MovieDbSettings : IMovieDbSettings
    {

        string ApiKey = "1ec1f1033a55a5f869c57ae78169f79e";
        

        string ApiUrl = "http://api.themoviedb.org/3/";

        string IMovieDbSettings.ApiKey => ApiKey;

        string IMovieDbSettings.ApiUrl => ApiUrl;
    }
}
