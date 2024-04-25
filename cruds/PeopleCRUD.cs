using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UF3_test.connections;
using UF3_test.model;
using System.IO;
using Newtonsoft.Json;


namespace UF3_test.cruds
{
    public class PeopleCRUD
    {
        public void LoadPeopleCollection()
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

       

        public void SelectFriendsOfPersonName(string name)
        {
            var database = MongoLocalConnection.GetDatabase("ATAQUEMALMONGO");
            var collection = database.GetCollection<BsonDocument>("PEOPLE");

            var filter = Builders<BsonDocument>.Filter.Eq("name", name);
            var personDocument = collection.Find(filter).First();
            var friends = personDocument.GetElement("friends");

            Console.WriteLine(friends.ToString());
        }

    }
}
