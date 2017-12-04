using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using System.Collections.Generic;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi;

namespace MovieSearch.Droid
{
	[Activity (Label = "MovieSearch.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        //private MovieServices _movieService;
        private List<Movie> _movieList;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            var movieInputText = this.FindViewById<EditText>(Resource.Id.movieTextInputLabel);
            var getMovieButton = this.FindViewById<Button>(Resource.Id.getMovieButton);
            var displayMovieTextView = this.FindViewById<TextView>(Resource.Id.isplayMovieSearchLabel);

            MovieDbFactory.RegisterSettings(new MovieDbSettings());
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            var movieService = new MovieServices(movieApi);
            MovieDownload.StorageClient storageClient = new MovieDownload.StorageClient();
            MovieDownload.ImageDownloader imageDownloader = new MovieDownload.ImageDownloader(storageClient);

            getMovieButton.Click += async (object sender, EventArgs e) =>
            {
                this._movieList = await movieService.getListOfMoviesMatchingSearch(movieInputText.Text);
//                await imageDownloader.getLocalPath(this._movieList);

                //var answer = getListOfMoviesMatchingSearch(movieInputText.Text);
                var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
                manager.HideSoftInputFromWindow(movieInputText.WindowToken, 0);
                displayMovieTextView.Text = this._movieList[0].Title;
            };
        }
	}
}


