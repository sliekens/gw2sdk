using System.Globalization;

using GuildWars2.Chat;


namespace GuildWars2.Tests.Features;

public class CoinTest
{

    [Test]

    [Arguments("⸻", 0)]

    [Arguments("1 copper", 1)]

    [Arguments("1 silver", 1_00)]

    [Arguments("1 gold", 1_00_00)]

    [Arguments("50 copper", 50)]

    [Arguments("50 silver", 50_00)]

    [Arguments("50 gold", 50_00_00)]

    [Arguments("1 silver, 2 copper", 1_02)]

    [Arguments("1 gold, 2 copper", 1_00_02)]

    [Arguments("1 gold, 2 silver", 1_02_00)]

    [Arguments("1 gold, 2 silver, 3 copper", 1_02_03)]

    [Arguments("214,748 gold, 36 silver, 47 copper", int.MaxValue)]

    [Arguments("-214,748 gold, -36 silver, -48 copper", int.MinValue)]

    public void Coins_are_formatted_for_humans(string expected, int amount)
    {
        // Number formatting depends on current culture

        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        Coin sut = amount;

        string actual = sut.ToString();

        Assert.Equal(expected, actual);
    }

    [Test]

    [Arguments(0, 0, 0, 0)]

    [Arguments(1_00, 0, 1, 0)]

    [Arguments(1_00_00, 1, 0, 0)]

    [Arguments(1_00_01, 1, 0, 1)]

    [Arguments(1_01_01, 1, 1, 1)]

    [Arguments(99_99_99, 99, 99, 99)]

    public void Coins_can_be_represented_in_gold_silver_and_copper(int amount, int gold, int silver, int copper)
    {

        Coin sut = amount;

        Assert.Equal(gold, sut.Gold);

        Assert.Equal(silver, sut.Silver);

        Assert.Equal(copper, sut.Copper);
    }

    [Test]

    public void Coins_can_be_equal()
    {

        Coin left = 10;

        Coin right = 10;

        Assert.Equal(left, right);
        // Also make sure it works with boxing

        Assert.True(Equals(left, right));
        // Also check operators

        Assert.True(left == right);

        Assert.False(left != right);
    }

    [Test]

    public void Coins_can_be_inequal()
    {

        Coin left = 10;

        Coin right = 20;

        Assert.NotEqual(left, right);
        // Also make sure it works with boxing

        Assert.False(Equals(left, right));
        // Also check operators

        Assert.True(left != right);

        Assert.False(left == right);
    }

    [Test]

    public void Coins_can_be_compared()
    {

        Coin head = 1;

        Coin body = 1_00;

        Coin tail = 1_00_00;

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

    [Test]

    public void Coins_can_be_specified_in_silvers()
    {

        Coin sut = new(12, 00);

        int actual = sut.Amount;

        Assert.Equal(1200, actual);
    }

    [Test]

    public void Coins_can_be_specified_in_gold()
    {

        Coin sut = new(12, 00, 00);

        int actual = sut.Amount;

        Assert.Equal(12_00_00, actual);
    }

    [Test]

    public void Coins_can_be_greater_than()
    {

        Coin one = 1;

        Assert.True(one > Coin.Zero);

        Assert.True(one >= Coin.Zero);

        Assert.True(one >= +one);
    }

    [Test]

    public void Coins_can_be_less_than()
    {

        Coin one = 1;

        Assert.True(Coin.Zero < one);

        Assert.True(Coin.Zero <= one);

        Assert.True(one <= +one);
    }

    [Test]

    public void Coins_can_be_added()
    {

        Coin one = 1;

        Coin two = 2;

        Coin three = one + two;

        Assert.Equal(3, three.Amount);
    }

    [Test]

    public void Coins_can_be_subtracted()
    {

        Coin three = 3;

        Coin two = 2;

        Coin one = three - two;

        Assert.Equal(1, one.Amount);
    }

    [Test]

    public void Coins_can_be_multiplied()
    {

        Coin two = 2;

        Coin three = 3;

        Coin six = two * three;

        Assert.Equal(6, six.Amount);
    }

    [Test]

    public void Coins_can_be_negated()
    {

        Coin sut = 1_00_00;

        Coin actual = -sut;

        Assert.Equal(-1_00_00, actual.Amount);
    }

    [Test]

    public void Coins_can_be_divided()
    {

        Coin six = 6;

        Coin three = 3;

        Coin two = six / three;

        Assert.Equal(2, two.Amount);
    }

    [Test]

    public void Coins_can_be_moduloed()
    {

        Coin ten = 10;

        Coin three = 3;

        Coin one = ten % three;

        Assert.Equal(1, one.Amount);
    }

    [Test]

    public void Coins_cannot_be_divided_by_zero()
    {

        Coin dividend = 1;

        void DivideByZero()
        {

            Coin _ = dividend / Coin.Zero;
        }


        Assert.Throws<DivideByZeroException>(DivideByZero);
    }

    [Test]

    public void Coins_can_be_incremented()
    {

        Coin coin = 1;

        coin++;

        Assert.Equal(2, coin.Amount);
    }

    [Test]

    public void Coins_can_be_decremented()
    {

        Coin coin = 2;

        coin--;

        Assert.Equal(1, coin.Amount);
    }

    [Test]

    public void Coins_can_be_assigned_to_int()
    {

        Coin two = 2;

        int intTwo = two;

        Assert.Equal(2, intTwo);
    }

    [Test]

    public void Coins_can_be_sorted()
    {
        // This should test the generic IComparable<T> which prevents unnecessary boxing
        // although there is no way to test that directly

        List<Coin> coins = [10, 5, 20];

        coins.Sort();

        Assert.Collection(coins, coin => Assert.Equal(5, coin.Amount), coin => Assert.Equal(10, coin.Amount), coin => Assert.Equal(20, coin.Amount));
    }

    [Test]

    public void Coins_can_be_sorted_when_boxed()
    {
        // This tests the non-generic IComparable which is used in boxing scenarios

        List<object> coins = [new Coin(10), new Coin(5), new Coin(20)];

        coins.Sort();

        Assert.Collection(coins.Cast<Coin>(), coin => Assert.Equal(5, coin.Amount), coin => Assert.Equal(10, coin.Amount), coin => Assert.Equal(20, coin.Amount));
    }

    [Test]

    public void Coins_can_be_sorted_after_null()
    {

        Coin sut = Coin.Zero;

        int actual = sut.CompareTo(null);

        Assert.Equal(1, actual);
    }

    [Test]

    public void Coins_cannot_be_sorted_after_other_types()
    {

        Coin sut = Coin.Zero;

        void CompareToString()
        {

            int _ = sut.CompareTo("0");
        }


        ArgumentException reason = Assert.Throws<ArgumentException>("obj", CompareToString);

        Assert.StartsWith("Object must be of type Coin", reason.Message, StringComparison.Ordinal);
    }

    [Test]

    public void Coins_can_be_linked_in_chat()
    {

        Coin sut = 1_00_00;

        CoinLink actual = sut.GetChatLink();

        Assert.Equal(sut.Amount, actual.Coins.Amount);
    }

    [Test]

    public void Coins_can_be_json_serialized_roundtrip()
    {

        Coin sut = 1_00_00;

        string json = System.Text.Json.JsonSerializer.Serialize(sut);

        Coin actual = System.Text.Json.JsonSerializer.Deserialize<Coin>(json);

        Assert.Equal(sut.Amount, actual.Amount);
    }
}
