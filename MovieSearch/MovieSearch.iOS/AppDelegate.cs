using DM.MovieApi;
using DM.MovieApi.ApiRequest;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using Foundation;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace MovieSearch.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window {
			get;
			set;
		}
        TabController tabController;

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method
            this.Window = new UIWindow(UIScreen.MainScreen.Bounds);

            MovieDbFactory.RegisterSettings( new MovieDbSettings());
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            var movieService = new MovieServices(movieApi);
            MovieDownload.StorageClient storageClient = new MovieDownload.StorageClient();
            MovieDownload.ImageDownloader imageDownloader = new MovieDownload.ImageDownloader(storageClient);

            var controller = new MovieSearchViewController(movieService, imageDownloader);
            var movieSearchNavigationController = new UINavigationController(controller);

           // List<Movie> topRatedMovies = new List<Movie>();
          //  topRatedMovies = movieService.getTopRatedMovies();

            var movieListController = new MovieListController(new List<Movie>() { }, new List<MovieDetail>() { });
            var movieListNavigationController = new UINavigationController(movieListController);
            var tabController = new TabController()
            {
                ViewControllers = new UIViewController[] { movieSearchNavigationController, movieListNavigationController}
            };





            this.Window.RootViewController = tabController; 
                //new UINavigationController(controller);
            this.Window.MakeKeyAndVisible();

            return true;
		}

		public override void OnResignActivation (UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground (UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground (UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated (UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate (UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}

        
	}
}


