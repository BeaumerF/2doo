using System;

namespace _2doo
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public String DoneButton { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
            if (Item.isDone)
                DoneButton = "Undone ❎";
            else
                DoneButton = "Done ☑️";
        }
    }
}
