# GW2SDK Tests
Yes! We have tests! Amazing! Right?!

Unless you don't know how to run the tests. Luckily there's a readme. Which I assume you're reading. If not... well, stop picking your nose and README!

## Configure API Keys

Some tests require an API key. Head on over to the [API Key Management](https://account.arena.net/applications) page and create one.

- Name: GW2SDK-Dev
- Permissions: all

Next you have to store the key in a place where the tests can find it. You have two options:

1. User secrets
2. Azure Key Vault

NB: the key name must match exactly! We check the name to prevent abuse.\
The reason is that some services associate API keys with logins, as long as you use a key name of _their_ choice.\
The only way for us to counter abuse is by demanding a key name of **our** choice. That way if a key leaks to the outside world, it can't be used to login to those other services.

### User Secrets

This is probably the easiest choice. You'll need the dotnet CLI and then configure your key with the user-secrets tool.

Open your favorite terminal inside the Tests directory and type the magic words:

```sh
/path/to/GW2SDK/GW2SDK.Tests$ dotnet user-secrets set ApiKey XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXXXXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX
```

### Azure Key Vault

The problem with user-secrets is in the name. They're your secrets, just for you. What if you want to run the tests on a build server that (hopefully) doesn't operate under your own identity?
Azure Key Vault comes to the rescue!

A full tutorial is out of scope for this document, but you can find one in the [Quickstart](https://docs.microsoft.com/en-us/azure/key-vault/quick-create-portal).\
What I _will_ do is show you how to create a vault and configure the key using the [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest).

```sh
# You'll need to have an Azure subscription for this already
az login

# Choose a location name from the available ones, I don't think it matters much
az account list-locations --output table

# Create a resource group
az group create --name "GW2SDK" --location westus

# Create a key vault with a globally unique name
az keyvault create --name "something-inspirational" --resource-group "GW2SDK" --location westus

# Add your API key to the vault (--name must be ApiKey, --vault-name is whatever you chose)
az keyvault secret set --vault-name "something-inspirational" --name "ApiKey" --value "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXXXXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
```

Now for the last part you need to configure the location of your vault. You can directly edit the `appsettings.json` file.

```diff
{
- "KeyVaultName": "gw2sdk-keys"
+ "KeyVaultName": "something-inspirational"
}
```

Alternatively you can set it as a user-secret.

```sh
/path/to/GW2SDK/GW2SDK.Tests$ dotnet user-secrets set KeyVaultName "something-inspirational"
```
