using ApiContracts;
using System.Net.Http.Json;

public class PostService
{
    private readonly HttpClient _client;

    public PostService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<PostDto>> GetAllAsync()
    {
        return await _client.GetFromJsonAsync<List<PostDto>>("posts");
    }

    public async Task CreateAsync(CreatePostDto dto)
    {
        var response = await _client.PostAsJsonAsync("posts", dto);
        response.EnsureSuccessStatusCode();
    }
    public async Task<PostDto?> GetByIdAsync(int id)
    {
        return await _client.GetFromJsonAsync<PostDto>($"posts/{id}");
    }
    public async Task UpdateAsync(int id, UpdatePostDto dto)
    {
        var response = await _client.PutAsJsonAsync($"posts/{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _client.DeleteAsync($"posts/{id}");
        response.EnsureSuccessStatusCode();
    }

}