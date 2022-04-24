using MongoDB.Bson;
namespace GameSever;
using MongoDB.Driver;
public class MongoConnection
{
    public static void Main()
    {
        MongoClient dbClient = new MongoClient("");

        List<BsonDocument> dbList = dbClient.ListDatabases().ToList();

        Console.WriteLine("The list of databases on this server is: ");
        foreach (var db in dbList)
        {
            Console.WriteLine(db);
        }  
    }

    
}
