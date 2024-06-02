using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlazorAdmin.Services;

public class OrderService : IOrderService
{
    private readonly ICatalogLookupDataService<CatalogBrand> _brandService;
    private readonly ICatalogLookupDataService<CatalogType> _typeService;
    private readonly HttpService _httpService;
    private readonly ILogger<OrderService> _logger;
    private readonly HttpClient _httpClient;
    private readonly IMediator _mediator;

    public OrderService(ICatalogLookupDataService<CatalogBrand> brandService,
        ICatalogLookupDataService<CatalogType> typeService,
        HttpService httpService,
        ILogger<OrderService> logger,
        HttpClient httpClient
        )
    {
        _brandService = brandService;
        _typeService = typeService;
        _httpService = httpService;
        _logger = logger;
        _httpClient = httpClient;
    }
    public async Task<List<Order>> List()
    {
        _logger.LogInformation("Fetching order items from API.");

        
        var itemListTask = _httpService.HttpGet<PagedOrderResponse>($"orders");


        var orderitemListTask = _httpService.HttpGet<PagedOrderItemResponse>($"order-items");
      


        await Task.WhenAll(itemListTask, orderitemListTask );

        
        var pagedResponse = await itemListTask;
        var orderpagedResponse = await orderitemListTask;

        
        if (pagedResponse == null || pagedResponse.Orders == null)
        {
            _logger.LogError("API response is null or OrderItems is null.");
            return new List<Order>();
        }

        
        var items = pagedResponse.Orders;
        var orderitems = orderpagedResponse.OrderItems;


        foreach (var item in items)
        {
            var relatedOrderItems = orderitems.Where(oi => oi.OrderId == item.Id);

            decimal total = 0;
            foreach (var orderItem in relatedOrderItems)
            {
                total += orderItem.Units * orderItem.UnitPrice;
            }
            item.Total = total;
        }

        if (items == null || items.Count == 0)
        {
            _logger.LogWarning("No order items found in API response.");
        }
      

        return items;
    }

    public async Task<List<OrderItem>> ListByOrderId(int id)
    {
        _logger.LogInformation("Fetching order items from API.");

        var orderitemListTask = _httpService.HttpGet<PagedOrderItemResponse>($"order-items/{id}");

        await Task.WhenAll(orderitemListTask);

        var pagedResponse = await orderitemListTask;

        var items = pagedResponse.OrderItems;

        return items;
    }

    public async Task<Order> Edit(Order order)
    {
        return (await _httpService.HttpPut<EditOrderItemResult>("orders", order)).Order;
    }

    public async Task<Order> GetById(int id)
    {
        
        var itemGetTask = _httpService.HttpGet<EditOrderItemResult>($"orders/{id}");

        await Task.WhenAll(itemGetTask);

        var order = await itemGetTask;

        var item = order.Order;
        
        return item;
    }
}
