namespace ApiContracts;

public record PostDto(
    int Id,
    string Title,
    string Body,
    int UserId
);