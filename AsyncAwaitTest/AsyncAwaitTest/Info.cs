namespace AsyncAwaitTest
{
    using AsyncAwaitTest.Models;
    using System.Collections.Generic;

    public static class Info
    {
        static Info()
        {
            Books = new List<BookInfo>
            {
                new BookInfo(1952, "The Yellow Wallpaper", "Charlotte Perkins Gilman"),
                new BookInfo(46, "A Christmas Carol", "Charles Dickens"),
                new BookInfo(25344, "The Scarlet Letter", "Nathaniel Hawthorne"),
                new BookInfo(98, "A Tale of Two Cities", "Charles Dickens"),
                new BookInfo(219, "Heart of Darkness", "Joseph Conrad"),
                new BookInfo(2701, "Moby Dick", "Herman Melville"),
                new BookInfo(11, "Alice's Adventures in Wonderland", "Lewis Carroll"),
                new BookInfo(43, "The Strange Case of Dr. Jekyll and Mr. Hyde", "Robert Louis Stevenson"),
                new BookInfo(1080, "A Modest Proposal", "Jonathan Swift"),
                new BookInfo(41, "The Legend of Sleepy Hollow", "Washington Irving"),
                new BookInfo(1342, "Pride and Prejudice", "Jane Austen"),
                new BookInfo(84, "Frankenstein", "Mary Wollstonecraft Shelley"),
                new BookInfo(1661, "The Adventures of Sherlock Holmes", "Arthur Conan Doyle"),
                new BookInfo(76, "Adventures of Huckleberry Finn", "Mark Twain"),
                new BookInfo(205, "Walden, and On The Duty Of Civil Disobedience", "Henry David Thoreau"),
                new BookInfo(2600, "War and Peace", "Graf Leo Tolstoy"),
                new BookInfo(74, "The Adventures of Tom Sawyer", "Mark Twain"),
                new BookInfo(16, "Peter Pan", "J. M. Barrie"),
                new BookInfo(4300, "Ulysses", "James Joyce"),
                new BookInfo(160, "The Awakening, and Selected Short Stories", "Kate Chopin"),
            };
        }

        public static List<BookInfo> Books { get; }
    }
}
