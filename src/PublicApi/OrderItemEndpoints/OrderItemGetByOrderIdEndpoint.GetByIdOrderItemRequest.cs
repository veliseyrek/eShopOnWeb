namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class GetByIdOrderItemRequest : BaseRequest
{
    public int OrderItemId { get; init; }

    public GetByIdOrderItemRequest(int orderItemId)
    {
        OrderItemId = orderItemId;
    }
}
