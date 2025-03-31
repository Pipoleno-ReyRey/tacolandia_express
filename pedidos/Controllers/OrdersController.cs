using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("orders/")]
public class OrdersController: ControllerBase{
    private readonly OrdersDishDb ordersDishDb;
    public OrdersController(OrdersDishDb ordersDishDb){
        this.ordersDishDb = ordersDishDb;
    }

    [HttpGet("getOrders")]
    public async Task<List<Order>> GetOrders(){
        return await ordersDishDb.Order.ToListAsync();
    }

    [HttpPost("postOrder/{customer}+{dishes(divided by ,)}")]
    public async Task<Order> PostOrders(string customer, string dishes){
        var count = 0.0f;
        var _dishes = dishes.Split(',');
        var menu = await ordersDishDb.Dish.ToListAsync();
        foreach(var dish in _dishes){
            
        }
        var order = new Order(customer, dishes, count);
        await ordersDishDb.Order.AddAsync(order);
        return order;
    }
}