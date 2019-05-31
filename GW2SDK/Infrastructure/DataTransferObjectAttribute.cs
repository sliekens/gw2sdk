using System;

namespace GW2SDK.Infrastructure
{
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DataTransferObjectAttribute : Attribute
    {
        public bool RootObject { get; set; } = true;
    }
}
