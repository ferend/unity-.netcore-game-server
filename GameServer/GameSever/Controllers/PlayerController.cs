using GameSever.Services;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace GameSever.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly PlayerServices _playerSerivces;
    // this is a get request
    //n query string
    // [HttpGet]
    // public Player Get([FromQuery] int id)
    // {
    //     var player = new Player() {Id = id};
    //     return player;
    // }


    public PlayerController(PlayerServices playerServices)
    {
        _playerSerivces = playerServices;
    }
    
    // get request via route
    [HttpGet("{id}")]
    public Player Get([FromQuery] int id)
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
