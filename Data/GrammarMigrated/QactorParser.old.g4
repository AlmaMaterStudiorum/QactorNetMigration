/*
 * ========================================================================
 * This metamodel is the basic model for a software factory that
 * aims at introducing a support for qactor-based systems 
 * Author: Antonio Natali, DISI University of Bologna
 * ========================================================================
 */

parser grammar  QactorParser   ;

 
options
{
	language = CSharp;
    tokenVocab = QactorLexer;
}
 
qactorSystemSpec:
	 name=ID  
	( mqttBroker = brokerSpec)? 
	( message   += messageX   )*
	( context   += contextP   )*
	( actor     += qActorDeclaration    )*
; 
   
brokerSpec : MqttBrokerSTRING brokerHost=STRING DuePuntiSTRING brokerPort=INT ; 
    
/*
 * ------------------------------------------
 * MESSAGE
* ------------------------------------------
 */
messageX :     	    outOnlyMessage | outInMessage  ;
outOnlyMessage : 	dispatch |  event | signal | token ; 
outInMessage: 		request  | reply | invitation ;
  
event: 		EventSTRING      name=ID  DuePuntiSTRING msg = pHead  ;
signal: 	SignalSTRING     name=ID  DuePuntiSTRING msg = pHead  ;
token:		TokenSTRING      name=ID  DuePuntiSTRING msg = pHead  ;
dispatch: 	DispatchSTRING   name=ID  DuePuntiSTRING msg = pHead  ;
request: 	RequestCapitalSTRING    name=ID  DuePuntiSTRING msg = pHead  ;
reply: 	    ReplySTRING      name=ID  DuePuntiSTRING msg = pHead  ;
invitation:	InvitationSTRING name=ID  DuePuntiSTRING msg = pHead  ;

/* 
 * Context
 */
contextP : 'Context'  name=ID 'ip'  ip=componentIP  ( mqtt = '+mqtt' )? ;
componentIP : '[' 'host=' host=STRING 'port=' port=INT ']' ; 
   
/*  
 * QActor
 */
qActorDeclaration : qActor | qActorCoded | qActorExternal ;
qActorExternal    : 'ExternalQActor' name=ID 'context' context =contextP ;
qActorCoded       : 'CodedQActor'    name=ID 'context' context =contextP 'className' className = STRING ;
qActor            : 'QActor'         name=ID 'context' context =contextP 	 
    '{'      	
    	( start = anyAction )?
		( states += state )*
	'}'
;  
  
/*
 * State
 */
state :
	'State' name=ID  ( normal = 'initial' )?
	//actionseq = ActionSequence
	'{' ( actions += stateAction )*  '}'
	( transition = transitionX )?
;

/*
 * StateAction
 */
stateAction    : 
	guardedStateAction | ifSolvedAction |															//pre
	print | printCurMsg | solveGoal |	discardMsg |  memoTime | durationX |							//general
	forward | emit | demand | answer | replyReq | delay | msgCond |	endActor |  updateResource | 	//qak kotlin
	codeRun | anyAction	| exec																		//extra code
;  
ifSolvedAction     : {IfSolvedAction} 'ifSolved' '{' ( solvedactions += stateAction )*  '}' //action=ActionSequence 
					 ('else'  '{' ( notsolvedactions += stateAction )*  '}')?
;
guardedStateAction : {GuardedStateAction} 'if' guard = anyAction '{' ( okactions += stateAction )*  '}'  //action=ActionSequence  
					 ('else'  '{' ( koactions += stateAction )*  '}')?
;

printCurMsg    :  {PrintCurMsg} 'printCurrentMessage'  ;
print          :  {Print} 'println' '(' args=pHead ')'    ; 
solveGoal      :  {SolveGoal} 'solve' '(' goal=pHead (',' resVar=variableX)? ')';
discardMsg     :  {DiscardMsg} 'discardMsg' (discard ='On' | 'Off') ;
memoTime       :  {MemoTime} 'memoCurrentTime'	store=VARID ;
durationX       :  {Duration} 'setDuration' store=VARID 'from' start=VARID;
 
