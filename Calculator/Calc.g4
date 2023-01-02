grammar Calc;

calculation: expression EOF;

expression
	: '(' expression ')' # parenthesis
	| expression operator expression # binary
	| Number # atom
	;

operator
	: '+' #add
	| '-' #sub
	| '*' #mul
	| '/' #div
	| '%' #mod
	| '**' #pow
	| '//' #root
	;

Number: [0-9]+ ('.'[0-9]+)?;

Ws: [\p{White_Space}] -> skip;