using System;

namespace GW2SDK.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DataTransferObjectAttribute : Attribute
    {
        public bool RootObject { get; set; } = true;
    }
}
