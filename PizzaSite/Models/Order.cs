using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PizzaShop.Models;

[Index("CustomerId", Name = "IX_Orders_CustomerId")]
public partial class Order
{
    public Order()
    {
        OrderDetails = new HashSet<OrderDetail>();
    }

    [Key]
    public int Id { get; set; }
    [Display(Name = "Placed")]
    public DateTime OrderPlaced { get; set; }
    [Display(Name = "Delivered")]
    public DateTime? OrderFulfilled { get; set; }
    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    [Display(Name = "Person")]
    public virtual Customer Customer { get; set; } = null!;
    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    public override string ToString() => $"{Id} Customer: {CustomerId}";

}