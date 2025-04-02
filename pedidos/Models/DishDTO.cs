public class DishDTO
    {
        public int? Id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public List<Ingredient>? ingredients { get; set; }
        public float? price { get; set; }
        public string? img { get; set; }

        public Dish GetDish(){
            var dish = new Dish();
            dish.name = name;
            dish.description = description;
            dish.price = price;
            dish.img = img;

            return dish;
        }

    }