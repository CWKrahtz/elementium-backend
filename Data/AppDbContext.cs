using Microsoft.EntityFrameworkCore;

namespace elementium_backend;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    //TODO: All my Endpoint controllers
    //Can use if statements for permissions
    public DbSet<Users> users {get; set;}
}
