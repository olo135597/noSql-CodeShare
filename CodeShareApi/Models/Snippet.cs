using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
   
    


   namespace CodeShareApi.Models
   {

[BsonIgnoreExtraElements]
   public class Snippet
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set; }

    [BsonElement("user_id")]
    public string UserId { get; set; }

    [BsonElement("uid")]
    public string Uid { get; set; }

    [BsonElement("content")]
    public string Content { get; set; } = null!;

    [BsonElement("title")]
    public string Title { get; set; } = null!;

    [BsonElement("language")]
    public string Language { get; set; } = null!;

    [BsonElement("created_at")]
    public String CreatedAt { get; set; } 
}


   }