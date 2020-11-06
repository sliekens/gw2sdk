using System;
using GW2SDK.Enums;

namespace GW2SDK.Annotations
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public sealed class ScopeAttribute : Attribute
    {
        public ScopeAttribute(Permission permission)
        {
            Permission = new [] { permission };
        }

        public ScopeAttribute(params Permission[] permissions)
        {
            Permission = permissions;
        }

        public Permission[] Permission { get; }
    }
}
