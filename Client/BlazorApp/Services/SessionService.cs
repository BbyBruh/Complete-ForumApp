namespace BlazorApp.Services;

using ApiContracts;

public class SessionService
{
    public UserDto? CurrentUser { get; private set; }

    public void SetUser(UserDto user)
    {
        CurrentUser = user;
    }

    public void Logout()
    {
        CurrentUser = null;
    }

    public bool IsLoggedIn => CurrentUser is not null;
}
