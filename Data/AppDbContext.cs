using Microsoft.EntityFrameworkCore;
using elementium_backend;

namespace elementium_backend;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    //TODO: All my Endpoint controllers
    //Can use if statements for permissions

    //public DBSet<ModelName> databasename {get; set;}
    public DbSet<Users> users { get; set; }
    public DbSet<UserSecurity> user_security { get; set; }
    public DbSet<Status> status { get; set; }
    public DbSet<AuthenticationLog> AuthenticationLogs { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Account> Accounts { get; set; }
}