using System.Text.Json;
using GW2SDK.Accounts.Banks;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Accounts.Banks.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Banks
{
    public class BankReaderTest : IClassFixture<BankFixture>
    {
        public BankReaderTest(BankFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly BankFixture _fixture;
        
        private static class BankFact
        {
            public static void Bank_is_not_empty(Bank actual) => Assert.NotEmpty(actual);

            public static void Bank_tabs_have_30_slots(Bank actual) => Assert.Equal(0, actual.Count % 30);
        }

        private static class BankSlotFact
        {
            public static void BankSlot_id_is_positive(BankSlot actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
            
            public static void BankSlot_count_is_positive(BankSlot actual) => Assert.InRange(actual.Count, 1, int.MaxValue);

            public static void BankSlot_remembers_character_bindings(BankSlot actual)
            {
                if (string.IsNullOrEmpty(actual.BoundTo))
                {
                    Assert.NotEqual(ItemBinding.Character, actual.Binding);
                }
                else
                {
                    Assert.Equal(ItemBinding.Character, actual.Binding);
                }
            }
        }


        [Fact]
        public void Bank_can_be_created_from_json()
        {
            var sut = new BankReader();

            using var document = JsonDocument.Parse(_fixture.Bank);

            var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

            BankFact.Bank_is_not_empty(actual);

            BankFact.Bank_tabs_have_30_slots(actual);

            Assert.All(actual,
                slot =>
                {
                    if (slot is null) return;
                    BankSlotFact.BankSlot_id_is_positive(slot);
                    BankSlotFact.BankSlot_count_is_positive(slot);
                    BankSlotFact.BankSlot_remembers_character_bindings(slot);
                });
        }
    }
}
