using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK.Mumble
{
    [PublicAPI]
    [Flags]
    [DefaultValue(None)]
    public enum UiState : uint
    {
        None,

        IsMapOpen = 0b_1,

        IsCompassTopRight = 0b_10,

        DoesCompassHaveRotationEnabled = 0b_100,

        GameHasFocus = 0b_1000,

        IsInCompetitiveGameMode = 0b_1_0000,

        TextboxHasFocus = 0b_10_0000,

        IsInCombat = 0b_100_0000
    }
}