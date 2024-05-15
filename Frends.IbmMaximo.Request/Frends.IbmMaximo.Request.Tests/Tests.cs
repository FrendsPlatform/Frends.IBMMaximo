using Frends.IbmMaximo.Request.Definitions;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using RestSharp;

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