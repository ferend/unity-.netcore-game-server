namespace SharedLibrary;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }

    public List<Player> Players{ get; set; }

}
