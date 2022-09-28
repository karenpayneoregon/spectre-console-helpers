using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShop.Models;

public partial class Customer
{
    public Customer()
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Orders = new HashSet<Order>();
    }

    [Key]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; }

    public override string ToString() => $"{FirstName} {LastName}";

}