using ApiContracts;
using System.Net.Http.Json;

public class UserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    public async Task<UserDto?> Login(LoginRequest dto)
    {
        var response = await _client.PostAsJsonAsync("auth/login", dto);
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<UserDto>();
    }

    public async Task<UserDto> Register(CreateUserDto dto)
    {
        var response = await _client.PostAsJsonAsync("users", dto);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UserDto>();
    }
}