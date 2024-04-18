using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Nodes;
using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using UF3_test.connections;
using UF3_test.model;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace UF3_test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("         ----------EA1----------         ");
            Console.WriteLine("1. Insert Students");
            Console.WriteLine("2. Select students where group = (your group)");
            Console.WriteLine("3. Select students with 100 in exam");
            Console.WriteLine("4. Select students with less than 50 in exam");
            Console.WriteLine("5. Select interests where student_id = 111222333");
            Console.WriteLine("6. Load all collections");
            Console.WriteLine("7. Exit");

            Console.WriteLine("Introdueix el número de l'exercici que vols executar: ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    InsertStudents();
                    break;
                case 2:
                    SelectStudentsWhereGroupA();
                    break;
                case 3:
                    SelectStudentsWith100InExam();
                    break;
                case 4:
                    SelectStudentsWithLessThan50InExam();
                    break;
                case 5:
                    SelectInterestsWhereStudentId();
                    break;
                case 6:
                    LoadAllCollections();
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opció no vàlida");
                    break;
            }

            
        }


        private static void InsertStudents()
        {
           
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("GRADES");

            var document = new BsonDocument
            {
                { "student_id", 111333444 },
                { "name" , "Arnau" },
                { "surname", "Molano" },
                { "class_id", "DAM"},
                { "group", "A" },
                { "scores", new BsonArray
                {
                        new BsonDocument{ {"type", "exam"}, {"score", 100 } },
                        new BsonDocument{ {"type", "teamWork"}, {"score", 50 } },
                    }
                },
               
            };

            var document2 = new BsonDocument
            {
                { "student_id", 111222333 },
                { "name" , "Eric" },
                { "surname", "Requena" },
                { "class_id", "Undefined"},
                { "group", "A" },
                { "interests", new BsonArray{"music", "gym", "code", "electronics" }}
            };
            collection.InsertOne(document);
            collection.InsertOne(document2);

        }



        private static void SelectStudentsWhereGroupA()
        {
            
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("GRADES");

            var filter = Builders<BsonDocument>.Filter.Eq("group", "A");
            var studentDocuments = collection.Find(filter).ToList();

            foreach (var student in studentDocuments)
            {
                Console.WriteLine(student.ToString());
            }

        }


        private static void SelectStudentsWith100InExam()
        {
            
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("GRADES");

            var filter = Builders<BsonDocument>.Filter.Eq("scores.type", "exam") & Builders<BsonDocument>.Filter.Eq("scores.score", 100);
            var studentDocuments = collection.Find(filter).ToList();

            foreach (var student in studentDocuments)
            {
                Console.WriteLine(student.ToString());
            }

        }

        private static void SelectStudentsWithLessThan50InExam()
        {
            
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("GRADES");

            //  var filter = Builders<BsonDocument>.Filter.Eq("scores.type", "exam") & Builders<BsonDocument>.Filter.Lt("scores.score", 50.00);

            var filter = Builders<BsonDocument>.Filter.ElemMatch<BsonDocument>("scores", Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("type", "exam"),
                Builders<BsonDocument>.Filter.Lt("score", 50.00)
            ));

            var studentDocuments = collection.Find(filter).ToList();

            foreach (var student in studentDocuments)
            {
                Console.WriteLine(student.ToString());
            }

        }


        private static void SelectInterestsWhereStudentId()
        {
            
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("GRADES");

            var filter = Builders<BsonDocument>.Filter.Eq("student_id", 111222333);
            var studentDocument = collection.Find(filter).FirstOrDefault();
            var interests = studentDocument.GetElement("interests");

            Console.WriteLine(interests.ToString());

        }


        private static void LoadAllCollections()
        {
          
            LoadStudentsCollection();
        }

        //LoadPeopleCollection
        private static void LoadPeopleCollection()
        {
            FileInfo file = new FileInfo("../../../files/people.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();
            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(fileString);

            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("PEOPLE");

            if (people != null)
            {
                foreach (var person in people)
                {
                    Console.WriteLine(person.name);
                    string json = JsonConvert.SerializeObject(person);
                    var document = new BsonDocument();
                    document.Add(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
                Console.WriteLine("People collection loaded");
            }    
        }

        //LoadBooksCollection
        private static void LoadBooksCollection()
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

        //LoadRestaurantsCollection
        private static void LoadRestaurantsCollection()
        {
            FileInfo file = new FileInfo("../../../files/restaurants.json");
            StreamReader sr = file.OpenText();

            // Creamos una lista para almacenar los restaurantes
            List<Restaurant> restaurants = new List<Restaurant>();

            // Leemos el archivo línea por línea
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                // Deserializamos cada línea como un objeto de restaurante
                Restaurant restaurant = JsonConvert.DeserializeObject<Restaurant>(line);
                restaurants.Add(restaurant);
            }

            sr.Close();

            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("RESTAURANTS");

            if (restaurants.Count > 0)
            {
                foreach (var restaurant in restaurants)
                {
                    Console.WriteLine(restaurant.name);
                    string json = JsonConvert.SerializeObject(restaurant);
                    var document = new BsonDocument();
                    document.Add(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
                Console.WriteLine("Restaurants collection loaded");
            }
        }

        //LoadProductsCollection
        private static void LoadProductsCollection()
        {
            FileInfo file = new FileInfo("../../../files/products.json");
            StreamReader sr = file.OpenText();

            List<Product> products = new List<Product>();

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Product product = JsonConvert.DeserializeObject<Product>(line);
                products.Add(product);
            }

            sr.Close();

            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("PRODUCTS");

            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    Console.WriteLine(product.name);
                    string json = JsonConvert.SerializeObject(product);
                    var document = BsonDocument.Parse(json);
                    collection.InsertOne(document);
                }
                Console.WriteLine("Products collection loaded");
            }
        }



        //LoadStudentsCollection
        private static void LoadStudentsCollection()
        {
            FileInfo file = new FileInfo("../../../files/students.json");
            StreamReader sr = file.OpenText();

            List<Student> students = new List<Student>();

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Student student = JsonConvert.DeserializeObject<Student>(line);
                students.Add(student);
            }

            sr.Close();

            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("STUDENTS");

            if (students.Count > 0)
            {
                foreach (var student in students)
                {
                    Console.WriteLine(student.firstname);
                    string json = JsonConvert.SerializeObject(student);
                    var document = BsonDocument.Parse(json);
                    collection.InsertOne(document);
                }
                Console.WriteLine("Students collection loaded");
            }
        }

        /*
        
        private static void GetAllDBs()
        {
            
            var dbClient = MongoLocalConnection.GetMongoClient();
            
            var dbList = dbClient.ListDatabases().ToList();
            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }

        }

        private static void GetCollections()
        {
            
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");

            var colList = database.ListCollections().ToList();
            Console.WriteLine("The list of collection on this database is: ");
            foreach (var col in colList)
            {
                Console.WriteLine(col);
            }
        }

        private static void SelectAllStudents()
        {

            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("GRADES");

            var studentDocuments = collection.Find(new BsonDocument()).ToList();

            foreach (var student in studentDocuments)
            {
                Console.WriteLine(student.ToString());
            }

        }
        
        private static void SelectOneStudent()
        {
            

            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("GRADES");

            var filter = Builders<BsonDocument>.Filter.Eq("student_id", 111222333);
            var studentDocument = collection.Find(filter).FirstOrDefault();
            Console.WriteLine(studentDocument.ToString());

        }

        private static void SelectStudentFields()
        {
            
            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var filter = Builders<BsonDocument>.Filter.Eq("student_id", 9999923);
            var studentDocument = collection.Find(filter).FirstOrDefault();
            var id = studentDocument.GetElement("student_id");
            var scores = studentDocument.GetElement("scores");

            Console.WriteLine(id.ToString());
            Console.WriteLine(scores.ToString());

        }

        */


    }
}