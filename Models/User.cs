using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models;

public class User
{

    [BsonElement("id")]
    public int id { get; set; }

    [BsonElement("email")]
    public string email { get; set; } = null!;

    [BsonElement("first_name")]
    public string first_name { get; set; } = null!;
        
    [BsonElement("last_name")]
    public string last_name { get; set; } = null!;


    [BsonElement("avatar")]
    public string avatar { get; set; } = null!;

}