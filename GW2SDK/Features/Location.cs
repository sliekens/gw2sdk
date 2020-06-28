using System;
using System.Collections.Generic;
using System.Linq;
using static System.Reflection.BindingFlags;
using static System.String;

namespace GW2SDK
{
    public sealed class Location : IComparable
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

        public int CompareTo(object obj) => Compare(Path, ((Location) obj).Path, StringComparison.Ordinal);

        public override bool Equals(object obj)
        {
            if (!(obj is Location other))
            {
                return false;
            }

            return Path == other.Path;
        }

        public override int GetHashCode() => Path.GetHashCode();

        public override string ToString() => Path;

        public static implicit operator string(Location location) => location.Path;

        public static IEnumerable<Location> GetValues() =>
            typeof(Location).GetFields(Public | Static | DeclaredOnly)
                .Select(l => l.GetValue(null))
                .Cast<Location>();

        // ReSharper disable InconsistentNaming
        public static readonly Location Account = new Location("/v2/account");

        public static readonly Location Account_Achievements = new Location("/v2/account/achievements");

        public static readonly Location Account_Bank = new Location("/v2/account/bank");

        public static readonly Location Account_BuildStorage = new Location("/v2/account/buildstorage");

        public static readonly Location Account_DailyCrafting = new Location("/v2/account/dailycrafting");

        public static readonly Location Account_Dungeons = new Location("/v2/account/dungeons");

        public static readonly Location Account_Dyes = new Location("/v2/account/dyes");

        public static readonly Location Account_Emotes = new Location("/v2/account/emotes");

        public static readonly Location Account_Finishers = new Location("/v2/account/finishers");

        public static readonly Location Account_Gliders = new Location("/v2/account/gliders");

        public static readonly Location Account_Home = new Location("/v2/account/home");

        public static readonly Location Account_Home_Cats = new Location("/v2/account/home/cats");

        public static readonly Location Account_Home_Nodes = new Location("/v2/account/home/nodes");

        public static readonly Location Account_Inventory = new Location("/v2/account/inventory");

        public static readonly Location Account_Luck = new Location("/v2/account/luck");

        public static readonly Location Account_Mail = new Location("/v2/account/mail");

        public static readonly Location Account_Mailcarriers = new Location("/v2/account/mailcarriers");

        public static readonly Location Account_Mapchests = new Location("/v2/account/mapchests");

        public static readonly Location Account_Masteries = new Location("/v2/account/masteries");

        public static readonly Location Account_Mastery_Points = new Location("/v2/account/mastery/points");

        public static readonly Location Account_Materials = new Location("/v2/account/materials");

        public static readonly Location Account_Minis = new Location("/v2/account/minis");

        public static readonly Location Account_Mounts = new Location("/v2/account/mounts");

        public static readonly Location Account_Mounts_Skins = new Location("/v2/account/mounts/skins");

        public static readonly Location Account_Mounts_Types = new Location("/v2/account/mounts/types");

        public static readonly Location Account_Novelties = new Location("/v2/account/novelties");

        public static readonly Location Account_Outfits = new Location("/v2/account/outfits");

        public static readonly Location Account_Pvp_Heroes = new Location("/v2/account/pvp/heroes");

        public static readonly Location Account_Raids = new Location("/v2/account/raids");

        public static readonly Location Account_Recipes = new Location("/v2/account/recipes");

        public static readonly Location Account_Skins = new Location("/v2/account/skins");

        public static readonly Location Account_Titles = new Location("/v2/account/titles");

        public static readonly Location Account_Wallet = new Location("/v2/account/wallet");

        public static readonly Location Account_Worldbosses = new Location("/v2/account/worldbosses");

        public static readonly Location Achievements = new Location("/v2/achievements");

        public static readonly Location Achievements_Categories = new Location("/v2/achievements/categories");

        public static readonly Location Achievements_Daily = new Location("/v2/achievements/daily");

        public static readonly Location Achievements_Daily_Tomorrow = new Location("/v2/achievements/daily/tomorrow");

        public static readonly Location Achievements_Groups = new Location("/v2/achievements/groups");

        public static readonly Location Adventures = new Location("/v2/adventures");

        public static readonly Location Adventures_Id_Leaderboards = new Location("/v2/adventures/:id/leaderboards");

        public static readonly Location Adventures_Id_Leaderboards_Board_Region = new Location("/v2/adventures/:id/leaderboards/:board/:region");

        public static readonly Location Backstory_Answers = new Location("/v2/backstory/answers");

        public static readonly Location Backstory_Questions = new Location("/v2/backstory/questions");

        public static readonly Location Build = new Location("/v2/build");

