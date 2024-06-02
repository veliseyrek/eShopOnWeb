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

    //[Microsoft.AspNetCore.Components.Inject]
    //public ICatalogItemService CatalogItemService { get; set; }

    //private List<CatalogItem> catalogItems = new List<CatalogItem>();


    private List<Order> orderItems = new List<Order>();

    private Details DetailsComponent { get; set; }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            orderItems = await OrderItemService.List();
            //catalogItems = await CatalogItemService.List();

            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    //[Microsoft.AspNetCore.Components.Inject]
    //public ICatalogItemService CatalogItemService1 { get; set; }

    //private List<CatalogItem> catalogItems1 = new List<CatalogItem>();
    //protected override async Task OnAfterRenderAsync(bool firstRender)
    //{
    //    if (firstRender)
    //    {
    //        catalogItems1 = await CatalogItemService1.List();

    //        CallRequestRefresh();
    //    }

    //    await base.OnAfterRenderAsync(firstRender);
    //}


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
