using System;
using GW2SDK.Features.Tokens;

namespace GW2SDK.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class ScopeAttribute : Attribute
    {
        public ScopeAttribute(Permission permission)
        {
            Permission = permission;
        }

        public Permission Permission { get; }
    }
}
