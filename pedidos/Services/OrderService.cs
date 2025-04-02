using Microsoft.EntityFrameworkCore;

public class OrderService{
    private readonly OrdersDishDb ordersDishDb;

    public OrderService(OrdersDishDb ordersDishDb){
        this.ordersDishDb=ordersDishDb;
    }

    public async Task<Order> PostOrder(OrderDTO orderDTO){
        List<DishDTO> dishes = orderDTO.dishes!;
        float? count = 0;
        foreach(DishDTO dish in dishes){
            foreach(Ingredient d in dish.ingredients!){
                dish.price += d.price;
            }
            count += dish.price;
        }

        Order order = new Order();
        order.nameCustomer = orderDTO.customer;
        order.count = count;
        order.date = DateTime.Now;

        await ordersDishDb.Order.AddAsync(order);
        await ordersDishDb.SaveChangesAsync();

        List<DishesOrder> dishesOrder = new List<DishesOrder>();
        foreach(DishDTO dish in dishes){
            var dishOrder = new DishesOrder();
            dishOrder.dishId = dish.Id;
            dishOrder.orderId = order.id;
            dishesOrder.Add(dishOrder);
        }

        await ordersDishDb.dishesOrders.AddRangeAsync(dishesOrder);
        await ordersDishDb.SaveChangesAsync();

        return order;
    }

    public async Task<List<OrderDTO>> GetOrders(){
        var orders = await ordersDishDb.Order.ToListAsync();
        var dishes = await ordersDishDb.Dish.ToListAsync();
        var orderDishes = await ordersDishDb.dishesOrders.ToListAsync();
        var ordersDTO = new List<OrderDTO>();
        foreach(Order order in orders){
            var dishesOrder = new List<DishDTO>();
            var orderDishes1 = await ordersDishDb.dishesOrders.Where(ord => ord.orderId == order.id).ToListAsync();
            foreach(var orderDish in orderDishes1){
                var x = ordersDishDb.Dish.FirstOrDefault(di => di.Id == orderDish.dishId)!;
                var dish = new DishDTO();
                dish.Id = x.Id;
                dish.name = x.name;
                dish.description = x.description;
                dish.ingredients = [];
                dish.price = x.price;
                dish.img = x.img;    
                dishesOrder.Add(dish);
            }
            ordersDTO.Add(new OrderDTO(){
                id = order.id,
                customer = order.nameCustomer,
                dishes = dishesOrder
            });
        }

        return ordersDTO;
    }
}