﻿using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class GetByIdOrderResponse : BaseResponse
{
    public GetByIdOrderResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdOrderResponse()
    {
    }

    public OrderDto Order { get; set; }
}
