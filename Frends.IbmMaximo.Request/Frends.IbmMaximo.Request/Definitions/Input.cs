using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.IbmMaximo.Request.Definitions;

/// <summary>
/// Request type choices.
/// </summary>
public enum RequestTypeChoose
{
    /// <summary>
    /// Custom request.
    /// </summary>
    CustomRequest,
    /// <summary>
    /// Generate service request.
    /// </summary>
    CreateWorkOrder,
    /// <summary>
    /// Create work order.
    /// </summary>
    GenerateServiceRequest,
    /// <summary>
    /// Get work order by its ID.
    /// </summary>
    GetWorkOrder,
    /// <summary>
    /// Updated work order with given ID.
    /// </summary>
    UpdateWorkOrder,
    /// <summary>
    /// Delete work order with given ID.
    /// </summary>
    DeleteWorkOrder,

    /// <summary>
    /// Get service request by its ID.
    /// </summary>
    GetServiceRequest,

    /// <summary>
    /// Updated service request with given ID.
    /// </summary>
    UpdateServiceRequest,

    /// <summary>
    /// Delete service request with given ID.
    /// </summary>
    DeleteServiceRequest
}

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Select request type to show correct editor for input.
    /// </summary>
    public RequestTypeChoose RequestType { get; set; }

    /// <summary>
    /// IBM Maximo base URL.
    /// </summary>
    /// <example>http://localhost:7001/</example>
    [DefaultValue("http://localhost:7001/")]
    [DisplayFormat(DataFormatString = "Text")]

    public string BaseUrl { get; set; }

    /// <summary>
    /// IBM Maximo resource to request.
    /// </summary>
    /// <example>/os/mxapiasset</example>
    [DefaultValue("/os/mxapiasset")]
    [DisplayFormat(DataFormatString = "Text")]
    [UIHint(nameof(RequestType), "", RequestTypeChoose.CustomRequest)]
    public string Resource { get; set; }

    /// <summary>
    /// The payload text to be sent with the request.
    /// </summary>
    /// <example>{ "Body": "Message" }</example>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.CustomRequest)]
    public dynamic Payload { get; set; }

    /// <summary>
    /// HTTP method to use for the request.
    /// </summary>
    /// <example>GET</example>
    [DefaultValue(HttpMethod.GET)]
    [UIHint(nameof(RequestType), "", RequestTypeChoose.CustomRequest)]
    public HttpMethod Method { get; set; }

    /// <summary>
    /// API key for authentication.
    /// </summary>
    /// <example>api-key</example>
    [DefaultValue("")]
    [DisplayFormat(DataFormatString = "Text")]
    [PasswordPropertyText]
    public string ApiKey { get; set; }

    /// <summary>
    /// Description for the new work order.
    /// </summary>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.CreateWorkOrder, RequestTypeChoose.UpdateWorkOrder)]
    public string WorkOrderDescription { get; set; }

    /// <summary>
    /// Description for the service request.
    /// </summary>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.GenerateServiceRequest, RequestTypeChoose.UpdateServiceRequest)]
    public string ServiceRequestDescription { get; set; }

    /// <summary>
    /// The person who reported the issue.
    /// </summary>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.GenerateServiceRequest, RequestTypeChoose.UpdateServiceRequest)]
    public string ReportedBy { get; set; }

    /// <summary>
    /// Location for new work order.
    /// </summary>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.CreateWorkOrder, RequestTypeChoose.UpdateWorkOrder)]
    public string WorkOrderLocation { get; set; }

    /// <summary>
    /// Identifies the ticket's location.
    /// </summary>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.GenerateServiceRequest, RequestTypeChoose.UpdateServiceRequest)]
    public string ServiceRequestLocation { get; set; }

    /// <summary>
    /// Asset number associated with the work order.
    /// </summary>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.CreateWorkOrder, RequestTypeChoose.UpdateWorkOrder)]
    public string WorkOrderAssetNum { get; set; }

    /// <summary>
    /// Asset number associated with the service request.
    /// </summary>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.GenerateServiceRequest, RequestTypeChoose.UpdateServiceRequest)]
    public string ServiceRequestAssetNum { get; set; }

    /// <summary>
    /// Site associated with the work order.
    /// </summary>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.CreateWorkOrder, RequestTypeChoose.UpdateWorkOrder)]
    public string Site { get; set; }

    /// <summary>
    /// Scheduled start time for the work order.
    /// </summary>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.CreateWorkOrder, RequestTypeChoose.UpdateWorkOrder)]
    public string ScheduledStart { get; set; }

    /// <summary>
    /// Reported date for the breakdown work order.
    /// </summary>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.CreateWorkOrder, RequestTypeChoose.UpdateWorkOrder)]
    public string ReportedDate { get; set; }

    /// <summary>
    /// Reported date for the breakdown work order.
    /// </summary>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.GetWorkOrder, RequestTypeChoose.UpdateWorkOrder, RequestTypeChoose.DeleteWorkOrder)]
    public string WorkOrderId { get; set; }

    /// <summary>
    /// Reported date for the breakdown work order.
    /// </summary>
    [UIHint(nameof(RequestType), "", RequestTypeChoose.GetServiceRequest, RequestTypeChoose.UpdateServiceRequest, RequestTypeChoose.DeleteServiceRequest)]
    public string ServiceRequestId { get; set; }
}
