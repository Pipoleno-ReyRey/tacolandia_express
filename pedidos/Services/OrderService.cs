using Microsoft.EntityFrameworkCore;

public class OrderService{
    private readonly OrdersDishDb ordersDishDb;

    public OrderService(OrdersDishDb ordersDishDb){
        this.ordersDishDb=ordersDishDb;
    }

    public async Task<Order> PostOrder(OrderDTO orderDTO){
        Order order = new Order();
        try{
            List<DishDTO> dishes = orderDTO.dishes!;
            float? count = 0;
            foreach(DishDTO dish in dishes){
                foreach(Ingredient d in dish.ingredients!){
                    dish.price += d.price;
                }
                count += dish.price;
            }

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
        } catch(Exception error){
            order.nameCustomer = error.Message;
            return order;
        }
        
    }

    public async Task<List<OrderDTO>> GetOrders(){
        var ordersDTO = new List<OrderDTO>();
        try{
            var orders = await ordersDishDb.Order.ToListAsync();
            var dishes = await ordersDishDb.Dish.ToListAsync();
            var orderDishes = await ordersDishDb.dishesOrders.ToListAsync();
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
                    dishes = dishesOrder,
                    count = order.count,
                    date = order.date
                });
            }

            return ordersDTO;
        }catch(Exception error){
            ordersDTO.Add(new OrderDTO(){id = null, customer = error.Message});
            return ordersDTO;
        }
        
    }

    public async Task<string> DeleteOrder(int id){
        await ordersDishDb.Order.Where(order => order.id == id).ExecuteDeleteAsync();
        await ordersDishDb.dishesOrders.Where(order => order.orderId == id).ExecuteDeleteAsync();
        return "orden borrada";
    }

    public async Task<OrderDTO> GetOrder(int id){
        var order = new OrderDTO();
        try{
            var orderDB = await ordersDishDb.Order.FirstOrDefaultAsync(x => x.id == id);
            var dishesOrder = await ordersDishDb.dishesOrders.Where(x => x.orderId == id).ToListAsync();
            foreach(var dish in dishesOrder){
                var dishDB = await ordersDishDb.Dish.FirstOrDefaultAsync(x => x.Id == dish.dishId);
                var ingredientsList = new List<Ingredient>();
                foreach(var ingredient in await ordersDishDb.ingredientDishes.Where(x => x.dishId == dish.dishId).ToListAsync()){
                    var x = await ordersDishDb.ingredients.FirstOrDefaultAsync(x => x.Id == ingredient.ingredientId)!;
                    ingredientsList.Add(new Ingredient(){Id = x!.Id, name = x.name, price = x.price,});
                }
                order.dishes!.Add(new DishDTO(){Id = dishDB!.Id, name = dishDB.name, description = dishDB.description, ingredients = ingredientsList, img = dishDB.img, price = dishDB.price});
            }

            order.id = orderDB!.id;
            order.customer = orderDB.nameCustomer;
            order.count = orderDB.count;
            order.date = orderDB.date;
            return order;
        } catch(Exception error){
            order.customer = error.Message;
            return order;
        }
        
    }
}