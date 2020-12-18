namespace AdventOfCode
{
    public abstract record ExpressionBase : Node
    {
        public abstract long Evaluate();
        public abstract long EvaluateV2();
    }
}