using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject(RootObject = false)]
    public class TraitFact
    {
        public string? Text { get; set; }

        public string Icon { get; set; } = "";

        /// <summary>The ID of the trait that activates this trait override.</summary>
        public int? RequiresTrait { get; set; }

        /// <summary>The index of the fact that is replaced by this fact when <see cref="RequiresTrait" /> is also active.</summary>
        public int? Overrides { get; set; }
    }
}
