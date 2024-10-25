using System;
using Antlr4.Runtime;
using static CalcParser;
using TNum = System.Double;

while (Console.ReadLine() is { Length: > 0 } line)
{
	var lexer = new CalcLexer(CharStreams.fromString(line));
	var parser = new CalcParser(new CommonTokenStream(lexer));
	var visitor = new ExpressionVisitor();
	var result = visitor.Visit(parser.expr());
	Console.WriteLine(result);
}

class ExpressionVisitor : CalcBaseVisitor<TNum>
{
	public override TNum VisitParenthesis(ParenthesisContext x) => Visit(x.expr());
	public override TNum VisitAtom(AtomContext x) => TNum.Parse(x.GetText(), provider: null!);
	public override TNum VisitBinary(BinaryContext x) => Operate(x.op(), Visit(x.expr(0)), Visit(x.expr(1)));

	private static TNum Operate(OpContext x, TNum left, TNum right) => x switch
	{
		AddContext => left + right,
		SubContext => left - right,
		MulContext => left * right,
		DivContext => left / right,
		ModContext => left % right,
		PowContext => TNum.Pow(left, right),
		RootContext => TNum.RootN(left, TNum.IsInteger(right) ? (Int32)right : throw new($"Root must be an integer: {right}")),
		_ => throw new($"Unknown operator: {x.GetText()}")
	};
}