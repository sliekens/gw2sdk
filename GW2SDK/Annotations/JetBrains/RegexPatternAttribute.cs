using System;

#nullable disable
namespace GW2SDK.Annotations
{
    /// <summary>
    /// Indicates that the marked parameter is a regular expression pattern.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class RegexPatternAttribute : Attribute
    {
    }
}
#nullable restore
