﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

/// <summary>
/// Updates a Catalog Item
/// </summary>
public class UpdateOrderEndpoint : IEndpoint<IResult, UpdateOrderRequest, IRepository<Order>>
{ 
    private readonly IUriComposer _uriComposer;

    public UpdateOrderEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/orders",
            [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
            (UpdateOrderRequest request, IRepository<Order> itemRepository) =>
            {
                return await HandleAsync(request, itemRepository);
            })
            .Produces<UpdateOrderResponse>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateOrderRequest request, IRepository<Order> itemRepository)
    {
        var response = new UpdateOrderResponse(request.CorrelationId());

        var existingItem = await itemRepository.GetByIdAsync(request.Id);

        if (existingItem == null)
        {
            return Results.NotFound();
        }

        existingItem.SetStatus("Approved");
       

        await itemRepository.UpdateAsync(existingItem);

        var dto = new OrderDto
        {
            Id = existingItem.Id,
          BuyerId= existingItem.BuyerId,
          Status = existingItem.Status,
          OrderDate = existingItem.OrderDate,
          Total = existingItem.Total()
        };
        response.Order = dto;
        return Results.Ok(response);
    }
}
