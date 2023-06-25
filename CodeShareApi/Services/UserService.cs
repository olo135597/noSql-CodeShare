using CodeShareApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

using System.Text;

namespace CodeShareApi.Services;

public class UserService
{
   
    private readonly IMongoCollection<User> _usersCollection;

    private readonly string key;

    public UserService(IOptions<CodeShareDatabaseSettings> codeShareDatabaseSettings)
    {
       var mongoClient = new MongoClient(
            codeShareDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            codeShareDatabaseSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            codeShareDatabaseSettings.Value.UsersCollectionName);
        
    }

   

    
    
   


    public async Task<List<User>> GetAsync() =>
    await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) =>
     await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(User newUser) =>
     await _usersCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, User updatedUser) =>
    await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(string id) =>
     await _usersCollection.DeleteOneAsync(x => x.Id == id);
}
