using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

[ApiController]
[Route("pedidos/")]
public class DishesController : ControllerBase{
    private readonly OrdersDishDb ordersDishDb;
    private readonly DishesService dishesService;
    public DishesController(OrdersDishDb ordersDishDb, DishesService dishesService){
        this.ordersDishDb = ordersDishDb;
        this.dishesService = dishesService;
    }

    [HttpGet("getDishes")]
    public async Task<List<DishDTO>> GetDishes(){
        var dishesList = await ordersDishDb.Dish.ToListAsync();
        return await dishesService.GetDishes(dishesList);
    }

    [HttpPost("postDish")]
    public async Task<DishDTO> PostDish([FromBody]DishDTO dishDTO){
        return await dishesService.AddDish(dishDTO);
    }

    [HttpDelete("deleteDish/{id}")]
    public async Task<string> DeleteDish(int id){
        await ordersDishDb.Dish.Where(d => d.Id == id).ExecuteDeleteAsync();
        return "plato borrado";
    }
}