using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace MovieSearch.iOS
{
    public class TabController : UITabBarController
    {
        UIViewController tab1, tab2;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.TabBar.BackgroundColor = UIColor.LightGray;
            this.TabBar.TintColor = UIColor.Red;
            this.SelectedIndex = 0;
        }
        
            
            /*tab1 = new UIViewController();
           // tab1 = movieSearchController;
            tab1.Title = "Green";
            tab1.View.BackgroundColor = UIColor.Green;
            tab1.TabBarItem = new UITabBarItem(UITabBarSystemItem.Favorites, 0);

            tab2 = new UIViewController();
            tab2.Title = "Orange";
            tab2.View.BackgroundColor = UIColor.Orange;

           var tabs = new UIViewController[] 
            {
                tab1, tab2
            };
            ViewControllers = tabs;
            */
        
    }
}