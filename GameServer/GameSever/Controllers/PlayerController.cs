using GameSever.Services;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace GameSever.Controllers;

[ApiController]
[Route("[controller]")]

public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerSerivces;
    private readonly GameDbContext _context;

    public PlayerController(IPlayerService playerService,  GameDbContext context)
    {
        _context = context;
        _playerSerivces = playerService;

        var user = new User()
        {
            Username = "testUser",
            PasswordHash = "123456",
            Salt = "notnull"

        };

        // Add new user to framework.
        _context.Add(user);
        _context.SaveChanges();

    }
    
    // this is a get request
    //n query string

    // get request via route
    
    [HttpGet("{id}")]
    public Player Get([FromRoute] int id)
    {
        var player = new Player() {Id = id};
        _playerSerivces.DoAdd();
        return player;
    }

    [HttpPost]
    public Player Post(Player player)
    {
        Console.WriteLine("Player has been added to server.");
        return player;
    }
    
}

