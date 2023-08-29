using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Auth;
using Playground.Application.DTO;
using Playground.Application.Security;
using Playground.Application.Users.GetUser;
using Playground.Application.Users.SignIn;
using Playground.Application.Users.SignUp;
using Playground.Core.Globals;
using Playground.Core.Roles;

namespace Playground.API.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenStorage _tokenStorage;

    public UsersController(IMediator mediator,
        ITokenStorage tokenStorage)
    {
        _mediator = mediator;
        _tokenStorage = tokenStorage;
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> Get()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User.Identity?.Name);
        var user = await _mediator.Send(new GetUserQuery() {UserId = userId});

        return user;
    }

    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(SignUpCommand command)
    {
        command = command with {UserId = Guid.NewGuid()};
        await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new {command.UserId}, null);
    }    
    
    
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> Post(SignInCommand command)
    {
        await _mediator.Send(command);
        var jwt = _tokenStorage.Get();
        return jwt;
    }    
    
    [HttpPost("preset/sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> Post()
    {
        var command = new SignInCommand("email@gmail.com", "12345!@#$%MQA");
        await _mediator.Send(command);
        var jwt = _tokenStorage.Get();
        AddCookie("token", jwt.AccessToken);
        return jwt;
    }
    
    private void AddCookie(string key, string value) => Response.Cookies.Append(key, value, new CookieOptions()
    {
        HttpOnly = true,
        Expires = DateTime.UtcNow.AddDays(7),
        Secure = true,
        SameSite = SameSiteMode.None
    });
}