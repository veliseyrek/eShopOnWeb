using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class ListPagedOrderItemResponse : BaseResponse
{
    public ListPagedOrderItemResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedOrderItemResponse()
    {
    }

    public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}
