using ApiContracts;
using System.Net.Http.Json;

public class CommentService
{
    private readonly HttpClient _client;

    public CommentService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<CommentDto>> GetForPost(int postId)
    {
        return await _client.GetFromJsonAsync<List<CommentDto>>($"comments/post/{postId}");
    }

    public async Task CreateAsync(CreateCommentDto dto)
    {
        var response = await _client.PostAsJsonAsync("comments", dto);
        response.EnsureSuccessStatusCode();
    }
    public async Task UpdateAsync(int id, UpdateCommentDto dto)
    {
        var response = await _client.PutAsJsonAsync($"comments/{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _client.DeleteAsync($"comments/{id}");
        response.EnsureSuccessStatusCode();
    }
    
    public async Task<CommentDto?> GetSingleAsync(int id)
    {
        return await _client.GetFromJsonAsync<CommentDto>($"comments/{id}");
    }

}