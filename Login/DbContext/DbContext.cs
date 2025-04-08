using Microsoft.EntityFrameworkCore;

public class LoginDB: DbContext {
    public DbSet<User> users { get; set; }
    public LoginDB(DbContextOptions options) : base(options) { 

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        
    }
}