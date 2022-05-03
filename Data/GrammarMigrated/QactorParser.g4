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
	( message   += messageX   )+
	( context   += contextP   )+
	( actor     += qActorDeclaration    )+
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
contextP : Context  name=ID IpSTRING  ip=componentIP  ( mqtt = MqttSTRING )? ;
componentIP : OpenSquareSTRING HostSTRING host=STRING PortSTRING port=INT CloseSquareSTRING ; 
   
/*  
 * QActor
 */
qActorDeclaration : qActor | qActorCoded | qActorExternal ;
qActorExternal    : ExternalQActorSTRING name=ID ContextBNoCapitalLetterSTRING context =contextP ;
qActorCoded       : CodedQActorSTRING    name=ID ContextBNoCapitalLetterSTRING context =contextP ClassNameSTRING className = STRING ;
qActor            : QActorSTRING         name=ID ContextBNoCapitalLetterSTRING context =contextP 	 
    OpenBraceSTRING      	
    	( start = anyAction )?
		( states += state )*
	CloseBraceSTRING
;  
  
/*
 * State
 */
state :
	StateSTRING name=ID  ( normal = InitialSTRING )?
	//actionseq = ActionSequence
	OpenBraceSTRING ( actions += stateAction )*  CloseBraceSTRING
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
ifSolvedAction     : {IfSolvedAction} IfSolvedSTRING OpenBraceSTRING ( solvedactions += stateAction )*  CloseBraceSTRING //action=ActionSequence 
					 (ElseSTRING  OpenBraceSTRING ( notsolvedactions += stateAction )*  CloseBraceSTRING)?
;
guardedStateAction : {GuardedStateAction} IfSTRING guard = anyAction OpenBraceSTRING ( okactions += stateAction )*  CloseBraceSTRING  //action=ActionSequence  
					 (ElseSTRING  OpenBraceSTRING ( koactions += stateAction )*  CloseBraceSTRING)?
;

printCurMsg    :  {PrintCurMsg} PrintCurrentMessageSTRING  ;
print          :  {Print} PrintlnSTRING OpenRoundSTRING args=pHead CloseRoundSTRING    ; 
solveGoal      :  {SolveGoal} SolveSTRING OpenRoundSTRING goal=pHead (VirgilaSTRING resVar=variableX)? CloseRoundSTRING;
discardMsg     :  {DiscardMsg} DiscardMsgSTRING (discard =OnSTRING | OffSTRING) ;
memoTime       :  {MemoTime} MemoCurrentTimeSTRING	store=VARID ;
durationX       :  {Duration} SetDurationSTRING store=VARID FromSTRING start=VARID;
 
forward   : ForwardSTRING dest=qActorDeclaration MSTRING msgref=dispatch DuePuntiSTRING val = pHead ;
emit      : EmitSTRING msgref=event DuePuntiSTRING val = pHead	;
demand    : RequestNoCapitalSTRING dest=qActorDeclaration MSTRING msgref=request  DuePuntiSTRING val = pHead ;
answer    : ReplyToSTRING reqref=request  WithSTRING    msgref=reply   DuePuntiSTRING val = pHead ;
replyReq  : AskForSTRING  reqref=request  RequestNoCapitalSTRING msgref=request DuePuntiSTRING val = pHead ;
 
delay     : delayInt | delayVar | delayVref | delaySol ;
delayInt  : DelaySTRING time=INT  ;
delayVar  : DelayVarSTRING    refvar     = variableX ;
delayVref : DelayVarRefSTRING reftime    = varRef ;
delaySol  : DelaySolSTRING    refsoltime = varSolRef ;
msgCond   :	OnMsgSTRING OpenRoundSTRING message=messageX DuePuntiSTRING msg = pHead CloseRoundSTRING OpenBraceSTRING ( condactions += stateAction )*  CloseBraceSTRING
			 (ElseSTRING ifnot = noMsgCond )? ;			 
endActor  : TerminateSTRING arg=INT;			
updateResource :  {UpdateResource} UpdateResourceSTRING val=anyAction      ; 
 
noMsgCond :	{NoMsgCond}  OpenBraceSTRING (  notcondactions += stateAction )*  CloseBraceSTRING  ;
anyAction : {AnyAction}   OpenSquareSTRING body=KCODE CloseSquareSTRING  ; 							//OpenSquareSTRING body=STRING CloseSquareSTRING; 

codeRun        : codeRunActor | codeRunSimple  ;
codeRunActor   : QrunSTRING   aitem=QualifiedName OpenRoundSTRING MyselfSTRING ( VirgilaSTRING args+=pHead (VirgilaSTRING args+=pHead)* )? CloseRoundSTRING ;
codeRunSimple  : RunSTRING    bitem=QualifiedName OpenRoundSTRING (args+=pHead (VirgilaSTRING args+=pHead)* )? CloseRoundSTRING;
 
exec      : MachineExecSTRING action=STRING ; 

/*
 * Transition
 */
transitionX         :  emptyTransition | nonEmptyTransition ;
emptyTransition    : GotoSTRING targetState=state  (IfSTRING eguard=anyAction ElseSTRING othertargetState=state )?  ;
 
nonEmptyTransition :  TransitionSTRING name=ID  (duration=timeout)? ( trans += inputTransition)* (ElseSTRING elseempty=emptyTransition)?;
timeout            : timeoutInt | timeoutVar | timeoutSol | timeoutVarRef;
timeoutInt         : WhenTimeSTRING    msec=INT                    ArrowSTRING targetState = state  ;   
timeoutVar         : WhenTimeVarSTRING    variable   = variableX    ArrowSTRING targetState = state  ;  
timeoutVarRef      : WhenTimeVarRefSTRING refvar     = varRef      ArrowSTRING targetState = state  ;  
timeoutSol         : WhenTimeSolSTRING    refsoltime = varSolRef   ArrowSTRING targetState = state  ;  

inputTransition    : eventTransSwitch | msgTransSwitch | requestTransSwitch |  replyTransSwitch ;
eventTransSwitch   : WhenEventSTRING   message=event    (AndSTRING  guard=anyAction  )?  ArrowSTRING  targetState=state  ;
msgTransSwitch     : WhenMsgSTRING     message=dispatch (AndSTRING  guard=anyAction  )?  ArrowSTRING  targetState=state  ;
requestTransSwitch : WhenRequestSTRING message=request  (AndSTRING  guard=anyAction  )?  ArrowSTRING  targetState=state  ;
replyTransSwitch   : WhenReplySTRING   message=reply    (AndSTRING  guard=anyAction  )?  ArrowSTRING  targetState=state  ;
   
/*
 * PROLOG like
 */ 
pHead :	pAtom | pStruct	| pStructRef ;
pAtom : pAtomString | variableX | pAtomNum | pAtomic | varRef | varSolRef | varRefInStr;
pAtomString : val = STRING ;
pAtomic     : val = ID ;
pAtomNum    : val = INT ;
pStructRef  : SimbolDollarSTRING struct = pStruct;  //
pStruct     : functor=ID OpenRoundSTRING (msgArg += pHead) (VirgilaSTRING msgArg += pHead)* CloseRoundSTRING ;  
variableX    : {Variable} varName= VARID ;
//USING vars (from solve or from code)
varRef      : SimbolDollarSTRING varName= VARID ;	
varRefInStr : SimbolHashSTRING varName= VARID ;
varSolRef   : SimbolAtSTRING varName= VARID ;	
