﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Novelties;

internal static class NoveltyJson
{
    public static Novelty GetNovelty(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember icon = "icon";
        RequiredMember slot = "slot";
        RequiredMember unlockItems = "unlock_item";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == description.Name)
            {
                description = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == slot.Name)
            {
                slot = member;
            }
            else if (member.Name == unlockItems.Name)
            {
                unlockItems = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Novelty
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetString()) ?? "",
            IconHref = icon.Map(value => value.GetStringRequired()),
            Slot = slot.Map(value => value.GetEnum<NoveltyKind>(missingMemberBehavior)),
            UnlockItems = unlockItems.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}