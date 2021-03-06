/*
 * ========================================================================
 * This metamodel is the basic model for a software factory that
 * aims at introducing a support for qactor-based systems 
 * Author: Antonio Natali, DISI University of Bologna
 * ========================================================================
 */

grammar Qactork ;


QActorSystem: 'System' ( trace = '-trace' )? (  logmsg = '-msglog' )? spec=QActorSystemSpec ;

ID: '^'?('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'_'|'0'..'9')*;
INT : ('0'..'9')+;
STRING:
			'"' ( '\\' . /* 'b'|'t'|'n'|'f'|'r'|'u'|'"'|"'"|'\\' */ | !('\\'|'"') )* '"' |
			"'" ( '\\' . /* 'b'|'t'|'n'|'f'|'r'|'u'|'"'|"'"|'\\' */ | !('\\'|"'") )* "'" ;
ML_COMMENT : '/*' -> '*/';
SL_COMMENT : '//' !('\n'|'\r')* ('\r'? '\n')?;

WS         : (' '|'\t'|'\r'|'\n')+;

ANY_OTHER: .;

VARID  :  ('A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'_'|'0'..'9')*;
KCODE  : '#' ( . )* '#'  ;

QualifiedName : ID ('.' ID)* ;

QActorSystemSpec:
	 name=ID  
	( mqttBroker = BrokerSpec)? 
	( message   += Message   )*
	( context   += Context   )*
	( actor     += QActorDeclaration    )*
;
BrokerSpec : 'mqttBroker' brokerHost=STRING ':' brokerPort=INT ;
/*
 * ------------------------------------------
 * MESSAGE
* ------------------------------------------
 */
Message :     	    OutOnlyMessage | OutInMessage  ;
OutOnlyMessage : 	Dispatch |  Event | Signal | Token ; 
OutInMessage: 		Request  | Reply | Invitation ;

Event: 		'Event'      name=ID  ':' msg = PHead  ;
Signal: 	'Signal'     name=ID  ':' msg = PHead  ;
Token:		'Token'      name=ID  ':' msg = PHead  ;
Dispatch: 	'Dispatch'   name=ID  ':' msg = PHead  ;
Request: 	'Request'    name=ID  ':' msg = PHead  ;
Reply: 	    'Reply'      name=ID  ':' msg = PHead  ;
Invitation:	'Invitation' name=ID  ':' msg = PHead  ;

/* 
 * Context
 */
Context : 'Context'  name=ID 'ip'  ip = ComponentIP  ( mqtt = '+mqtt' )? ;
ComponentIP : {ComponentIP} '[' 'host=' host=STRING 'port=' port=INT ']' ; 

/* 
 * QActor
 */
QActorDeclaration : QActor | QActorCoded | QActorExternal ;
QActorExternal    : 'ExternalQActor' name=ID 'context' context = [ Context ] ;
QActorCoded       : 'CodedQActor'    name=ID 'context' context = [ Context ] 'className' className = STRING ;
QActor            : 'QActor'         name=ID 'context' context = [ Context ]  	 
    '{'      	
    	( start = AnyAction )?
		( states += State )*
	'}'
; 
 
/*
 * State
 */
State :
	'State' name=ID  ( normal = 'initial' )?
	//actionseq = ActionSequence
	'{' ( actions += StateAction )*  '}'
	( transition = Transition )?
;

/*
 * StateAction
 */
StateAction    : 
	GuardedStateAction | IfSolvedAction |															//pre
	Print | PrintCurMsg | SolveGoal |	DiscardMsg |  MemoTime | Duration |							//general
	Forward | Emit | Demand | Answer | ReplyReq | Delay | MsgCond |	EndActor |  UpdateResource | 	//qak kotlin
	CodeRun | AnyAction	| Exec																		//extra code
;  
IfSolvedAction     : {IfSolvedAction} 'ifSolved' '{' ( solvedactions += StateAction )*  '}' //action=ActionSequence 
					 ('else'  '{' ( notsolvedactions += StateAction )*  '}')?
;
GuardedStateAction : {GuardedStateAction} 'if' guard = AnyAction '{' ( okactions += StateAction )*  '}'  //action=ActionSequence  
					 ('else'  '{' ( koactions += StateAction )*  '}')?
;

PrintCurMsg    :  {PrintCurMsg} 'printCurrentMessage'  ;
Print          :  {Print} 'println' '(' args=PHead ')'    ; 
SolveGoal      :  {SolveGoal} 'solve' '(' goal=PHead (',' resVar=Variable)? ')';
DiscardMsg     :  {DiscardMsg} 'discardMsg' (discard ='On' | 'Off') ;
MemoTime       :  {MemoTime} 'memoCurrentTime'	store=VARID ;
Duration       :  {Duration} 'setDuration' store=VARID 'from' start=VARID;

Forward   : 'forward' dest=[QActorDeclaration] '-m' msgref=[Dispatch] ':' val = PHead ;
Emit      : 'emit' msgref=[Event] ':' val = PHead	;
Demand    : 'request' dest=[QActorDeclaration] '-m' msgref=[Request]  ':' val = PHead ;
Answer    : 'replyTo' reqref=[Request]  'with'    msgref=[Reply]   ':' val = PHead ;
ReplyReq  : 'askFor'  reqref=[Request]  'request' msgref=[Request] ':' val = PHead ;

Delay     : DelayInt | DelayVar | DelayVref | DelaySol ;
DelayInt  : 'delay' time=INT  ;
DelayVar  : 'delayVar'    refvar     = Variable ;
DelayVref : 'delayVarRef' reftime    = VarRef ;
DelaySol  : 'delaySol'    refsoltime = VarSolRef ;
MsgCond   :	'onMsg' '(' message=[Message] ':' msg = PHead ')' '{' ( condactions += StateAction )*  '}'
			 ('else' ifnot = NoMsgCond )? ;			 
EndActor  : 'terminate' arg=INT;			
UpdateResource :  {UpdateResource} 'updateResource' val=AnyAction      ; 
 
NoMsgCond :	{NoMsgCond}  '{' (  notcondactions += StateAction )*  '}'  ;
AnyAction : {AnyAction}   '[' body=KCODE ']'  ; 							//'[' body=STRING ']'; 

CodeRun        : CodeRunActor | CodeRunSimple  ;
CodeRunActor   : 'qrun'   aitem=QualifiedName '(' 'myself' ( ',' args+=PHead (',' args+=PHead)* )? ')' ;
CodeRunSimple  : 'run'    bitem=QualifiedName '(' (args+=PHead (',' args+=PHead)* )? ')';

Exec      : 'machineExec' action=STRING ; 

/*
 * Transition
 */
Transition         :  EmptyTransition | NonEmptyTransition ;
EmptyTransition    : 'Goto' targetState=[State]  ('if' eguard=AnyAction 'else' othertargetState=[State] )?  ;

NonEmptyTransition :  'Transition' name=ID  (duration=Timeout)? ( trans += InputTransition)* ('else' elseempty=EmptyTransition)?;
Timeout            : TimeoutInt | TimeoutVar | TimeoutSol | TimeoutVarRef;
TimeoutInt         : 'whenTime'    msec=INT                    '->' targetState = [State]  ;   
TimeoutVar         : 'whenTimeVar'    variable   = Variable    '->' targetState = [State]  ;  
TimeoutVarRef      : 'whenTimeVarRef' refvar     = VarRef      '->' targetState = [State]  ;  
TimeoutSol         : 'whenTimeSol'    refsoltime = VarSolRef   '->' targetState = [State]  ;  

InputTransition    : EventTransSwitch | MsgTransSwitch | RequestTransSwitch |  ReplyTransSwitch ;
EventTransSwitch   : 'whenEvent'   message=[Event]    ('and'  guard=AnyAction  )?  '->'  targetState=[State]  ;
MsgTransSwitch     : 'whenMsg'     message=[Dispatch] ('and'  guard=AnyAction  )?  '->'  targetState=[State]  ;
RequestTransSwitch : 'whenRequest' message=[Request]  ('and'  guard=AnyAction  )?  '->'  targetState=[State]  ;
ReplyTransSwitch   : 'whenReply'   message=[Reply]    ('and'  guard=AnyAction  )?  '->'  targetState=[State]  ;
 
/*
 * PROLOG like
 */ 
PHead :	PAtom | PStruct	| PStructRef ;
PAtom : PAtomString | Variable | PAtomNum | PAtomic | VarRef | VarSolRef | VarRefInStr;
PAtomString : val = STRING ;
PAtomic     : val = ID ;
PAtomNum    : val = INT ;
PStructRef  : '$' struct = PStruct;  //
PStruct     : functor=ID '(' (msgArg += PHead) (',' msgArg += PHead)* ')' ;  //At least one arg is required
Variable    : {Variable} varName= VARID ;
//USING vars (from solve or from code)
VarRef      : '$' varName= VARID ;	//in msg payload  e.g. modelChange(robot,$Curmove) => $Curmove
VarRefInStr : '#' varName= VARID ;	//in msg payload. e.g. modelChange(robot,#M)       => ${getCurSol('M').toString()}
VarSolRef   : '@' varName= VARID ;	//in run          e.g. run itunibo....doMove( @M ) => getCurSol('V').toString()