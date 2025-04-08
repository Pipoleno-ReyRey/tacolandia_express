using System.ComponentModel.DataAnnotations;

public class User{
    [Key]
    public int? id { get; set; }
    [StringLength(maximumLength: 150)]
    public string? name { get; set;}
    [StringLength(maximumLength: 150)]
    public string? email { get; set;}
    [StringLength(maximumLength: 150)]
    public string? phone { get; set;}
    [StringLength(maximumLength: 150)]
    public string? password { get; set;}
    [StringLength(maximumLength:5)]
    public string? role { get; set; }
}