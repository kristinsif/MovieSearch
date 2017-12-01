using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace MovieSearch.iOS
{
    class MovieCollectionDataSource : UICollectionViewController
    {
    /*    private readonly List<Movie> _movieList;

        public MovieCollectionController(UICollectionViewFlowLayout layout, List<Movie> movieList) : base(layout)
        {
            this._movieList = movieList;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Favorites, 1);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Collection";
            this.CollectionView.BackgroundColor = UIColor.White;
            this.CollectionView.ContentInset = new UIEdgeInsets(10, 10, 10, 10);

            this.CollectionView.RegisterClassForCell(typeof(MovieCollectionCell), PersonCollectionCell.CellId);
            this.CollectionView.Source = new PersonCollectionDataSource(this._movieList);
        }
        */
    }
}