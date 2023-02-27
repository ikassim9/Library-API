using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library_Api.Models;

public class Book
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
 
    public string Id { get; set; } = String.Empty;

    [BsonElement("book_title")]
    public string Title { get; set; } = String.Empty;
    [BsonElement("author")]
    public string Author { get; set; } = String.Empty;
    [BsonElement("publication_date")]
    public string PublicationDate { get; set; } = String.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = String.Empty;

    [BsonElement("book_cover")]
    public string BookCover { get; set; } = String.Empty;
}
