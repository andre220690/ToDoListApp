namespace ToDoListApp.Auth.Models;

public class AuthResponse
{
    public string Token { get; set; }
    public bool IsAuthorized { get; set; }
}
