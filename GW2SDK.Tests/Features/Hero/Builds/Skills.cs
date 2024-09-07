using GuildWars2.Chat;
using GuildWars2.Hero;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Builds.Facts;
using GuildWars2.Hero.Builds.Skills;
using GuildWars2.Tests.TestInfrastructure;
using Range = GuildWars2.Hero.Builds.Facts.Range;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class Skills
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Builds.GetSkills();

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            skill =>
            {
                Assert.True(skill.Id > 0);
                Assert.Empty(skill.SkillFlags.Other);

                if (skill is ActionSkill action)
                {
                    Assert.NotEmpty(action.Name);
                    Assert.NotEmpty(action.Description);
                    Assert.NotEmpty(action.IconHref);
                    Assert.NotEmpty(action.Professions);
                    Assert.All(
                        action.Professions,
                        profession => Assert.True(profession.IsDefined())
                    );
                    Assert.True(action.WeaponType.IsDefined());
                    Assert.True(action.Slot.IsDefined());

                    if (action.FlipSkillId.HasValue)
                    {
                        Assert.True(action.FlipSkillId.Value > 0);
                    }

                    if (action.NextSkillId.HasValue)
                    {
                        Assert.True(action.NextSkillId.Value > 0);
                    }

                    if (action.PreviousSkillId.HasValue)
                    {
                        Assert.True(action.PreviousSkillId.Value > 0);
                    }

                    if (action.SpecializationId.HasValue)
                    {
                        Assert.True(action.SpecializationId.Value > 0);
                    }

                    switch (action)
                    {
                        case WeaponSkill weaponSkill:
                            if (weaponSkill.Attunement.HasValue)
                            {
                                Assert.Single(
                                    weaponSkill.Professions,
                                    profession => profession == ProfessionName.Elementalist
                                );
                                Assert.True(weaponSkill.Attunement.Value.IsDefined());
                            }

                            if (weaponSkill.DualAttunement.HasValue)
                            {
                                Assert.Single(
                                    weaponSkill.Professions,
                                    profession => profession == ProfessionName.Elementalist
                                );
                                Assert.True(weaponSkill.DualAttunement.Value.IsDefined());
                                Assert.True(weaponSkill.Attunement.HasValue);
                            }

                            if (weaponSkill.Cost.HasValue)
                            {
                                Assert.True(weaponSkill.Cost.Value > 0);
                            }

                            if (weaponSkill.Offhand.HasValue)
                            {
                                Assert.True(weaponSkill.Offhand.Value.IsDefined());
                            }

                            if (weaponSkill.Initiative.HasValue)
                            {
                                Assert.Single(
                                    weaponSkill.Professions,
                                    profession => profession == ProfessionName.Thief
                                );
                                Assert.True(weaponSkill.Initiative.Value > 0);
                            }

                            break;
                        case SlotSkill slotSkill:
                            if (slotSkill.ToolbeltSkillId.HasValue)
                            {
                                Assert.Single(
                                    slotSkill.Professions,
                                    profession => profession == ProfessionName.Engineer
                                );
                                Assert.True(slotSkill.ToolbeltSkillId.Value > 0);
                            }

                            if (slotSkill.Attunement.HasValue)
                            {
                                Assert.Single(
                                    slotSkill.Professions,
                                    profession => profession == ProfessionName.Elementalist
                                );
                                Assert.True(slotSkill.Attunement.Value.IsDefined());
                            }

                            if (slotSkill.Cost.HasValue)
                            {
                                Assert.True(slotSkill.Cost.Value > 0);
                            }

                            if (slotSkill.BundleSkillIds is not null)
                            {
                                Assert.All(slotSkill.BundleSkillIds, id => Assert.True(id > 0));
                            }

                            if (slotSkill.SubskillIds is not null)
                            {
                                Assert.NotEmpty(slotSkill.SubskillIds);
                                Assert.All(
                                    slotSkill.SubskillIds,
                                    subskill =>
                                    {
                                        Assert.True(subskill.Id > 0);

                                        if (subskill.Attunement.HasValue)
                                        {
                                            Assert.Single(
                                                slotSkill.Professions,
                                                profession =>
                                                    profession == ProfessionName.Elementalist
                                            );
                                            Assert.True(subskill.Attunement.Value.IsDefined());
                                        }

                                        if (subskill.Form.HasValue)
                                        {
                                            Assert.Single(
                                                slotSkill.Professions,
                                                profession => profession == ProfessionName.Ranger
                                            );
                                            Assert.True(subskill.Form.Value.IsDefined());
                                        }
                                    }
                                );
                            }

                            if (slotSkill is EliteSkill { TransformSkillIds: not null } elite)
                            {
                                Assert.NotEmpty(elite.TransformSkillIds);
                                Assert.All(elite.TransformSkillIds, id => Assert.True(id > 0));
                            }

                            break;
                        case ProfessionSkill professionSkill:
                            if (professionSkill.Attunement.HasValue)
                            {
                                Assert.Single(
                                    professionSkill.Professions,
                                    profession => profession == ProfessionName.Elementalist
                                );
                                Assert.True(professionSkill.Attunement.Value.IsDefined());
                            }

                            if (professionSkill.Cost.HasValue)
                            {
                                Assert.True(professionSkill.Cost.Value > 0);
                            }

                            if (professionSkill.TransformSkills is not null)
                            {
                                Assert.NotEmpty(professionSkill.TransformSkills);
                                Assert.All(
                                    professionSkill.TransformSkills,
                                    id => Assert.True(id > 0)
                                );
                            }

                            break;
                    }
                }
                else
                {
                    Assert.NotNull(skill.Name);
                    Assert.NotNull(skill.Description);
                    Assert.NotNull(skill.IconHref);
                }

                if (skill.Facts is not null)
                {
                    Assert.All(
                        skill.Facts,
                        fact =>
                        {
                            Assert.NotNull(fact.Text);
                            Assert.NotEmpty(fact.IconHref);

                            switch (fact)
                            {
                                case AttributeAdjustment attributeAdjustment:
                                    if (attributeAdjustment.Target.HasValue)
                                    {
                                        Assert.True(attributeAdjustment.Target.Value.IsDefined());
                                    }

                                    if (attributeAdjustment.Value.HasValue)
                                    {
                                        Assert.True(attributeAdjustment.Value.Value > 0);
                                    }

                                    if (attributeAdjustment.HitCount.HasValue)
                                    {
                                        Assert.True(attributeAdjustment.HitCount.Value > 0);
                                    }

                                    break;
                                case AttributeConversion attributeConversion:
                                    Assert.True(attributeConversion.Percent > 0);
                                    Assert.True(attributeConversion.Source.IsDefined());
                                    Assert.True(attributeConversion.Target.IsDefined());
                                    Assert.NotEqual(
                                        attributeConversion.Source,
                                        attributeConversion.Target
                                    );
                                    break;
                                case Buff buff:
                                    Assert.NotEmpty(buff.Status);
                                    Assert.NotNull(buff.Description);

                                    if (buff.ApplyCount.HasValue)
                                    {
                                        Assert.True(buff.ApplyCount.Value >= 0);
                                    }

                                    if (buff.Duration.HasValue)
                                    {
                                        Assert.True(buff.Duration.Value >= TimeSpan.Zero);
                                    }

                                    break;
                                case ComboField comboField:
                                    Assert.True(comboField.Field.IsDefined());
                                    break;
                                case ComboFinisher comboFinisher:
                                    Assert.True(comboFinisher.Percent > 0);
                                    Assert.True(comboFinisher.FinisherName.IsDefined());
                                    break;
                                case Damage damage:
                                    Assert.True(damage.HitCount > 0);
                                    Assert.True(damage.DamageMultiplier > 0);
                                    break;
                                case Distance distance:
                                    Assert.True(distance.Length >= 0);
                                    break;
                                case Duration duration:
                                    Assert.True(duration.Length > TimeSpan.Zero);
                                    break;
                                case HealingAdjust adjustment:
                                    Assert.True(adjustment.HitCount > 0);
                                    break;
                                case Number number:
                                    Assert.True(number.Value >= 0);
                                    break;
                                case Radius radius:
                                    Assert.True(radius.Distance > 0);
                                    break;
                                case Range range:
                                    Assert.True(range.Distance > 0);
                                    break;
                                case Recharge recharge:
                                    Assert.True(recharge.Duration > TimeSpan.Zero);
                                    break;
                                case Time time:
                                    Assert.True(time.Duration >= TimeSpan.Zero);
                                    break;
                            }
                        }
                    );
                }

                if (skill.TraitedFacts is not null)
                {
                    Assert.All(
                        skill.TraitedFacts,
                        fact =>
                        {
                            Assert.True(fact.RequiresTrait > 0);
                            Assert.NotNull(fact.Fact.Text);
                            Assert.NotEmpty(fact.Fact.IconHref);
                            switch (fact.Fact)
                            {
                                case AttributeAdjustment attributeAdjustment:
                                    if (attributeAdjustment.Target.HasValue)
                                    {
                                        Assert.True(attributeAdjustment.Target.Value.IsDefined());
                                    }

                                    if (attributeAdjustment.Value.HasValue)
                                    {
                                        Assert.True(attributeAdjustment.Value.Value > 0);
                                    }

                                    if (attributeAdjustment.HitCount.HasValue)
                                    {
                                        Assert.True(attributeAdjustment.HitCount.Value > 0);
                                    }

                                    break;
                                case AttributeConversion attributeConversion:
                                    Assert.True(attributeConversion.Percent > 0);
                                    Assert.True(attributeConversion.Source.IsDefined());
                                    Assert.True(attributeConversion.Target.IsDefined());
                                    Assert.NotEqual(
                                        attributeConversion.Source,
                                        attributeConversion.Target
                                    );
                                    break;
                                case Buff buff:
                                    Assert.NotNull(buff.Status);
                                    Assert.NotNull(buff.Description);

                                    if (buff.ApplyCount.HasValue)
                                    {
                                        Assert.True(buff.ApplyCount.Value >= 0);
                                    }

                                    if (buff.Duration.HasValue)
                                    {
                                        Assert.True(buff.Duration.Value >= TimeSpan.Zero);
                                    }

                                    break;
                                case ComboField comboField:
                                    Assert.True(comboField.Field.IsDefined());
                                    break;
                                case ComboFinisher comboFinisher:
                                    Assert.True(comboFinisher.Percent > 0);
                                    Assert.True(comboFinisher.FinisherName.IsDefined());
                                    break;
                                case Damage damage:
                                    Assert.True(damage.HitCount > 0);
                                    Assert.True(damage.DamageMultiplier > 0);
                                    break;
                                case Distance distance:
                                    Assert.True(distance.Length >= 0);
                                    break;
                                case Duration duration:
                                    Assert.True(duration.Length > TimeSpan.Zero);
                                    break;
                                case HealingAdjust adjustment:
                                    Assert.True(adjustment.HitCount > 0);
                                    break;
                                case Number number:
                                    Assert.True(number.Value >= 0);
                                    break;
                                case Radius radius:
                                    Assert.True(radius.Distance > 0);
                                    break;
                                case Range range:
                                    Assert.True(range.Distance > 0);
                                    break;
                                case Recharge recharge:
                                    Assert.True(recharge.Duration > TimeSpan.Zero);
                                    break;
                                case Time time:
                                    Assert.True(time.Duration >= TimeSpan.Zero);
                                    break;
                            }
                        }
                    );
                }

                var chatLink = skill.GetChatLink();
                Assert.Equal(skill.Id, chatLink.SkillId);
                Assert.Equal(skill.ChatLink, chatLink.ToString());

                var chatLinkRoundtrip = SkillLink.Parse(chatLink.ToString());
                Assert.Equal(chatLink.ToString(), chatLinkRoundtrip.ToString());
            }
        );
    }
}
