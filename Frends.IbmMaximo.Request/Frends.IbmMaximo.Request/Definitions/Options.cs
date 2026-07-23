using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.IbmMaximo.Request.Definitions;

/// <summary>
/// Options parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// Whether to throw an exception on failure or return a result with Success = false.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// Custom error message to use when an error occurs. If empty, the exception message is used.
    /// </summary>
    /// <example>IBM Maximo request failed</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; set; } = string.Empty;
}
