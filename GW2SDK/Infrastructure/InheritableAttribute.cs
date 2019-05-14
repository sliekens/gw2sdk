using System;

namespace GW2SDK.Infrastructure
{
    /// <summary>Use this attribute to mark non-abstract classes that are designed to be inheritable.</summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal sealed class InheritableAttribute : Attribute
    {
    }
}
