namespace ApiContracts;

public record CommentDto(
    int Id,
    string Body,
    int UserId,
    int PostId,
    string UserName
);