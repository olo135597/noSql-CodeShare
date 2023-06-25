using CodeShareApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;




namespace CodeShareApi.Services;

public class SnippetService
{
    private readonly IMongoCollection<Snippet> _snippets;


    public SnippetService(IOptions<CodeShareDatabaseSettings> codeShareDatabaseSettings)
    {
       var mongoClient = new MongoClient(
            codeShareDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            codeShareDatabaseSettings.Value.DatabaseName);

        _snippets = mongoDatabase.GetCollection<Snippet>(
            codeShareDatabaseSettings.Value.SnippetsCollectionName);
    }

    // Other methods


        public async Task<List<Snippet>> GetAsync()
        {
           return await _snippets.Find(s => true).ToListAsync();
        }

    // Get a snippet by id
    public async Task<Snippet> GetAsync(string id)
    {
        return await _snippets.Find(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Snippet> GetUserAsync(string userId)
    {
        return await _snippets.Find(s => userId == s.UserId).FirstOrDefaultAsync();
    }

    // Get snippets by user id
    public async Task<List<Snippet>> GetByUserIdAsync(string uid)
    {
        return await _snippets.Find(s => s.UserId == uid).ToListAsync();
    }

    // Create a new snippet
    public async Task CreateAsync(Snippet snippet)
    {
        await _snippets.InsertOneAsync(snippet);
    }

    // Update an existing snippet
    public async Task UpdateAsync(string id, Snippet snippet)
    {
        await _snippets.ReplaceOneAsync(s => s.Id == id, snippet);
    }

    // Delete a snippet by id
    public async Task RemoveAsync(string id)
    {
        await _snippets.DeleteOneAsync(s => s.Id == id);
    }

    public async Task RemoveUserAsync(string userId)
    {
        await _snippets.DeleteManyAsync(s => s.UserId == userId);
    }

    
}

    

    


