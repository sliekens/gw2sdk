using GuildWars2.Chat;
using GuildWars2.Hero;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Builds.Facts;
using GuildWars2.Hero.Builds.Skills;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure.Composition;

using Range = GuildWars2.Hero.Builds.Facts.Range;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class Skills(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Skill> actual, MessageContext context) = await sut.Hero.Builds.GetSkills(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();
        foreach (Skill skill in actual)
        {
            await Assert.That(skill.Id).IsGreaterThan(0);
            await Assert.That(skill.SkillFlags.Other).IsEmpty();
            if (skill is ActionSkill action)
            {
                await Assert.That(action)
                    .Member(a => a.Name, m => m.IsNotNull())
                    .And.Member(a => a.Description, m => m.IsNotNull());
                MarkupSyntaxValidator.Validate(action.Description);
                await Assert.That(action.IconUrl is null or { IsAbsoluteUri: true }).IsTrue();
                await Assert.That(action.Professions).IsNotEmpty();
                foreach (Extensible<ProfessionName> profession in action.Professions)
                {
                    await Assert.That(profession.IsDefined()).IsTrue();
                }

                await Assert.That(action)
                    .Member(a => a.WeaponType.IsDefined(), m => m.IsTrue())
                    .And.Member(a => a.Slot.IsDefined(), m => m.IsTrue());
                if (action.FlipSkillId.HasValue)
                {
                    await Assert.That(action.FlipSkillId.Value).IsGreaterThan(0);
                }

                if (action.NextSkillId.HasValue)
                {
                    await Assert.That(action.NextSkillId.Value).IsGreaterThan(0);
                }

                if (action.PreviousSkillId.HasValue)
                {
                    await Assert.That(action.PreviousSkillId.Value).IsGreaterThan(0);
                }

                if (action.SpecializationId.HasValue)
                {
                    await Assert.That(action.SpecializationId.Value).IsGreaterThan(0);
                }

                switch (action)
                {
                    case WeaponSkill weaponSkill:
                        if (weaponSkill.Attunement.HasValue)
                        {
                            await Assert.That(weaponSkill)
                                .Member(w => w.Professions.Count(profession => profession == ProfessionName.Elementalist), m => m.IsEqualTo(1))
                                .And.Member(w => w.Attunement!.Value.IsDefined(), m => m.IsTrue());
                        }

                        if (weaponSkill.DualAttunement.HasValue)
                        {
                            await Assert.That(weaponSkill)
                                .Member(w => w.Professions.Count(profession => profession == ProfessionName.Elementalist), m => m.IsEqualTo(1))
                                .And.Member(w => w.DualAttunement!.Value.IsDefined(), m => m.IsTrue())
                                .And.Member(w => w.Attunement.HasValue, m => m.IsTrue());
                        }

                        if (weaponSkill.Cost.HasValue)
                        {
                            await Assert.That(weaponSkill.Cost.Value).IsGreaterThan(0);
                        }

                        if (weaponSkill.Offhand.HasValue)
                        {
                            await Assert.That(weaponSkill.Offhand.Value.IsDefined()).IsTrue();
                        }

                        if (weaponSkill.Initiative.HasValue)
                        {
                            await Assert.That(weaponSkill)
                                .Member(w => w.Professions.Count(profession => profession == ProfessionName.Thief), m => m.IsEqualTo(1))
                                .And.Member(w => w.Initiative!.Value, m => m.IsGreaterThan(0));
                        }

                        break;
                    case SlotSkill slotSkill:
                        if (slotSkill.ToolbeltSkillId.HasValue)
                        {
                            await Assert.That(slotSkill)
                                .Member(s => s.Professions.Count(profession => profession == ProfessionName.Engineer), m => m.IsEqualTo(1))
                                .And.Member(s => s.ToolbeltSkillId!.Value, m => m.IsGreaterThan(0));
                        }

                        if (slotSkill.Attunement.HasValue)
                        {
                            await Assert.That(slotSkill)
                                .Member(s => s.Professions.Count(profession => profession == ProfessionName.Elementalist), m => m.IsEqualTo(1))
                                .And.Member(s => s.Attunement!.Value.IsDefined(), m => m.IsTrue());
                        }

                        if (slotSkill.Cost.HasValue)
                        {
                            await Assert.That(slotSkill.Cost.Value).IsGreaterThan(0);
                        }

                        if (slotSkill.BundleSkillIds is not null)
                        {
                            foreach (int id in slotSkill.BundleSkillIds)
                            {
                                await Assert.That(id).IsGreaterThan(0);
                            }
                        }

                        if (slotSkill.SubskillIds is not null)
                        {
                            await Assert.That(slotSkill.SubskillIds).IsNotEmpty();
                            foreach (Subskill subskill in slotSkill.SubskillIds)
                            {
                                await Assert.That(subskill.Id).IsGreaterThan(0);
                                if (subskill.Attunement.HasValue)
                                {
                                    await Assert.That(slotSkill)
                                        .Member(s => s.Professions.Count(profession => profession == ProfessionName.Elementalist), m => m.IsEqualTo(1))
                                        .And.Member(s => subskill.Attunement!.Value.IsDefined(), m => m.IsTrue());
                                }

                                if (subskill.Form.HasValue)
                                {
                                    await Assert.That(slotSkill)
                                        .Member(s => s.Professions.Count(profession => profession == ProfessionName.Ranger), m => m.IsEqualTo(1))
                                        .And.Member(s => subskill.Form!.Value.IsDefined(), m => m.IsTrue());
                                }
                            }
                        }

                        if (slotSkill is EliteSkill { TransformSkillIds: not null } elite)
                        {
                            await Assert.That(elite.TransformSkillIds).IsNotEmpty();
                            foreach (int id in elite.TransformSkillIds)
                            {
                                await Assert.That(id).IsGreaterThan(0);
                            }
                        }

                        break;
                    case ProfessionSkill professionSkill:
                        if (professionSkill.Attunement.HasValue)
                        {
                            await Assert.That(professionSkill)
                                .Member(p => p.Professions.Count(profession => profession == ProfessionName.Elementalist), m => m.IsEqualTo(1))
                                .And.Member(p => p.Attunement!.Value.IsDefined(), m => m.IsTrue());
                        }

                        if (professionSkill.Cost.HasValue)
                        {
                            await Assert.That(professionSkill.Cost.Value).IsGreaterThan(0);
                        }

                        if (professionSkill.TransformSkills is not null)
                        {
                            await Assert.That(professionSkill.TransformSkills).IsNotEmpty();
                            foreach (int id in professionSkill.TransformSkills)
                            {
                                await Assert.That(id).IsGreaterThan(0);
                            }
                        }

                        break;
                    case BundleSkill bundleSkill:
                        // Nothing to verify
                        break;
                    case ToolbeltSkill toolbeltSkill:
                        // Nothing to verify
                        break;
                    case LockedSkill lockedSkill:
                        // Nothing to verify
                        break;
                    case MonsterSkill monsterSkill:
                        // Nothing to verify
                        break;
                    case PetSkill petSkill:
                        // Nothing to verify
                        break;
                    default:
                        throw new TUnit.Assertions.Exceptions.AssertionException($"Unexpected action skill type: {action.GetType().Name}");
                }
            }
            else
            {
                await Assert.That(skill)
                    .Member(s => s.Name, m => m.IsNotNull())
                    .And.Member(s => s.Description, m => m.IsNotNull());
                MarkupSyntaxValidator.Validate(skill.Description);
                await Assert.That(skill.IconUrl is null or { IsAbsoluteUri: true }).IsTrue();
            }

            if (skill.Facts is not null)
            {
                foreach (Fact fact in skill.Facts)
                {
                    await Assert.That(fact)
                        .Member(f => f.Text, m => m.IsNotNull());
                    MarkupSyntaxValidator.Validate(fact.Text);
                    await Assert.That(fact.IconUrl is null or { IsAbsoluteUri: true }).IsTrue();
                    switch (fact)
                    {
                        case AttributeAdjustment attributeAdjustment:
                            if (attributeAdjustment.Target.HasValue)
                            {
                                await Assert.That(attributeAdjustment.Target.Value.IsDefined()).IsTrue();
                            }

                            if (attributeAdjustment.Value.HasValue)
                            {
                                await Assert.That(attributeAdjustment.Value.Value).IsGreaterThan(0);
                            }

                            if (attributeAdjustment.HitCount.HasValue)
                            {
                                await Assert.That(attributeAdjustment.HitCount.Value).IsGreaterThan(0);
                            }

                            break;
                        case AttributeConversion attributeConversion:
                            await Assert.That(attributeConversion)
                                .Member(a => a.Percent, m => m.IsGreaterThan(0))
                                .And.Member(a => a.Source.IsDefined(), m => m.IsTrue())
                                .And.Member(a => a.Target.IsDefined(), m => m.IsTrue())
                                .And.Member(a => a.Source, m => m.IsNotEqualTo(attributeConversion.Target));
                            break;
                        case Buff buff:
                            await Assert.That(buff)
                                .Member(b => b.Status, m => m.IsNotEmpty())
                                .And.Member(b => b.Description, m => m.IsNotNull());
                            MarkupSyntaxValidator.Validate(buff.Description);
                            if (buff.ApplyCount.HasValue)
                            {
                                await Assert.That(buff.ApplyCount.Value).IsGreaterThanOrEqualTo(0);
                            }

                            if (buff.Duration.HasValue)
                            {
                                await Assert.That(buff.Duration.Value).IsGreaterThanOrEqualTo(TimeSpan.Zero);
                            }

                            break;
                        case ComboField comboField:
                            await Assert.That(comboField.Field.IsDefined()).IsTrue();
                            break;
                        case ComboFinisher comboFinisher:
                            await Assert.That(comboFinisher.Percent).IsGreaterThan(0);
                            await Assert.That(comboFinisher.FinisherName.IsDefined()).IsTrue();
                            break;
                        case Damage damage:
                            await Assert.That(damage.HitCount).IsGreaterThan(0);
                            await Assert.That(damage.DamageMultiplier).IsGreaterThan(0);
                            break;
                        case Distance distance:
                            await Assert.That(distance.Length).IsGreaterThanOrEqualTo(0);
                            break;
                        case Duration duration:
                            await Assert.That(duration.Length).IsGreaterThan(TimeSpan.Zero);
                            break;
                        case HealingAdjust adjustment:
                            await Assert.That(adjustment.HitCount).IsGreaterThan(0);
                            break;
                        case Number number:
                            await Assert.That(number.Value).IsGreaterThanOrEqualTo(0);
                            break;
                        case Radius radius:
                            await Assert.That(radius.Distance).IsGreaterThan(0);
                            break;
                        case Range range:
                            await Assert.That(range.Distance).IsGreaterThan(0);
                            break;
                        case Recharge recharge:
                            await Assert.That(recharge.Duration).IsGreaterThan(TimeSpan.Zero);
                            break;
                        case Time time:
                            await Assert.That(time.Duration).IsGreaterThanOrEqualTo(TimeSpan.Zero);
                            break;
                        case Unblockable unblockable:
                            // Nothing to verify
                            break;
                        case NoData noData:
                            // Nothing to verify
                            break;
                        case StunBreak stunBreak:
                            // Nothing to verify
                            break;
                        case Percentage percentage:
                            await Assert.That(percentage.Percent).IsGreaterThanOrEqualTo(-100).And.IsLessThanOrEqualTo(500);
                            break;
                        case Fact baseFact:
                            // This handles the base Fact type - nothing specific to verify
                            break;
                        default:
                            throw new TUnit.Assertions.Exceptions.AssertionException($"Unexpected fact type: {fact.GetType().Name}");
                    }
                }
            }

            if (skill.TraitedFacts is not null)
            {
                foreach (TraitedFact fact in skill.TraitedFacts)
                {
                    await Assert.That(fact.RequiresTrait).IsGreaterThan(0);
                    await Assert.That(fact.Fact.Text).IsNotNull();
                    MarkupSyntaxValidator.Validate(fact.Fact.Text);
                    await Assert.That(fact.Fact.IconUrl is null or { IsAbsoluteUri: true }).IsTrue();
                    switch (fact.Fact)
                    {
                        case AttributeAdjustment attributeAdjustment:
                            if (attributeAdjustment.Target.HasValue)
                            {
                                await Assert.That(attributeAdjustment.Target.Value.IsDefined()).IsTrue();
                            }

                            if (attributeAdjustment.Value.HasValue)
                            {
                                await Assert.That(attributeAdjustment.Value.Value).IsGreaterThan(0);
                            }

                            if (attributeAdjustment.HitCount.HasValue)
                            {
                                await Assert.That(attributeAdjustment.HitCount.Value).IsGreaterThan(0);
                            }

                            break;
                        case AttributeConversion attributeConversion:
                            await Assert.That(attributeConversion.Percent).IsGreaterThan(0);
                            await Assert.That(attributeConversion.Source.IsDefined()).IsTrue();
                            await Assert.That(attributeConversion.Target.IsDefined()).IsTrue();
                            await Assert.That(attributeConversion.Source).IsNotEqualTo(attributeConversion.Target);
                            break;
                        case Buff buff:
                            await Assert.That(buff.Status).IsNotNull();
                            await Assert.That(buff.Description).IsNotNull();
                            MarkupSyntaxValidator.Validate(buff.Description);
                            if (buff.ApplyCount.HasValue)
                            {
                                await Assert.That(buff.ApplyCount.Value).IsGreaterThanOrEqualTo(0);
                            }

                            if (buff.Duration.HasValue)
                            {
                                await Assert.That(buff.Duration.Value).IsGreaterThanOrEqualTo(TimeSpan.Zero);
                            }

                            break;
                        case ComboField comboField:
                            await Assert.That(comboField.Field.IsDefined()).IsTrue();
                            break;
                        case ComboFinisher comboFinisher:
                            await Assert.That(comboFinisher.Percent).IsGreaterThan(0);
                            await Assert.That(comboFinisher.FinisherName.IsDefined()).IsTrue();
                            break;
                        case Damage damage:
                            await Assert.That(damage.HitCount).IsGreaterThan(0);
                            await Assert.That(damage.DamageMultiplier).IsGreaterThan(0);
                            break;
                        case Distance distance:
                            await Assert.That(distance.Length).IsGreaterThanOrEqualTo(0);
                            break;
                        case Duration duration:
                            await Assert.That(duration.Length).IsGreaterThan(TimeSpan.Zero);
                            break;
                        case HealingAdjust adjustment:
                            await Assert.That(adjustment.HitCount).IsGreaterThan(0);
                            break;
                        case Number number:
                            await Assert.That(number.Value).IsGreaterThanOrEqualTo(0);
                            break;
                        case Radius radius:
                            await Assert.That(radius.Distance).IsGreaterThan(0);
                            break;
                        case Range range:
                            await Assert.That(range.Distance).IsGreaterThan(0);
                            break;
                        case Recharge recharge:
                            await Assert.That(recharge.Duration).IsGreaterThan(TimeSpan.Zero);
                            break;
                        case Time time:
                            await Assert.That(time.Duration).IsGreaterThanOrEqualTo(TimeSpan.Zero);
                            break;
                        case NoData noData:
                            // Nothing to verify
                            break;
                        case Percentage percentage:
                            await Assert.That(percentage.Percent).IsGreaterThanOrEqualTo(-75).And.IsLessThanOrEqualTo(500);
                            break;
                        case Fact baseFact:
                            // This handles the base Fact type - nothing specific to verify
                            break;
                        default:
                            throw new TUnit.Assertions.Exceptions.AssertionException($"Unexpected fact type: {fact.Fact.GetType().Name}");
                    }
                }
            }

            SkillLink chatLink = skill.GetChatLink();
            await Assert.That(chatLink.SkillId).IsEqualTo(skill.Id);
            await Assert.That(skill.ChatLink).IsEqualTo(chatLink.ToString());
            SkillLink chatLinkRoundtrip = SkillLink.Parse(chatLink.ToString());
            await Assert.That(chatLinkRoundtrip.ToString()).IsEqualTo(chatLink.ToString());
        }
    }
}
