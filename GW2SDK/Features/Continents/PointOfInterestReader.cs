using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public sealed class PointOfInterestReader : IJsonReader<PointOfInterest>,
        IJsonReader<Landmark>,
        IJsonReader<Waypoint>,
        IJsonReader<Vista>,
        IJsonReader<UnlockerPointOfInterest>
    {
        Landmark IJsonReader<Landmark>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new OptionalMember<string>("name");
            var floor = new RequiredMember<int>("floor");
            var coordinates = new RequiredMember<double[]>("coord");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("landmark"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(floor.Name))
                {
                    floor = floor.From(member.Value);
                }
                else if (member.NameEquals(coordinates.Name))
                {
                    coordinates = coordinates.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Landmark
            {
                Id = id.GetValue(),
                Name = name.GetValueOrEmpty(),
                Floor = floor.GetValue(),
                Coordinates = coordinates.Select(value => value.GetArray(item => item.GetDouble())),
                ChatLink = chatLink.GetValue()
            };
        }

        public PointOfInterest Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            switch (json.GetProperty("type").GetString())
            {
                case "landmark":
                    return ((IJsonReader<Landmark>) this).Read(json, missingMemberBehavior);
                case "waypoint":
                    return ((IJsonReader<Waypoint>) this).Read(json, missingMemberBehavior);
                case "vista":
                    return ((IJsonReader<Vista>) this).Read(json, missingMemberBehavior);
                case "unlock":
                    return ((IJsonReader<UnlockerPointOfInterest>) this).Read(json, missingMemberBehavior);
            }

            var name = new OptionalMember<string>("name");
            var floor = new RequiredMember<int>("floor");
            var coordinates = new RequiredMember<double[]>("coord");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(floor.Name))
                {
                    floor = floor.From(member.Value);
                }
                else if (member.NameEquals(coordinates.Name))
                {
                    coordinates = coordinates.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new PointOfInterest
            {
                Id = id.GetValue(),
                Name = name.GetValueOrEmpty(),
                Floor = floor.GetValue(),
                Coordinates = coordinates.Select(value => value.GetArray(item => item.GetDouble())),
                ChatLink = chatLink.GetValue()
            };
        }

        UnlockerPointOfInterest IJsonReader<UnlockerPointOfInterest>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new OptionalMember<string>("name");
            var floor = new RequiredMember<int>("floor");
            var coordinates = new RequiredMember<double[]>("coord");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var icon = new RequiredMember<string>("icon");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("unlock"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(floor.Name))
                {
                    floor = floor.From(member.Value);
                }
                else if (member.NameEquals(coordinates.Name))
                {
                    coordinates = coordinates.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new UnlockerPointOfInterest
            {
                Id = id.GetValue(),
                Name = name.GetValueOrEmpty(),
                Floor = floor.GetValue(),
                Coordinates = coordinates.Select(value => value.GetArray(item => item.GetDouble())),
                ChatLink = chatLink.GetValue(),
                Icon = icon.GetValue()
            };
        }

        Vista IJsonReader<Vista>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new OptionalMember<string>("name");
            var floor = new RequiredMember<int>("floor");
            var coordinates = new RequiredMember<double[]>("coord");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("vista"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(floor.Name))
                {
                    floor = floor.From(member.Value);
                }
                else if (member.NameEquals(coordinates.Name))
                {
                    coordinates = coordinates.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Vista
            {
                Id = id.GetValue(),
                Name = name.GetValueOrEmpty(),
                Floor = floor.GetValue(),
                Coordinates = coordinates.Select(value => value.GetArray(item => item.GetDouble())),
                ChatLink = chatLink.GetValue()
            };
        }

        Waypoint IJsonReader<Waypoint>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new OptionalMember<string>("name");
            var floor = new RequiredMember<int>("floor");
            var coordinates = new RequiredMember<double[]>("coord");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("waypoint"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(floor.Name))
                {
                    floor = floor.From(member.Value);
                }
                else if (member.NameEquals(coordinates.Name))
                {
                    coordinates = coordinates.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Waypoint
            {
                Id = id.GetValue(),
                Name = name.GetValueOrEmpty(),
                Floor = floor.GetValue(),
                Coordinates = coordinates.Select(value => value.GetArray(item => item.GetDouble())),
                ChatLink = chatLink.GetValue()
            };
        }
    }
}
