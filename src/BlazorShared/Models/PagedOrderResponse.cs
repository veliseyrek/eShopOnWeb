using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Models;
public class PagedOrderResponse
{
    public List<Order> Orders { get; set; } = new List<Order>();
    public int PageCount { get; set; } = 0;
}
