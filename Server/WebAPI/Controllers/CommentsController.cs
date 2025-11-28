using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("comments")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;

    public CommentsController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateComment(CreateCommentDto dto)
    {
        Comment comment = new Comment
        {
            Body = dto.Body,
            UserId = dto.UserId,
            PostId = dto.PostId
        };

        await _commentRepository.AddAsync(comment);

        // Reload with relations
        var fullComment = await _commentRepository.GetSingleAsync(comment.Id);

        if (fullComment == null)
            return StatusCode(500, "Failed to load comment after creation");

        return Ok(new CommentDto(
            fullComment.Id,
            fullComment.Body,
            fullComment.UserId,
            fullComment.PostId,
            fullComment.User.UserName
        ));
    }

    [HttpGet("post/{postId:int}")]
    public ActionResult<List<CommentDto>> GetForPost(int postId)
    {
        var comments = _commentRepository.GetManyByPost(postId)
            .Select(c => new CommentDto(
                c.Id,
                c.Body,
                c.UserId,
                c.PostId,
                c.User.UserName
            ))
            .ToList();

        return Ok(comments);
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<CommentDto>> UpdateComment(int id, UpdateCommentDto dto)
    {
        var comment = await _commentRepository.GetSingleAsync(id);
        if (comment is null) return NotFound("Comment not found");

        // AUTHORIZATION CHECK
        if (comment.UserId != dto.UserId)
            return Unauthorized("You cannot edit someone else's comment.");

        comment.Body = dto.Body;

        await _commentRepository.UpdateAsync(comment);

        var updated = await _commentRepository.GetSingleAsync(id);

        return Ok(new CommentDto(
            updated.Id,
            updated.Body,
            updated.UserId,
            updated.PostId,
            updated.User.UserName
        ));
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteComment(int id, [FromQuery] int userId)
    {
        var comment = await _commentRepository.GetSingleAsync(id);
        if (comment is null) return NotFound("Comment not found");

        // AUTHORIZATION
        if (comment.UserId != userId)
            return Unauthorized("You cannot delete someone else's comment.");

        await _commentRepository.DeleteAsync(comment);

        return NoContent();
    }


}