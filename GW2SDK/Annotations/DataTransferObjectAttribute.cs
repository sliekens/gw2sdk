using System;

namespace GW2SDK.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class DataTransferObjectAttribute : Attribute
    {
        public bool RootObject { get; set; } = true;
    }
}
