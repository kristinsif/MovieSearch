﻿using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch
{
    public class MovieServices
    {
        private IApiMovieRequest _movieApi;
        private int i = 0;
        private ApiSearchResponse<MovieInfo> _response;

        public MovieServices(IApiMovieRequest movieApi)
        {
            _movieApi = movieApi;
        }

        public async Task<List<Movie>> getListOfMoviesMatchingSearch(string nameField)
        {
            List<Movie> responseMovieList = new List<Movie>();           

            if (nameField.Length == 0)
            {
                return responseMovieList;
            }
            else
            {
                this._response = await _movieApi.SearchByTitleAsync(nameField);
                foreach (MovieInfo info in _response.Results)
                {
                    ApiQueryResponse<MovieCredit> cast = await _movieApi.GetCreditsAsync(info.Id);
                    List<string> actors = new List<string>();
                    
                    int number = 3;

                    if (cast.Item.CastMembers.Count < 3)
                    {
                        number = cast.Item.CastMembers.Count;
                    }
                    for (int i = 0; i < number; i++)
                    {                   
                        actors.Add(cast.Item.CastMembers[i].Name);
                       
                    }

                    responseMovieList.Add(new Movie() { Title = info.Title, Year = info.ReleaseDate, Actors = actors, ImageUrl = info.PosterPath});
                }
            }
            return responseMovieList;
        }

        public async Task<List<MovieDetail>> getListOfMovieDetails()
        {
            List<MovieDetail> movieDetailList = new List<MovieDetail>();
            string runTime = "";

            foreach (MovieInfo info in _response.Results)
            {
                var movie = await _movieApi.FindByIdAsync(info.Id);
                runTime = movie.Item.Runtime.ToString();
                movieDetailList.Add(new MovieDetail()
                {
                    Title = info.Title,
                    Overview = info.Overview,
                    Year = info.ReleaseDate,
                    Genre = info.Genres,
                    ImageUrl = info.PosterPath,
                    RunningTime = runTime
                });
            }           
            return movieDetailList;
        }

    } 
}
