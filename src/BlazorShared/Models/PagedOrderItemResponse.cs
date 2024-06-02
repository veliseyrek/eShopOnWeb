using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Models;
public class PagedOrderItemResponse
{
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public int PageCount { get; set; } = 0;
}
