using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library_Api.Models;

public class Book
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("book_title")]
    public string? BookTitle { get; set; }
    [BsonElement("author")]
    public string? Author { get; set; }

}
