lexer grammar  QactorLexer   ; 


ID: '^'?('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'_'|'0'..'9')*;
INT : ('0'..'9')+;


STRING : '"' .*? '"' ; // match anything in "..."


ML_COMMENT
    : '/*' .*? '*/' -> skip
;

SL_COMMENT
    : '//' ~[\r\n]* -> skip
;


WS         : (' '|'\t'|'\r'|'\n')+;

ANY_OTHER: .;

VARID  :  ('A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'_'|'0'..'9')*;
KCODE  : '#' ( . )*? '#'  ;

QualifiedName : ID ('.' ID)* ;

DuePuntiSTRING : ':';
ExternalQActorSTRING : 'ExternalQActor'; 
MqttBrokerSTRING : 'mqttBroker';
EventSTRING : 'Event';
SignalSTRING : 'Signal';
TokenSTRING : 'Token';
DispatchSTRING : 'Dispatch';
RequestCapitalSTRING : 'Request' ;
ReplySTRING : 'Reply';
InvitationSTRING : 'Invitation';
Context : 'Context';
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
OpenRoundSTRING  :'(';
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
VirgilaSTRING : ',';
SimbolDollarSTRING : '$';
SimbolHashSTRING : '#';
SimbolAtSTRING : '@';








