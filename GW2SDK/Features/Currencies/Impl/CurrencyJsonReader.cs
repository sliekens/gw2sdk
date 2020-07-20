using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Currencies.Impl
{
    public sealed class CurrencyJsonReader : JsonObjectReader<Currency>
    {
        private CurrencyJsonReader()
        {
            Map("id",          to => to.Id);
            Map("name",        to => to.Name);
            Map("description", to => to.Description);
            Map("order",       to => to.Order);
            Map("icon",        to => to.Icon);
            Compile();
        }

        public static JsonObjectReader<Currency> Instance { get; } = new CurrencyJsonReader();
    }
}
