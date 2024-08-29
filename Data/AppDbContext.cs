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

    public DbSet<LoginForm> Login { get; set; }
    public DbSet<RegisterForm> Register { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //User -> Authentication Log
        //  one-to-many
        modelBuilder.Entity<Users>()
            .HasMany(u => u.AuthenticationLog)
            .WithOne(al => al.User)
            .HasForeignKey(al => al.UserId);

        //User -> UserSecurity
        //  one-to-one
        modelBuilder.Entity<Users>()
            .HasOne(u => u.UserSecurity)
            .WithOne(us => us.Users)
            .HasForeignKey<UserSecurity>(us => us.UserId);

        //User -> Account
        //  one-to-one
        modelBuilder.Entity<Users>()
            .HasOne(u => u.Account)
            .WithOne(a => a.User)
            .HasForeignKey<Account>(a => a.UserId);

        //Do not understand this
        //Account -> Transaction
        //  one-to-many
        //      AccountId - FromAccountId
        //      AccountId - ToAccountId
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.FromAccount)
            .WithMany(a => a.FromTransactions)
            .HasForeignKey(t => t.FromAccountId);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.ToAccount)
            .WithMany(a => a.ToTransactions)
            .HasForeignKey(t => t.ToAccountId);

        //Status -> Account
        modelBuilder.Entity<Status>()
            .HasMany(s => s.Accounts)
            .WithOne(a => a.Status)
            .HasForeignKey(a => a.AccountStatusId);
    }
}