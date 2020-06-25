# GW2SDK tests

Before you can run the tests, there are a few things you need to do.

- Set up API keys
- Make your keys available to the test suite

## Configure API keys

Some tests require an API key. Head on over to the [API Key Management](https://account.arena.net/applications) page and create two keys.

- Name: GW2SDK-Basic, permissions: 'account' only
- Name: GW2SDK-Full, permissions: all available permissions

NB: the key names must start with "GW2SDK"! We check the name to prevent abuse.\
The reason is that some services associate API keys with logins, as long as you use a key name of _their_ choice.\
The only way for us to counter abuse is by demanding a key name of **our** choice. That way if a key leaks to the outside world, it can't be used to login to those other services.

### User secrets

Next you'll need to add your keys to the test suite with the user-secrets tool.

Open your favorite terminal inside the Tests directory and type the magic words:

```sh
cd /path/to/GW2SDK/GW2SDK.Tests
dotnet user-secrets set ApiKeyBasic XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXXXXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX
dotnet user-secrets set ApiKeyFull YYYYYYYY-YYYY-YYYY-YYYY-YYYYYYYYYYYYYYYYYYYY-YYYY-YYYY-YYYY-YYYYYYYYYYYY
```
