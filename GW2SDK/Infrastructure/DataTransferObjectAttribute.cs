using System;

namespace GW2SDK.Infrastructure
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DataTransferObjectAttribute : Attribute
    {
        public bool RootObject { get; set; } = true;
    }
}
