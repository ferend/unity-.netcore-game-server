namespace GameSever.Services;

public interface IPlayerService
{
    void DoAdd();
}
public class PlayerServices : IPlayerService
{
    public void DoAdd()
    {
        Console.WriteLine("Doadd initialized  ");
    }
}

public class MockPlayerServices : IPlayerService
{
    public void DoAdd()
    {
        Console.WriteLine(" Doadd initialized from mock playerservice  ");
    }
}
