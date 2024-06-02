using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class OrderFilterSpecification : Specification<OrderItem> 
{
    public OrderFilterSpecification(int? orderId) 
    {
        Query.Where(i => (!orderId.HasValue || i.OrderId == orderId));
    }
}
