grammar Calc;

calculation: expr EOF;

expr
	: '(' expr ')' # parenthesis
	| expr operator expr # binary
	| Number # atom
	;

operator
	: Plus #add
	| Minus #sub
	| Asterisk #mul
	| Slash #div
	| Percent #mod
	| Asterisk Asterisk #pow
	| Slash Slash #root
	;

Number: [0-9]+ ('.'[0-9]+)?;

Plus: '+';
Minus: '-';
Asterisk: '*';
Slash: '/';
Percent: '%';

Ws: [\p{White_Space}] -> skip;