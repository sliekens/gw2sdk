using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static System.Reflection.BindingFlags;
using static System.String;

namespace GW2SDK
{
    // TODO: (maybe) add methods for replacing route values?
    // For: can provide better validations than simple string operations, e.g. Set("id", value) can throw if the template does not contain /:id/
    // Against: not every route parameter has a placeholder, e.g. "/v2/continents/:id" is missing
    // 
    [PublicAPI]
    public sealed class Location
    {
        private Location(string path)
        {
            if (IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));
            }

            Path = path;
        }

        public string Path { get; }

        public override string ToString() => Path;

        public static implicit operator string(Location location) => location.Path;

        public static IEnumerable<Location> GetValues() =>
            typeof(Location).GetFields(Public | Static | DeclaredOnly).Select(l => l.GetValue(null)).Cast<Location>();

        // ReSharper disable InconsistentNaming
        public static readonly Location Account = new("/v2/account");

        public static readonly Location Account_Achievements = new("/v2/account/achievements");

        public static readonly Location Account_Bank = new("/v2/account/bank");

        public static readonly Location Account_BuildStorage = new("/v2/account/buildstorage");

        public static readonly Location Account_DailyCrafting = new("/v2/account/dailycrafting");

        public static readonly Location Account_Dungeons = new("/v2/account/dungeons");

        public static readonly Location Account_Dyes = new("/v2/account/dyes");

        public static readonly Location Account_Emotes = new("/v2/account/emotes");

        public static readonly Location Account_Finishers = new("/v2/account/finishers");

        public static readonly Location Account_Gliders = new("/v2/account/gliders");

        public static readonly Location Account_Home = new("/v2/account/home");

        public static readonly Location Account_Home_Cats = new("/v2/account/home/cats");

        public static readonly Location Account_Home_Nodes = new("/v2/account/home/nodes");

        public static readonly Location Account_Inventory = new("/v2/account/inventory");

        public static readonly Location Account_Luck = new("/v2/account/luck");

        public static readonly Location Account_Mail = new("/v2/account/mail");

        public static readonly Location Account_MailCarriers = new("/v2/account/mailcarriers");

        public static readonly Location Account_Mapchests = new("/v2/account/mapchests");

        public static readonly Location Account_Masteries = new("/v2/account/masteries");

        public static readonly Location Account_Mastery_Points = new("/v2/account/mastery/points");

        public static readonly Location Account_Materials = new("/v2/account/materials");

        public static readonly Location Account_Minis = new("/v2/account/minis");

        public static readonly Location Account_Mounts = new("/v2/account/mounts");

        public static readonly Location Account_Mounts_Skins = new("/v2/account/mounts/skins");

        public static readonly Location Account_Mounts_Types = new("/v2/account/mounts/types");

        public static readonly Location Account_Novelties = new("/v2/account/novelties");

        public static readonly Location Account_Outfits = new("/v2/account/outfits");

        public static readonly Location Account_Pvp_Heroes = new("/v2/account/pvp/heroes");

        public static readonly Location Account_Raids = new("/v2/account/raids");

        public static readonly Location Account_Recipes = new("/v2/account/recipes");

        public static readonly Location Account_Skins = new("/v2/account/skins");

        public static readonly Location Account_Titles = new("/v2/account/titles");

        public static readonly Location Account_Wallet = new("/v2/account/wallet");

        public static readonly Location Account_Worldbosses = new("/v2/account/worldbosses");

        public static readonly Location Achievements = new("/v2/achievements");

        public static readonly Location Achievements_Categories = new("/v2/achievements/categories");

        public static readonly Location Achievements_Daily = new("/v2/achievements/daily");

        public static readonly Location Achievements_Daily_Tomorrow = new("/v2/achievements/daily/tomorrow");

        public static readonly Location Achievements_Groups = new("/v2/achievements/groups");

        public static readonly Location Adventures = new("/v2/adventures");

        public static readonly Location Adventures_Id_Leaderboards = new("/v2/adventures/:id/leaderboards");

        public static readonly Location Adventures_Id_Leaderboards_Board_Region =
            new("/v2/adventures/:id/leaderboards/:board/:region");

        public static readonly Location Backstory_Answers = new("/v2/backstory/answers");

        public static readonly Location Backstory_Questions = new("/v2/backstory/questions");

        public static readonly Location Build = new("/v2/build");

        public static readonly Location Characters = new("/v2/characters");

        public static readonly Location Characters_Id_Backstory = new("/v2/characters/:id/backstory");

        public static readonly Location Characters_Id_BuildTabs = new("/v2/characters/:id/buildtabs");

        public static readonly Location Characters_Id_BuildTabs_Active = new("/v2/characters/:id/buildtabs/active");

        public static readonly Location Characters_Id_Core = new("/v2/characters/:id/core");

        public static readonly Location Characters_Id_Crafting = new("/v2/characters/:id/crafting");

        public static readonly Location Characters_Id_Dungeons = new("/v2/characters/:id/dungeons");

        public static readonly Location Characters_Id_Equipment = new("/v2/characters/:id/equipment");

        public static readonly Location Characters_Id_EquipmentTabs = new("/v2/characters/:id/equipmenttabs");

        public static readonly Location Characters_Id_EquipmentTabs_Active =
            new("/v2/characters/:id/equipmenttabs/active");

        public static readonly Location Characters_Id_Heropoints = new("/v2/characters/:id/heropoints");

        public static readonly Location Characters_Id_Inventory = new("/v2/characters/:id/inventory");

        public static readonly Location Characters_Id_Quests = new("/v2/characters/:id/quests");

        public static readonly Location Characters_Id_Recipes = new("/v2/characters/:id/recipes");

        public static readonly Location Characters_Id_Sab = new("/v2/characters/:id/sab");

        public static readonly Location Characters_Id_Skills = new("/v2/characters/:id/skills");

        public static readonly Location Characters_Id_Specializations = new("/v2/characters/:id/specializations");

        public static readonly Location Characters_Id_Training = new("/v2/characters/:id/training");

        public static readonly Location Colors = new("/v2/colors");

        public static readonly Location Commerce_Delivery = new("/v2/commerce/delivery");

        public static readonly Location Commerce_Exchange = new("/v2/commerce/exchange");

        public static readonly Location Commerce_Listings = new("/v2/commerce/listings");

        public static readonly Location Commerce_Prices = new("/v2/commerce/prices");

        public static readonly Location Commerce_Transactions = new("/v2/commerce/transactions");

        public static readonly Location Continens = new("/v2/continents");

        public static readonly Location CreateSubtoken = new("/v2/createsubtoken");

        public static readonly Location Currencies = new("/v2/currencies");

        public static readonly Location DailyCrafting = new("/v2/dailycrafting");

        public static readonly Location Dungeons = new("/v2/dungeons");

        public static readonly Location Emblem = new("/v2/emblem");

        public static readonly Location Emotes = new("/v2/emotes");

        public static readonly Location Events = new("/v2/events");

        public static readonly Location EventsState = new("/v2/events-state");

        public static readonly Location Files = new("/v2/files");

        public static readonly Location Finishers = new("/v2/finishers");

        public static readonly Location Gemstore_Catalog = new("/v2/gemstore/catalog");

        public static readonly Location Gliders = new("/v2/gliders");

        public static readonly Location Guild_Id = new("/v2/guild/:id");

        public static readonly Location Guild_Id_Log = new("/v2/guild/:id/log");

        public static readonly Location Guild_Id_Members = new("/v2/guild/:id/members");

        public static readonly Location Guild_Id_Ranks = new("/v2/guild/:id/ranks");

        public static readonly Location Guild_Id_Stash = new("/v2/guild/:id/stash");

        public static readonly Location Guild_Id_Storage = new("/v2/guild/:id/storage");

        public static readonly Location Guild_Id_Teams = new("/v2/guild/:id/teams");

        public static readonly Location Guild_Id_Treasury = new("/v2/guild/:id/treasury");

        public static readonly Location Guild_Id_Upgrades = new("/v2/guild/:id/upgrades");

        public static readonly Location Guild_Permissions = new("/v2/guild/permissions");

        public static readonly Location Guild_Search = new("/v2/guild/search");

        public static readonly Location Guild_Upgrades = new("/v2/guild/upgrades");

        public static readonly Location Home = new("/v2/home");

        public static readonly Location Home_Cats = new("/v2/home/cats");

        public static readonly Location Home_Nodes = new("/v2/home/nodes");

        public static readonly Location Items = new("/v2/items");

        public static readonly Location ItemStats = new("/v2/itemstats");

        public static readonly Location Legends = new("/v2/legends");

        public static readonly Location Mailcarries = new("/v2/mailcarriers");

        public static readonly Location Mapchests = new("/v2/mapchests");

        public static readonly Location Maps = new("/v2/maps");

        public static readonly Location Masteries = new("/v2/masteries");

        public static readonly Location Materials = new("/v2/materials");

        public static readonly Location Minis = new("/v2/minis");

        public static readonly Location Mounts = new("/v2/mounts");

        public static readonly Location Mounts_Skins = new("/v2/mounts/skins");

        public static readonly Location Mounts_Types = new("/v2/mounts/types");

        public static readonly Location Novelties = new("/v2/novelties");

        public static readonly Location Outfits = new("/v2/outfits");

        public static readonly Location Pets = new("/v2/pets");

        public static readonly Location Professions = new("/v2/professions");

        public static readonly Location Pvp = new("/v2/pvp");

        public static readonly Location Pvp_Amulets = new("/v2/pvp/amulets");

        public static readonly Location Pvp_Games = new("/v2/pvp/games");

        public static readonly Location Pvp_Heroes = new("/v2/pvp/heroes");

        public static readonly Location Pvp_Ranks = new("/v2/pvp/ranks");

        public static readonly Location Pvp_Rewardtracks = new("/v2/pvp/rewardtracks");

        public static readonly Location Pvp_Runes = new("/v2/pvp/runes");

        public static readonly Location Pvp_Seasons = new("/v2/pvp/seasons");

        public static readonly Location Pvp_Seasons_Id_Leaderboards = new("/v2/pvp/seasons/:id/leaderboards");

        public static readonly Location Pvp_Seasons_Id_Leaderboards_Board_Region =
            new("/v2/pvp/seasons/:id/leaderboards/:board/:region");

        public static readonly Location Pvp_Sigils = new("/v2/pvp/sigils");

        public static readonly Location Pvp_Standings = new("/v2/pvp/standings");

        public static readonly Location Pvp_Stats = new("/v2/pvp/stats");

        public static readonly Location Quaggans = new("/v2/quaggans");

        public static readonly Location Quests = new("/v2/quests");

        public static readonly Location Races = new("/v2/races");

        public static readonly Location Raids = new("/v2/raids");

        public static readonly Location Recipes = new("/v2/recipes");

        public static readonly Location Recipes_Search = new("/v2/recipes/search");

        public static readonly Location Skills = new("/v2/skills");

        public static readonly Location Skins = new("/v2/skins");

        public static readonly Location Specializations = new("/v2/specializations");

        public static readonly Location Stories = new("/v2/stories");

        public static readonly Location Stories_Seasons = new("/v2/stories/seasons");

        public static readonly Location Titles = new("/v2/titles");

        public static readonly Location Tokeninfo = new("/v2/tokeninfo");

        public static readonly Location Traits = new("/v2/traits");

        public static readonly Location Vendors = new("/v2/vendors");

        public static readonly Location Worldbosses = new("/v2/worldbosses");

        public static readonly Location Worlds = new("/v2/worlds");

        public static readonly Location Wvw_Abilities = new("/v2/wvw/abilities");

        public static readonly Location Wvw_Matches = new("/v2/wvw/matches");

        public static readonly Location Wvw_Matches_Overview = new("/v2/wvw/matches/overview");

        public static readonly Location Wvw_Matches_Scores = new("/v2/wvw/matches/scores");

        public static readonly Location Wvw_Matches_Stats = new("/v2/wvw/matches/stats");

        public static readonly Location Wvw_Matches_Stats_Id_Guilds_GuildId =
            new("/v2/wvw/matches/stats/:id/guilds/:guild_id");

        public static readonly Location Wvw_Matches_Stats_Id_Teams_Team_Top_Kdr =
            new("/v2/wvw/matches/stats/:id/teams/:team/top/kdr");

        public static readonly Location Wvw_Matches_Stats_Id_Teams_Team_Top_Kills =
            new("/v2/wvw/matches/stats/:id/teams/:team/top/kills");

        public static readonly Location Wvw_Objectives = new("/v2/wvw/objectives");

        public static readonly Location Wvw_Ranks = new("/v2/wvw/ranks");

        public static readonly Location Wvw_Rewardtracks = new("/v2/wvw/rewardtracks");

        public static readonly Location Wvw_Upgrades = new("/v2/wvw/upgrades");
        // ReSharper restore InconsistentNaming
    }
}
