grammar Calc;

calculation: expression EOF;

expression
	: expression operator expression # binary
	| Number # atom
	;

operator
	: Plus #addition
	| Minus #subtraction
	| Asterisk #multiplication
	| Slash #division
	| Percent #modulo
	| Asterisk Asterisk #power
	| Slash Slash #root
	;

Number: [0-9]+ ('.'[0-9]+)?;

Plus: '+';
Minus: '-';
Asterisk: '*';
Slash: '/';
Percent: '%';

Ws: [\p{White_Space}] -> skip;