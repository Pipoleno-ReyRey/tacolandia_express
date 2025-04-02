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
    public async Task<Order> GetOrders([FromBody] OrderDTO orderDTO){
        return await orderService.PostOrder(orderDTO);
    }

    [HttpGet("getOrders")]
    public async Task<List<OrderDTO>> GetOrders(){
        return await orderService.GetOrders();
    }
}