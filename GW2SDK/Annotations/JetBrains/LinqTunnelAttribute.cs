﻿using System;
// ReSharper disable All

#nullable disable
namespace GW2SDK.Annotations
{
    /// <summary>
    /// Indicates that method is pure LINQ method, with postponed enumeration (like Enumerable.Select,
    /// .Where). This annotation allows inference of [InstantHandle] annotation for parameters
    /// of delegate type by analyzing LINQ method chains.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class LinqTunnelAttribute : Attribute
    {
    }
}
#nullable restore
