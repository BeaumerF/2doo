using System;

using Xamarin.Forms;

namespace _2doo
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page itemsPage = new ItemsPage();

            Children.Add(itemsPage);

            Title = "2doo";
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
