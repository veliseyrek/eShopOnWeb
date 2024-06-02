using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace BlazorAdmin.Services;

public class CachedOrderServiceDecorator : IOrderService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly OrderService _orderItemService;
    private ILogger<CachedOrderServiceDecorator> _logger;

    public CachedOrderServiceDecorator(ILocalStorageService localStorageService,
        OrderService orderItemService,
        ILogger<CachedOrderServiceDecorator> logger)
    {
        _localStorageService = localStorageService;
        _orderItemService = orderItemService;
        _logger = logger;
    }

    public async Task<Order> Edit(Order order)
    {
        var result = await _orderItemService.Edit(order);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<Order> GetById(int id)
    {
        return (await List()).FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<Order>> List()
    {
        string key = "orders";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<Order>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading orders from local storage.");
            if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.UtcNow)
            {
                return cacheEntry.Value;
            }
            else
            {
                _logger.LogInformation($"Loading {key} from local storage.");
                await _localStorageService.RemoveItemAsync(key);
            }
        }

        var items = await _orderItemService.List();
        var entry = new CacheEntry<List<Order>>(items);
        await _localStorageService.SetItemAsync(key, entry);
        return items;
    }

    public async Task<List<OrderItem>> ListByOrderId(int id)
    {
        var items = await _orderItemService.ListByOrderId(id);
        return items;
    }

    private async Task RefreshLocalStorageList()
    {
        string key = "orders";

        await _localStorageService.RemoveItemAsync(key);
        var items = await _orderItemService.List();
        var entry = new CacheEntry<List<Order>>(items);
        await _localStorageService.SetItemAsync(key, entry);
    }
}
