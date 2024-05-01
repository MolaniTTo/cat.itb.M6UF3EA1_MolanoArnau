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
    public class RestaurantCRUD
    { 
        public void LoadRestaurantsCollection()
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

        
        public void SelectRestaurantsOnBoroughAndCuisine(string borough , string cuisine)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("RESTAURANTS");

            var filter = Builders<BsonDocument>.Filter.Eq("borough", borough);
            filter = filter & Builders<BsonDocument>.Filter.Eq("cuisine", cuisine);
            var restaurants = collection.Find(filter).ToList();
                
            foreach (var restaurant in restaurants)
            {
                Console.WriteLine(restaurant.ToString());
            }
         
        }

        public void SelectRestaurantsByZipCode(string zipCode)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("RESTAURANTS");

            var filter = Builders<BsonDocument>.Filter.Eq("address.zipcode", zipCode);
            var restaurants = collection.Find(filter).ToList();

            foreach (var restaurant in restaurants)
            {
                Console.WriteLine(restaurant.GetElement("name").Value);
            } 
        }

        public void UpdateZipcodeFromStreet(string street, string zipcode)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("RESTAURANTS");

            var filter = Builders<BsonDocument>.Filter.Eq("address.street", street);
            var update = Builders<BsonDocument>.Update.Set("address.zipcode", zipcode);

            var result = collection.UpdateOne(filter, update);
            Console.WriteLine(result);

        }


        public void AddNewAttributeStarsWhereCuisineIsCaribbean(string cuisine)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("RESTAURANTS");

            var filter = Builders<BsonDocument>.Filter.Eq("cuisine", cuisine);
            var update = Builders<BsonDocument>.Update.Set("stars", "*****");

            var result = collection.UpdateMany(filter, update);

            Console.WriteLine("Restaurants updated: " + result.ModifiedCount);

            var restaurants = collection.Find(filter).ToList();

            foreach (var restaurant in restaurants)
            {
                Console.WriteLine(restaurant.ToString());
            }
        }


        public void DeleteRestaurantsWhereCuisineIs(string cuisine)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("RESTAURANTS");

            var filter = Builders<BsonDocument>.Filter.Eq("cuisine", cuisine);
            var result = collection.DeleteMany(filter);

            Console.WriteLine("Restaurants deleted: " + result.DeletedCount);
        }



        //AGREGACIONS


        public void CountRestaurantsByCuisineSortDescending()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<Restaurant>("RESTAURANTS");
            var aggregate = collection.Aggregate()
                .Group(new BsonDocument { { "_id", "$cuisine" }, { "count", new BsonDocument("$sum", 1) } })
                .Sort(new BsonDocument("count", -1));

            foreach (var restaurant in aggregate.ToList())
            {
                Console.WriteLine(restaurant);
            }

        }

        public void CountGradesByRestaurant()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<Restaurant>("RESTAURANTS");
            
            var aggregate = collection.Aggregate()
                .Project(new BsonDocument { { "name", 1 }, { "grades", 1 } })
                .Unwind("grades")
                .Group(new BsonDocument { { "_id", "$name" }, { "count", new BsonDocument("$sum", 1) } });

            foreach (var restaurant in aggregate.ToList())
            {
                Console.WriteLine(restaurant);
            }
        }

        public void ShowMaxScoreByRestaurant()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<Restaurant>("RESTAURANTS");

            var aggregate = collection.Aggregate()
                .Project(new BsonDocument { { "name", 1 }, { "grades", 1 } })
                .Unwind("grades").Group(new BsonDocument { { "_id", "$name" }, { "maxScore", new BsonDocument("$max", "$grades.score") } });

            foreach (var restaurant in aggregate.ToList())
            {
                Console.WriteLine(restaurant);
            }
        }

        
        public void ShowCuisineTypesByBorough()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<Restaurant>("RESTAURANTS");

            var aggregate = collection.Aggregate()
                .Project(new BsonDocument { { "borough", 1 }, { "cuisine", 1 } })
                .Group(new BsonDocument { { "_id", "$borough" }, { "cuisineTypes", new BsonDocument("$addToSet", "$cuisine") } });

            foreach (var restaurant in aggregate.ToList())
            {
                Console.WriteLine(restaurant);
            }
        }















    }
}
