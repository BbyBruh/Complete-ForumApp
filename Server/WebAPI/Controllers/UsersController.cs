using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public UsersController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto dto)
    {
        User user = new(dto.UserName, dto.Password);
        await userRepository.AddAsync(user);

        return Ok(new UserDto(user.Id, user.UserName));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var user = await userRepository.GetSingleAsync(id);
        if (user == null) return NotFound();

        return Ok(new UserDto(user.Id, user.UserName));
    }
}