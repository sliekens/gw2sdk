using GuildWars2.Hero;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Builds.Skills;
using GuildWars2.Tests.TestInfrastructure;

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

                var chatLink = skill.GetChatLink();
                Assert.Equal(skill.Id, chatLink.SkillId);

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
                        {
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
                        }
                        case ProfessionSkill professionSkill:
                        {
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
                }
                else
                {
                    Assert.NotNull(skill.Name);
                    Assert.NotNull(skill.Description);
                    Assert.NotNull(skill.IconHref);
                }
            }
        );
    }
}
