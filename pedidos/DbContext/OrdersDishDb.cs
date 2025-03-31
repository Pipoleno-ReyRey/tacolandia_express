using Microsoft.EntityFrameworkCore;

public class OrdersDishDb: DbContext{
    public DbSet<Order> Order { get; set; }
    public DbSet<Dish> Dish { get; set; }
    public DbSet<Ingredient> ingredients{ get; set; }
    public DbSet<IngredientDish> ingredientDishes{ get; set; }
    public OrdersDishDb(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IngredientDish>().HasKey(d => new { d.ingredientId, d.dishId });
    }
}