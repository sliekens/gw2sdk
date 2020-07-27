using System.Linq.Expressions;
using System.Reflection;

namespace GW2SDK.Impl.JsonReaders
{
    internal class ReaderInfo
    {
        public delegate BlockExpression Process(ParameterExpression jsonPropertyExpression, LabelTarget continueLabelExpr);

        public MappingSignificance MappingSignificance { get; set; }

        public string PropertyName { get; set; } = default!;

        public ParameterExpression propertySeenExpr { get; set; } = default!;

        public ParameterExpression propertyValueExpr { get; set; } = default!;

        public MemberInfo Destination { get; set; } = default!;

        public Process OnMatch { get; set; } = default!;
    }
}
