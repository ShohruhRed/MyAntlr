grammar Simple;

program: line* EOF;

line: stetement| ifBlock | whileBlock;

stetement: (assignment|functionalCall) ';';

ifBlock: 'if' expression block ('else' elseIfBlock);

elseIfBlock: block | ifBlock;

whileBlock: WHILE expression block ('else' elseIfBlock);

WHILE: 'while' | 'until';

assignment: IDENTIFIER '=' expression;

functionalCall: IDENTIFIER '('(expression(',' expression)*)?')';

expression
	: constant								#constantExpression
	| IDENTIFIER							#identifierExpression
	| functionalCall						#functionalExpression
	| '(' expression ')'					#parenthesizedExpression
	| '!' expression						#notExpression
	| expression multOP expression			#multiplicativeExpression
	| expression addOP expression			#additiveExpression
	| expression compareOP expression		#comparisonExpression
	| expression boolOP expression			#booleanExpression
	;

multOP: '*' |'/'| '%';
addOP: '+'|'-';
compareOP: '=='| '!=' | '<=' | '>=' |;
boolOP: BOOL_OPERATOR;

BOOL_OPERATOR: 'and' | 'or' | 'xor';

constant: INTEGER | FLOAT | STRING | BOOL | NULL;

INTEGER: [0-9]+;
FLOAT:	[0-9]+ '.' [0-9]+;
STRING:	('"' ~'"'* '"')|('\'' ~'\''* '\'' );
BOOL: 'true' | 'false';
NULL:	'null';

block: '{}'line*'{}';

WS: [\t\r\n]+ -> skip;

IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;