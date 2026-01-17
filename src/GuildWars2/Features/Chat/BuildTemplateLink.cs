using System.ComponentModel;

using GuildWars2.Hero;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Training;

namespace GuildWars2.Chat;

/// <summary>Represents a build template chat link.</summary>
public sealed record BuildTemplateLink : Link
{
    /// <summary>The profession.</summary>
    public required ProfessionName Profession { get; init; }

    /// <summary>The first specialization.</summary>
    public Specialization? Specialization1 { get; init; }

    /// <summary>The second specialization.</summary>
    public Specialization? Specialization2 { get; init; }

    /// <summary>The third specialization.</summary>
    public Specialization? Specialization3 { get; init; }

    /// <summary>The selected skills.</summary>
    public required SkillPalette Skills { get; init; }

    /// <summary>The selected sklls while underwater.</summary>
    public required SkillPalette AquaticSkills { get; init; }

    /// <summary>The selected pet skills (Ranger only).</summary>
    public SelectedPets? Pets { get; init; }

    /// <summary>The selected legends (Revenant only).</summary>
    public Legends? Legends { get; init; }

    /// <summary>The first kind of weapon used in this build.</summary>
    [DefaultValue(WeaponType.None)]
    public WeaponType Weapon1 { get; init; } = WeaponType.None;

    /// <summary>The second kind of weapon used in this build.</summary>
    [DefaultValue(WeaponType.None)]
    public WeaponType Weapon2 { get; init; } = WeaponType.None;

    /// <summary>The third kind of weapon used in this build.</summary>
    [DefaultValue(WeaponType.None)]
    public WeaponType Weapon3 { get; init; } = WeaponType.None;

#pragma warning disable CA1819 // Properties should not return arrays
    // TODO: reconsider collection type
    /// <summary>The skill IDs of weapon skill overrides.</summary>
    public int[] SkillOverrides { get; init; } = [];
#pragma warning restore CA1819 // Properties should not return arrays

    /// <summary>Gets the build represented by this chat link.</summary>
    /// <param name="gw2Client">An API client to fetch the build.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<Build> GetBuild(
        Gw2Client gw2Client,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ThrowHelper.ThrowIfNull(gw2Client);

        Profession? profession = await gw2Client.Hero.Training.GetProfessionByName(
                Profession,
                language,
                missingMemberBehavior,
                cancellationToken
            )
            .ValueOnly()
            .ConfigureAwait(false);
        Dictionary<int, Hero.Builds.Specialization> specializations = [];
        if (SelectedSpecializationIds().Any())
        {
            specializations = await gw2Client.Hero.Builds.GetSpecializationsByIds(
                    SelectedSpecializationIds(),
                    language,
                    missingMemberBehavior,
                    cancellationToken
                )
                .AsDictionary(static specialization => specialization.Id)
                .ValueOnly()
                .ConfigureAwait(false);
        }

        return new Build
        {
            Name = profession.Name,
            Profession = Profession,
            Specialization1 = SelectedSpecialization(Specialization1),
            Specialization2 = SelectedSpecialization(Specialization2),
            Specialization3 = SelectedSpecialization(Specialization3),
            Skills = SkillBar(Skills),
            AquaticSkills = SkillBar(AquaticSkills),
            Pets = Pets,
            Legends = await Legends().ConfigureAwait(false)
        };

        IEnumerable<int> SelectedSpecializationIds()
        {
            if (Specialization1.HasValue)
            {
                yield return Specialization1.Value.Id;
            }

            if (Specialization2.HasValue)
            {
                yield return Specialization2.Value.Id;
            }

            if (Specialization3.HasValue)
            {
                yield return Specialization3.Value.Id;
            }
        }

