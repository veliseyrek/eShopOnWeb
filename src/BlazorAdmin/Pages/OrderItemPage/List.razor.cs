using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorAdmin.Services;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace BlazorAdmin.Pages.OrderItemPage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IOrderService OrderItemService { get; set; }


    private List<Order> orderItems = new List<Order>();

    private Details DetailsComponent { get; set; }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            orderItems = await OrderItemService.List();

            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async void DetailsClick(int id)
    {
        await DetailsComponent.Open(id);
    }

    private async Task ReloadOrders()
    {
        orderItems = await OrderItemService.List();
        StateHasChanged();
    }
}
