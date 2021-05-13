using System;

namespace GW2SDK.Annotations
{
    /// <summary>Use this attribute to mark non-abstract classes that are designed to be inheritable.</summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class InheritableAttribute : Attribute
    {
    }
}
