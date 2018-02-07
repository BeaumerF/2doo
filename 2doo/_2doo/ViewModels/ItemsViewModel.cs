using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace _2doo
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Tasks :";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });

            MessagingCenter.Subscribe<SayItPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });

            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "DeleteItem", async (obj, item) =>
            {
                var _item = item as Item;
                Items.Remove(_item);
                await DataStore.DeleteItemAsync(_item.Id);
            });

            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "DoneItem", async (obj, _item) =>
            {
                await ExecuteLoadItemsCommand();
            });

            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "UpdateItem", async (obj, item) =>
            {
                await ExecuteLoadItemsCommand();
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
