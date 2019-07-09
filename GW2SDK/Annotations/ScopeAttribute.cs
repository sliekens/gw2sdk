using System;
using GW2SDK.Enums;

namespace GW2SDK.Annotations
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public sealed class ScopeAttribute : Attribute
    {
        public ScopeAttribute(Permission permission)
        {
            Permission = permission;
        }

        public Permission Permission { get; }
    }
}
