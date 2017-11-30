using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieListController : UITableViewController
    {
        private readonly List<Movie> _movieList;

        public MovieListController(List<Movie> movieList)
        {
            this._movieList = movieList;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Movie List";


            this.TableView.Source = new MovieListDataSource(this._movieList);
        }
    }
}