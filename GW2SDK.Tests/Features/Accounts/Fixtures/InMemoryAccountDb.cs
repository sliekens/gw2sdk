namespace GW2SDK.Tests.Features.Accounts.Fixtures
{
    public class InMemoryAccountDb
    {
        public string BasicAccount { get; private set; }

        public string FullAccount { get; private set; }

        public void SetBasicAccount(string json)
        {
            BasicAccount = json;
        }

        public void SetFullAccount(string json)
        {
            FullAccount = json;
        }
    }
}
