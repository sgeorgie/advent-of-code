namespace AdventOfCode
{
    public record ValueExpression : ExpressionBase
    {
        public ValueExpression(long value)
        {
            Value = value;
        }

        public long Value { get; }

        public override long Evaluate() => Value;

        public override long EvaluateV2() => Value;
    }
}