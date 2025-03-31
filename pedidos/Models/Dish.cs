using System.ComponentModel.DataAnnotations;

public class Dish
    {
        [Key]
        public int? Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string? name { get; set; }
        [StringLength(maximumLength: 170)]
        public string? description { get; set; }
        public float? price { get; set; }
        public string? img { get; set; }

    }