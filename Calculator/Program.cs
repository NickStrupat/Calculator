using System;
using System.Numerics;
using Antlr4.Runtime;
using static CalcParser;

for (;;)
{
	var line = Console.ReadLine();
	if (String.IsNullOrEmpty(line))
		break;
	
	var lexer = new CalcLexer(CharStreams.fromString(line));
	var parser = new CalcParser(new CommonTokenStream(lexer));

	var visitor = new ExpressionVisitor<BigRational>();
	var result = visitor.Visit(parser.calculation().expression());
	
	Console.WriteLine(result);
}

sealed class ExpressionVisitor<T> : CalcBaseVisitor<T> where T : INumber<T>, IPowerFunctions<T>
{
	public override T VisitParenthesis(ParenthesisContext context) => Visit(context.expression());
	public override T VisitAtom(AtomContext context) => T.Parse(context.GetText(), provider: null);
	public override T VisitBinary(BinaryContext bc) => Operate(bc.@operator(), Visit(bc.expression(0)), Visit(bc.expression(1)));

	private static T Operate(OperatorContext context, T left, T right) => context switch
	{
		AddContext => left + right,
		SubContext => left - right,
		MulContext => left * right,
		DivContext => left / right,
		ModContext => left % right,
		PowContext => T.Pow(left, right),
		RootContext => T.Pow(left, T.One / right),
		_ => throw new ArgumentOutOfRangeException(nameof(context), "Unknown operator")
	};
}
