using Microsoft.AspNetCore.Mvc;

namespace GameSever.Controllers;
[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    /// Both of these endpoints will be accessed from same route, it will just forward auth. BUT if you have two endpoints for post you need to declare route.
    
    /// it will be logical to retrun something in endpoint requests.
    [HttpPost(("register"))]
    public IActionResult Register()
    {
        return Ok();
    }
    
    [HttpPost(("login"))]
    public IActionResult Login()
    {
        return Ok();

    }
    
}
