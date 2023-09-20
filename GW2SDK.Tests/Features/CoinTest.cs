namespace GuildWars2.Tests.Features;

public class CoinTest
{
    [Theory]
    [InlineData("1 copper", 1)]
    [InlineData("1 silver", 1_00)]
    [InlineData("1 gold", 1_00_00)]
    [InlineData("50 copper", 50)]
    [InlineData("50 silver", 50_00)]
    [InlineData("50 gold", 50_00_00)]
    [InlineData("1 silver, 2 copper", 1_02)]
    [InlineData("1 gold, 2 copper", 1_00_02)]
    [InlineData("1 gold, 2 silver", 1_02_00)]
    [InlineData("1 gold, 2 silver, 3 copper", 1_02_03)]
    public void Coins_are_formatted_for_humans(string expected, int amount)
    {
        Coin sut = new(amount);

        var actual = sut.ToString();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Zero_coins_is_formatted_as_long_dash()
    {
        Coin nil = default;
        var actual = nil.ToString();

        Assert.Equal("⸻", actual);
    }

    [Fact]
    public void Number_of_coins_cannot_be_negative()
    {
        static void NegativeCoins()
        {
            // ReSharper disable once UnusedVariable
            Coin coin = new(-1);
        }

        Assert.Throws<ArgumentOutOfRangeException>("amount", NegativeCoins);
    }

    [Fact]
    public void Coins_can_be_equal()
    {
        Coin left = new(10);
        Coin right = new(10);

        Assert.Equal(left, right);
    }

    [Fact]
    public void Coins_can_be_inequal()
    {
        Coin left = new(10);
        Coin right = new(20);

        Assert.NotEqual(left, right);
    }

    [Fact]
    public void Coins_can_be_compared()
    {
        Coin head = new(1);
        Coin body = new(1_00);
        Coin tail = new(1_00_00);

        Assert.Equal(0, head.CompareTo(new Coin(head.Amount)));
        Assert.Equal(1, head.CompareTo(Coin.Zero));
        Assert.Equal(-1, head.CompareTo(body));

        Assert.Equal(0, body.CompareTo(new Coin(body.Amount)));
        Assert.Equal(1, body.CompareTo(head));
        Assert.Equal(-1, body.CompareTo(tail));

        Assert.Equal(0, tail.CompareTo(new Coin(tail.Amount)));
        Assert.Equal(1, tail.CompareTo(body));
        Assert.Equal(-1, tail.CompareTo(new Coin(int.MaxValue)));
    }

    [Fact]
    public void Coins_can_be_added()
    {
        Coin part1 = new(1);
        Coin part2 = new(2);

        var actual = part1 + part2;

        Assert.Equal(3, actual.Amount);
    }

    [Fact]
    public void Coins_cannot_be_divided_by_zero()
    {
        Coin dividend = new(1);

        void DivideByZero()
        {
            var _ = dividend / Coin.Zero;
        }

        Assert.Throws<DivideByZeroException>(DivideByZero);
    }

    [Fact]
    public void Coins_can_be_specified_in_silvers()
    {
        Coin sut = new(12, 00);

        var actual = sut.Amount;

        Assert.Equal(1200, actual);
    }

    [Fact]
    public void Coins_can_be_specified_in_gold()
    {
        Coin sut = new(12, 00, 00);

        var actual = sut.Amount;

        Assert.Equal(12_00_00, actual);
    }
}
