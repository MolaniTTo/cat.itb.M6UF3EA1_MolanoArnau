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
            var collection = database.GetCollection<Book>("BOOKS");

            var isbn = collection.AsQueryable().Select(x => x.isbn).ToList();

            foreach (var i in isbn)
            {
                Console.WriteLine(i);
            }
        }

        public void SelectTitleCategoriesFromBooksOrderByPageCount()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("BOOKS");

            var filter = Builders<BsonDocument>.Filter.Empty;

            var sort = Builders<BsonDocument>.Sort.Descending("pageCount");

            var projection = Builders<BsonDocument>.Projection.Include("title").Include("categories");

            var books = collection.Find(filter).Project(projection).Sort(sort).ToList();

            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

       
        public void SelectTitleAuthorsFromBooksByAuthor(string author)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("BOOKS");

            var filter = Builders<BsonDocument>.Filter.Eq("authors", author);

            var projection = Builders<BsonDocument>.Projection.Include("title").Include("authors");

            var books = collection.Find(filter).Project(projection).ToList();

            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        public void SelectTitleAuthorsPageCountFromBooksByPageCountAndCategory(int minPageCount, int maxPageCount, string category)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("BOOKS");

            var filter = Builders<BsonDocument>.Filter.And(
                               Builders<BsonDocument>.Filter.Gte("pageCount", minPageCount),
                                              Builders<BsonDocument>.Filter.Lte("pageCount", maxPageCount),
                                                             Builders<BsonDocument>.Filter.Eq("categories", category)
                                                                        );

            var projection = Builders<BsonDocument>.Projection.Include("title").Include("authors").Include("pageCount");

            var books = collection.Find(filter).Project(projection).ToList();

            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        /*e) Mostra el title, authors dels llibres on han participat almenys els autors: "Charlie Collins" i "Robi Sen".
Al mètode li passarem un Array de Strings (String[]) amb els noms dels autors.*/

        public void SelectTitleAuthorsFromBooksByAuthors(string[] authors)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("BOOKS");

            var filter = Builders<BsonDocument>.Filter.All("authors", authors);

            var projection = Builders<BsonDocument>.Projection.Include("title").Include("authors");

            var books = collection.Find(filter).Project(projection).ToList();

            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        public void SelectTitleAuthorsFromBooksByAuthors2(string[] authors)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("BOOKS");

            var filter = Builders<BsonDocument>.Filter.All("authors", authors);

            var projection = Builders<BsonDocument>.Projection.Include("title").Include("authors");

            var books = collection.Find(filter).Project(projection).ToList();

            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        public void SelectTitleAuthorsFromBooksByAuthors3(string[] authors)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("BOOKS");

            var filter = Builders<BsonDocument>.Filter.All("authors", authors);

            var projection = Builders<BsonDocument>.Projection.Include("title").Include("authors");

            var books = collection.Find(filter).Project(projection).ToList();

            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }


        public void SelectTitleAndAuthorsWhereAuthorsContains(string[] authors)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("BOOKS");

            var filter = Builders<BsonDocument>.Filter.All("authors", authors);

            var projection = Builders<BsonDocument>.Projection.Include("title").Include("authors");

            var books = collection.Find(filter).Project(projection).ToList();

            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        public void SelectTitleAuthorsFromBooksByCategoryAndAuthor(string category, string author)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("BOOKS");

            var filter = Builders<BsonDocument>.Filter.And(
            Builders<BsonDocument>.Filter.Eq("categories", category),
            Builders<BsonDocument>.Filter.Ne("authors", author)
            );

            var projection = Builders<BsonDocument>.Projection.Include("title").Include("authors");

            var sort = Builders<BsonDocument>.Sort.Ascending("title");

            var books = collection.Find(filter).Project(projection).Sort(sort).ToList();

            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        





    }
}
