﻿using System;
// ReSharper disable All

#nullable disable
namespace GW2SDK.Annotations
{
    /// <summary>
    /// Indicates that IEnumerable passed as a parameter is not enumerated.
    /// Use this annotation to suppress the 'Possible multiple enumeration of IEnumerable' inspection.
    /// </summary>
    /// <example><code>
    /// static void ThrowIfNull&lt;T&gt;([NoEnumeration] T v, string n) where T : class
    /// {
    ///   // custom check for null but no enumeration
    /// }
    /// 
    /// void Foo(IEnumerable&lt;string&gt; values)
    /// {
    ///   ThrowIfNull(values, nameof(values));
    ///   var x = values.ToList(); // No warnings about multiple enumeration
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class NoEnumerationAttribute : Attribute
    {
    }
}
#nullable restore
