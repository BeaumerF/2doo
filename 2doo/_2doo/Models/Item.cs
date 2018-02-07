using System;

namespace _2doo
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public String DaysNumber { get; set; }
        public bool isDone { get; set; }
        public String isValid { get; set; }
    }
}
