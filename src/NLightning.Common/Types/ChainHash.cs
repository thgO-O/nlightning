namespace NLightning.Common.Types;

/// <summary>
/// Represents a chain hash.
/// </summary>
/// <remarks>
/// A chain hash is a 32 byte hash that is used to identify a chain.
/// </remarks>
public readonly struct ChainHash
{
    /// <summary>
    /// The length of a chain hash.
    /// </summary>
    public const int LENGTH = 32;

    private readonly byte[] _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChainHash"/> struct.
    /// </summary>
    /// <param name="value">The value of the chain hash.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is not 32 bytes.</exception>
    public ChainHash(byte[] value)
    {
        if (value.Length != 32)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "ChainHash must be 32 bytes");
        }

        _value = value;
    }

    /// <summary>
    /// Compares two chain hashes for equality.
    /// </summary>
    /// <param name="other">The chain hash to compare to.</param>
    /// <returns>True if the chain hashes are equal, otherwise false.</returns>
    private readonly bool Equals(ChainHash other)
    {
        return _value.SequenceEqual(other._value);
    }

    public override readonly bool Equals(object? obj)
    {
        if (obj is ChainHash other)
        {
            return Equals(other);
        }

        return false;
    }

    public override readonly int GetHashCode()
    {
        return BitConverter.ToInt32(_value, 0);
    }

    public static implicit operator byte[](ChainHash c) => c._value;
    public static implicit operator ReadOnlyMemory<byte>(ChainHash c) => c._value;
    public static implicit operator ChainHash(byte[] value) => new(value);

    public static bool operator ==(ChainHash left, ChainHash right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ChainHash left, ChainHash right)
    {
        return !(left == right);
    }

    public ValueTask SerializeAsync(Stream stream)
    {
        return stream.WriteAsync(_value);
    }

    public static async Task<ChainHash> DeserializeAsync(Stream stream)
    {
        var buffer = new byte[LENGTH];
        await stream.ReadExactlyAsync(buffer);
        return new ChainHash(buffer);
    }
}