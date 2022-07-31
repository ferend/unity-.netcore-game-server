using GameSever.Services;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Requests;
using SharedLibrary.Responses;

namespace GameSever.Controllers;
[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    /// Both of these endpoints will be accessed from same route, it will just forward auth. BUT if you have two endpoints for post you need to declare route.
    /// it will be logical to return something in endpoint requests.

    private readonly IAuthenticationService _authenticationService;
    
    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost(("register"))]
    public IActionResult Register(AuthenticationRequest request)
    {
        var (success,content) = _authenticationService.Register(request.Username, request.Password);
        if (!success)
        {
            // returns username not available.
            return BadRequest(content);
        }
        
        // Forward to login screen after successfull register.
        return Login(request);
    }
    
    [HttpPost(("login"))]
    public IActionResult Login(AuthenticationRequest request)
    {
        var (success,content) = _authenticationService.Login(request.Username, request.Password);
        if (!success)
        {
            // returns username not available.
            return BadRequest(content);
        }

        return Ok(new AuthenticationResponse() { Token = content});
    }
    
}

