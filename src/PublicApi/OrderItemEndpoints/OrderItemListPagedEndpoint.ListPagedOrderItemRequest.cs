namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class ListPagedOrderItemRequest : BaseRequest
{
    public int? OrderId { get; init; }

    public ListPagedOrderItemRequest(int? orderId)
    {
       OrderId = orderId;
    }
}
