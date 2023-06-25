using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeShareApi.Models
{
    // User model
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
   
    public string Id { get; set; }



    [BsonElement("username")]
    public string Username { get; set; } = null!;

    [BsonElement("githubName")]
    public string GithubName { get; set; } 

    [BsonElement("region")]
    public string region { get; set; }

    [BsonElement("password")]
    public string Password { get; set; } = null!;

    [BsonElement("created_at")]
    public string CreatedAt { get; set; } 
}


 
}
