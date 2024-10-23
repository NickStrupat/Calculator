grammar Calc;

expr
	: '(' expr ')' # parenthesis
	| expr op expr # binary
	| Number # atom
	;

op
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