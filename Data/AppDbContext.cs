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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            //u == user ; al == authentication log
        modelBuilder.Entity<Users>()
            .HasMany(u => u.AuthenticationLog)
            .WithOne(al => al.Users)
            .HasForeignKey(al => al.UserId);

        //Do not understand this
        //Account -> Transaction
        //  one-to-many
        //      AccountId - FromAccountId
        //      AccountId - ToAccountId
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.FromAccount)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.FromAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.ToAccount)
            .WithMany()
            .HasForeignKey(t => t.ToAccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}