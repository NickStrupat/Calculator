using System.Numerics;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using static CalcParser;
using OperandType = System.Numerics.BigRational;

for (;;)
{
	var line = Console.ReadLine() ?? String.Empty;
	var lexer = new CalcLexer(CharStreams.fromString(line));
	var parser = new CalcParser(new CommonTokenStream(lexer));
	var result = CalcListener<OperandType>.GetResult(parser.calculation().expression());
	Console.WriteLine(result.ToString());
}

class CalcListener<TOperand> : CalcBaseListener
	where TOperand : INumber<TOperand>, IPowerFunctions<TOperand>
{
	private CalcListener() {}
	private Func<TOperand, TOperand, TOperand> operation = (_, y) => y;
	private TOperand result = TOperand.Zero;
	
	public override void EnterAtom(AtomContext context) => result = operation(result, TOperand.Parse(context.GetText(), provider:null));
	public override void EnterAddition(AdditionContext context) => operation = (x, y) => x + y;
	public override void EnterSubtraction(SubtractionContext context) => operation = (x, y) => x - y;
	public override void EnterMultiplication(MultiplicationContext context) => operation = (x, y) => x * y;
	public override void EnterDivision(DivisionContext context) => operation = (x, y) => x / y;
	public override void EnterModulo(ModuloContext context) => operation = (x, y) => x % y;
	public override void EnterPower(PowerContext context) => operation = TOperand.Pow;
	public override void EnterRoot(RootContext context) => operation = (x, y) => TOperand.Pow(x, TOperand.One / y);

	public static TOperand GetResult(ExpressionContext expression)
	{
		var listener = new CalcListener<TOperand>();
		ParseTreeWalker.Default.Walk(listener, expression);
		return listener.result;
	}
}
