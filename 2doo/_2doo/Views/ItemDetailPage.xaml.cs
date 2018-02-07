using System;

using Xamarin.Forms;

namespace _2doo
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void DeleteItem_Clicked(object sender, System.EventArgs e)
        {
            var answer = await DisplayAlert("Delete?", "You are about to delete this task. Are you sure?", "Yes", "No");
            if (answer)
            {
                MessagingCenter.Send(this, "DeleteItem", viewModel.Item);
                await Navigation.PopToRootAsync();
            }
        }

        async void DoneItem_Clicked(object sender, System.EventArgs e)
        {
            if (viewModel.Item.isDone)
            {
                viewModel.Item.isDone = false;
                viewModel.Item.isValid = "❌";
            }
            else
            {
                viewModel.Item.isDone = true;
                viewModel.Item.isValid = "✔️";
            }
            viewModel.Item.DaysNumber = viewModel.Item.isValid + " " + viewModel.Item.Text + " (Finished in " + (viewModel.Item.Deadline - DateTime.Today).TotalDays.ToString() + " days)";
            MessagingCenter.Send(this, "DoneItem", viewModel.Item);
            await Navigation.PopToRootAsync();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            viewModel.Item.DaysNumber = viewModel.Item.isValid + " " + viewModel.Item.Text + " (Finished in " + (viewModel.Item.Deadline - DateTime.Today).TotalDays.ToString() + " days)";
            MessagingCenter.Send(this, "UpdateItem", viewModel.Item);
            await Navigation.PopToRootAsync();
        }
    }
}
