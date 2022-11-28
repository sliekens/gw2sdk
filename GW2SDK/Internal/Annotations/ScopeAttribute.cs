using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using static System.AttributeTargets;

namespace GuildWars2.Annotations;

[AttributeUsage(Class | Method | Property, AllowMultiple = true)]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
internal sealed class ScopeAttribute : Attribute
{
    public ScopeAttribute(params Permission[] permissions)
    {
        Permission = permissions;
    }

    public ScopeAttribute(ScopeRequirement requirement, params Permission[] permissions)
    {
        Requirement = requirement;
        Permission = permissions;
    }

    internal Permission[] Permission { get; }

    internal ScopeRequirement Requirement { get; }
}

[DefaultValue(All)]
internal enum ScopeRequirement
{
    All,

    Any
}
