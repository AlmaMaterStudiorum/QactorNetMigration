lexer grammar  ToyLexer ; 

MULT : '*';
DIV  : '/';
PLUS : '+';
SUB  : '-';

NUMBER
 : ( D* '.' )? D+
 ;

SPACES
 : [ \t\r\n] -> skip
 ;
 OPENROUND : '(';
 CLOSEROUND : ')';

fragment D : [0-9];