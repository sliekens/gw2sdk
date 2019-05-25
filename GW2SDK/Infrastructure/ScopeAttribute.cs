using System;
using GW2SDK.Features.Common;

namespace GW2SDK.Infrastructure
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
