# Authentication

The library provides methods to retrieve information about a player account, such
as the characters, bank, inventory, wallet, unlocked skins, etc. You need a valid
access token to use these methods.

``` csharp
var characters = await gw2.Accounts.GetCharacters(
    accessToken: "API key or subtoken"
);
```

To obtain an API key, you need a Guild Wars 2 account and log in to
<https://account.arena.net/applications>, create a New Key, and store it
somewhere safely in your application secrets (not in source control.)

You can use the `TokenProvider` class to validate an access token that you received
from someone else. You can also use it to create a subtoken with a shorter time-to-live
or with fewer privileges. This is useful if you wish to give someone else limited
access to your account. For example: if you build a modular application that can
be extended with 3rd-party plugins, you may ask the user for a full-access master
token and then create a subtoken for each plugin, giving it only the permissions
it needs and the time it needs.
