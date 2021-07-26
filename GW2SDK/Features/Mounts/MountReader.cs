using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Mounts
{
    [PublicAPI]
    public sealed class MountReader : IMountReader, IJsonReader<Mount>, IJsonReader<MountName>, IJsonReader<MountSkin>
    {
        Mount IJsonReader<Mount>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<MountName>("id");
            var name = new RequiredMember<string>("name");
            var defaultSkin = new RequiredMember<int>("default_skin");
            var skins = new RequiredMember<int[]>("skins");
            var skills = new RequiredMember<SkillReference[]>("skills");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(defaultSkin.Name))
                {
                    defaultSkin = defaultSkin.From(member.Value);
                }
                else if (member.NameEquals(skins.Name))
                {
                    skins = skins.From(member.Value);
                }
                else if (member.NameEquals(skills.Name))
                {
                    skills = skills.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Mount
            {
                Id = id.Select(value => MountId.Read(value, missingMemberBehavior)),
                Name = name.GetValue(),
                DefaultSkin = defaultSkin.GetValue(),
                Skins = skins.Select(value => value.GetArray(item => item.GetInt32())),
                Skills = skills.Select(value => value.GetArray(item => ReadSkill(item, missingMemberBehavior)))
            };
        }

        MountName IJsonReader<MountName>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var text = json.GetStringRequired();
            return text switch
            {
                "griffon" => MountName.Griffon,
                "jackal" => MountName.Jackal,
                "raptor" => MountName.Raptor,
                "roller_beetle" => MountName.RollerBeetle,
                "skimmer" => MountName.Skimmer,
                "skyscale" => MountName.Skyscale,
                "springer" => MountName.Springer,
                "warclaw" => MountName.Warclaw,
                _ when missingMemberBehavior is MissingMemberBehavior.Error => UnexpectedMemberError(),
                _ => (MountName) text.GetDeterministicHashCode()
            };

            MountName UnexpectedMemberError()
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(text));
            }
        }

        public MountSkin Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var icon = new RequiredMember<string>("icon");
            var dyeSlots = new RequiredMember<DyeSlot[]>("dye_slots");
            var mount = new RequiredMember<MountName>("mount");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(dyeSlots.Name))
                {
                    dyeSlots = dyeSlots.From(member.Value);
                }
                else if (member.NameEquals(mount.Name))
                {
                    mount = mount.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new MountSkin
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Icon = icon.GetValue(),
                DyeSlots = dyeSlots.Select(value => value.GetArray(item => ReadDyeSlot(item, missingMemberBehavior))),
                Mount = mount.Select(value => MountId.Read(value, missingMemberBehavior))
            };
        }

        public IJsonReader<Mount> Mount => this;

        public IJsonReader<MountName> MountId => this;

        public IJsonReader<MountSkin> MountSkin => this;

        public IJsonReader<int> MountSkinId { get; } = new Int32JsonReader();

        private SkillReference ReadSkill(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var slot = new RequiredMember<SkillSlot>("slot");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(slot.Name))
                {
                    slot = slot.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new SkillReference
            {
                Id = id.GetValue(),
                Slot = slot.GetValue(missingMemberBehavior)
            };
        }

        private DyeSlot ReadDyeSlot(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var colorId = new RequiredMember<int>("color_id");
            var material = new RequiredMember<Material>("material");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(colorId.Name))
                {
                    colorId = colorId.From(member.Value);
                }
                else if (member.NameEquals(material.Name))
                {
                    material = material.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new DyeSlot
            {
                ColorId = colorId.GetValue(),
                Material = material.GetValue(missingMemberBehavior)
            };
        }
    }
}
