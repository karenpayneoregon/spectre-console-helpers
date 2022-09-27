using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PizzaShop.Models;

[Index("OrderId", Name = "IX_OrderDetails_OrderId")]
[Index("ProductId", Name = "IX_OrderDetails_ProductId")]
public partial class OrderDetail
{
    [Key]
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderDetails")]
    public virtual Order Order { get; set; } = null!;
    [ForeignKey("ProductId")]
    [InverseProperty("OrderDetails")]
    public virtual Product Product { get; set; } = null!;

    public override string ToString() => $"{Id} Order id: {OrderId}";

}