using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class Items
    {
        [Key]
        public int? id {  get; set; }
        [StringLength(maximumLength:150)]
        public string? name { get; set; }
        [StringLength(maximumLength: 150)]
        public string? description { get; set; }
        public int? amount {  get; set; }

    }
}
