using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UF3_test.cruds
{
    public class GeneralCRUD
    {
        public void DropCollections(string databaseName, string collectionName)
        {
            try
            {
                // Conexión a la base de datos
                var client = new MongoClient();
                var database = client.GetDatabase(databaseName);

                // Mostrar el número de documentos en la colección antes de eliminarla
                var collection = database.GetCollection<BsonDocument>(collectionName);
                var numDocumentsBefore = collection.EstimatedDocumentCount();
                Console.WriteLine($"Número de documentos en la colección antes de eliminar: {numDocumentsBefore}");

                // Eliminar la colección
                database.DropCollection(collectionName);

                // Mostrar los nombres de las colecciones que quedan en la base de datos después de la eliminación
                Console.WriteLine("Colecciones restantes en la base de datos después de eliminar:");
                var collectionNames = database.ListCollectionNames().ToList();
                foreach (var name in collectionNames)
                {
                    Console.WriteLine(name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al eliminar la colección: {e.Message}");
            }
        }
    }
}
