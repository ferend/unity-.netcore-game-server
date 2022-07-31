using System.Text.Json.Serialization;

namespace SharedLibrary;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }


    // one-to-many relation update database // <dotnet ef update>ß 
    public User User{ get; set; }

}
