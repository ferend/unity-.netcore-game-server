using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace GameSever.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    // this is a get request
    [HttpGet]
    public Player Get([FromQuery] int id)
    {
        var player = new Player() {Id = id};
        return player;
    }

    [HttpPost]
    public Player Post(Player player)
    {
        Console.WriteLine("Player has been added to server.");
        return player;
    }
}