        public static readonly Location Characters = new Location("/v2/characters");

        public static readonly Location Characters_Id_Backstory = new Location("/v2/characters/:id/backstory");

        public static readonly Location Characters_Id_BuildTabs = new Location("/v2/characters/:id/buildtabs");

        public static readonly Location Characters_Id_BuildTabs_Active = new Location("/v2/characters/:id/buildtabs/active");

        public static readonly Location Characters_Id_Core = new Location("/v2/characters/:id/core");

        public static readonly Location Characters_Id_Crafting = new Location("/v2/characters/:id/crafting");

        public static readonly Location Characters_Id_Dungeons = new Location("/v2/characters/:id/dungeons");

        public static readonly Location Characters_Id_Equipment = new Location("/v2/characters/:id/equipment");

        public static readonly Location Characters_Id_EquipmentTabs = new Location("/v2/characters/:id/equipmenttabs");

        public static readonly Location Characters_Id_EquipmentTabs_Active = new Location("/v2/characters/:id/equipmenttabs/active");

        public static readonly Location Characters_Id_Heropoints = new Location("/v2/characters/:id/heropoints");

        public static readonly Location Characters_Id_Inventory = new Location("/v2/characters/:id/inventory");

        public static readonly Location Characters_Id_Quests = new Location("/v2/characters/:id/quests");

        public static readonly Location Characters_Id_Recipes = new Location("/v2/characters/:id/recipes");

        public static readonly Location Characters_Id_Sab = new Location("/v2/characters/:id/sab");

        public static readonly Location Characters_Id_Skills = new Location("/v2/characters/:id/skills");

        public static readonly Location Characters_Id_Specializations = new Location("/v2/characters/:id/specializations");

        public static readonly Location Characters_Id_Training = new Location("/v2/characters/:id/training");

        public static readonly Location Colors = new Location("/v2/colors");

        public static readonly Location Commerce_Delivery = new Location("/v2/commerce/delivery");

        public static readonly Location Commerce_Exchange = new Location("/v2/commerce/exchange");

        public static readonly Location Commerce_Listings = new Location("/v2/commerce/listings");

        public static readonly Location Commerce_Prices = new Location("/v2/commerce/prices");

        public static readonly Location Commerce_Transactions = new Location("/v2/commerce/transactions");

        public static readonly Location Continens = new Location("/v2/continents");

        public static readonly Location CreateSubtoken = new Location("/v2/createsubtoken");

        public static readonly Location Currencies = new Location("/v2/currencies");

        public static readonly Location DailyCrafting = new Location("/v2/dailycrafting");

        public static readonly Location Dungeons = new Location("/v2/dungeons");

        public static readonly Location Emblem = new Location("/v2/emblem");

        public static readonly Location Emotes = new Location("/v2/emotes");

        public static readonly Location Events = new Location("/v2/events");

        public static readonly Location EventsState = new Location("/v2/events-state");

        public static readonly Location Files = new Location("/v2/files");

        public static readonly Location Finishers = new Location("/v2/finishers");

        public static readonly Location Gemstore_Catalog = new Location("/v2/gemstore/catalog");

        public static readonly Location Gliders = new Location("/v2/gliders");

        public static readonly Location Guild_Id = new Location("/v2/guild/:id");

        public static readonly Location Guild_Id_Log = new Location("/v2/guild/:id/log");

        public static readonly Location Guild_Id_Members = new Location("/v2/guild/:id/members");

        public static readonly Location Guild_Id_Ranks = new Location("/v2/guild/:id/ranks");

        public static readonly Location Guild_Id_Stash = new Location("/v2/guild/:id/stash");

        public static readonly Location Guild_Id_Storage = new Location("/v2/guild/:id/storage");

        public static readonly Location Guild_Id_Teams = new Location("/v2/guild/:id/teams");

        public static readonly Location Guild_Id_Treasury = new Location("/v2/guild/:id/treasury");

        public static readonly Location Guild_Id_Upgrades = new Location("/v2/guild/:id/upgrades");

        public static readonly Location Guild_Permissions = new Location("/v2/guild/permissions");

        public static readonly Location Guild_Search = new Location("/v2/guild/search");

        public static readonly Location Guild_Upgrades = new Location("/v2/guild/upgrades");

        public static readonly Location Home = new Location("/v2/home");

        public static readonly Location Home_Cats = new Location("/v2/home/cats");

        public static readonly Location Home_Nodes = new Location("/v2/home/nodes");

        public static readonly Location Items = new Location("/v2/items");

        public static readonly Location ItemStats = new Location("/v2/itemstats");

        public static readonly Location Legends = new Location("/v2/legends");

