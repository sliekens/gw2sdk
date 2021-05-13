using System;

namespace GW2SDK.Annotations
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    internal sealed class ScopeAttribute : Attribute
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
