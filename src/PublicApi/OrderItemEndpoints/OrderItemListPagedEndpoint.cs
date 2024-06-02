using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

/// <summary>
/// List Catalog Types
/// </summary>
public class OrderItemListPagedEndpoint : IEndpoint<IResult, IRepository<OrderItem>>
{
    private readonly IMapper _mapper;

    public OrderItemListPagedEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/order-items",
            async (IRepository<OrderItem> orderRepository) =>
            {
                return await HandleAsync(orderRepository);
            })
            .Produces<ListPagedOrderItemResponse>()
            .WithTags("OrderItemEndpoints");
    }

    public async Task<IResult> HandleAsync(IRepository<OrderItem> orderRepository)
    {
        var response = new ListPagedOrderItemResponse();

        var items = await orderRepository.ListAsync();

        response.OrderItems.AddRange(items.Select(_mapper.Map<OrderItemDto>));

        return Results.Ok(response);
    }
}
