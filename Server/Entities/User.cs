using System.Diagnostics.CodeAnalysis;

namespace Entities;

public class User
{
    public int Id { get; private set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }

    public List<Post> Posts { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();

    private User() { } // EF

    [SetsRequiredMembers]
    public User(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}