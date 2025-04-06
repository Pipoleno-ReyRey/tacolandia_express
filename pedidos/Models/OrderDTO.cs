public class OrderDTO{
    public int? id { get; set; }
    public string? customer { get; set; }
    public List<DishDTO>? dishes { get; set; }
    public float? count { get; set; }
    public DateTime date { get; set; }

}