        SelectedSpecialization? SelectedSpecialization(Specialization? specialization)
        {
            if (specialization.HasValue)
            {
                (int id, SelectedTrait adept, SelectedTrait master, SelectedTrait grandmaster) = specialization.Value;
                IReadOnlyList<int> traits = specializations[id].MajorTraitIds;
                return new SelectedSpecialization
                {
                    Id = id,
                    AdeptTraitId = adept switch
                    {
                        SelectedTrait.Top => traits[0],
                        SelectedTrait.Middle => traits[1],
                        SelectedTrait.Bottom => traits[2],
                        SelectedTrait.None or _ => null
                    },
                    MasterTraitId = master switch
                    {
                        SelectedTrait.Top => traits[3],
                        SelectedTrait.Middle => traits[4],
                        SelectedTrait.Bottom => traits[5],
                        SelectedTrait.None or _ => null
                    },
                    GrandmasterTraitId = grandmaster switch
                    {
                        SelectedTrait.Top => traits[6],
                        SelectedTrait.Middle => traits[7],
                        SelectedTrait.Bottom => traits[8],
                        SelectedTrait.None or _ => null
                    }
                };
            }

            return null;
        }

        SkillBar SkillBar(SkillPalette skills)
        {
            return new SkillBar
            {
                HealSkillId = SkillByPalette(skills.Heal),
                UtilitySkillId1 = SkillByPalette(skills.Utility1),
                UtilitySkillId2 = SkillByPalette(skills.Utility2),
                UtilitySkillId3 = SkillByPalette(skills.Utility3),
                EliteSkillId = SkillByPalette(skills.Elite)
            };

            int? SkillByPalette(int? paletteId)
            {
                if (paletteId.HasValue
                    && profession.SkillsByPalette.TryGetValue(paletteId.Value, out int skillId))
                {
                    return skillId;
                }

                return null;
            }
        }

