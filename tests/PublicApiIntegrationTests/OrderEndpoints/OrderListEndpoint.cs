using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
namespace PublicApiIntegrationTests.OrderEndpoints;
[TestClass]
public class OrderListEndpoint
{

    [TestMethod]
    public async Task ReturnsCorrectOrdersGivenPageIndex1()
    {
        
        var client = ProgramTest.NewClient;
        var response = await client.GetAsync($"/api/orders");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<ListOrdersResponse>();
        var totalItem = model!.Orders.Count();

        var response2 = await client.GetAsync($"/api/orders");
        response.EnsureSuccessStatusCode();
        var stringResponse2 = await response2.Content.ReadAsStringAsync();
        var model2 = stringResponse2.FromJson<ListOrdersResponse>();

        var totalExpected = totalItem;

        Assert.AreEqual(0, totalItem);
    }

    [DataTestMethod]
    [DataRow("orders")]
    public async Task SuccessFullMutipleParallelCall(string endpointName)
    {
        var client = ProgramTest.NewClient;
        var tasks = new List<Task<HttpResponseMessage>>();

        for (int i = 0; i < 100; i++)
        {
            var task = client.GetAsync($"/api/{endpointName}");
            tasks.Add(task);
        }
        await Task.WhenAll(tasks.ToList());
        var totalKO = tasks.Count(t => t.Result.StatusCode != HttpStatusCode.OK);

        Assert.AreEqual(0, totalKO);
    }

    //[TestMethod]
    //public async Task ReturnsFourOrders()
    //{
    //    var client = ProgramTest.NewClient;
    //    var response = await client.GetAsync("/api/orders");
    //    response.EnsureSuccessStatusCode();
    //    var stringResponse = await response.Content.ReadAsStringAsync();
    //    var model = stringResponse.FromJson<ListOrdersResponse>();

    //    Assert.AreEqual(4, model!.Orders.Count());
    //}
}
