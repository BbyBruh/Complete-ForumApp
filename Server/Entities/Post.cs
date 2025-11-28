namespace Entities;

public class Post
{
    public int Id { get; private set; }

    public required string Title { get; set; }
    public required string Body { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public List<Comment> Comments { get; set; } = new();

    public Post() { }

    public Post(string title, string body, int userId)
    {
        Title = title;
        Body = body;
        UserId = userId;
    }
}