using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreGraphics;

namespace MovieSearch.iOS.Views
{
    public class MovieCell : UITableViewCell
    {
        private readonly UILabel _titleLabel;
        private readonly UILabel _actorLabel;
        private readonly UIImageView _imageView;

        public MovieCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.Gray;

            this._imageView = new UIImageView();
            {
                Frame = new CGRect(5, 5, 33, 33);
            };

            this._titleLabel = new UILabel()
            {
                Frame = new CGRect(75, 15, this.ContentView.Bounds.Width , 20),
                Font = UIFont.FromName("Cochin-BoldItalic", 15f),
                TextColor = UIColor.FromRGB(127, 51, 0),
                BackgroundColor = UIColor.Clear
            };

            this._actorLabel = new UILabel()
            {
                Frame = new CGRect(75, 40, this.ContentView.Bounds.Width, 20),
                Font = UIFont.FromName("Cochin-BoldItalic", 12f),
                TextColor = UIColor.FromRGB(100, 51, 0),
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.Clear
            };

            

            this.ContentView.AddSubviews(new UIView[] { this._imageView, this._titleLabel, this._actorLabel });
            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        public void UpdateCell(Movie movie)
        {
            this._titleLabel.Text = $"{movie.Title} ({movie.Year:yyyy})";
            this._actorLabel.Text = movie.Actors[0] + ", " + movie.Actors[1] + ", " + movie.Actors[2];
            this._imageView.Image = UIImage.FromFile(movie.ImageUrl);
            
            //movie.Title + "(" + movie.Year + ")";

        }
    }
}