namespace CodeShareApi.Models;

public class CodeShareDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string UsersCollectionName { get; set; } = null!;
     public string SnippetsCollectionName { get; set; } = null!;
}