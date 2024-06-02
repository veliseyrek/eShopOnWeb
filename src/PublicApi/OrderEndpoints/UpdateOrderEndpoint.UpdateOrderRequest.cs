using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class UpdateOrderRequest : BaseRequest
{
    [Range(1, 10000)]
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public string Status { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal Total { get; set; }
}
