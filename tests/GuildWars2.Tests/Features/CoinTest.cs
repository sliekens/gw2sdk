using System.Globalization;
using System.Text.Json;

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
    public async Task Coins_are_formatted_for_humans(string expected, int amount)
    {
        // Number formatting depends on current culture
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        Coin sut = amount;
        string actual = sut.ToString();
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    [Arguments(0, 0, 0, 0)]
    [Arguments(1_00, 0, 1, 0)]
    [Arguments(1_00_00, 1, 0, 0)]
    [Arguments(1_00_01, 1, 0, 1)]
    [Arguments(1_01_01, 1, 1, 1)]
    [Arguments(99_99_99, 99, 99, 99)]
    public async Task Coins_can_be_represented_in_gold_silver_and_copper(int amount, int gold, int silver, int copper)
    {
        Coin sut = amount;
        await Assert.That(sut).Member(c => c.Gold, g => g.IsEqualTo(gold))
            .And.Member(c => c.Silver, s => s.IsEqualTo(silver))
            .And.Member(c => c.Copper, c => c.IsEqualTo(copper));
    }

    [Test]
    public async Task Coins_can_be_equal()
    {
        Coin left = 10;
        Coin right = 10;
        await Assert.That(left).IsEqualTo(right);
        // Also make sure it works with boxing
        await Assert.That(Equals(left, right)).IsTrue();
        // Also check operators
        await Assert.That(left == right).IsTrue();
        await Assert.That(left != right).IsFalse();
    }

    [Test]
    public async Task Coins_can_be_inequal()
    {
        Coin left = 10;
        Coin right = 20;
        await Assert.That(left).IsNotEqualTo(right);
        // Also make sure it works with boxing
        await Assert.That(Equals(left, right)).IsFalse();
        // Also check operators
        await Assert.That(left != right).IsTrue();
        await Assert.That(left == right).IsFalse();
    }

    [Test]
    public async Task Coins_can_be_compared()
    {
        Coin head = 1;
        Coin body = 1_00;
        Coin tail = 1_00_00;
        await Assert.That(head.CompareTo(new Coin(head.Amount))).IsEqualTo(0);
        await Assert.That(head.CompareTo(Coin.Zero)).IsEqualTo(1);
        await Assert.That(head.CompareTo(body)).IsEqualTo(-1);
        await Assert.That(body.CompareTo(new Coin(body.Amount))).IsEqualTo(0);
        await Assert.That(body.CompareTo(head)).IsEqualTo(1);
        await Assert.That(body.CompareTo(tail)).IsEqualTo(-1);
        await Assert.That(tail.CompareTo(new Coin(tail.Amount))).IsEqualTo(0);
        await Assert.That(tail.CompareTo(body)).IsEqualTo(1);
        await Assert.That(tail.CompareTo(new Coin(int.MaxValue))).IsEqualTo(-1);
    }

    [Test]
    public async Task Coins_can_be_specified_in_silvers()
    {
        Coin sut = new(12, 00);
        int actual = sut.Amount;
        await Assert.That(actual).IsEqualTo(1200);
    }

    [Test]
    public async Task Coins_can_be_specified_in_gold()
    {
        Coin sut = new(12, 00, 00);
        int actual = sut.Amount;
        await Assert.That(actual).IsEqualTo(12_00_00);
    }

    [Test]
    public async Task Coins_can_be_greater_than()
    {
        Coin one = 1;
        await Assert.That(one > Coin.Zero).IsTrue();
        await Assert.That(one >= Coin.Zero).IsTrue();
        await Assert.That(one >= +one).IsTrue();
    }

    [Test]
    public async Task Coins_can_be_less_than()
    {
        Coin one = 1;
        await Assert.That(Coin.Zero < one).IsTrue();
        await Assert.That(Coin.Zero <= one).IsTrue();
        await Assert.That(one <= +one).IsTrue();
    }

    [Test]
    public async Task Coins_can_be_added()
    {
        Coin one = 1;
        Coin two = 2;
        Coin three = one + two;
        await Assert.That(three.Amount).IsEqualTo(3);
    }

    [Test]
    public async Task Coins_can_be_subtracted()
    {
        Coin three = 3;
        Coin two = 2;
        Coin one = three - two;
        await Assert.That(one.Amount).IsEqualTo(1);
    }

    [Test]
    public async Task Coins_can_be_multiplied()
    {
        Coin two = 2;
        Coin three = 3;
        Coin six = two * three;
        await Assert.That(six.Amount).IsEqualTo(6);
    }

    [Test]
    public async Task Coins_can_be_negated()
    {
        Coin sut = 1_00_00;
        Coin actual = -sut;
        await Assert.That(actual.Amount).IsEqualTo(-1_00_00);
    }

    [Test]
    public async Task Coins_can_be_divided()
    {
        Coin six = 6;
        Coin three = 3;
        Coin two = six / three;
        await Assert.That(two.Amount).IsEqualTo(2);
    }

    [Test]
    public async Task Coins_can_be_moduloed()
    {
        Coin ten = 10;
        Coin three = 3;
        Coin one = ten % three;
        await Assert.That(one.Amount).IsEqualTo(1);
    }

    [Test]
    public async Task Coins_cannot_be_divided_by_zero()
    {
        Coin dividend = 1;
        void DivideByZero()
        {
            Coin _ = dividend / Coin.Zero;
        }

        await Assert.That(DivideByZero).Throws<DivideByZeroException>();
    }

    [Test]
    public async Task Coins_can_be_incremented()
    {
        Coin coin = 1;
        coin++;
        await Assert.That(coin.Amount).IsEqualTo(2);
    }

    [Test]
    public async Task Coins_can_be_decremented()
    {
        Coin coin = 2;
        coin--;
        await Assert.That(coin.Amount).IsEqualTo(1);
    }

    [Test]
    public async Task Coins_can_be_assigned_to_int()
    {
        Coin two = 2;
        int intTwo = two;
        await Assert.That(intTwo).IsEqualTo(2);
    }

    [Test]
    public async Task Coins_can_be_sorted()
    {
        // This should test the generic IComparable<T> which prevents unnecessary boxing
        // although there is no way to test that directly
        List<Coin> coins = [10, 5, 20];
        coins.Sort();
        await Assert.That(coins).Count().IsEqualTo(3);
        await Assert.That(coins[0].Amount).IsEqualTo(5);
        await Assert.That(coins[1].Amount).IsEqualTo(10);
        await Assert.That(coins[2].Amount).IsEqualTo(20);
    }

    [Test]
    public async Task Coins_can_be_sorted_when_boxed()
    {
        // This tests the non-generic IComparable which is used in boxing scenarios
        List<object> coins = [new Coin(10), new Coin(5), new Coin(20)];
        coins.Sort();
        List<Coin> coinList = [.. coins.Cast<Coin>()];
        await Assert.That(coinList).Count().IsEqualTo(3);
        await Assert.That(coinList[0].Amount).IsEqualTo(5);
        await Assert.That(coinList[1].Amount).IsEqualTo(10);
        await Assert.That(coinList[2].Amount).IsEqualTo(20);
    }

    [Test]
    public async Task Coins_can_be_sorted_after_null()
    {
        Coin sut = Coin.Zero;
        int actual = sut.CompareTo(null);
        await Assert.That(actual).IsEqualTo(1);
    }

    [Test]
    public async Task Coins_cannot_be_sorted_after_other_types()
    {
        Coin sut = Coin.Zero;
        void CompareToString()
        {
            int _ = sut.CompareTo("0");
        }

        await Assert.That(CompareToString).Throws<ArgumentException>().WithParameterName("obj").And.Member(ex => ex.Message, m => m.StartsWith("Object must be of type Coin"));
    }

    [Test]
    public async Task Coins_can_be_linked_in_chat()
    {
        Coin sut = 1_00_00;
        CoinLink actual = sut.GetChatLink();
        await Assert.That(actual.Coins.Amount).IsEqualTo(sut.Amount);
    }

    [Test]
    public async Task Coins_can_be_json_serialized_roundtrip()
    {
        Coin sut = 1_00_00;
#if NET7_0_OR_GREATER
        string json = JsonSerializer.Serialize(sut, Common.TestJsonContext.Default.Coin);
        Coin actual = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.Coin)!;
#else
        string json = JsonSerializer.Serialize(sut);
        Coin actual = JsonSerializer.Deserialize<Coin>(json)!;
#endif
        await Assert.That(actual.Amount).IsEqualTo(sut.Amount);
    }
}
