using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.iOS.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieListController : UITableViewController
    {
        private readonly List<Movie> _movieList;
        private readonly List<MovieDetail> _movieDetail;
        MovieDetail detailMovie = new MovieDetail();

        public MovieListController(List<Movie> movieList, List<MovieDetail> movieDetail)
        {
            this._movieList = movieList;
            this._movieDetail = movieDetail;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Movie List";

            this.TableView.Source = new MovieListDataSource(this._movieList, this._OnSelectedMovie);

        }

        private void _OnSelectedMovie(int row)
        {
            detailMovie = new MovieDetail()
            {
                Title = _movieDetail[row].Title,
                ImageUrl = _movieDetail[row].ImageUrl,
                Genre = _movieDetail[row].Genre,
                RunningTime = _movieDetail[row].RunningTime,
                Year = _movieDetail[row].Year,
                Overview = _movieDetail[row].Overview
            };
            this.NavigationController.PushViewController(new MovieDetailController(detailMovie), true);
        }
    }
}