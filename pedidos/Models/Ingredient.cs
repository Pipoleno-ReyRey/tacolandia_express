using System.ComponentModel.DataAnnotations;

public class Ingredient
    {
        [Key]
        public int? Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string? name { get; set; }
        public float? price { get; set; }
    }