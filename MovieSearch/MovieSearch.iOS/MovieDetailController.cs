using System;
using CoreGraphics;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieDetailController : UIViewController
    {

        private readonly MovieDetail _movieDetail;


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
                Frame = new CGRect(20, 50, this.View.Bounds.Width - 2, 50),
                Text = $"{_movieDetail.Title} ({_movieDetail.Year:yyyy})"
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
                Frame = new CGRect(20, 100, this.View.Bounds.Width - 2, 50),
                Text = genres,

            };
            return titleLabel;
        }

        private UILabel DescriptionLabel()
        {
            var descrtiptionLabel = new UILabel()
            {
                Frame = new CGRect(20, 150, this.View.Bounds.Width - 20, 200),
                Text = _movieDetail.Overview,
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 0
            };
            return descrtiptionLabel;
        }


        private UIImageView ImageView()
        {
            var imageView = new UIImageView()
            {
                Frame = new CGRect(20, 350, 20, 20),
                Image = UIImage.FromFile(_movieDetail.ImageUrl)
            };
            return imageView;
        }



    }
}