        public static readonly Location Mailcarries = new Location("/v2/mailcarriers");

        public static readonly Location Mapchests = new Location("/v2/mapchests");

        public static readonly Location Maps = new Location("/v2/maps");

        public static readonly Location Masteries = new Location("/v2/masteries");

        public static readonly Location Materials = new Location("/v2/materials");

        public static readonly Location Minis = new Location("/v2/minis");

        public static readonly Location Mounts = new Location("/v2/mounts");

        public static readonly Location Mounts_Skins = new Location("/v2/mounts/skins");

        public static readonly Location Mounts_Types = new Location("/v2/mounts/types");

        public static readonly Location Novelties = new Location("/v2/novelties");

        public static readonly Location Outfits = new Location("/v2/outfits");

        public static readonly Location Pets = new Location("/v2/pets");

        public static readonly Location Professions = new Location("/v2/professions");

        public static readonly Location Pvp = new Location("/v2/pvp");

        public static readonly Location Pvp_Amulets = new Location("/v2/pvp/amulets");

        public static readonly Location Pvp_Games = new Location("/v2/pvp/games");

        public static readonly Location Pvp_Heroes = new Location("/v2/pvp/heroes");

        public static readonly Location Pvp_Ranks = new Location("/v2/pvp/ranks");

        public static readonly Location Pvp_Rewardtracks = new Location("/v2/pvp/rewardtracks");

        public static readonly Location Pvp_Runes = new Location("/v2/pvp/runes");

        public static readonly Location Pvp_Seasons = new Location("/v2/pvp/seasons");

        public static readonly Location Pvp_Seasons_Id_Leaderboards = new Location("/v2/pvp/seasons/:id/leaderboards");

        public static readonly Location Pvp_Seasons_Id_Leaderboards_Board_Region = new Location("/v2/pvp/seasons/:id/leaderboards/:board/:region");

        public static readonly Location Pvp_Sigils = new Location("/v2/pvp/sigils");

        public static readonly Location Pvp_Standings = new Location("/v2/pvp/standings");

        public static readonly Location Pvp_Stats = new Location("/v2/pvp/stats");

        public static readonly Location Quaggans = new Location("/v2/quaggans");

        public static readonly Location Quests = new Location("/v2/quests");

        public static readonly Location Races = new Location("/v2/races");

        public static readonly Location Raids = new Location("/v2/raids");

        public static readonly Location Recipes = new Location("/v2/recipes");

        public static readonly Location Recipes_Search = new Location("/v2/recipes/search");

        public static readonly Location Skills = new Location("/v2/skills");

        public static readonly Location Skins = new Location("/v2/skins");

        public static readonly Location Specializations = new Location("/v2/specializations");

        public static readonly Location Stories = new Location("/v2/stories");

        public static readonly Location Stories_Seasons = new Location("/v2/stories/seasons");

        public static readonly Location Titles = new Location("/v2/titles");

        public static readonly Location Tokeninfo = new Location("/v2/tokeninfo");

        public static readonly Location Traits = new Location("/v2/traits");

        public static readonly Location Vendors = new Location("/v2/vendors");

        public static readonly Location Worldbosses = new Location("/v2/worldbosses");

        public static readonly Location Worlds = new Location("/v2/worlds");

        public static readonly Location Wvw_Abilities = new Location("/v2/wvw/abilities");

        public static readonly Location Wvw_Matches = new Location("/v2/wvw/matches");

        public static readonly Location Wvw_Matches_Overview = new Location("/v2/wvw/matches/overview");

        public static readonly Location Wvw_Matches_Scores = new Location("/v2/wvw/matches/scores");

        public static readonly Location Wvw_Matches_Stats = new Location("/v2/wvw/matches/stats");

        public static readonly Location Wvw_Matches_Stats_Id_Guilds_GuildId = new Location("/v2/wvw/matches/stats/:id/guilds/:guild_id");

        public static readonly Location Wvw_Matches_Stats_Id_Teams_Team_Top_Kdr = new Location("/v2/wvw/matches/stats/:id/teams/:team/top/kdr");

        public static readonly Location Wvw_Matches_Stats_Id_Teams_Team_Top_Kills = new Location("/v2/wvw/matches/stats/:id/teams/:team/top/kills");

        public static readonly Location Wvw_Objectives = new Location("/v2/wvw/objectives");

        public static readonly Location Wvw_Ranks = new Location("/v2/wvw/ranks");

        public static readonly Location Wvw_Rewardtracks = new Location("/v2/wvw/rewardtracks");

        public static readonly Location Wvw_Upgrades = new Location("/v2/wvw/upgrades");
        // ReSharper restore InconsistentNaming
    }
}
