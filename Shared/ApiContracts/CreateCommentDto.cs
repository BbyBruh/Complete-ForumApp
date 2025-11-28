namespace ApiContracts;

public record CreateCommentDto(string Body, int UserId, int PostId);