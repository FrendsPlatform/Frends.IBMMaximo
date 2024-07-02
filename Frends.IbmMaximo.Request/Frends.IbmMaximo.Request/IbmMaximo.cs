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
using System.Collections.Generic;
using System.Dynamic;

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
        switch (input.RequestType)
        {
            case RequestTypeChoose.CustomRequest:
                return await CustomRequest(input, cancellationToken);
            case RequestTypeChoose.CreateWorkOrder:
                return await CreateWorkOrder(input, cancellationToken);
            case RequestTypeChoose.GenerateServiceRequest:
                return await GenerateServiceRequest(input, cancellationToken);
            case RequestTypeChoose.GetWorkOrder:
                return await GetWorkOrder(input, cancellationToken);
            case RequestTypeChoose.UpdateWorkOrder:
                return await UpdateWorkOrder(input, cancellationToken);
            case RequestTypeChoose.DeleteWorkOrder:
                return await DeleteWorkOrder(input, cancellationToken);
            case RequestTypeChoose.GetServiceRequest:
                return await GetServiceRequest(input, cancellationToken);
            case RequestTypeChoose.UpdateServiceRequest:
                return await UpdateServiceRequest(input, cancellationToken);
            case RequestTypeChoose.DeleteServiceRequest:
                return await DeleteServiceRequest(input, cancellationToken);
            default:
                return new Result(false, null, $"Unsupported request type: {input.RequestType}");
        }
    }

    /// <summary>
    /// Creates a custom request in IBM Maximo.
    /// </summary>
    /// <param name="input">Input parameters for the custom request.</param>
    /// <param name="cancellationToken">Frends cancellation token.</param>
    /// <returns>Object { bool Success, string Error, dynamic Response }</returns>
    public static async Task<Result> CustomRequest(
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
        if (input.Payload != null)
        {
            request.AddJsonBody((object)input.Payload);
        }

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

    /// <summary>
    /// Creates a work order in IBM Maximo.
    /// </summary>
    /// <param name="input">Input parameters for the work order.</param>
    /// <param name="cancellationToken">Frends cancellation token.</param>
    /// <returns>Object { bool Success, string Error, dynamic Response }</returns>
    public static async Task<Result> CreateWorkOrder(
        [PropertyTab] Input input,
        CancellationToken cancellationToken)
    {
        input.Resource = "maximo/oslc/os/mxapiwo";
        input.Method = HttpMethod.POST;
        input.Payload = new
        {
            description = input.WorkOrderDescription,
            siteid = input.Site,
            assetnum = input.WorkOrderAssetNum,
            location = input.WorkOrderLocation,
            schedstart = input.ScheduledStart,
            reportdate = input.ReportedDate
        };

        return await CustomRequest(input, cancellationToken);
    }


    /// <summary>
    /// Generates a service request in IBM Maximo.
    /// </summary>
    /// <param name="input">Input parameters for the service request.</param>
    /// <param name="cancellationToken">Frends cancellation token.</param>
    /// <returns>Object { bool Success, string Error, dynamic Response }</returns>
    public static async Task<Result> GenerateServiceRequest(
    [PropertyTab] Input input,
    CancellationToken cancellationToken)
    {
        input.Resource = "maximo/oslc/os/mxsr";
        input.Method = HttpMethod.POST;
        input.Payload = new
        {
            description = input.ServiceRequestDescription,
            reportedBy = input.ReportedBy,
            location = input.ServiceRequestLocation,
            assetnum = input.ServiceRequestAssetNum
        };

        return await CustomRequest(input, cancellationToken);
    }

    /// <summary>
    /// Gets a work order in IBM Maximo.
    /// </summary>
    /// <param name="input">Input parameters for the work order.</param>
    /// <param name="cancellationToken">Frends cancellation token.</param>
    /// <returns>Object { bool Success, string Error, dynamic Response }</returns>
    public static async Task<Result> GetWorkOrder(
        [PropertyTab] Input input,
        CancellationToken cancellationToken)
    {
        input.Resource = $"maximo/oslc/os/mxapiwo/{input.WorkOrderId}";
        input.Method = HttpMethod.GET;

        return await CustomRequest(input, cancellationToken);
    }

    // <summary>
    /// Updates a work order in IBM Maximo.
    /// </summary>
    /// <param name="input">Input parameters for the work order.</param>
    /// <param name="cancellationToken">Frends cancellation token.</param>
    /// <returns>Object { bool Success, string Error, dynamic Response }</returns>
    public static async Task<Result> UpdateWorkOrder(
        [PropertyTab] Input input,
        CancellationToken cancellationToken)
    {
        input.Resource = $"maximo/oslc/os/mxapiwo/{input.WorkOrderId}";
        input.Method = HttpMethod.POST;

        var payload = new ExpandoObject() as IDictionary<string, object>;

        if (!string.IsNullOrWhiteSpace(input.WorkOrderDescription)) payload["description"] = input.WorkOrderDescription;
        if (!string.IsNullOrWhiteSpace(input.Site)) payload["siteid"] = input.Site;
        if (!string.IsNullOrWhiteSpace(input.WorkOrderAssetNum)) payload["assetnum"] = input.WorkOrderAssetNum;
        if (!string.IsNullOrWhiteSpace(input.WorkOrderLocation)) payload["location"] = input.WorkOrderLocation;
        if (!string.IsNullOrWhiteSpace(input.ScheduledStart)) payload["schedstart"] = input.ScheduledStart;
        if (!string.IsNullOrWhiteSpace(input.ReportedDate)) payload["reportdate"] = input.ReportedDate;

        input.Payload = payload;

        return await CustomRequest(input, cancellationToken);
    }

    /// <summary>
    /// Deletes a work order in IBM Maximo.
    /// </summary>
    /// <param name="input">Input parameters for the work order.</param>
    /// <param name="cancellationToken">Frends cancellation token.</param>
    /// <returns>Object { bool Success, string Error, dynamic Response }</returns>
    public static async Task<Result> DeleteWorkOrder(
        [PropertyTab] Input input,
        CancellationToken cancellationToken)
    {
        input.Resource = $"maximo/oslc/os/mxapiwo/{input.WorkOrderId}";
        input.Method = HttpMethod.DELETE;

        return await CustomRequest(input, cancellationToken);
    }

    /// <summary>
    /// Gets a service request in IBM Maximo.
    /// </summary>
    /// <param name="input">Input parameters for the service request.</param>
    /// <param name="cancellationToken">Frends cancellation token.</param>
    /// <returns>Object { bool Success, string Error, dynamic Response }</returns>
    public static async Task<Result> GetServiceRequest(
        [PropertyTab] Input input,
        CancellationToken cancellationToken)
    {
        input.Resource = $"maximo/oslc/os/mxapisr/{input.ServiceRequestId}";
        input.Method = HttpMethod.GET;

        return await CustomRequest(input, cancellationToken);
    }

    /// <summary>
    /// Updates a service request in IBM Maximo.
    /// </summary>
    /// <param name="input">Input parameters for the service request.</param>
    /// <param name="cancellationToken">Frends cancellation token.</param>
    /// <returns>Object { bool Success, string Error, dynamic Response }</returns>
    public static async Task<Result> UpdateServiceRequest(
        [PropertyTab] Input input,
        CancellationToken cancellationToken)
    {
        input.Resource = $"maximo/oslc/os/mxapisr/{input.ServiceRequestId}";
        input.Method = HttpMethod.POST;

        var payload = new ExpandoObject() as IDictionary<string, object>;

        if (!string.IsNullOrWhiteSpace(input.ServiceRequestDescription)) payload["description"] = input.ServiceRequestDescription;
        if (!string.IsNullOrWhiteSpace(input.ReportedBy)) payload["reportedBy"] = input.ReportedBy;
        if (!string.IsNullOrWhiteSpace(input.ServiceRequestLocation)) payload["location"] = input.ServiceRequestLocation;
        if (!string.IsNullOrWhiteSpace(input.ServiceRequestAssetNum)) payload["assetnum"] = input.WorkOrderAssetNum;

        input.Payload = payload;

        return await CustomRequest(input, cancellationToken);
    }

    /// <summary>
    /// Deletes a service request in IBM Maximo.
    /// </summary>
    /// <param name="input">Input parameters for the service request.</param>
    /// <param name="cancellationToken">Frends cancellation token.</param>
    /// <returns>Object { bool Success, string Error, dynamic Response }</returns>
    public static async Task<Result> DeleteServiceRequest(
        [PropertyTab] Input input,
        CancellationToken cancellationToken)
    {
        input.Resource = $"maximo/oslc/os/mxapisr/{input.ServiceRequestId}";
        input.Method = HttpMethod.DELETE;

        return await CustomRequest(input, cancellationToken);
    }
}