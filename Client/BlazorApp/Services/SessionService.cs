namespace BlazorApp.Services;

using ApiContracts;

public class SessionService
{
    private UserDto? _currentUser;

    public bool IsLoggedIn => _currentUser is not null;

    public UserDto? CurrentUser => _currentUser;

    public void SetUser(UserDto user)
    {
        _currentUser = user;
    }

    public void Clear()
    {
        _currentUser = null;
    }
    
    public void Logout()
    {
        _currentUser = null;
    }
}
