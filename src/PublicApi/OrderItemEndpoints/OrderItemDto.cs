using System;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class OrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int Units { get; set; }
    public decimal UnitPrice { get; set; }
    public string ProductName { get; set; }
}
