namespace ASP_NET_Securing_JWT.Users;

public class User
{
    public string Fname { get; set; } = string.Empty;
    public string Lname { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
}