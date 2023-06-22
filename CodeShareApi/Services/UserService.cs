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

    public UserService(IOptions<CodeShareDatabaseSettings> codeShareDatabaseSettings, IConfiguration configuration)
    {
       var mongoClient = new MongoClient(
            codeShareDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            codeShareDatabaseSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            codeShareDatabaseSettings.Value.UsersCollectionName);

            this.key = configuration.GetSection("JwtKey").ToString();
        
    }

   

    
    
    public string Authenticate(string username, string password)
    {
        var user = this._usersCollection.Find(x => x.Username == username && x.Password == password).FirstOrDefault();
        if (user == null)
        return null;

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenKey = Encoding.ASCII.GetBytes(key);

        var tokenDescriptor = new SecurityTokenDescriptor(){

        Subject = new ClaimsIdentity(new Claim[] {
            new Claim(ClaimTypes.Name, username),
        }),

        Expires = DateTime.UtcNow.AddHours(1),

        SigningCredentials = new SigningCredentials (
             new SymmetricSecurityKey(tokenKey),
             SecurityAlgorithms.HmacSha256Signature
        )

        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

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