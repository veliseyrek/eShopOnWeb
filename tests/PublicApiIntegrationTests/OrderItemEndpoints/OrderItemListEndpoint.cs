using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace PublicApiIntegrationTests.OrderItemEndpoints;
[TestClass]
public class OrderItemListEndpoint
{
    [TestMethod]
    public async Task ReturnsCorrectOrderItemsGivenPageIndex1()
    {

        var client = ProgramTest.NewClient;
        var response = await client.GetAsync($"/api/order-items");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<ListPagedOrderItemResponse>();
        var totalItem = model!.OrderItems.Count();

        var response2 = await client.GetAsync($"/api/order-items");
        response.EnsureSuccessStatusCode();
        var stringResponse2 = await response2.Content.ReadAsStringAsync();
        var model2 = stringResponse2.FromJson<ListPagedOrderItemResponse>();

        var totalExpected = totalItem;

        Assert.AreEqual(0, totalItem);
    }
}
