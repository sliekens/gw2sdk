﻿using System.Reflection;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.PatternsAndPractices;

public class DesignedForClientCodeTest(AssemblyFixture fixture) : IClassFixture<AssemblyFixture>
{
    [Fact]
    public void Every_visible_type_is_designed_for_client_code()
    {
        /*
         * The goal of this test is to ensure that all public types are intended to be used in client code.
         * This avoids leaking private API (e.g. helper functions) into consumer code.
         *
         * Note that the goal is not to make every helper object internal, but to think carefully about their class design.
         * Especially regarding how easily they can be replaced or evolved between versions.
         */
        var visible = fixture.Assembly.ExportedTypes
            .Where(type => type.Namespace!.StartsWith("GuildWars2"))
            .ToList();
        Assert.All(
            visible,
            type =>
            {
                if (Enumerable.Any(
                        type.GetCustomAttributes(),
                        att => att.GetType().Name == "PublicAPIAttribute"
                    ))
                {
                    return;
                }

                throw new ApplicationException(
                    $"Type '{type}' is visible to client code, make it internal or mark it as [PublicAPI]."
                );
            }
        );
    }

    [Fact]
    public void Every_type_designed_for_client_code_is_visible()
    {
        var invisible = fixture.Assembly.DefinedTypes.Where(type => type.IsNotPublic).ToList();
        Assert.All(
            invisible,
            type =>
            {
                if (Enumerable.Any(
                        type.GetCustomAttributes(),
                        att => att.GetType().Name == "PublicAPIAttribute"
                    ))
                {
                    throw new ApplicationException(
                        $"Type '{type}' is invisible to client code, make it public or remove [PublicAPI]."
                    );
                }
            }
        );
    }
}
