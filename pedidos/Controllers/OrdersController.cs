using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("orders/")]
public class OrdersController: ControllerBase{
    private readonly OrderService orderService;
    public OrdersController(OrderService orderService){
        this.orderService = orderService;
    }

    [HttpPost("postOrders")]
    public async Task<IActionResult> PostOrder([FromBody] OrderDTO orderDTO){
        return Ok(await orderService.PostOrder(orderDTO));
    }

    [HttpGet("getOrders")]
    public async Task<ActionResult<List<OrderDTO>>> GetOrders(){
        if(!(await orderService.GetOrders()).Any(x => x.id == null)){
            return Ok(await orderService.GetOrders());
        } else{
            return BadRequest((await orderService.GetOrders())[0].customer);
        }
        
    }

    [HttpDelete("deleteOrder/{id}")]
    public async Task<IActionResult> DeleteOrder(int id){
        if(await orderService.DeleteOrder(id) == "eliminado"){
            return Ok(await orderService.DeleteOrder(id));
        } else {
            return BadRequest(await orderService.DeleteOrder(id));
        }
    }

    [HttpGet("getOrder/{id}")]
    public async Task<ActionResult<OrderDTO>> GetOrder(int id){
        if((await orderService.GetOrder(id)).id != null){
            return Ok(await orderService.GetOrder(id));
        } else{
            return BadRequest((await orderService.GetOrder(id)).customer);
        }
    }
}