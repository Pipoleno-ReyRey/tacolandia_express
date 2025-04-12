using Microsoft.EntityFrameworkCore;
using Storage.Models;

public class StorageDB: DbContext
{
    public DbSet<Items> items {  get; set; }
    public StorageDB(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
