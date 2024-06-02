using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderGetByIdEndpoint : IEndpoint<IResult, GetByIdOrderRequest, IRepository<Order>>
{
    private readonly IUriComposer _uriComposer;
    private readonly IMapper _mapper;

    public OrderGetByIdEndpoint(IUriComposer uriComposer, IMapper mapper)
    {
        _uriComposer = uriComposer;
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders/{Id}",
            async (int Id, IRepository<Order> itemRepository) =>
            {
                return await HandleAsync(new GetByIdOrderRequest(Id), itemRepository);
            })
            .Produces<GetByIdOrderResponse>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(GetByIdOrderRequest request, IRepository<Order> itemRepository)
    {
        var response = new GetByIdOrderResponse(request.CorrelationId());

        var item = await itemRepository.GetByIdAsync(request.OrderId);
        if (item is null)
            return Results.NotFound();

        response.Order = new OrderDto
        {
            Id = item.Id,
          BuyerId= item.BuyerId,
          OrderDate = item.OrderDate,
          Status = item.Status
        };
        return Results.Ok(response);
    }
}

