using Microsoft.EntityFrameworkCore;
using SharedLibrary;

namespace GameSever;

public class GameDbContext : DbContext
{
    
    
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {
        
    }
    
    //Database gateway injection
    
    public DbSet<User> Users { get; set; }

}
