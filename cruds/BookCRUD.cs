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




            
    }
}