        async Task<SelectedLegends?> Legends()
        {
            if (this.Legends is null)
            {
                return null;
            }

            (Dictionary<int, Legend> legends, _) = await gw2Client.Hero.Builds
                .GetLegends(missingMemberBehavior, cancellationToken)
                .AsDictionary(static legend => legend.Code)
                .ConfigureAwait(false);

            return new SelectedLegends
            {
                Terrestrial1 = LegendByCode(this.Legends.ActiveTerrestrialLegend),
                Terrestrial2 = LegendByCode(this.Legends.InactiveTerrestrialLegend),
                Aquatic1 = LegendByCode(this.Legends.ActiveAquaticLegend),
                Aquatic2 = LegendByCode(this.Legends.InactiveAquaticLegend)
            };

            string? LegendByCode(int? code)
            {
                if (code.HasValue && legends.TryGetValue(code.Value, out Legend? legend))
                {
                    return legend.Id;
                }

                return null;
            }
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        LinkBuffer buffer = new(stackalloc byte[100]);
        buffer.WriteUInt8(LinkHeader.BuildTemplate);
        buffer.WriteUInt8(Profession());
        buffer.WriteUInt8(SpecializationId(Specialization1));
        buffer.WriteUInt8(Traits(Specialization1));
        buffer.WriteUInt8(SpecializationId(Specialization2));
        buffer.WriteUInt8(Traits(Specialization2));
        buffer.WriteUInt8(SpecializationId(Specialization3));
        buffer.WriteUInt8(Traits(Specialization3));
        buffer.WriteUInt16((ushort)(Skills.Heal ?? default));
        buffer.WriteUInt16((ushort)(AquaticSkills.Heal ?? default));
        buffer.WriteUInt16((ushort)(Skills.Utility1 ?? default));
        buffer.WriteUInt16((ushort)(AquaticSkills.Utility1 ?? default));
        buffer.WriteUInt16((ushort)(Skills.Utility2 ?? default));
        buffer.WriteUInt16((ushort)(AquaticSkills.Utility2 ?? default));
        buffer.WriteUInt16((ushort)(Skills.Utility3 ?? default));
        buffer.WriteUInt16((ushort)(AquaticSkills.Utility3 ?? default));
        buffer.WriteUInt16((ushort)(Skills.Elite ?? default));
        buffer.WriteUInt16((ushort)(AquaticSkills.Elite ?? default));
        if (this.Profession == ProfessionName.Ranger)
        {
            buffer.WriteUInt8((byte)(Pets?.Terrestrial1 ?? 0));
            buffer.WriteUInt8((byte)(Pets?.Terrestrial2 ?? 0));
            buffer.WriteUInt8((byte)(Pets?.Aquatic1 ?? 0));
            buffer.WriteUInt8((byte)(Pets?.Aquatic2 ?? 0));
            buffer.Padding(12);
        }
        else if (this.Profession == ProfessionName.Revenant)
        {
            buffer.WriteUInt8((byte)(Legends?.ActiveTerrestrialLegend ?? 0));
            buffer.WriteUInt8((byte)(Legends?.InactiveTerrestrialLegend ?? 0));
            buffer.WriteUInt8((byte)(Legends?.ActiveAquaticLegend ?? 0));
            buffer.WriteUInt8((byte)(Legends?.InactiveAquaticLegend ?? 0));
            buffer.WriteUInt16((ushort)(Legends?.InactiveTerrestrialSkills.Utility1 ?? 0));
            buffer.WriteUInt16((ushort)(Legends?.InactiveTerrestrialSkills.Utility2 ?? 0));
            buffer.WriteUInt16((ushort)(Legends?.InactiveTerrestrialSkills.Utility3 ?? 0));
            buffer.WriteUInt16((ushort)(Legends?.InactiveAquaticSkills.Utility1 ?? 0));
            buffer.WriteUInt16((ushort)(Legends?.InactiveAquaticSkills.Utility2 ?? 0));
            buffer.WriteUInt16((ushort)(Legends?.InactiveAquaticSkills.Utility3 ?? 0));
        }
        else
        {
            buffer.Padding(16);
        }

        ushort weapon1 = Weapon(Weapon1);
        ushort weapon2 = Weapon(Weapon2);
        ushort weapon3 = Weapon(Weapon3);
        if (weapon3 != 0)
        {
            buffer.WriteUInt8(3);
            buffer.WriteUInt16(weapon1);
            buffer.WriteUInt16(weapon2);
            buffer.WriteUInt16(weapon3);
        }
        else if (weapon2 != 0)
        {
            buffer.WriteUInt8(2);
            buffer.WriteUInt16(weapon1);
            buffer.WriteUInt16(weapon2);
        }
        else if (weapon1 != 0)
        {
            buffer.WriteUInt8(1);
            buffer.WriteUInt16(weapon1);
        }
        else
        {
            buffer.WriteUInt8(0);
        }

        buffer.WriteUInt8((byte)SkillOverrides.Length);
        for (int index = 0; index < (byte)SkillOverrides.Length; index++)
        {
            int skillOverride = SkillOverrides[index];
            buffer.WriteInt32(skillOverride);
        }

        return buffer.ToString();

        byte Profession()
        {
            return this.Profession switch
            {
                ProfessionName.Guardian => 1,
                ProfessionName.Warrior => 2,
                ProfessionName.Engineer => 3,
                ProfessionName.Ranger => 4,
                ProfessionName.Thief => 5,
                ProfessionName.Elementalist => 6,
                ProfessionName.Mesmer => 7,
                ProfessionName.Necromancer => 8,
                ProfessionName.Revenant => 9,
                ProfessionName.None or _ => 0
            };
        }

        byte SpecializationId(Specialization? specialization)
        {
            return (byte)(specialization?.Id ?? 0);
        }

        byte Traits(Specialization? specialization)
        {
            if (specialization is null)
            {
                return 0;
            }

            (_, SelectedTrait adeptTrait, SelectedTrait masterTrait, SelectedTrait grandmasterTrait) = specialization.Value;

            return (byte)((byte)adeptTrait
                | ((byte)masterTrait << 2)
                | ((byte)grandmasterTrait << 4));
        }

        static ushort Weapon(WeaponType weapon)
        {
            return weapon switch
            {
                WeaponType.Axe => 0x0005,
                WeaponType.Longbow => 0x0023,
                WeaponType.Dagger => 0x002f,
                WeaponType.Focus => 0x0031,
                WeaponType.Greatsword => 0x0032,
                WeaponType.Hammer => 0x0033,
                WeaponType.Mace => 0x0035,
                WeaponType.Pistol => 0x0036,
                WeaponType.Rifle => 0x0055,
                WeaponType.Scepter => 0x0056,
                WeaponType.Shield => 0x0057,
                WeaponType.Staff => 0x0059,
                WeaponType.Sword => 0x005a,
                WeaponType.Torch => 0x0066,
                WeaponType.Warhorn => 0x0067,
                WeaponType.ShortBow => 0x006b,
                WeaponType.Spear => 0x0109,
                WeaponType.Trident => 0,
                WeaponType.HarpoonGun => 0,
                WeaponType.LargeBundle => 0,
                WeaponType.None or WeaponType.Nothing => 0,
                WeaponType.Unknown or _ => 0
            };
        }
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static BuildTemplateLink Parse(string chatLink)
    {
        return Parse(chatLink.AsSpan());
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static BuildTemplateLink Parse(in ReadOnlySpan<char> chatLink)
    {
        Span<byte> bytes = GetBytes(chatLink);
        LinkBuffer buffer = new(bytes);
        if (buffer.ReadUInt8() != LinkHeader.BuildTemplate)
        {
            ThrowHelper.ThrowBadArgument("Expected a build template chat link.", nameof(chatLink));
        }

        byte profession = buffer.ReadUInt8();
        byte specializationId1 = buffer.ReadUInt8();
        byte traits1 = buffer.ReadUInt8();
        byte specializationId2 = buffer.ReadUInt8();
        byte traits2 = buffer.ReadUInt8();
        byte specializationId3 = buffer.ReadUInt8();
        byte traits3 = buffer.ReadUInt8();
        ushort healSkill = buffer.ReadUInt16();
        ushort healAquaticSkill = buffer.ReadUInt16();
        ushort utilitySkill1 = buffer.ReadUInt16();
        ushort utilityAquaticSkill1 = buffer.ReadUInt16();
        ushort utilitySkill2 = buffer.ReadUInt16();
        ushort utilityAquaticSkill2 = buffer.ReadUInt16();
        ushort utilitySkill3 = buffer.ReadUInt16();
        ushort utilityAquaticSkill3 = buffer.ReadUInt16();
        ushort eliteSkill = buffer.ReadUInt16();
        ushort eliteAquaticSkill = buffer.ReadUInt16();

        // The next 16 bytes are used for profession-specific data
        SelectedPets? pets = null;
        Legends? legends = null;
        if (profession == 4) // Ranger
        {
            byte terrestrialPet1 = buffer.ReadUInt8();
            byte terrestrialPet2 = buffer.ReadUInt8();
            byte aquaticPet1 = buffer.ReadUInt8();
            byte aquaticPet2 = buffer.ReadUInt8();
            pets = new SelectedPets
            {
                Terrestrial1 = NullIfZero(terrestrialPet1),
                Terrestrial2 = NullIfZero(terrestrialPet2),
                Aquatic1 = NullIfZero(aquaticPet1),
                Aquatic2 = NullIfZero(aquaticPet2)
            };
            buffer.Padding(12);
        }
        else if (profession == 9) // Revenant
        {
            byte activeTerrestrialLegend = buffer.ReadUInt8();
            byte inactiveTerrestrialLegend = buffer.ReadUInt8();
            byte activeAquaticLegend = buffer.ReadUInt8();
            byte inactiveAquaticLegend = buffer.ReadUInt8();
            ushort inactiveTerrestrialUtilitySkill1 = buffer.ReadUInt16();
            ushort inactiveTerrestrialUtilitySkill2 = buffer.ReadUInt16();
            ushort inactiveTerrestrialUtilitySkill3 = buffer.ReadUInt16();
            ushort inactiveAquaticUtilitySkill1 = buffer.ReadUInt16();
            ushort inactiveAquaticUtilitySkill2 = buffer.ReadUInt16();
            ushort inactiveAquaticUtilitySkill3 = buffer.ReadUInt16();
            legends = new Legends
            {
                ActiveTerrestrialLegend = NullIfZero(activeTerrestrialLegend),
                InactiveTerrestrialLegend = NullIfZero(inactiveTerrestrialLegend),
                ActiveAquaticLegend = NullIfZero(activeAquaticLegend),
                InactiveAquaticLegend = NullIfZero(inactiveAquaticLegend),
                InactiveTerrestrialSkills =
                    SkillPalette(
                        default,
                        inactiveTerrestrialUtilitySkill1,
                        inactiveTerrestrialUtilitySkill2,
                        inactiveTerrestrialUtilitySkill3,
                        default
                    ),
                InactiveAquaticSkills = SkillPalette(
                    default,
                    inactiveAquaticUtilitySkill1,
                    inactiveAquaticUtilitySkill2,
                    inactiveAquaticUtilitySkill3,
                    default
                )
            };
        }
        else
        {
            buffer.Padding(16);
        }

        WeaponType weapon1 = WeaponType.None;
        WeaponType weapon2 = WeaponType.None;
        WeaponType weapon3 = WeaponType.None;
        int[]? skillOverrides = null;
        if (!buffer.EndOfFile)
        {
            byte weaponCount = buffer.ReadUInt8();
            for (int i = 0; i < weaponCount; i++)
            {
                ushort weapon = buffer.ReadUInt16();
                if (weapon1 == WeaponType.None)
                {
                    weapon1 = Weapon(weapon);
                }
                else if (weapon2 == WeaponType.None)
                {
                    weapon2 = Weapon(weapon);
                }
                else if (weapon3 == WeaponType.None)
                {
                    weapon3 = Weapon(weapon);
                }
            }

            byte skillOverridesCount = buffer.ReadUInt8();
            for (int i = 0; i < skillOverridesCount; i++)
            {
                skillOverrides ??= new int[skillOverridesCount];
                skillOverrides[i] = buffer.ReadInt32();
            }
        }

        return new BuildTemplateLink
        {
            Profession = Profession(profession),
            Specialization1 = Specialization(specializationId1, traits1),
            Specialization2 = Specialization(specializationId2, traits2),
            Specialization3 = Specialization(specializationId3, traits3),
            Skills = SkillPalette(
                healSkill,
                utilitySkill1,
                utilitySkill2,
                utilitySkill3,
                eliteSkill
            ),
            AquaticSkills =
                SkillPalette(
                    healAquaticSkill,
                    utilityAquaticSkill1,
                    utilityAquaticSkill2,
                    utilityAquaticSkill3,
                    eliteAquaticSkill
                ),
            Pets = pets,
            Legends = legends,
            Weapon1 = weapon1,
            Weapon2 = weapon2,
            Weapon3 = weapon3,
            SkillOverrides = skillOverrides ?? []
        };

        static ProfessionName Profession(int professionId)
        {
            return professionId switch
            {
                1 => ProfessionName.Guardian,
                2 => ProfessionName.Warrior,
                3 => ProfessionName.Engineer,
                4 => ProfessionName.Ranger,
                5 => ProfessionName.Thief,
                6 => ProfessionName.Elementalist,
                7 => ProfessionName.Mesmer,
                8 => ProfessionName.Necromancer,
                9 => ProfessionName.Revenant,
                _ => default
            };
        }

        static Specialization? Specialization(int specializationId, int traits)
        {
            if (specializationId == 0)
            {
                return null;
            }

            SelectedTrait adeptTrait = (SelectedTrait)(traits & 0b11);
            SelectedTrait masterTrait = (SelectedTrait)((traits >> 2) & 0b11);
            SelectedTrait grandmasterTrait = (SelectedTrait)((traits >> 4) & 0b11);
            return new Specialization(specializationId, adeptTrait, masterTrait, grandmasterTrait);
        }

        static SkillPalette SkillPalette(
            ushort heal,
            ushort utility1,
            ushort utility2,
            ushort utility3,
            ushort elite
        )
        {
            return new SkillPalette(
                NullIfZero(heal),
                NullIfZero(utility1),
                NullIfZero(utility2),
                NullIfZero(utility3),
                NullIfZero(elite)
            );
        }

        static int? NullIfZero(int value)
        {
            return value == 0 ? null : value;
        }

        static WeaponType Weapon(int weaponId)
        {
            return weaponId switch
            {
                0x0005 => WeaponType.Axe,
                0x0023 => WeaponType.Longbow,
                0x002f => WeaponType.Dagger,
                0x0031 => WeaponType.Focus,
                0x0032 => WeaponType.Greatsword,
                0x0033 => WeaponType.Hammer,
                0x0035 => WeaponType.Mace,
                0x0036 => WeaponType.Pistol,
                0x0055 => WeaponType.Rifle,
                0x0056 => WeaponType.Scepter,
                0x0057 => WeaponType.Shield,
                0x0059 => WeaponType.Staff,
                0x005a => WeaponType.Sword,
                0x0066 => WeaponType.Torch,
                0x0067 => WeaponType.Warhorn,
                0x006b => WeaponType.ShortBow,
                0x0109 => WeaponType.Spear,
                _ => WeaponType.None
            };
        }
    }
}
