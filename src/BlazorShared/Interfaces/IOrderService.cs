using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;
public interface IOrderService
{
    Task<List<Order>> List();
    Task<Order> GetById(int id);
    Task<Order> Edit(Order order);
    Task<List<OrderItem>> ListByOrderId(int id);
}
