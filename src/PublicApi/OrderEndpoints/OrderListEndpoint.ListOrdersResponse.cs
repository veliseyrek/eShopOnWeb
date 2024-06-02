using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.PublicApi.CatalogTypeEndpoints;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class ListOrdersResponse : BaseResponse
{
    public ListOrdersResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListOrdersResponse()
    {
    }

    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
}
