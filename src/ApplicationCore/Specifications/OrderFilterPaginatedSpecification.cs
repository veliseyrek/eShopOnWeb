using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class OrderFilterPaginatedSpecification : Specification<Order>
{
    public OrderFilterPaginatedSpecification(int skip, int take, int? brandId, int? typeId)
        : base()
    {
        //if (take == 0)
        //{
        //    take = int.MaxValue;
        //}
        //Query
        //    .Where(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
        //    (!typeId.HasValue || i.CatalogTypeId == typeId))
        //    .Skip(skip).Take(take);
    }
}
