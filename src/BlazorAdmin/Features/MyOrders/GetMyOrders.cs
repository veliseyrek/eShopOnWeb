using System.Collections.Generic;
using BlazorShared.Models.ViewModel;
using MediatR;

namespace BlazorAdmin.Features.MyOrders;


public class GetMyOrders : IRequest<IEnumerable<OrderViewModel>>
{
    public string UserName { get; set; }

    public GetMyOrders(string userName)
    {
        UserName = userName;
    }
}
