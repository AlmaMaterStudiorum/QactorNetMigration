lexer grammar  QactorProgLexer   ; 



TraceSTRING:'-trace';
MsglogSTRING:'-msglog';
SYSTEMSTRING : 'System' ;
DuePuntiSTRING : ':';
ExternalQActorSTRING : 'ExternalQActor'; 
MqttBrokerSTRING : 'mqttBroker';
EventSTRING : 'Event';
SignalSTRING : 'Signal';
TokenSTRING : 'Token';
DispatchSTRING : 'Dispatch';
REQUESTCAPITALSTRING : 'Request' ;
ReplySTRING : 'Reply';
InvitationSTRING : 'Invitation';
ContextSTRING : 'Context';
IpSTRING : 'ip';
MqttSTRING : '+mqtt';
OpenSquareSTRING : '[' ;
HostSTRING : 'host=';
PortSTRING : 'port=';
CloseSquareSTRING : ']';
CodedQActorSTRING : 'CodedQActor';
QActorSTRING : 'QActor';
ContextBNoCapitalLetterSTRING : 'context';
ClassNameSTRING : 'className';
OpenBraceSTRING : '{';
CloseBraceSTRING : '}';
StateSTRING : 'State';
InitialSTRING : 'initial';
IfSolvedSTRING : 'ifSolved';
ElseSTRING : 'else';
PrintCurrentMessageSTRING : 'printCurrentMessage';
PrintlnSTRING : 'println';
OpenRoundSTRING  : '(';
CloseRoundSTRING : ')';
SolveSTRING : 'solve';
DiscardMsgSTRING : 'discardMsg';
MemoCurrentTimeSTRING : 'memoCurrentTime';
SetDurationSTRING : 'setDuration';
OnSTRING : 'On';
OffSTRING : 'Off';
FromSTRING : 'from';
ForwardSTRING : 'forward';
EmitSTRING : 'emit';
MSTRING : '-m';
RequestNoCapitalSTRING : 'request';
ReplyToSTRING : 'replyTo';
AskForSTRING : 'askFor';
WithSTRING : 'with';
DelaySTRING : 'delay';
DelayVarSTRING : 'delayVar';
DelayVarRefSTRING : 'delayVarRef';
DelaySolSTRING : 'delaySol';
OnMsgSTRING : 'onMsg';
TerminateSTRING : 'terminate';
UpdateResourceSTRING : 'updateResource';
QrunSTRING : 'qrun';
MyselfSTRING : 'myself';
RunSTRING : 'run';
MachineExecSTRING : 'machineExec';
GotoSTRING : 'Goto';
IfSTRING : 'if';
TransitionSTRING : 'Transition';
WhenTimeSTRING : 'whenTime';
ArrowSTRING : '->';
WhenTimeVarSTRING : 'whenTimeVar';
WhenTimeVarRefSTRING : 'whenTimeVarRef';
WhenTimeSolSTRING : 'whenTimeSol';
WhenEventSTRING : 'whenEvent';
WhenMsgSTRING : 'whenMsg';
WhenRequestSTRING : 'whenRequest';
WhenReplySTRING : 'whenReply';
AndSTRING : 'and';
VirgolaSTRING : ',';
SimbolDollarSTRING : '$';
SimbolHashSTRING : '#';
SimbolAtSTRING : '@';
PuntoSTRING : '.';


ID: '^'?('a'..'z') ('a'..'z'|'A'..'Z'|'_'|'0'..'9')*;
//ID: '^'?('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'_'|'0'..'9')*;
//ID: [a-z_][a-z0-9_]*;

INT : [0-9]+;


STRING : '"' .*? '"' ; // match anything in "..."


ML_COMMENT
    : '/*' .*? '*/' -> skip
;

SL_COMMENT
    : '//' ~[\r\n]* -> skip
;


//WS         : [' '|'\t'|'\r'|'\n']+;
//WS         : (' '|'\t')+ -> skip;





VARID  :  ('A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'_'|'0'..'9')*;
KCODE  : '#' ( . )*? '#'  ;

WS         : (' '|'\t'|'\r'|'\n')+;

ANY_OTHER: .;

//WS : [ \t\r\n\f]+ -> channel(HIDDEN) ;
//WS         : [ \t\r\n]+ -> skip;
//NEWLINE    : ('\r'|'\n')+ -> skip;


