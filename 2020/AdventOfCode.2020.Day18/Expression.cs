using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public record Expression : ExpressionBase
    {
        public Expression(List<Node> nodes)
        {
            Nodes = nodes;
        }

        public List<Node> Nodes { get; }

        public static Expression Parse(string str)
        {
            var operators = new[] {'+', '*'};
            var numbers = new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

            var nodes = new List<Node>();

            for (int i = 0; i < str.Length; i++)
            {
                var next = str[i];

                if (next == ' ')
                    continue;

                if (operators.Contains(next))
                {
                    nodes.Add(Operator.Parse(next));
                    continue;
                }

                if (numbers.Contains(next))
                {
                    string num = next.ToString();

                    // Parse the whole number
                    while ((i + 1) < str.Length && numbers.Contains(str[i+1]))
                    {
                        i++;
                        num += str[i];
                    }

                    nodes.Add(new ValueExpression(long.Parse(num)));
                    continue;
                }

                if (next == '(')
                {
                    // Find the closing parentheses
                    var openParentheses = 1;
                    var startingIndex = i;

                    while (openParentheses != 0)
                    {
                        i++;
                        if (str[i] == '(')
                            openParentheses++;
                        else if (str[i] == ')')
                            openParentheses--;
                    }

                    nodes.Add(Parse(str[(startingIndex + 1)..(i)]));
                    continue;
                }
            }

            return new Expression(nodes);
        }

        public override long Evaluate()
        {
            var result = ((ExpressionBase) Nodes[0]).Evaluate();

            for (int i = 1; i < Nodes.Count; i += 2)
            {
                var op = (Operator) Nodes[i];
                var expr = (ExpressionBase) Nodes[i + 1];

                if (op is Addition)
                    result += expr.Evaluate();
                else if (op is Multiplication)
                    result *= expr.Evaluate();
            }

            return result;
        }

        public override long EvaluateV2()
        {
            var newNodes = new List<long>();

            var first = (ExpressionBase)Nodes[0];
            newNodes.Add(first.EvaluateV2());

            for (int i = 1; i < Nodes.Count; i += 2)
            {
                var op = (Operator) Nodes[i];
                var expr = (ExpressionBase) Nodes[i + 1];

                if (op is Addition)
                {
                    var result = newNodes[^1] + expr.EvaluateV2();
                    newNodes[^1] = result;
                    continue;
                }

                newNodes.Add(expr.EvaluateV2());
            }

            return newNodes.Aggregate(1L, (agg, value) => agg * value);
        }
    }
}