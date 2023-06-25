using CodeShareApi.Models;
using CodeShareApi.Services;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

using System.Text;


namespace CodeShareApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService) =>
       _userService = userService;

    
    [HttpGet]
    public async Task<List<User>> Get() =>
    await _userService.GetAsync();

    
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {

        
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
     newUser.Id = randomNumber;

        
         DateTime currentDateTimeUtc = DateTime.UtcNow;

// Get the desired time zone
TimeZoneInfo myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

// Convert the UTC time to the desired time zone
DateTime rightDateTime = TimeZoneInfo.ConvertTimeFromUtc(currentDateTimeUtc, myTimeZone);

// Format and print the date and time
string utcString = rightDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");


    

     newUser.CreatedAt = utcString;
    await _userService.CreateAsync(newUser);
        return CreatedAtAction(nameof(Get), new { id = newUser.Id}, newUser);

       
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User updatedUser)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        updatedUser.Id = user.Id;

      
           DateTime currentDateTimeUtc = DateTime.UtcNow;

// Get the desired time zone
TimeZoneInfo myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

// Convert the UTC time to the desired time zone
DateTime rightDateTime = TimeZoneInfo.ConvertTimeFromUtc(currentDateTimeUtc, myTimeZone);

// Format and print the date and time
string utcString = rightDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");



      updatedUser.CreatedAt = utcString;
        await _userService.UpdateAsync(id, updatedUser);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await _userService.RemoveAsync(id);

        return NoContent();
    }

    
}
