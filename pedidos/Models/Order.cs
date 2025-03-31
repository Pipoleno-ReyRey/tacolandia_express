using System.ComponentModel.DataAnnotations;

public class Order
    {
        [Key]
        public int? id { get; set; }
        [StringLength(maximumLength: 150)]
        public string? nameCustomer { get; set; }
        public string? dishesOrded { get; set; }
        public float? count { get; set; }
        public DateTime date { get; set; }

        public Order(string nameCustomer, string dishesOrded, float count) 
        { 
            this.nameCustomer = nameCustomer;
            this.count = count;
            date = DateTime.Today;
        }
    }