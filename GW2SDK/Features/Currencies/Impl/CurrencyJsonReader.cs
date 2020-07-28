using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Currencies.Impl
{
    public sealed class CurrencyJsonReader : JsonObjectReader2<Currency>
    {
        private CurrencyJsonReader()
        {
            Configure(
                currency =>
                {
                    currency.Map("id",          to => to.Id);
                    currency.Map("name",        to => to.Name);
                    currency.Map("description", to => to.Description);
                    currency.Map("order",       to => to.Order);
                    currency.Map("icon",        to => to.Icon);
                }
            );
        }

        public static IJsonReader<Currency> Instance { get; } = new CurrencyJsonReader();
    }
}
