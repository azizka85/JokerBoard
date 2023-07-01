using EQUTech.Core.Grpc.Models.JokerBoard;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http.Json;

namespace EQUTech.JokerBoard.Web.Test.IntegrationTests;

[TestClass]
public sealed class CategoryTest
{
    private readonly TestServer _server;
    private readonly HttpClient _client;

    public CategoryTest()
    {
        _server = new TestServer
        (
            new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(ConfigurationManager.Configuration)
        );

        _client = _server.CreateClient();
    }

    [TestMethod]
    public void List()
    {
        var response = _client.GetAsync("/category/list").Result;

        response.EnsureSuccessStatusCode();

        var result = response.Content.ReadFromJsonAsync<List<CategoryItem>>().Result;

        Assert.IsNotNull(result);
    }
}
