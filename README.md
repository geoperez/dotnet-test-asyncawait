# dotnet-test-asyncawait
.NET Core Test for Interns

1 - Create a method to download TXT files and save them into a local folder named "Books" in desktop (Async/Await).

   - The book list is in the static class **Info**, through **Books** property. 
   - The book's download URL is form by the book's id, as in **GetUrl** method from **Utils** class.

2 - Generate the Bag-of-Words (BoW) of each file.
   
   - [Bag-of-Words](https://en.wikipedia.org/wiki/Bag-of-words_model)

3 - Implement a Producer/Consumer pattern for receiving the BoW of each file and generate a single final BoW file (the union of all individual BoW).

   - Run all files in parallel (Parallel.For).
   - You must use some sort of synchronization mechanism (ManualResentEvent, Semaphore, etc).
   - *Note: Use of any type of Concurrent Collections is forbidden.*

4 - Log to a file, all events and error, you consider necessary (Starting download, download finished, Processing file, etc).
