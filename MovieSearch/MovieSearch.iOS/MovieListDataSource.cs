using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.iOS.Views;

namespace MovieSearch.iOS
{
    public class MovieListDataSource : UITableViewSource
    {
        private readonly List<Movie> _movieList;
        private readonly Action<int> _onSelectedMovie;

        public readonly NSString MovieListCellId = new NSString("MovieListCell");

        public MovieListDataSource(List<Movie> movieList, Action<int> onSelectedMovie)
        {
            this._movieList = movieList;
            this._onSelectedMovie = onSelectedMovie;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (MovieCell) tableView.DequeueReusableCell((NSString)this.MovieListCellId);
            if (cell == null)
            {
                cell = new MovieCell(this.MovieListCellId);
            }

            cell.UpdateCell(this._movieList[indexPath.Row]);
                     
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._movieList.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            this._onSelectedMovie(indexPath.Row);
        }


    }
}