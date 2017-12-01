using System;
using CoreGraphics;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieDetailController : UIViewController
    {

        private readonly MovieDetail _movieDetail;
        private const double StartX = 20;
        private const double StartY = 20;


        public MovieDetailController(MovieDetail movieDetail)
        {
            this._movieDetail = movieDetail;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Movie Info";
            this.View.BackgroundColor = UIColor.White;

            var titleLabel = TitleLabel();
            var imageView = ImageView();
            var genreLabel = GenreLabel();
            var descriptionLabel = DescriptionLabel();

            this.View.AddSubviews(new UIView[] { descriptionLabel, titleLabel, genreLabel, imageView });

        }



        private UILabel TitleLabel()
        {
            var titleLabel = new UILabel()
            {
                Frame = new CGRect(StartX, 80, this.View.Bounds.Width - StartX, 20),
                Text = $"{_movieDetail.Title} ({_movieDetail.Year:yyyy})",
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 0
            };
            return titleLabel;
        }



        private UILabel GenreLabel()
        {
            string genres = "";
            for (int i = 0; i < _movieDetail.Genre.Count; i++)
            {
                if (i == _movieDetail.Genre.Count - 1)
                {
                    genres += _movieDetail.Genre[i].Name;
                }
                else
                {
                    genres += _movieDetail.Genre[i].Name + ", ";

                }
            }
            var titleLabel = new UILabel()
            {
                Frame = new CGRect(StartX, 100, this.View.Bounds.Width - StartX, 20),
                Text = $"{genres} | {_movieDetail.RunningTime} min",
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 0,
                Font = UIFont.FromName("Verdana-Italic", 12f),


            };
            return titleLabel;
        }

        private UILabel DescriptionLabel()
        {
            var descrtiptionLabel = new UILabel()
            {
                Frame = new CGRect(StartX, 320, this.View.Bounds.Width - StartX * 2, 200),
                Text = _movieDetail.Overview,
                Lines = 0,
                Font = UIFont.FromName("Verdana-Italic", 12f)

            };
            return descrtiptionLabel;
        }


        private UIImageView ImageView()
        {
            var imageView = new UIImageView()
            {
                Frame = new CGRect(StartX, 150, this.View.Bounds.Width / 2 - StartX * 2, this.View.Bounds.Width / 2 - StartX * 2),
                Image = UIImage.FromFile(_movieDetail.ImageUrl)
            };
            return imageView;
        }



    }
}
