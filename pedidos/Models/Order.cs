using System.ComponentModel.DataAnnotations;

public class Order
    {
        [Key]
        public int? id { get; set; }
        [StringLength(maximumLength: 150)]
        public string? nameCustomer { get; set; }
        public float? count { get; set; }
        public DateTime date { get; set; }
    }