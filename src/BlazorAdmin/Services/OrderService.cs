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

public class OrderService : BlazorShared.Interfaces.IOrderService
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

    //public async Task<List<OrderViewModel>> GetOrders()
    //{
    //    var items = await _orderService.GetOrders();

    //    List<OrderViewModel> orderViewModels = new List<OrderViewModel>();

    //    foreach (var order in items)
    //    {
    //        var orderViewModel = new OrderViewModel
    //        {
    //            BuyerId = order.BuyerId,
    //            OrderDate = order.OrderDate,
    //            Status = order.Status,
    //            Total = order.Total()
    //        };

    //        orderViewModels.Add(orderViewModel);
    //    }


    //    return orderViewModels;
    //}
    //public async Task<List<Order>> ListPaged(int pageSize)
    //{
    //    _logger.LogInformation("Fetching order items from API.");

    //    var brandListTask = _brandService.List();
    //    var typeListTask = _typeService.List();
    //    var itemListTask = _httpService.HttpGet<PagedOrderResponse>($"orders?PageSize=10");
    //    await Task.WhenAll(brandListTask, typeListTask, itemListTask);
    //    var brands = brandListTask.Result;
    //    var types = typeListTask.Result;
    //    var items = itemListTask.Result.Orders;
    //    return items;
    //}
    //public async Task<List<OrderItem>> List()
    //{
    //    _logger.LogInformation("Fetching order items from API.");
    //    var brandListTask = _brandService.List();
    //    var typeListTask = _typeService.List();
    //    var itemListTask = _httpService.HttpGet<PagedOrderItemResponse>($"orders");
    //    // Tüm görevlerin tamamlanmasını bekleyin
    //    await Task.WhenAll(brandListTask, typeListTask, itemListTask);

    //    //var response = await _httpClient.GetFromJsonAsync<OrderItem>($"https://localhost:5099/api/orders");

    //    // Görevlerin sonuçlarını await kullanarak alın
    //    var brands = await brandListTask;
    //    var types = await typeListTask;
    //    var items = (await itemListTask).OrderItems;
    //    return items;
    //}

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
        //var name = items[0].BuyerId;
        //var viewModel = await _mediator.Send(new GetMyOrders(name));

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



}
