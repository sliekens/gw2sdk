using System;

namespace GW2SDK.Annotations
{
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DataTransferObjectAttribute : Attribute
    {
        public bool RootObject { get; set; } = true;
    }
}
