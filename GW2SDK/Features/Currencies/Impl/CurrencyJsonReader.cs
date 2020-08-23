using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Currencies.Impl
{
    public sealed class CurrencyJsonReader : JsonObjectReader<Currency>
    {
        private CurrencyJsonReader()
        {
            Configure(MapCurrency);
        }

        public static IJsonReader<Currency> Instance { get; } = new CurrencyJsonReader();

        private static void MapCurrency(JsonObjectMapping<Currency> currency)
        {
            currency.Map("id", to => to.Id);
            currency.Map("name", to => to.Name);
            currency.Map("description", to => to.Description);
            currency.Map("order", to => to.Order);
            currency.Map("icon", to => to.Icon);
        }
    }
}
