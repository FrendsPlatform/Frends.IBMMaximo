using Frends.IbmMaximo.Request.Definitions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Frends.IbmMaximo.Request;

/// <summary>
/// IBM Maximo Request Task.
/// </summary>
public static class IbmMaximo
{
    internal static Func<string, IRestClient> RestClientConstructor = baseUrl => new RestClient(baseUrl);

    /// <summary>
    /// Frends Task for making IBM Maximo requests.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.IbmMaximo.Request)
    /// </summary>
    /// <param name="input">Input parameters.</param>
    /// <param name="cancellationToken">Frends cancellation token.</param>
    /// <returns>Object { bool Success, string Error, dynamic Response }</returns>
    public static async Task<Result> Request(
        [PropertyTab] Input input,
        CancellationToken cancellationToken)
    {
        var client = RestClientConstructor(input.BaseUrl);
        var request = new RestRequest(
            $"{input.Resource}",
            GetMethod(input.Method));
        
        // https://developer.ibm.com/apis/catalog/maximo--maximo-manage-rest-api/Getting+Started
        request.AddHeader("apikey", input.ApiKey);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Content-Type", "application/json");
        request.AddBody(input.Payload, ContentType.Json);
        
        var result = await client.ExecuteAsync(request, cancellationToken);
        
        return new Result(result.IsSuccessful, JToken.Parse(result.Content), result.ErrorMessage);
    }

    internal static Method GetMethod(HttpMethod method)
    {
        return method switch
        {
            HttpMethod.GET => Method.Get,
            HttpMethod.POST => Method.Post,
            HttpMethod.PUT => Method.Put,
            HttpMethod.PATCH => Method.Patch,
            HttpMethod.DELETE => Method.Delete,
            _ => throw new Exception($"Unsupported HTTP method {method}.")
        };
    }
}