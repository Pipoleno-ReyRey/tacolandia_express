using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

[ApiController]
[Route("pedidos/")]
public class DishesController : ControllerBase{
    private readonly OrdersDishDb ordersDishDb;
    public DishesController(OrdersDishDb ordersDishDb){
        this.ordersDishDb = ordersDishDb;
    }

    [HttpGet("getDishes")]
    public async Task<List<Dish>> GetDishes(){
        return await ordersDishDb.Dish.ToListAsync();
    }

    [HttpPost("postDish")]
    public async Task<DishDTO> PostDish([FromBody]DishDTO dishDTO){
        var dish = dishDTO.GetDish();
        var ingredients = await ordersDishDb.ingredients.ToListAsync();
        var newIngredients = new List<Ingredient>();
        try{
            foreach(var ingrediente in dishDTO.ingredients!){
            if(!ingredients.Any(ing => ing.name == ingrediente.name && ing.price == ingrediente.price)){
                newIngredients.Add(ingrediente);
                }
            }

            await ordersDishDb.ingredients.AddRangeAsync(newIngredients);
            await ordersDishDb.Dish.AddAsync(dish);
            await ordersDishDb.SaveChangesAsync();

            var dishIngredients = new List<IngredientDish>();
            ingredients = await ordersDishDb.ingredients.ToListAsync();
            var idDish = dish.Id;
            foreach(var ingrediente in dishDTO.ingredients!){
                if(ingredients.Any(ing => ing.name == ingrediente.name && ing.price == ingrediente.price)){
                    var idIngredient = ingredients.FirstOrDefault(ing => ing.name == ingrediente.name && ing.price == ingrediente.price)!.Id;
                    var ingredientDish = new IngredientDish();
                    ingredientDish.dishId = idDish;
                    ingredientDish.ingredientId = idIngredient;
                    dishIngredients.Add(ingredientDish);
                }
            }
            
            await ordersDishDb.AddRangeAsync(dishIngredients);
            await ordersDishDb.SaveChangesAsync();

        } catch(MySqlException error){
            dish = new Dish();
            dish.name = error.Message;
        }
        
        return dishDTO;
    }

}