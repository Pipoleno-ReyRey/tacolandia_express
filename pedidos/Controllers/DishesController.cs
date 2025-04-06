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
    public async Task<ActionResult<List<DishDTO>>> GetDishes(){
        var dishesList = await ordersDishDb.Dish.ToListAsync();
        if(dishesList.Count>0 && !(await dishesService.GetDishes(dishesList)).Any(x => x.Id == null)){
            return Ok(await dishesService.GetDishes(dishesList));
        } else{
            return BadRequest((await dishesService.GetDishes(dishesList))[0].name);
        }   
    }

    [HttpGet("getDish/{id}")]
    public async Task<ActionResult<DishDTO>> GetDish(int id){
        if((await dishesService.GetDish(id)).Id == null){
            return NotFound((await dishesService.GetDish(id)).name);
        } else{
            return Ok(await dishesService.GetDish(id));
        }
    }

    [HttpPost("postDish")]
    public async Task<IActionResult> PostDish([FromBody]DishDTO dishDTO){
        return Ok(await dishesService.AddDish(dishDTO));
    }

    [HttpDelete("deleteDish/{id}")]
    public async Task<ActionResult<string>> DeleteDish(int id){
        if(await dishesService.DeleteDish(id) == "eliminar"){
            return Ok(await dishesService.DeleteDish(id));
        } else{
            return BadRequest(await dishesService.DeleteDish(id));
        }
        
    }
}