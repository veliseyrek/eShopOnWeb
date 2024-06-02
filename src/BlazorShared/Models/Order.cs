using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Models;
public class Order
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public string Status { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal Total { get; set; }
}
