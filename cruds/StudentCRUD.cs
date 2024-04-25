using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UF3_test.connections;
using UF3_test.model;
using Newtonsoft.Json;

namespace UF3_test.cruds
{
    public class StudentCRUD
    {
        public void LoadStudentsCollection()
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
    }
}
