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
using UF3_test.cruds;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace UF3_test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            PeopleCRUD peopleCRUD = new PeopleCRUD();
            ProductCRUD productCRUD = new ProductCRUD();
            BookCRUD bookCRUD = new BookCRUD();
            RestaurantCRUD restaurantCRUD = new RestaurantCRUD();
            StudentCRUD studentCRUD = new StudentCRUD();




            Console.WriteLine("         ----------EA1----------         ");
            Console.WriteLine("1. Insert Students");
            Console.WriteLine("2. Select students where group = (your group)");
            Console.WriteLine("3. Select students with 100 in exam");
            Console.WriteLine("4. Select students with less than 50 in exam");
            Console.WriteLine("5. Select interests where student_id = 111222333");
            Console.WriteLine("6. Load all collections");
            Console.WriteLine();
            Console.WriteLine("         ----------EA2----------        ");
            Console.WriteLine("7. Select friends of Arianna Crammer");
            Console.WriteLine("8. Select all of restaurants where borough = Manhattan and cuisine = Seafood");
            Console.WriteLine("9. Select name restaurants with ZipCode = 11234");
            Console.WriteLine("10. Select title and authors orderBy PageCount");
            Console.WriteLine("11. Select title isbn and pageCount of books with more than 250 pages");
            Console.WriteLine("12. Update stock = 150 wherre price is between 600 abd 1000");
            Console.WriteLine("13. Add discount = 0.2 where stock > 100 ");
            Console.WriteLine("14. Update zipcode to 30033 of street Charles Street");
            Console.WriteLine("15. Add stars where cuisine = Caribbean");
            Console.WriteLine("16. Delete products where price is between 400 and 600");
            Console.WriteLine("17. Delete product with name = 'Mac mini'");
            Console.WriteLine("18. Delete restaurants where cuisine = 'Delicatessen'");
            Console.WriteLine("19. Delete first category of product with name = 'MakBook Air'");
            Console.WriteLine("0. Exit");
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
                    peopleCRUD.SelectFriendsOfPersonName("Arianna Cramer");
                    break;
                case 8:
                    restaurantCRUD.SelectRestaurantsOnBoroughAndCuisine("Manhattan", "Seafood");
                    break;
                case 9:
                    restaurantCRUD.SelectRestaurantsByZipCode("11234");
                    break; 
                case 10:
                    bookCRUD.SelectTitleAndAuthorsFromBooksOrderByPageCount();
                    break;
                case 11:
                    bookCRUD.SelectTitleIsbnPageCountFromBooksWherePageCountGreatherThan250();
                    break;
                case 12:
                    productCRUD.UpdateProductStockWherePriceIsBetween600And1000();
                    break;
                case 13:
                    productCRUD.InsertNewAttributeDiscountToProductsWhereStockIsGreatherThan100();
                break;
                case 14:
                    restaurantCRUD.UpdateZipcodeFromStreet("Charles Street", "30033");
                    break;
                case 15:
                    restaurantCRUD.AddNewAttributeStarsWhereCuisineIsCaribbean("Caribbean");
                    break;
                case 16:
                    productCRUD.DeleteProductsWherePriceBetween400And600();
                    break;
                case 17:
                    productCRUD.DeleteProductWithName("Mac mini");
                    break;
                case 18:
                    restaurantCRUD.DeleteRestaurantsWhereCuisineIs("Caribbean");
                    break;
                case 19:
                    productCRUD.DeleteFirstCategoryFromProductMacBookAir("MakBook Air");
                    break;
                case 20:
                    LoadAllCollections();
                    break;
                case 0:
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
            PeopleCRUD peopleCRUD = new PeopleCRUD();
            ProductCRUD productCRUD = new ProductCRUD();
            BookCRUD bookCRUD = new BookCRUD();
            RestaurantCRUD restaurantCRUD = new RestaurantCRUD();
            StudentCRUD studentCRUD = new StudentCRUD();

            peopleCRUD.LoadPeopleCollection();
            productCRUD.LoadProductsCollection();
            bookCRUD.LoadBooksCollection();
            restaurantCRUD.LoadRestaurantsCollection();
            studentCRUD.LoadStudentsCollection();
          
        }

      
    }
}