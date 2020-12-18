using System;

namespace AdventOfCode
{
    public abstract record Operator : Node
    {
        public static Operator Parse(char op)
        {
            if (op == '+') return new Addition();
            if (op == '*') return new Multiplication();

            throw new Exception();
        }
    }
}