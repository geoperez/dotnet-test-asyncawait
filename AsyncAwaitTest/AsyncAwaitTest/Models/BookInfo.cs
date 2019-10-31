namespace AsyncAwaitTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BookInfo
    {
        public BookInfo(int id, string title, string author)
        {
            Id = id;
            Title = title;
            Author = author;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public int TotalWords { get; set; }

        public Dictionary<string, int> Bow { get; set; }
    }
}
