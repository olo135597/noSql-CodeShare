using CodeShareApi.Models;
using CodeShareApi.Services;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
namespace CodeShareApi.Controllers;
[ApiController]
[Route("api/user/{userId}/snippets")]
public class SnippetController : ControllerBase
{
    private readonly SnippetService _snippetService;
    private readonly UserService _userService;

    public SnippetController(SnippetService snippetService, UserService userService)
    {
        _snippetService = snippetService;
        _userService = userService;
    }

    // Other code

    // Get all snippets for a user
    // Get all snippets for a user
    
[HttpGet]
public async Task<ActionResult<List<Snippet>>> GetSnippets([FromRoute] string userId)
{
    // Check if the user exists
    var user = await _userService.GetAsync(userId);

    if (user is null)
    {
        return NotFound();
    }

    // Get all snippets for the user
    var snippets = await _snippetService.GetByUserIdAsync(userId);

    return Ok(snippets);
}

// Get a snippet by id for a user
[HttpGet("{snippetId:length(24)}")]
public async Task<ActionResult<Snippet>> GetSnippet([FromRoute] string userId, [FromRoute] string snippetId)
{
     // Check if the user exists
     var user = await _userService.GetAsync(userId);

     if (user is null)
     {
         return NotFound();
     }

     // Check if the snippet exists and belongs to the user
     var snippet = await _snippetService.GetAsync(snippetId);

     if (snippet is null || snippet.UserId != userId)
     {
         return NotFound();
     }

     return Ok(snippet);
}

// Create a new snippet for a user
[HttpPost]
public async Task<IActionResult> CreateSnippet([FromRoute] string userId, [FromBody] Snippet newSnippet)
{

    
     
     // Check if the user exists
     var user = await _userService.GetAsync(userId);

     if (user is null)
     {
         return NotFound();
     }

     // Set the user id for the new snippet
     newSnippet.UserId = userId;

           Random rnd = new Random();

// Create a StringBuilder object
StringBuilder sb = new StringBuilder();

// Generate 24 random digits and append them to the string builder
for (int i = 0; i < 24; i++)
{
    // Generate a random digit between 0 and 9
    int digit = rnd.Next(0, 10);

    // Append the digit to the string builder
    sb.Append(digit);
}

// Get the random string of 24 digits
string randomNumber = sb.ToString();
     newSnippet.Id = randomNumber;

     // Create the new snippet
   // Get the current UTC time
DateTime currentDateTimeUtc = DateTime.UtcNow;

// Get the desired time zone
TimeZoneInfo myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

// Convert the UTC time to the desired time zone
DateTime rightDateTime = TimeZoneInfo.ConvertTimeFromUtc(currentDateTimeUtc, myTimeZone);

// Format and print the date and time
string utcString = rightDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");


     newSnippet.CreatedAt = utcString;
     
     // Save the new snippet to the database
 await _snippetService.CreateAsync(newSnippet);

     return CreatedAtAction(nameof(GetSnippet), new { userId = userId, snippetId = newSnippet.Id }, newSnippet);
}

// Update an existing snippet for a user
[HttpPut("{snippetId:length(24)}")]
public async Task<IActionResult> UpdateSnippet([FromRoute] string userId, [FromRoute] string snippetId, [FromBody] Snippet updatedSnippet)
{
     // Check if the user exists
     var user = await _userService.GetAsync(userId);
   

     if (user is null)
     {
         return NotFound();
     }

     // Check if the snippet exists and belongs to the user
     var snippet = await _snippetService.GetAsync(snippetId);

     if (snippet is null || snippet.UserId != userId)
     {
         return NotFound();
     }

     // Set the snippet id and user id for the updated snippet
     updatedSnippet.Id = snippet.Id;
     updatedSnippet.UserId = userId;
     
     // Update the snippet
     await _snippetService.UpdateAsync(snippetId, updatedSnippet);
     
     DateTime currentDateTimeUtc = DateTime.UtcNow;

// Get the desired time zone
TimeZoneInfo myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

// Convert the UTC time to the desired time zone
DateTime rightDateTime = TimeZoneInfo.ConvertTimeFromUtc(currentDateTimeUtc, myTimeZone);

// Format and print the date and time
string utcString = rightDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");


     updatedSnippet.CreatedAt = utcString;

     return NoContent();
}

// Delete a snippet by id for a user
[HttpDelete("{snippetId:length(24)}")]
public async Task<IActionResult> DeleteSnippet([FromRoute] string userId, [FromRoute] string snippetId)
{
     // Check if the user exists
     var user = await _userService.GetAsync(userId);

     if (user is null)
     {
         return NotFound();
     }

     // Check if the snippet exists and belongs to the user
     var snippet = await _snippetService.GetAsync(snippetId);

     if (snippet is null || snippet.UserId != userId)
     {
         return NotFound();
     }

     // Delete the snippet
     await _snippetService.RemoveAsync(snippetId);

     return NoContent();
}

}
