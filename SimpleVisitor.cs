using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using MyAntlr.Content;

namespace MyAntlr
{

   
    public class SimpleVisitor : SimpleBaseVisitor<object?>
    {
        private Dictionary<string, object?> Variables { get; } = new();
        public override object? VisitAssignment([NotNull] SimpleParser.AssignmentContext context)
        {
            var varName = context.IDENTIFIER().GetText();
            var value = Visit(context.expression());
            Variables[varName] = value;

            return null;
        }

        public override object? VisitIdentifierExpression([NotNull] SimpleParser.IdentifierExpressionContext context)
        {
            var varName = context.IDENTIFIER().GetText();

            if(!Variables.ContainsKey(varName))
            {
                throw new Exception($"Variable {varName} is not defined");
            }
            return Variables[varName];
        }

        public override object? VisitConstant([NotNull] SimpleParser.ConstantContext context)
        {
            if (context.INTEGER() is { } i)
                return int.Parse(i.GetText());

            if (context.FLOAT() is { } f)
                return float.Parse(f.GetText());

            if (context.INTEGER() is { } s)
                return s.GetText()[1..^1];

            if (context.BOOL() is { } b)
                return b.GetText() == "true";

            if (context.NULL() is { } )
                return null;

            throw new NotImplementedException();
        }

        public override object? VisitAdditiveExpression([NotNull] SimpleParser.AdditiveExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            var op = context.addOP().GetText();

            return op switch
            {
                "+" => Add(left, right),
                //"-" => Substract(left, right),
                _ => throw new NotImplementedException(),
            };
        }

        private object? Add(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l + r;

            if (left is float lf && right is float rf)
                return lf + rf;

            if (left is int lInt && right is float rFloat)
                return lInt + rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat + rInt;

            if (left is string || right is string)
                return $"{left}{right}";


            throw new Exception($"Cannot add values of types{left?.GetType()} and {right?.GetType()}.");


        }
    }
}
