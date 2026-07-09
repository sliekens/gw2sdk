namespace GuildWars2.ArchitectureTests;

public sealed class FeatureAttribute(string name) : PropertyAttribute("Category", name);
