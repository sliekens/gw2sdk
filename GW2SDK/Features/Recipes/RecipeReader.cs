using JetBrains.Annotations;
using GW2SDK.Json;
using System;
using System.Text.Json;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed class RecipeReader : IRecipeReader,
        IJsonReader<AmuletRecipe>,
        IJsonReader<AxeRecipe>,
        IJsonReader<BackpackRecipe>,
        IJsonReader<BagRecipe>,
        IJsonReader<BootsRecipe>,
        IJsonReader<BulkRecipe>,
        IJsonReader<CoatRecipe>,
        IJsonReader<ComponentRecipe>,
        IJsonReader<ConsumableRecipe>,
        IJsonReader<DaggerRecipe>,
        IJsonReader<DessertRecipe>,
        IJsonReader<DyeRecipe>,
        IJsonReader<EarringRecipe>,
        IJsonReader<FeastRecipe>,
        IJsonReader<FocusRecipe>,
        IJsonReader<FoodRecipe>,
        IJsonReader<GlovesRecipe>,
        IJsonReader<GreatswordRecipe>,
        IJsonReader<GuildConsumableRecipe>,
        IJsonReader<GuildConsumableWvwRecipe>,
        IJsonReader<GuildDecorationRecipe>,
        IJsonReader<HammerRecipe>,
        IJsonReader<SpearRecipe>,
        IJsonReader<HelmRecipe>,
        IJsonReader<IngredientCookingRecipe>,
        IJsonReader<InscriptionRecipe>,
        IJsonReader<InsigniaRecipe>,
        IJsonReader<LegendaryComponentRecipe>,
        IJsonReader<LeggingsRecipe>,
        IJsonReader<LongbowRecipe>,
        IJsonReader<MaceRecipe>,
        IJsonReader<MealRecipe>,
        IJsonReader<PistolRecipe>,
        IJsonReader<PotionRecipe>,
        IJsonReader<RefinementRecipe>,
        IJsonReader<RefinementEctoplasmRecipe>,
        IJsonReader<RefinementObsidianRecipe>,
        IJsonReader<RifleRecipe>,
        IJsonReader<RingRecipe>,
        IJsonReader<ScepterRecipe>,
        IJsonReader<SeasoningRecipe>,
        IJsonReader<ShieldRecipe>,
        IJsonReader<ShortbowRecipe>,
        IJsonReader<ShouldersRecipe>,
        IJsonReader<SnackRecipe>,
        IJsonReader<SoupRecipe>,
        IJsonReader<HarpoonGunRecipe>,
        IJsonReader<StaffRecipe>,
        IJsonReader<SwordRecipe>,
        IJsonReader<TorchRecipe>,
        IJsonReader<TridentRecipe>,
        IJsonReader<UpgradeComponentRecipe>,
        IJsonReader<WarhornRecipe>
    {
        AmuletRecipe IJsonReader<AmuletRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Amulet"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new AmuletRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        AxeRecipe IJsonReader<AxeRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Axe"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new AxeRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        BackpackRecipe IJsonReader<BackpackRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Backpack"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new BackpackRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        BagRecipe IJsonReader<BagRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Bag"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new BagRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        BootsRecipe IJsonReader<BootsRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Boots"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new BootsRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        BulkRecipe IJsonReader<BulkRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Bulk"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new BulkRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        CoatRecipe IJsonReader<CoatRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Coat"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new CoatRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        ComponentRecipe IJsonReader<ComponentRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Component"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new ComponentRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        ConsumableRecipe IJsonReader<ConsumableRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Consumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new ConsumableRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        DaggerRecipe IJsonReader<DaggerRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Dagger"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new DaggerRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        DessertRecipe IJsonReader<DessertRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Dessert"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new DessertRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        DyeRecipe IJsonReader<DyeRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Dye"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new DyeRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        EarringRecipe IJsonReader<EarringRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Earring"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new EarringRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        FeastRecipe IJsonReader<FeastRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Feast"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new FeastRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        FocusRecipe IJsonReader<FocusRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Focus"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new FocusRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        FoodRecipe IJsonReader<FoodRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Food"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new FoodRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        GlovesRecipe IJsonReader<GlovesRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Gloves"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new GlovesRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        GreatswordRecipe IJsonReader<GreatswordRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Greatsword"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new GreatswordRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        GuildConsumableRecipe IJsonReader<GuildConsumableRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var guildIngredients = new OptionalMember<GuildIngredient[]>("guild_ingredients");
            var outputUpgradeId = new RequiredMember<int>("output_upgrade_id");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("GuildConsumable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
                }
                else if (member.NameEquals(guildIngredients.Name))
                {
                    guildIngredients = guildIngredients.From(member.Value);
                }
                else if (member.NameEquals(outputUpgradeId.Name))
                {
                    outputUpgradeId = outputUpgradeId.From(member.Value);
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

            return new GuildConsumableRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                GuildIngredients = guildIngredients.Select(value => value.GetArray(item => ReadGuildIngredient(item, missingMemberBehavior))),
                OutputUpgradeId = outputUpgradeId.GetValue(),
                ChatLink = chatLink.GetValue()
            };
        }

        GuildConsumableWvwRecipe IJsonReader<GuildConsumableWvwRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var outputUpgradeId = new NullableMember<int>("output_upgrade_id");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("GuildConsumableWvw"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
                }
                else if (member.NameEquals(outputUpgradeId.Name))
                {
                    outputUpgradeId = outputUpgradeId.From(member.Value);
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

            return new GuildConsumableWvwRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                OutputUpgradeId = outputUpgradeId.GetValue(),
                ChatLink = chatLink.GetValue()
            };
        }

        GuildDecorationRecipe IJsonReader<GuildDecorationRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var guildIngredients = new OptionalMember<GuildIngredient[]>("guild_ingredients");
            var outputUpgradeId = new RequiredMember<int>("output_upgrade_id");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("GuildDecoration"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
                }
                else if (member.NameEquals(guildIngredients.Name))
                {
                    guildIngredients = guildIngredients.From(member.Value);
                }
                else if (member.NameEquals(outputUpgradeId.Name))
                {
                    outputUpgradeId = outputUpgradeId.From(member.Value);
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

            return new GuildDecorationRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                GuildIngredients = guildIngredients.Select(value => value.GetArray(item => ReadGuildIngredient(item, missingMemberBehavior))),
                OutputUpgradeId = outputUpgradeId.GetValue(),
                ChatLink = chatLink.GetValue()
            };
        }

        HammerRecipe IJsonReader<HammerRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Hammer"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new HammerRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        SpearRecipe IJsonReader<SpearRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Harpoon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new SpearRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        HelmRecipe IJsonReader<HelmRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Helm"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new HelmRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        IngredientCookingRecipe IJsonReader<IngredientCookingRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("IngredientCooking"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new IngredientCookingRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        InscriptionRecipe IJsonReader<InscriptionRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Inscription"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new InscriptionRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        InsigniaRecipe IJsonReader<InsigniaRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Insignia"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new InsigniaRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        LegendaryComponentRecipe IJsonReader<LegendaryComponentRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("LegendaryComponent"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new LegendaryComponentRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        LeggingsRecipe IJsonReader<LeggingsRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Leggings"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new LeggingsRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        LongbowRecipe IJsonReader<LongbowRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("LongBow"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new LongbowRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        MaceRecipe IJsonReader<MaceRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Mace"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new MaceRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        MealRecipe IJsonReader<MealRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Meal"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new MealRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        PistolRecipe IJsonReader<PistolRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Pistol"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new PistolRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        PotionRecipe IJsonReader<PotionRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Potion"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new PotionRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        public Recipe Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("type").GetString())
            {
                case "Amulet":
                    return ((IJsonReader<AmuletRecipe>)this).Read(json, missingMemberBehavior);
                case "Axe":
                    return ((IJsonReader<AxeRecipe>)this).Read(json, missingMemberBehavior);
                case "Backpack":
                    return ((IJsonReader<BackpackRecipe>)this).Read(json, missingMemberBehavior);
                case "Bag":
                    return ((IJsonReader<BagRecipe>)this).Read(json, missingMemberBehavior);
                case "Boots":
                    return ((IJsonReader<BootsRecipe>)this).Read(json, missingMemberBehavior);
                case "Bulk":
                    return ((IJsonReader<BulkRecipe>)this).Read(json, missingMemberBehavior);
                case "Coat":
                    return ((IJsonReader<CoatRecipe>)this).Read(json, missingMemberBehavior);
                case "Component":
                    return ((IJsonReader<ComponentRecipe>)this).Read(json, missingMemberBehavior);
                case "Consumable":
                    return ((IJsonReader<ConsumableRecipe>)this).Read(json, missingMemberBehavior);
                case "Dagger":
                    return ((IJsonReader<DaggerRecipe>)this).Read(json, missingMemberBehavior);
                case "Dessert":
                    return ((IJsonReader<DessertRecipe>)this).Read(json, missingMemberBehavior);
                case "Dye":
                    return ((IJsonReader<DyeRecipe>)this).Read(json, missingMemberBehavior);
                case "Earring":
                    return ((IJsonReader<EarringRecipe>)this).Read(json, missingMemberBehavior);
                case "Feast":
                    return ((IJsonReader<FeastRecipe>)this).Read(json, missingMemberBehavior);
                case "Focus":
                    return ((IJsonReader<FocusRecipe>)this).Read(json, missingMemberBehavior);
                case "Food":
                    return ((IJsonReader<FoodRecipe>)this).Read(json, missingMemberBehavior);
                case "Gloves":
                    return ((IJsonReader<GlovesRecipe>)this).Read(json, missingMemberBehavior);
                case "Greatsword":
                    return ((IJsonReader<GreatswordRecipe>)this).Read(json, missingMemberBehavior);
                case "GuildConsumable":
                    return ((IJsonReader<GuildConsumableRecipe>)this).Read(json, missingMemberBehavior);
                case "GuildConsumableWvw":
                    return ((IJsonReader<GuildConsumableWvwRecipe>)this).Read(json, missingMemberBehavior);
                case "GuildDecoration":
                    return ((IJsonReader<GuildDecorationRecipe>)this).Read(json, missingMemberBehavior);
                case "Hammer":
                    return ((IJsonReader<HammerRecipe>)this).Read(json, missingMemberBehavior);
                case "Harpoon":
                    return ((IJsonReader<SpearRecipe>)this).Read(json, missingMemberBehavior);
                case "Helm":
                    return ((IJsonReader<HelmRecipe>)this).Read(json, missingMemberBehavior);
                case "IngredientCooking":
                    return ((IJsonReader<IngredientCookingRecipe>)this).Read(json, missingMemberBehavior);
                case "Inscription":
                    return ((IJsonReader<InscriptionRecipe>)this).Read(json, missingMemberBehavior);
                case "Insignia":
                    return ((IJsonReader<InsigniaRecipe>)this).Read(json, missingMemberBehavior);
                case "LegendaryComponent":
                    return ((IJsonReader<LegendaryComponentRecipe>)this).Read(json, missingMemberBehavior);
                case "Leggings":
                    return ((IJsonReader<LeggingsRecipe>)this).Read(json, missingMemberBehavior);
                case "LongBow":
                    return ((IJsonReader<LongbowRecipe>)this).Read(json, missingMemberBehavior);
                case "Mace":
                    return ((IJsonReader<MaceRecipe>)this).Read(json, missingMemberBehavior);
                case "Meal":
                    return ((IJsonReader<MealRecipe>)this).Read(json, missingMemberBehavior);
                case "Pistol":
                    return ((IJsonReader<PistolRecipe>)this).Read(json, missingMemberBehavior);
                case "Potion":
                    return ((IJsonReader<PotionRecipe>)this).Read(json, missingMemberBehavior);
                case "Refinement":
                    return ((IJsonReader<RefinementRecipe>)this).Read(json, missingMemberBehavior);
                case "RefinementEctoplasm":
                    return ((IJsonReader<RefinementEctoplasmRecipe>)this).Read(json, missingMemberBehavior);
                case "RefinementObsidian":
                    return ((IJsonReader<RefinementObsidianRecipe>)this).Read(json, missingMemberBehavior);
                case "Rifle":
                    return ((IJsonReader<RifleRecipe>)this).Read(json, missingMemberBehavior);
                case "Ring":
                    return ((IJsonReader<RingRecipe>)this).Read(json, missingMemberBehavior);
                case "Scepter":
                    return ((IJsonReader<ScepterRecipe>)this).Read(json, missingMemberBehavior);
                case "Seasoning":
                    return ((IJsonReader<SeasoningRecipe>)this).Read(json, missingMemberBehavior);
                case "Shield":
                    return ((IJsonReader<ShieldRecipe>)this).Read(json, missingMemberBehavior);
                case "ShortBow":
                    return ((IJsonReader<ShortbowRecipe>)this).Read(json, missingMemberBehavior);
                case "Shoulders":
                    return ((IJsonReader<ShouldersRecipe>)this).Read(json, missingMemberBehavior);
                case "Snack":
                    return ((IJsonReader<SnackRecipe>)this).Read(json, missingMemberBehavior);
                case "Soup":
                    return ((IJsonReader<SoupRecipe>)this).Read(json, missingMemberBehavior);
                case "Speargun":
                    return ((IJsonReader<HarpoonGunRecipe>)this).Read(json, missingMemberBehavior);
                case "Staff":
                    return ((IJsonReader<StaffRecipe>)this).Read(json, missingMemberBehavior);
                case "Sword":
                    return ((IJsonReader<SwordRecipe>)this).Read(json, missingMemberBehavior);
                case "Torch":
                    return ((IJsonReader<TorchRecipe>)this).Read(json, missingMemberBehavior);
                case "Trident":
                    return ((IJsonReader<TridentRecipe>)this).Read(json, missingMemberBehavior);
                case "UpgradeComponent":
                    return ((IJsonReader<UpgradeComponentRecipe>)this).Read(json, missingMemberBehavior);
                case "Warhorn":
                    return ((IJsonReader<WarhornRecipe>)this).Read(json, missingMemberBehavior);
            }

            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
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
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new Recipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        private Ingredient ReadIngredient(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var type = new RequiredMember<IngredientKind>("type");
            var id = new RequiredMember<int>("id");
            var count = new RequiredMember<int>("count");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(type.Name))
                {
                    type = type.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(count.Name))
                {
                    count = count.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Ingredient
            {
                Kind = type.GetValue(missingMemberBehavior),
                Id = id.GetValue(),
                Count = count.GetValue()
            };
        }

        private GuildIngredient ReadGuildIngredient(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var upgradeId = new RequiredMember<int>("upgrade_id");
            var count = new RequiredMember<int>("count");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(upgradeId.Name))
                {
                    upgradeId = upgradeId.From(member.Value);
                }
                else if (member.NameEquals(count.Name))
                {
                    count = count.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new GuildIngredient
            {
                UpgradeId = upgradeId.GetValue(),
                Count = count.GetValue()
            };
        }

        RefinementEctoplasmRecipe IJsonReader<RefinementEctoplasmRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("RefinementEctoplasm"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new RefinementEctoplasmRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        RefinementObsidianRecipe IJsonReader<RefinementObsidianRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("RefinementObsidian"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new RefinementObsidianRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        RefinementRecipe IJsonReader<RefinementRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Refinement"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new RefinementRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        RifleRecipe IJsonReader<RifleRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Rifle"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new RifleRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        RingRecipe IJsonReader<RingRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Ring"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new RingRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        ScepterRecipe IJsonReader<ScepterRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Scepter"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new ScepterRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        SeasoningRecipe IJsonReader<SeasoningRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Seasoning"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new SeasoningRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        ShieldRecipe IJsonReader<ShieldRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Shield"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new ShieldRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        ShortbowRecipe IJsonReader<ShortbowRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("ShortBow"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new ShortbowRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        ShouldersRecipe IJsonReader<ShouldersRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Shoulders"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new ShouldersRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        SnackRecipe IJsonReader<SnackRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Snack"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new SnackRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        SoupRecipe IJsonReader<SoupRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Soup"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new SoupRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        HarpoonGunRecipe IJsonReader<HarpoonGunRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Speargun"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new HarpoonGunRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        StaffRecipe IJsonReader<StaffRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Staff"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new StaffRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        SwordRecipe IJsonReader<SwordRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Sword"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new SwordRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        TorchRecipe IJsonReader<TorchRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Torch"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new TorchRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        TridentRecipe IJsonReader<TridentRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Trident"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new TridentRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        UpgradeComponentRecipe IJsonReader<UpgradeComponentRecipe>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("UpgradeComponent"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new UpgradeComponentRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        WarhornRecipe IJsonReader<WarhornRecipe>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var outputItemId = new RequiredMember<int>("output_item_id");
            var outputItemCount = new RequiredMember<int>("output_item_count");
            var minRating = new RequiredMember<int>("min_rating");
            var timeToCraft = new RequiredMember<TimeSpan>("time_to_craft_ms");
            var disciplines = new RequiredMember<CraftingDisciplineName[]>("disciplines");
            var flags = new RequiredMember<RecipeFlag[]>("flags");
            var ingredients = new RequiredMember<Ingredient[]>("ingredients");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Warhorn"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(outputItemId.Name))
                {
                    outputItemId = outputItemId.From(member.Value);
                }
                else if (member.NameEquals(outputItemCount.Name))
                {
                    outputItemCount = outputItemCount.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(minRating.Name))
                {
                    minRating = minRating.From(member.Value);
                }
                else if (member.NameEquals(timeToCraft.Name))
                {
                    timeToCraft = timeToCraft.From(member.Value);
                }
                else if (member.NameEquals(disciplines.Name))
                {
                    disciplines = disciplines.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(ingredients.Name))
                {
                    ingredients = ingredients.From(member.Value);
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

            return new WarhornRecipe
            {
                Id = id.GetValue(),
                OutputItemId = outputItemId.GetValue(),
                OutputItemCount = outputItemCount.GetValue(),
                MinRating = minRating.GetValue(),
                TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
                Disciplines = disciplines.GetValue(missingMemberBehavior),
                Flags = flags.GetValue(missingMemberBehavior),
                Ingredients = ingredients.Select(value => value.GetArray(item => ReadIngredient(item, missingMemberBehavior))),
                ChatLink = chatLink.GetValue()
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}
