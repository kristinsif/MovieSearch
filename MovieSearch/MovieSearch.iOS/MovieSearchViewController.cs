using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using System.Threading;

namespace MovieSearch.iOS
{
    public class MovieSearchViewController : UIViewController
    {
        private const double StartX = 20;
        private const double StartY = 80;
        private const double Height = 50;
        private List<Movie> _movieList;
        private List<MovieDetail> _movieDetailList;
        private MovieServices _movieService;
        private MovieDownload.ImageDownloader _imageDownloader;
        private int i = 0;
        

        public MovieSearchViewController(MovieServices movieService, MovieDownload.ImageDownloader imageDownloader)
        {
            _movieService = movieService;
            _imageDownloader = imageDownloader;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.White;
            this.Title = "Movie search";
            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("Movie search", UIBarButtonItemStyle.Plain, null);

            var promptLabel = PromptLabel();
            var nameField = UiTextField();
            var spinner = ActivityIndicatorView();
      
            var navigateButton = NavigationButton(nameField, spinner);
            this.View.AddSubviews(new UIView[] { promptLabel, nameField, navigateButton, spinner});
        }

        private UIButton NavigationButton(UITextField nameField, UIActivityIndicatorView spinner)
        {
            var navigateButton = UIButton.FromType(UIButtonType.RoundedRect);
            navigateButton.Frame = new CGRect(StartX, StartY + 2 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            navigateButton.SetTitle("Get Movies", UIControlState.Normal);

            navigateButton.TouchUpInside += async (sender, args) =>
            {
                navigateButton.Enabled = false;
                spinner.StartAnimating();

                nameField.ResignFirstResponder();
                
                
                this._movieList = await _movieService.getListOfMoviesMatchingSearch(nameField.Text);
                await _imageDownloader.getLocalPath(this._movieList);
                this._movieDetailList = await _movieService.getListOfMovieDetails();
                await _imageDownloader.getImageUrl(this._movieDetailList);

                this.NavigationController.PushViewController(new MovieListController(_movieList, _movieDetailList), true);
                navigateButton.Enabled = true;
                spinner.StopAnimating();                                                
                
            };
            return navigateButton;
        }

        private UITextField UiTextField()
        {
            var nameField = new UITextField()
            {
                Frame = new CGRect(StartX, StartY + Height, this.View.Bounds.Width - 2 * StartX, Height),
                BorderStyle = UITextBorderStyle.RoundedRect,
                Text = "" 
            };
            return nameField;
        }

        private UILabel PromptLabel()
        {
            var promptLabel = new UILabel()
            {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
                Text = "Enter words in a movie title: "
            };
            return promptLabel;
        }

        private UIActivityIndicatorView ActivityIndicatorView()
        {
            var spinner = new UIActivityIndicatorView()
            {
                Frame = new CGRect(StartX, StartY + 3 * Height, this.View.Bounds.Width - 2 * StartX, Height),
                Color = UIColor.Black
            };
            
            return spinner;
        }
    }
}