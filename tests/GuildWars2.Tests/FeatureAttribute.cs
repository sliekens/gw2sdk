namespace GuildWars2.Tests;

public sealed class FeatureAttribute(string name) : PropertyAttribute("Category", name);
