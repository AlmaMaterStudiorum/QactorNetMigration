parser grammar  QactorProgParser   ;

options
{
	language = CSharp;
    tokenVocab = QactorProgLexer;
}



program :  qActorSystem qactorSystemSpec;

qActorSystem: SYSTEMSTRING ( trace = TraceSTRING )? (  logmsg = MsglogSTRING )? name=ID ;

qactorSystemSpec: brokerSpec? messageSpec* contexSpec* actorSpec* EOF; 

   

brokerSpec  : MqttBrokerSTRING  brokerHost=STRING DuePuntiSTRING brokerPort=INT WS; 
messageSpec : message+=messageP ;
contexSpec  : context+=contextP ;
actorSpec   : actor+=qActorDeclaration ;
    
/*
 * ------------------------------------------
 * MESSAGE
* ------------------------------------------
 */
messageP :     	    outOnlyMessage | outInMessage  ;
outOnlyMessage : 	dispatch |  event | signal | token ; 
outInMessage: 		request  | reply | invitation ;
  


event: 		EventSTRING				name=ID  DuePuntiSTRING  msg=pHead  WS;
signal: 	SignalSTRING			name=ID  DuePuntiSTRING  msg=pHead  WS;
token:		TokenSTRING				name=ID  DuePuntiSTRING  msg=pHead  WS;
dispatch: 	DispatchSTRING			name=ID  DuePuntiSTRING  msg=pHead  WS;
request: 	REQUESTCAPITALSTRING	name=ID  DuePuntiSTRING  msg=pHead  WS;
reply: 	    ReplySTRING				name=ID  DuePuntiSTRING  msg=pHead  WS;
invitation:	InvitationSTRING		name=ID  DuePuntiSTRING  msg=pHead  WS;

/* 
 * Context
 */
contextP : ContextSTRING  name=ID IpSTRING  ip=componentIP  ( mqtt = MqttSTRING )? ;
componentIP : OpenSquareSTRING HostSTRING host=STRING PortSTRING port=INT CloseSquareSTRING ; 
   
/*  
 * QActor
 */
qActorDeclaration : qActor | qActorCoded | qActorExternal ;
qActorExternal    : ExternalQActorSTRING name=ID ContextBNoCapitalLetterSTRING context =contextP ;
qActorCoded       : CodedQActorSTRING    name=ID ContextBNoCapitalLetterSTRING context =contextP ClassNameSTRING className = STRING ;
qActor            : QActorSTRING         name=ID ContextBNoCapitalLetterSTRING context =contextP OpenBraceSTRING ( start = anyAction )? ( states += state )* CloseBraceSTRING;  
  
/*
 * State
 */ //actionseq = ActionSequence
state : StateSTRING name=ID  ( normal = InitialSTRING )? OpenBraceSTRING ( actions += stateAction )*  CloseBraceSTRING ( transition = transitionX )? ;

/*
 * StateAction
 */
stateAction    : guardedStateAction | ifSolvedAction |	print | printCurMsg | solveGoal |	discardMsg |  memoTime | durationX | forward | emit | demand | answer | replyReq | delay | msgCond | endActor |  updateResource | codeRun | anyAction| exec	;  
ifSolvedAction     : IfSolvedSTRING OpenBraceSTRING ( solvedactions += stateAction )*  CloseBraceSTRING (ElseSTRING  OpenBraceSTRING ( notsolvedactions += stateAction )*  CloseBraceSTRING)?;
guardedStateAction : IfSTRING guard = anyAction OpenBraceSTRING ( okactions += stateAction )*  CloseBraceSTRING    (ElseSTRING  OpenBraceSTRING ( koactions += stateAction )*  CloseBraceSTRING)?;

printCurMsg    :  PrintCurrentMessageSTRING  ;
print          :  PrintlnSTRING OpenRoundSTRING args=pHead CloseRoundSTRING    ; 
solveGoal      :  SolveSTRING OpenRoundSTRING goal=pHead (VirgolaSTRING resVar=variableX)? CloseRoundSTRING;
discardMsg     :  DiscardMsgSTRING (discard =OnSTRING | OffSTRING) ;
memoTime       :  MemoCurrentTimeSTRING	store=VARID ;
durationX      :  SetDurationSTRING store=VARID FromSTRING start=VARID;
 
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
msgCond   :	OnMsgSTRING OpenRoundSTRING message=messageP DuePuntiSTRING msg = pHead CloseRoundSTRING OpenBraceSTRING ( condactions += stateAction )*  CloseBraceSTRING (ElseSTRING ifnot = noMsgCond )? ;			 
endActor  : TerminateSTRING arg=INT;			
updateResource : UpdateResourceSTRING val=anyAction      ; 
 
noMsgCond :	OpenBraceSTRING (  notcondactions += stateAction )*  CloseBraceSTRING  ;
anyAction : OpenSquareSTRING body=KCODE CloseSquareSTRING  ; 							
//OpenSquareSTRING body=STRING CloseSquareSTRING; 


virgolaPHead   : VirgolaSTRING args+=pHead ;
qualifiedName : first=ID (PuntoSTRING second=ID)* ;
codeRun        : codeRunActor | codeRunSimple  ;
codeRunActor   : QrunSTRING   aitem=qualifiedName OpenRoundSTRING MyselfSTRING (virgolaPHead (virgolaPHead)* )? CloseRoundSTRING ;

codeRunSimple  : RunSTRING    bitem=qualifiedName OpenRoundSTRING (args+=pHead (virgolaPHead)* )? CloseRoundSTRING;
 
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
pStruct     : functor=ID OpenRoundSTRING msgArg += pHead (VirgolaSTRING msgArg += pHead)* CloseRoundSTRING ;  
variableX    : varName= VARID ;
//USING vars (from solve or from code)
varRef      : SimbolDollarSTRING varName= VARID ;	
varRefInStr : SimbolHashSTRING varName= VARID ;
varSolRef   : SimbolAtSTRING varName= VARID ;	

