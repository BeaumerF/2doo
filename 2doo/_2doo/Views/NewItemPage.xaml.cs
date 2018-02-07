using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace _2doo
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            var today = DateTime.Today;

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description.",
                Deadline = DateTime.Today,
                isDone = false,
                isValid = "❌"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Item.DaysNumber = Item.isValid + " " + Item.Text + " (Finished in " + (Item.Deadline - DateTime.Today).TotalDays.ToString() + " days)";
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}
