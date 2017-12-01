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
                Frame = new CGRect(5, 5, 5, 5);
            };

            this._titleLabel = new UILabel()
            {
                Frame = new CGRect(75, 5, this.ContentView.Bounds.Width - 80, 20),
                Font = UIFont.FromName("ArialMT", 15f),
                TextColor = UIColor.FromRGB(127, 51, 0),
                BackgroundColor = UIColor.Clear
            };

            this._actorLabel = new UILabel()
            {
                Frame = new CGRect(75, 25, this.ContentView.Bounds.Width -80, 20), 
                Font = UIFont.FromName("Cochin-BoldItalic", 14f),
                TextColor = UIColor.FromRGB(100, 51, 0),
                BackgroundColor = UIColor.Clear
            };
          
            this.ContentView.AddSubviews(new UIView[] { this._imageView, this._titleLabel, this._actorLabel });
            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        public void UpdateCell(Movie movie)
        {
            string actors = "";
            this._titleLabel.Text = $"{movie.Title} ({movie.Year:yyyy})";
            for(int i = 0; i < movie.Actors.Count; i++)
            {
                
                if(movie.Actors.Count -1 == i)
                {
                    actors += movie.Actors[i];
                }
                else
                {
                    actors += movie.Actors[i] + ", ";
                }
            }
            this._actorLabel.Text = actors;
            var image = UIImage.FromFile(movie.ImageUrl);
            this._imageView.Image = image;
            
        }
    }
}