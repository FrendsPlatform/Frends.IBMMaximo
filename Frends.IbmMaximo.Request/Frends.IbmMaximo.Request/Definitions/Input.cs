using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.IbmMaximo.Request.Definitions;

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// IBM Maximo base URL.
    /// </summary>
    /// <example>http://localhost:7001/maximo/oslc</example>
    [DefaultValue("http://localhost:7001/maximo/oslc")]
    [DisplayFormat(DataFormatString = "Text")]
    public string BaseUrl { get; set; }
    
    /// <summary>
    /// IBM Maximo resource to request.
    /// </summary>
    /// <example>/os/mxapiasset</example>
    [DefaultValue("/os/mxapiasset")]
    [DisplayFormat(DataFormatString = "Text")]
    public string Resource { get; set; }
    
    /// <summary>
    /// The payload text to be sent with the request.
    /// </summary>
    /// <example>{ "Body": "Message" }</example>
    [UIHint(nameof(HttpMethod), "",
        HttpMethod.POST, HttpMethod.DELETE, HttpMethod.PATCH, HttpMethod.PUT)]
    public string Payload { get; set; }
    
    /// <summary>
    /// HTTP method to use for the request.
    /// </summary>
    /// <example>GET</example>
    [DefaultValue(HttpMethod.GET)]
    public HttpMethod Method { get; set; }
    
    /// <summary>
    /// API key for authentication.
    /// </summary>
    /// <example>api-key</example>
    [DefaultValue("")]
    [DisplayFormat(DataFormatString = "Text")]
    [PasswordPropertyText]
    public string ApiKey { get; set; }
}