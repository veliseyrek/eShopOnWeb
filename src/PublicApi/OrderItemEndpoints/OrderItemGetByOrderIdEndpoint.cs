﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

/// <summary>
/// Get a Catalog Item by Id
/// </summary>
public class OrderItemGetByOrderIdEndpoint : IEndpoint<IResult, GetByIdOrderItemRequest, IRepository<OrderItem>>
{
    private readonly IUriComposer _uriComposer;
    private readonly IMapper _mapper;

    public OrderItemGetByOrderIdEndpoint(IUriComposer uriComposer, IMapper mapper)
    {
        _uriComposer = uriComposer;
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/order-items/{orderItemId}",
            async (int orderItemId, IRepository<OrderItem> itemRepository) =>
            {
                return await HandleAsync(new GetByIdOrderItemRequest(orderItemId), itemRepository);
            })
            .Produces<GetByIdOrderItemResponse>()
            .WithTags("OrderItemEndpoints");
    }

    public async Task<IResult> HandleAsync(GetByIdOrderItemRequest request, IRepository<OrderItem> itemRepository)
    {
        var response = new GetByIdOrderItemResponse(request.CorrelationId());


        var pagedSpec = new OrderFilterSpecification(

           orderId: request.OrderItemId);

        var item = await itemRepository.ListAsync(pagedSpec);


        if (item is null)
            return Results.NotFound();


        response.OrderItems.AddRange(item.Select(_mapper.Map<OrderItemDto>));
       
        return Results.Ok(response);
    }
}

