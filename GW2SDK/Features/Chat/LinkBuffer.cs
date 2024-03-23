#if !NET
using System.Buffers;
#endif

namespace GuildWars2.Chat;

/// <summary>Utilities for interacting with a (stack-allocated) byte buffer.</summary>
/// <param name="buffer">The buffer, unsurprisingly.</param>
internal ref struct LinkBuffer(Span<byte> buffer)
{
    public Span<byte> Buffer = buffer;

    private int length;

    /// <summary>Skip a byte in the buffer, returning the index of the skipped byte.</summary>
    /// <returns>The index of the skipped byte.</returns>
    public int Skip() => ++length - 1;

    public void Padding(int count) => length += count;

    public bool EndOfFile => length >= Buffer.Length;

    public byte ReadUInt8() => Buffer[length++];

    public void WriteUInt8(byte value) => Buffer[length++] = value;

    public ushort ReadUInt16() => (ushort)(Buffer[length++] | (Buffer[length++] << 8));

    public void WriteUInt16(ushort value)
    {
        Buffer[length++] = (byte)(value & 0xFF);
        Buffer[length++] = (byte)((value >> 8) & 0xFF);
    }

    public int ReadUInt24() =>
        Buffer[length++] | (Buffer[length++] << 8) | (Buffer[length++] << 16);

    public void WriteUInt24(int value)
    {
        Buffer[length++] = (byte)(value & 0xFF);
        Buffer[length++] = (byte)((value >> 8) & 0xFF);
        Buffer[length++] = (byte)((value >> 16) & 0xFF);
    }

    public int ReadInt32() =>
        Buffer[length++]
        | (Buffer[length++] << 8)
        | (Buffer[length++] << 16)
        | (Buffer[length++] << 24);

    public void WriteInt32(int value)
    {
        Buffer[length++] = (byte)(value & 0xFF);
        Buffer[length++] = (byte)((value >> 8) & 0xFF);
        Buffer[length++] = (byte)((value >> 16) & 0xFF);
        Buffer[length++] = (byte)((value >> 24) & 0xFF);
    }

    public override string ToString()
    {
#if NET
        return Wrapped(Convert.ToBase64String(Buffer[..length]));
#else

        // Unfortunately there is no Convert.ToBase64String overload that takes a Span<byte> in older .NET,
        // but we can use ArrayPool to avoid allocating a new array
        var arr = ArrayPool<byte>.Shared.Rent(length);
        try
        {
            Buffer[..length].CopyTo(arr);
            return Wrapped(Convert.ToBase64String(arr, 0, length));
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(arr);
        }
#endif
        static string Wrapped(string base64)
        {
            return $"[&{base64}]";
        }
    }
}
