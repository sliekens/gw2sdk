using System.Linq.Expressions;
using System.Reflection;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders
{
    internal class ReaderInfo
    {
        public delegate BlockExpression Process(ParameterExpression jsonPropertyExpression, LabelTarget continueLabelExpr);

        public MappingSignificance MappingSignificance { get; set; }

        public string PropertyName { get; set; } = default!;

        public ParameterExpression PropertySeenExpr { get; set; } = default!;

        public ParameterExpression PropertyValueExpr { get; set; } = default!;

        public MemberInfo Destination { get; set; } = default!;

        public Process OnMatch { get; set; } = default!;
    }
}
