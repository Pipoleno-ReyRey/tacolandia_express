using Microsoft.EntityFrameworkCore;

public class DishesService{
    private readonly OrdersDishDb ordersDishDb;
    public DishesService(OrdersDishDb _ordersDishDb){
        ordersDishDb = _ordersDishDb;
    }

    public async Task<DishDTO> AddDish(DishDTO dishDTO){
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

        } catch(Exception error){
            dish = new Dish();
            dish.name = error.Message;
        }
        return dishDTO;
    }

    public async Task<List<DishDTO>> GetDishes(List<Dish> dishesList){
        var dishes = new List<DishDTO>();

        try{
            var ingredientDish = await ordersDishDb.ingredientDishes.ToListAsync();
            var ingredients = await ordersDishDb.ingredients.ToListAsync();
            foreach(Dish dish in dishesList){
                List<Ingredient> ingredientsOfDish = new List<Ingredient>();
                var ingredientsList = ingredientDish.Where(ing => ing.dishId == dish.Id).ToList();
                foreach(IngredientDish ingredient in ingredientsList){
                    var ingredient_ = ingredients.FirstOrDefault(ing => ing.Id == ingredient.ingredientId)!;
                    ingredientsOfDish.Add(ingredient_);
                }
                DishDTO dishDTO = new DishDTO();
                dishDTO.Id = dish.Id;
                dishDTO.name = dish.name;
                dishDTO.description = dish.description;
                dishDTO.ingredients = ingredientsOfDish;
                dishDTO.price = dish.price;
                dishDTO.img = dish.img;

                dishes.Add(dishDTO);
            }
            return dishes;

        }catch(Exception error){
            dishes.Add(new DishDTO(){name = error.Message});
            return dishes;
        }
    
    }

    public async Task<DishDTO> GetDish(int id){
        try{
            var dish = await ordersDishDb.Dish.FirstOrDefaultAsync(d => d.Id == id);
            var dishIngredients = await ordersDishDb.ingredientDishes.Where(x => x.dishId == id).ToListAsync();
            var ingredients = new List<Ingredient>();
            foreach(var ingredients1 in dishIngredients){
                ingredients.Add(ordersDishDb.ingredients.FirstOrDefault(x => x.Id == ingredients1.ingredientId)!);
            }
            return new DishDTO(){Id = id, name = dish!.name, description = dish.description, ingredients = ingredients, price = dish.price, img = dish.img};
        
        } catch (Exception error){
            return new DishDTO(){name = error.Message};
        }
    }

    public async Task<string> DeleteDish(int id){
        try{
            await ordersDishDb.Dish.Where(x => x.Id == id).ExecuteDeleteAsync();
            await ordersDishDb.dishesOrders.Where(x => x.dishId == id).ExecuteDeleteAsync();
            await ordersDishDb.ingredientDishes.Where(x => x.dishId == id).ExecuteDeleteAsync();
            return "eliminado"; 
        }catch(Exception error){
            return error.Message;
        }

    } 
}