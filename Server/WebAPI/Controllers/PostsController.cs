using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("posts")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _postRepository;

    public PostsController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePost(CreatePostDto dto)
    {
        Post post = new Post
        {
            Title = dto.Title,
            Body = dto.Body,
            UserId = dto.UserId
        };
        await _postRepository.AddAsync(post);

        return Ok(new PostDto(
            post.Id,
            post.Title,
            post.Body,
            post.UserId
        ));

    }

    [HttpGet]
    public ActionResult<List<PostDto>> GetPosts()
    {
        var posts = _postRepository.GetMany()
            .Select(p => new PostDto(
                p.Id,
                p.Title,
                p.Body,
                p.UserId
            ))
            .ToList();

        return Ok(posts);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostDto>> GetPost(int id)
    {
        var post = await _postRepository.GetSingleAsync(id);
        if (post == null) return NotFound();

        return Ok(new PostDto(
            post.Id,
            post.Title,
            post.Body,
            post.UserId
        ));

    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PostDto>> UpdatePost(int id, UpdatePostDto dto)
    {
        var post = await _postRepository.GetSingleAsync(id);
        if (post is null) return NotFound("Post not found");

        // AUTHORIZATION: Only owner may edit
        if (post.UserId != dto.UserId)
            return Unauthorized("You are not allowed to edit this post.");

        post.Title = dto.Title;
        post.Body = dto.Body;

        await _postRepository.UpdateAsync(post);

        return Ok(new PostDto(post.Id, post.Title, post.Body, post.UserId));
    }

    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePost(int id, [FromQuery] int userId)
    {
        var post = await _postRepository.GetSingleAsync(id);
        if (post is null) return NotFound("Post not found");

        // AUTHORIZATION: Only owner may delete
        if (post.UserId != userId)
            return Unauthorized("You are not allowed to delete this post.");

        await _postRepository.DeleteAsync(post);

        return NoContent();
    }
    
}