forward   : 'forward' dest=qActorDeclaration '-m' msgref=dispatch ':' val = pHead ;
emit      : 'emit' msgref=event ':' val = pHead	;
demand    : 'request' dest=qActorDeclaration '-m' msgref=request  ':' val = pHead ;
answer    : 'replyTo' reqref=request  'with'    msgref=reply   ':' val = pHead ;
replyReq  : 'askFor'  reqref=request  'request' msgref=request ':' val = pHead ;
 
delay     : delayInt | delayVar | delayVref | delaySol ;
delayInt  : 'delay' time=INT  ;
delayVar  : 'delayVar'    refvar     = variableX ;
delayVref : 'delayVarRef' reftime    = varRef ;
delaySol  : 'delaySol'    refsoltime = varSolRef ;
msgCond   :	'onMsg' '(' message=messageX ':' msg = pHead ')' '{' ( condactions += stateAction )*  '}'
			 ('else' ifnot = noMsgCond )? ;			 
endActor  : 'terminate' arg=INT;			
updateResource :  {UpdateResource} 'updateResource' val=anyAction      ; 
 
noMsgCond :	{NoMsgCond}  '{' (  notcondactions += stateAction )*  '}'  ;
anyAction : {AnyAction}   '[' body=KCODE ']'  ; 							//'[' body=STRING ']'; 

codeRun        : codeRunActor | codeRunSimple  ;
codeRunActor   : 'qrun'   aitem=QualifiedName '(' 'myself' ( ',' args+=pHead (',' args+=pHead)* )? ')' ;
codeRunSimple  : 'run'    bitem=QualifiedName '(' (args+=pHead (',' args+=pHead)* )? ')';
 
exec      : 'machineExec' action=STRING ; 

/*
 * Transition
 */
transitionX         :  emptyTransition | nonEmptyTransition ;
emptyTransition    : 'Goto' targetState=state  ('if' eguard=anyAction 'else' othertargetState=state )?  ;
 
nonEmptyTransition :  'Transition' name=ID  (duration=timeout)? ( trans += inputTransition)* ('else' elseempty=emptyTransition)?;
timeout            : timeoutInt | timeoutVar | timeoutSol | timeoutVarRef;
timeoutInt         : 'whenTime'    msec=INT                    '->' targetState = state  ;   
timeoutVar         : 'whenTimeVar'    variable   = variableX    '->' targetState = state  ;  
timeoutVarRef      : 'whenTimeVarRef' refvar     = varRef      '->' targetState = state  ;  
timeoutSol         : 'whenTimeSol'    refsoltime = varSolRef   '->' targetState = state  ;  

inputTransition    : eventTransSwitch | msgTransSwitch | requestTransSwitch |  replyTransSwitch ;
eventTransSwitch   : 'whenEvent'   message=event    ('and'  guard=anyAction  )?  '->'  targetState=state  ;
msgTransSwitch     : 'whenMsg'     message=dispatch ('and'  guard=anyAction  )?  '->'  targetState=state  ;
requestTransSwitch : 'whenRequest' message=request  ('and'  guard=anyAction  )?  '->'  targetState=state  ;
replyTransSwitch   : 'whenReply'   message=reply    ('and'  guard=anyAction  )?  '->'  targetState=state  ;
   
/*
 * PROLOG like
 */ 
pHead :	pAtom | pStruct	| pStructRef ;
pAtom : pAtomString | variableX | pAtomNum | pAtomic | varRef | varSolRef | varRefInStr;
pAtomString : val = STRING ;
pAtomic     : val = ID ;
pAtomNum    : val = INT ;
pStructRef  : '$' struct = pStruct;  //
pStruct     : functor=ID '(' (msgArg += pHead) (',' msgArg += pHead)* ')' ;  
variableX    : {Variable} varName= VARID ;
//USING vars (from solve or from code)
varRef      : '$' varName= VARID ;	
varRefInStr : '#' varName= VARID ;
varSolRef   : '@' varName= VARID ;	