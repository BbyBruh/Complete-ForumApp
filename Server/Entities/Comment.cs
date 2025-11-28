namespace Entities;

public class Comment
{
    public int Id { get; private set; }

    public required string Body { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;

    public Comment() { }

    public Comment(string body, int userId, int postId)
    {
        Body = body;
        UserId = userId;
        PostId = postId;
    }
}