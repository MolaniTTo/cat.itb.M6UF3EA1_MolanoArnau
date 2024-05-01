using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UF3_test.connections;
using UF3_test.model;
using Newtonsoft.Json;

namespace UF3_test.cruds
{
    public class BookCRUD
    {
        public void LoadBooksCollection()
        {
            FileInfo file = new FileInfo("../../../files/books.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(fileString);

            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("BOOKS");

            if (books != null)
            {
                foreach (var book in books)
                {
                    Console.WriteLine(book.title);
                    string json = JsonConvert.SerializeObject(book);
                    var document = new BsonDocument();
                    document.Add(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
                Console.WriteLine("Books collection loaded");
            }
        }

        public void LoadBooks2Collection()
        {
            FileInfo file = new FileInfo("../../../files/books2.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();
            List<Book_> books = JsonConvert.DeserializeObject<List<Book_>>(fileString);

            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("BOOKS2");

            if (books != null)
            {
                foreach (var book in books)
                {
                    Console.WriteLine(book.title);
                    string json = JsonConvert.SerializeObject(book);
                    var document = new BsonDocument();
                    document.Add(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
                Console.WriteLine("Books2 collection loaded");
            }
        }


        public void SelectTitleAndAuthorsFromBooksOrderByPageCount()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("BOOKS");

            var filter = Builders<BsonDocument>.Filter.Empty;

            var sort = Builders<BsonDocument>.Sort.Ascending("pageCount");

            var projection = Builders<BsonDocument>.Projection.Include("title").Include("authors");

            var books = collection.Find(filter).Project(projection).Sort(sort).ToList();

            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }


        public void SelectTitleIsbnPageCountFromBooksWherePageCountGreatherThan250()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("BOOKS");

            var filter = Builders<BsonDocument>.Filter.Gt("pageCount", 250);

            var projection = Builders<BsonDocument>.Projection.Include("title").Include("isbn").Include("pageCount");

            var books = collection.Find(filter).Project(projection).ToList();

            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        public void SelectIsbnFromBooks()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var booksCollection2 = database.GetCollection<Book_>("BOOKS2");

            var isbn = from book in booksCollection2.AsQueryable<Book_>()
            select book.isbn;

            foreach (var book in isbn)
            {
                Console.WriteLine(book);
            }
        }


        public void SelectTitleCategoriesFromBooksOrderByPageCount()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var booksCollection2 = database.GetCollection<Book_>("BOOKS2");

            var books = from book in booksCollection2.AsQueryable<Book_>()
                        orderby book.pageCount
                        select new { book.title, book.categories };

            foreach (var book in books)
            {
                Console.Write(book.title + " ");
                foreach (var category in book.categories)
                {
                    Console.Write(category + " ");
                }
                Console.WriteLine();
            }  
        }
           

       
        public void SelectTitleAuthorsFromBooksByAuthor(string author)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<Book_>("BOOKS2");

            var books = from book in collection.AsQueryable<Book_>()
                        where book.authors.Contains(author)
                        select new { book.title, book.authors };

            foreach (var book in books)
            {
                Console.WriteLine(book.title);
                foreach (var a in book.authors)
                {
                    Console.WriteLine(a);
                }
                Console.WriteLine();
            }
        }


        public void SelectTitleAuthorsPageCountFromBooksByPageCountAndCategory(int minPageCount, int maxPageCount, string category)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<Book_>("BOOKS2");

            var books = from book in collection.AsQueryable<Book_>()
                        where book.pageCount >= minPageCount && book.pageCount <= maxPageCount && book.categories.Contains(category)
                        select new { book.title, book.authors, book.pageCount };

            foreach (var book in books)
            {
                Console.WriteLine(book.title);
                foreach (var a in book.authors)
                {
                    Console.WriteLine(a);
                }
                Console.WriteLine(book.pageCount);
                Console.WriteLine();
            }
           
        }


        public void SelectTitleAuthorsFromBooksByAuthors(string[] authors)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<Book_>("BOOKS2");

            var books = from book in collection.AsQueryable<Book_>()
                        where authors.All(a => book.authors.Contains(a))
                        select new { book.title, book.authors };

            foreach (var book in books)
            {
                Console.WriteLine(book.title);
                foreach (var a in book.authors)
                {
                    Console.WriteLine(a);
                }
                Console.WriteLine();
            }

        }


        public void SelectTitleAuthorsFromBooksByCategoryAndAuthor(string category, string author)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<Book_>("BOOKS2");

            var books = from book in collection.AsQueryable<Book_>()
                        where book.categories.Contains(category) && !book.authors.Contains(author)
                        orderby book.title
                        select new { book.title, book.authors };

            foreach (var book in books)
            {
                Console.WriteLine(book.title);
                foreach (var a in book.authors)
                {
                    Console.WriteLine(a);
                }
                Console.WriteLine();
            }
          
        }

        





    }
}
