using Frends.IbmMaximo.Request.Definitions;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Frends.IbmMaximo.Request.Tests;

[TestFixture]
class Tests
{
    private Mock<IRestClient> _restClientMock;

    [SetUp]
    public void Setup()
    {
        _restClientMock = new Mock<IRestClient>();
        IbmMaximo.RestClientConstructor = _ => _restClientMock.Object;
    }

    [Test]
    public async Task BasicTest()
    {
        SetupHttpRequest();

        var input = new Input
        {
            BaseUrl = "http://localhost:7001/maximo/oslc",
            Resource = "/os/mxapiasset",
            Payload = "",
            Method = HttpMethod.GET
        };

        var result = await IbmMaximo.Request(input, CancellationToken.None);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Response);
        Assert.AreEqual((string)result.Response.hello, "world");
        Assert.IsNull(result.Error);
    }

    [Test]
    public void GetMethodTest()
    {
        Assert.AreEqual(Method.Get, IbmMaximo.GetMethod(HttpMethod.GET));
        Assert.AreEqual(Method.Post, IbmMaximo.GetMethod(HttpMethod.POST));
        Assert.AreEqual(Method.Delete, IbmMaximo.GetMethod(HttpMethod.DELETE));
        Assert.AreEqual(Method.Put, IbmMaximo.GetMethod(HttpMethod.PUT));
        Assert.AreEqual(Method.Patch, IbmMaximo.GetMethod(HttpMethod.PATCH));
    }

    [Test]
    public async Task CreateWorkOrderTest()
    {
        SetupHttpRequest();

        var input = new Input
        {
            BaseUrl = "http://localhost:7001",
            ApiKey = "apikey",
            WorkOrderDescription = "decription",
            Site = "siteid",
            WorkOrderAssetNum = "assetnum",
            WorkOrderLocation = "location",
            ScheduledStart = "2024-01-01T00:00:00Z",

        };

        var result = await IbmMaximo.CreateWorkOrder(input, CancellationToken.None);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Response);
        Assert.AreEqual((string)result.Response.hello, "world");
        Assert.IsNull(result.Error);
    }

    [Test]
    public async Task GenerateServiceRequestTest()
    {
        SetupHttpRequest();

        var input = new Input
        {
            BaseUrl = "http://localhost:7001/maximo/oslc",
            ApiKey = "apikey",
            ServiceRequestDescription = "Service request description",
            ReportedBy = "ReportedBy",
            ServiceRequestLocation = "location",
            ServiceRequestAssetNum = "assetnum"
        };

        var result = await IbmMaximo.GenerateServiceRequest(input, CancellationToken.None);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Response);
        Assert.AreEqual((string)result.Response.hello, "world");
        Assert.IsNull(result.Error);
    }

    [Test]
    public async Task GetWorkOrderTest()
    {
        SetupHttpRequest();

        var input = new Input
        {
            BaseUrl = "http://localhost:7001",
            ApiKey = "apikey",
            WorkOrderId = "12345"
        };

        var result = await IbmMaximo.GetWorkOrder(input, CancellationToken.None);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Response);
        Assert.AreEqual((string)result.Response.hello, "world");
        Assert.IsNull(result.Error);
    }

    [Test]
    public async Task UpdateWorkOrderTest()
    {
        SetupHttpRequest();

        var input = new Input
        {
            BaseUrl = "http://localhost:7001",
            ApiKey = "apikey",
            WorkOrderId = "12345",
            Payload = new { description = "Updated description" }
        };

        var result = await IbmMaximo.UpdateWorkOrder(input, CancellationToken.None);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Response);
        Assert.AreEqual((string)result.Response.hello, "world");
        Assert.IsNull(result.Error);
    }

    [Test]
    public async Task DeleteWorkOrderTest()
    {
        SetupHttpRequest();

        var input = new Input
        {
            BaseUrl = "http://localhost:7001",
            ApiKey = "apikey",
            WorkOrderId = "12345"
        };

        var result = await IbmMaximo.DeleteWorkOrder(input, CancellationToken.None);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Response);
        Assert.AreEqual((string)result.Response.hello, "world");
        Assert.IsNull(result.Error);
    }

    [Test]
    public async Task GetServiceRequestTest()
    {
        SetupHttpRequest();

        var input = new Input
        {
            BaseUrl = "http://localhost:7001",
            ApiKey = "apikey",
            ServiceRequestId = "12345"
        };

        var result = await IbmMaximo.GetServiceRequest(input, CancellationToken.None);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Response);
        Assert.AreEqual((string)result.Response.hello, "world");
        Assert.IsNull(result.Error);
    }

    [Test]
    public async Task UpdateServiceRequestTest()
    {
        SetupHttpRequest();

        var input = new Input
        {
            BaseUrl = "http://localhost:7001",
            ApiKey = "apikey",
            ServiceRequestId = "12345",
            Payload = new { description = "Updated service request description" }
        };

        var result = await IbmMaximo.UpdateServiceRequest(input, CancellationToken.None);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Response);
        Assert.AreEqual((string)result.Response.hello, "world");
        Assert.IsNull(result.Error);
    }

    [Test]
    public async Task DeleteServiceRequestTest()
    {
        SetupHttpRequest();

        var input = new Input
        {
            BaseUrl = "http://localhost:7001",
            ApiKey = "apikey",
            ServiceRequestId = "12345"
        };

        var result = await IbmMaximo.DeleteServiceRequest(input, CancellationToken.None);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Response);
        Assert.AreEqual((string)result.Response.hello, "world");
        Assert.IsNull(result.Error);
    }


    private void SetupHttpRequest(bool success = true, string content = "{ \"hello\": \"world\" }")
    {
        _restClientMock.Setup(o => o
                .ExecuteAsync(
                    It.IsAny<RestRequest>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                IsSuccessStatusCode = success,
                Content = content,
                ResponseStatus = ResponseStatus.Completed
            });
    }
}
