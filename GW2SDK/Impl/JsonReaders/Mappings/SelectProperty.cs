using System.Diagnostics.CodeAnalysis;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    [return: MaybeNull]
    public delegate TProperty SelectProperty<in TObject, out TProperty>(TObject clrType);
}
