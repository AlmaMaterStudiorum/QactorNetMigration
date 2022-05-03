parser grammar  ToyParser   ;

options
{
	language = CSharp;
    tokenVocab = ToyLexer;
}

parse
 : expr_list+=expr+ EOF
 ;
/*
expr
 : OPENROUND expr CLOSEROUND           #NestedExpr
 | SUB expr                            #UnaryExpr
 | lhs=expr op=( MULT | DIV ) rhs=expr #MultDivExpr
 | lhs=expr op=( PLUS | SUB ) rhs=expr #PlusSubExpr
 | NUMBER                              #NumberExpr
 ;
 */
 expr
 : OPENROUND expr CLOSEROUND           #NestedExpr
 | SUB expr                            #UnaryExpr
 | lhs=expr op=MULT	rhs=expr		   #MultExpr
 | lhs=expr op=DIV  rhs=expr           #DivExpr
 | lhs=expr op=PLUS rhs=expr		   #PlusExpr
 | lhs=expr op=SUB  rhs=expr           #SubExpr
 | NUMBER                              #NumberExpr
 ;

