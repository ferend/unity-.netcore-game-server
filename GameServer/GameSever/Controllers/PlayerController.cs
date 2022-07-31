using GameSever.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using SharedLibrary.Requests;

namespace GameSever.Controllers;


[Authorize]
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
    public Player Post(CreatePlayerRequest request)
    {
        var userId = int.Parse( User.FindFirst("id").Value);
        var user = _context.Users.Include(u => u.Players).First(u => u.Id == userId);
        var player = new Player()
        {
            Name = request.Name,
            User = user
        };

        _context.Add(player);
        _context.SaveChanges();
        Console.WriteLine("Player has been added to server.");
        return player;
    }
    
}

