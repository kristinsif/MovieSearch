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
        private IApiMovieRequest movieApi;
        private object activityIndicatorViewStyle;
        private int i = 0;
        

        public MovieSearchViewController(IApiMovieRequest movieList)
        {
            movieApi = movieList;
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
                List<Movie> responseMovieList = new List<Movie>();
                MovieDownload.StorageClient sclient = new MovieDownload.StorageClient();
                MovieDownload.ImageDownloader idownl = new MovieDownload.ImageDownloader(sclient);

                List<string> mc = new List<string>();
                
                // responseMovieList.Add("");
                
                if (nameField.Text.Length == 0)
                {
                    this.NavigationController.PushViewController(new MovieListController(responseMovieList), true);
                    navigateButton.Enabled = true;
                    spinner.StopAnimating();
                }
                else
                {

                    ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(nameField.Text);
                   
                    foreach (MovieInfo info in response.Results)
                    {
                        List<string> actors = new List<string>();
                        string poster = info.PosterPath;
                        CancellationTokenSource cts = new CancellationTokenSource();
                        string localPath = idownl.LocalPathForFilename(poster);
                        await idownl.DownloadImage(poster, localPath, CancellationToken.None);
                                                                   
                            ApiQueryResponse<MovieCredit> cast = await movieApi.GetCreditsAsync(info.Id);

                            if (cast.Item.CastMembers.Count == 0)
                            {
                                actors.Add("haukuri");
                                actors.Add("haukuris");
                                actors.Add("haukuria");
                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                { 
                                    actors.Add(cast.Item.CastMembers[i].Name);                                 
                                }
                            }

                        responseMovieList.Add(new Movie() { Title = info.Title, Year = info.ReleaseDate, Actors = actors, ImageUrl = localPath});
                    }

                    this.NavigationController.PushViewController(new MovieListController(responseMovieList), true);
                    navigateButton.Enabled = true;
                    spinner.StopAnimating();
                }
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