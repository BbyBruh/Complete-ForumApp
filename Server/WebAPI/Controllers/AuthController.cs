using ApiContracts;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public AuthController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginRequest dto)
    {
        var user = await userRepository.GetByUserNameAsync(dto.UserName);

        if (user == null || user.Password != dto.Password)
            return Unauthorized("Invalid username or password");

        return Ok(new UserDto(user.Id, user.UserName));
    }
}