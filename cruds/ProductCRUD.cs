using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using UF3_test.connections;
using UF3_test.model;
using Newtonsoft.Json;

namespace UF3_test.cruds
{
    public class ProductCRUD
    {
        public void LoadProductsCollection()
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


        public void UpdateProductStockWherePriceIsBetween600And1000()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("PRODUCTS");

            var filter = Builders<BsonDocument>.Filter.Gt("price", 600) & Builders<BsonDocument>.Filter.Lt("price", 1000);
            var update = Builders<BsonDocument>.Update.Set("stock", 150);

            var result = collection.UpdateMany(filter, update);

            Console.WriteLine("Products updated: " + result.ModifiedCount);

        }

        public void InsertNewAttributeDiscountToProductsWhereStockIsGreatherThan100()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("PRODUCTS");

            var filter = Builders<BsonDocument>.Filter.Gt("stock", 100);
            var update = Builders<BsonDocument>.Update.Set("discount", 0.20);

            var result = collection.UpdateMany(filter, update);

            Console.WriteLine("Products updated: " + result.ModifiedCount);
           

        }

        public void AggregateCategorySmartTVToProductAppleTV()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("PRODUCTS");

            var filter = Builders<BsonDocument>.Filter.Eq("name", "Apple TV");

            var update = Builders<BsonDocument>.Update.Set("category", "Smart TV");

            var result = collection.Aggregate().Match(filter).ToList();

            if (result.Count > 0)
            {
                Console.WriteLine("Products updated: " + result.Count);
            }
            else
            {
                    Console.WriteLine("No products updated");
            }
        }

        public void DeleteProductsWherePriceBetween400And600()
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("PRODUCTS");

            var filter = Builders<BsonDocument>.Filter.Gt("price", 400) & Builders<BsonDocument>.Filter.Lt("price", 600);

            var result = collection.DeleteMany(filter);

            Console.WriteLine("Products deleted: " + result.DeletedCount);
        }

        public void DeleteProductWithName(string name)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("PRODUCTS");

            var filter = Builders<BsonDocument>.Filter.Eq("name", name);
            var result = collection.DeleteOne(filter);

            Console.WriteLine("Products deleted: " + result.DeletedCount);
        }


        public void DeleteFirstCategoryFromProductMacBookAir(string name)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("PRODUCTS");

            var filter = Builders<BsonDocument>.Filter.Eq("name", name);
            var update = Builders<BsonDocument>.Update.Unset("categories.0");

            var result = collection.UpdateOne(filter, update);

            Console.WriteLine("Products updated: " + result.ModifiedCount);
        }



       












    }
}
