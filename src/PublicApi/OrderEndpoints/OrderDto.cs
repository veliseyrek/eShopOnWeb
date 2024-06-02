using System;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderDto
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public string Status { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal Total { get; set; }
    //public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    //public decimal Total()
    //{
    //    var total = 0m;
    //    foreach (var item in _orderItems)
    //    {
    //        total += item.UnitPrice * item.Units;
    //    }
    //    return total;
    //}
}
