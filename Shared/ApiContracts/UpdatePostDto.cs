namespace ApiContracts;

public record UpdatePostDto(
    string Title,
    string Body,
    int UserId
);