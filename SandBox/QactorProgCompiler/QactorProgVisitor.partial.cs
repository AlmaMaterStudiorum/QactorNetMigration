namespace QactorProgCompiler
{
    public partial class QactorProgVisitor : QactorProgParserBaseVisitor<object>
    {
        public override object VisitProgram(QactorProgParser.ProgramContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth()); 
            if (context.qActorSystem() != null)
            {
                this.VisitQActorSystem(context.qActorSystem());
            }
            if (context.qactorSystemSpec() != null)
            {
                this.VisitQactorSystemSpec(context.qactorSystemSpec());
            }

            return ReturnValue;
        }

        public override object VisitQactorSystemSpec(QactorProgParser.QactorSystemSpecContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.brokerSpec() != null)
            {
                this.VisitBrokerSpec(context.brokerSpec());
            }
            if (context.messageSpec() != null)
            {
                foreach (var item in context.messageSpec())
                {
                    this.VisitMessageSpec(item);
                }
            }
            if (context.contexSpec() != null)
            {
                foreach (var item in context.contexSpec())
                {
                    this.VisitContexSpec(item);
                }
            }
            if (context.actorSpec() != null)
            {
                foreach (var item in context.actorSpec())
                {
                    this.VisitActorSpec(item);
                }
            }
            return ReturnValue;
        }

        public override object VisitMessageSpec(QactorProgParser.MessageSpecContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            this.VisitMessageP(context.messageP());

            return ReturnValue;
        }

        public override object VisitContexSpec(QactorProgParser.ContexSpecContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            this.VisitContextP(context.contextP());

            return ReturnValue;
        }

        public override object VisitActorSpec(QactorProgParser.ActorSpecContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            this.VisitQActorDeclaration(context.qActorDeclaration());

            return ReturnValue;
        }

        public override object VisitMessageP(QactorProgParser.MessagePContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.outOnlyMessage() != null)
            {
                this.VisitOutOnlyMessage(context.outOnlyMessage());
            }
            if (context.outInMessage() != null)
            {
                this.VisitOutInMessage(context.outInMessage());
            }
            return ReturnValue;
        }

        public override object VisitOutOnlyMessage(QactorProgParser.OutOnlyMessageContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.@event() != null)
            {
                this.VisitEvent(context.@event());
            }
            if (context.signal() != null)
            {
                this.VisitSignal(context.signal());
            }
            if (context.token() != null)
            {
                this.VisitToken(context.token());
            }
            if (context.dispatch() != null)
            {
                this.VisitDispatch(context.dispatch());
            }
            return ReturnValue;
        }

        public override object VisitOutInMessage(QactorProgParser.OutInMessageContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.request() != null)
            {
                this.VisitRequest(context.request());
            }
            if (context.reply() != null)
            {
                this.VisitReply(context.reply());
            }
            if (context.invitation() != null)
            {
                this.VisitInvitation(context.invitation());
            }
            return ReturnValue;
        }

        public override object VisitEvent(QactorProgParser.EventContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            Trace("name", context.name.Text, context.Depth());


            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitSignal(QactorProgParser.SignalContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            Trace("name", context.name.Text, context.Depth());
            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitToken(QactorProgParser.TokenContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            Trace("name", context.name.Text, context.Depth());
            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitDispatch(QactorProgParser.DispatchContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            Trace("name", context.name.Text, context.Depth());
            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitRequest(QactorProgParser.RequestContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            Trace("name", context.name.Text, context.Depth());
            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitReply(QactorProgParser.ReplyContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            Trace("name", context.name.Text, context.Depth());
            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitInvitation(QactorProgParser.InvitationContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            Trace("name", context.name.Text, context.Depth());
            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitContextP(QactorProgParser.ContextPContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.componentIP() != null)
            {
                this.VisitComponentIP(context.componentIP());
            }
            return ReturnValue;
        }

        public override object VisitQActorDeclaration(QactorProgParser.QActorDeclarationContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.qActorExternal() != null)
            {
                this.VisitQActorExternal(context.qActorExternal());
            }
            if (context.qActorCoded() != null)
            {
                this.VisitQActorCoded(context.qActorCoded());
            }
            if (context.qActor() != null)
            {
                this.VisitQActor(context.qActor());
            }
            return ReturnValue;
        }

        public override object VisitQActorExternal(QactorProgParser.QActorExternalContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.contextP() != null)
            {
                this.VisitContextP(context.contextP());
            }
            return ReturnValue;
        }

        public override object VisitQActorCoded(QactorProgParser.QActorCodedContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.contextP() != null)
            {
                this.VisitContextP(context.contextP());
            }
            return ReturnValue;
        }

        public override object VisitQActor(QactorProgParser.QActorContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            Trace("name", context.name.Text, context.Depth());
            if (context.contextP() != null)
            {
                this.VisitContextP(context.contextP());
            }
            if (context.state() != null)
            {
                foreach (var item in context.state())
                {
                    this.VisitState(item);
                }
            }
            if (context.anyAction() != null)
            {
                this.VisitAnyAction(context.anyAction());
            }
            return ReturnValue;
        }

        public override object VisitState(QactorProgParser.StateContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            Trace("name", context.name.Text, context.Depth());
            if (context.stateAction() != null)
            {
                foreach (var item in context.stateAction())
                {
                    this.VisitStateAction(item);
                }
            }
            if (context.transitionX() != null)
            {
                this.VisitTransitionX(context.transitionX());
            }
            return ReturnValue;
        }

        public override object VisitStateAction(QactorProgParser.StateActionContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.ifSolvedAction() != null)
            {
                this.VisitIfSolvedAction(context.ifSolvedAction());
            }
            if (context.guardedStateAction() != null)
            {
                this.VisitGuardedStateAction(context.guardedStateAction());
            }
            if (context.printCurMsg() != null)
            {
                this.VisitPrintCurMsg(context.printCurMsg());
            }
            if (context.print() != null)
            {
                this.VisitPrint(context.print());
            }
            if (context.solveGoal() != null)
            {
                this.VisitSolveGoal(context.solveGoal());
            }
            if (context.discardMsg() != null)
            {
                this.VisitDiscardMsg(context.discardMsg());
            }
            if (context.memoTime() != null)
            {
                this.VisitMemoTime(context.memoTime());
            }
            if (context.durationX() != null)
            {
                this.VisitDurationX(context.durationX());
            }
            if (context.forward() != null)
            {
                this.VisitForward(context.forward());
            }
            if (context.emit() != null)
            {
                this.VisitEmit(context.emit());
            }
            if (context.demand() != null)
            {
                this.VisitDemand(context.demand());
            }
            if (context.answer() != null)
            {
                this.VisitAnswer(context.answer());
            }
            if (context.replyReq() != null)
            {
                this.VisitReplyReq(context.replyReq());
            }
            if (context.delay() != null)
            {
                this.VisitDelay(context.delay());
            }
            if (context.msgCond() != null)
            {
                this.VisitMsgCond(context.msgCond());
            }
            if (context.endActor() != null)
            {
                this.VisitEndActor(context.endActor());
            }
            if (context.updateResource() != null)
            {
                this.VisitUpdateResource(context.updateResource());
            }
            if (context.anyAction() != null)
            {
                this.VisitAnyAction(context.anyAction());
            }
            if (context.codeRun() != null)
            {
                this.VisitCodeRun(context.codeRun());
            }
            if (context.exec() != null)
            {
                this.VisitExec(context.exec());
            }
            return ReturnValue;
        }

        public override object VisitIfSolvedAction(QactorProgParser.IfSolvedActionContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.stateAction() != null)
            {
                foreach (var item in context.stateAction())
                {
                    this.VisitStateAction(item);
                }
            }
            return ReturnValue;
        }

        public override object VisitGuardedStateAction(QactorProgParser.GuardedStateActionContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.stateAction() != null)
            {
                foreach (var item in context.stateAction())
                {
                    this.VisitStateAction(item);
                }
            }
            if (context.anyAction() != null)
            {
                this.VisitAnyAction(context.anyAction());
            }
            return ReturnValue;
        }

        public override object VisitPrint(QactorProgParser.PrintContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitSolveGoal(QactorProgParser.SolveGoalContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            if (context.variableX() != null)
            {
                this.VisitVariableX(context.variableX());
            }
            return ReturnValue;
        }

        public override object VisitForward(QactorProgParser.ForwardContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.dispatch() != null)
            {
                this.VisitDispatch(context.dispatch());
            }
            if (context.qActorDeclaration() != null)
            {
                this.VisitQActorDeclaration(context.qActorDeclaration());
            }

            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitEmit(QactorProgParser.EmitContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.@event() != null)
            {
                this.VisitEvent(context.@event());
            }
            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitDemand(QactorProgParser.DemandContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.request() != null)
            {
                this.VisitRequest(context.request());
            }
            if (context.qActorDeclaration() != null)
            {
                this.VisitQActorDeclaration(context.qActorDeclaration());
            }

            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitAnswer(QactorProgParser.AnswerContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            //Trace("name", context.name.Text, context.Depth());

            if (context.request() != null)
            {
                this.VisitRequest(context.request());
            }
            if (context.reply() != null)
            {
                this.VisitReply(context.reply());
            }
            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitReplyReq(QactorProgParser.ReplyReqContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.request() != null)
            {
                foreach (var item in context.request())
                {
                    this.VisitRequest(item);
                }
            }
            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitDelay(QactorProgParser.DelayContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.delayInt() != null)
            {
                this.VisitDelayInt(context.delayInt());
            }
            if (context.delayVar() != null)
            {
                this.VisitDelayVar(context.delayVar());
            }
            if (context.delayVref() != null)
            {
                this.VisitDelayVref(context.delayVref());
            }
            if (context.delaySol() != null)
            {
                this.VisitDelaySol(context.delaySol());
            }
            return ReturnValue;
        }

        public override object VisitDelayVar(QactorProgParser.DelayVarContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.variableX() != null)
            {
                this.VisitVariableX(context.variableX());
            }
            return ReturnValue;
        }

        public override object VisitDelayVref(QactorProgParser.DelayVrefContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.varRef() != null)
            {
                this.VisitVarRef(context.varRef());
            }
            return ReturnValue;
        }

        public override object VisitDelaySol(QactorProgParser.DelaySolContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.varSolRef() != null)
            {
                this.VisitVarSolRef(context.varSolRef());
            }
            return ReturnValue;
        }

        public override object VisitMsgCond(QactorProgParser.MsgCondContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.messageP() != null)
            {
                this.VisitMessageP(context.messageP());
            }

            if (context.stateAction() != null)
            {
                foreach (var item in context.stateAction())
                {
                    this.VisitStateAction(item);
                }
            }
            if (context.noMsgCond() != null)
            {
                this.VisitNoMsgCond(context.noMsgCond());
            }
            if (context.pHead() != null)
            {
                this.VisitPHead(context.pHead());
            }
            return ReturnValue;
        }

        public override object VisitUpdateResource(QactorProgParser.UpdateResourceContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.anyAction() != null)
            {
                this.VisitAnyAction(context.anyAction());
            }
            return ReturnValue;
        }

        public override object VisitNoMsgCond(QactorProgParser.NoMsgCondContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.stateAction() != null)
            {
                foreach (var item in context.stateAction())
                {
                    this.VisitStateAction(item);
                }
            }
            return ReturnValue;
        }

        public override object VisitQualifiedName(QactorProgParser.QualifiedNameContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            
            return ReturnValue;
        }

        public override object VisitCodeRun(QactorProgParser.CodeRunContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.codeRunActor() != null)
            {
                this.VisitCodeRunActor(context.codeRunActor());
            }
            if (context.codeRunSimple() != null)
            {
                this.VisitCodeRunSimple(context.codeRunSimple());
            }
            return ReturnValue;
        }

        public override object VisitCodeRunActor(QactorProgParser.CodeRunActorContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.qualifiedName() != null)
            {
                this.VisitQualifiedName(context.qualifiedName());
            }
            return ReturnValue;
        }

        public override object VisitCodeRunSimple(QactorProgParser.CodeRunSimpleContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.qualifiedName() != null)
            {
                this.VisitQualifiedName(context.qualifiedName());
            }

            foreach (var item in context._args)
            {
                this.VisitPHead(item);
            }

            if (context.virgolaPHead() != null)
            {
                foreach (var item in context.virgolaPHead())
                {
                    this.VisitVirgolaPHead(item);
                }
            }
            return ReturnValue;
        }

        public override object VisitTransitionX(QactorProgParser.TransitionXContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.emptyTransition() != null)
            {
                this.VisitEmptyTransition(context.emptyTransition());
            }
            if (context.nonEmptyTransition() != null)
            {
                this.VisitNonEmptyTransition(context.nonEmptyTransition());
            }
            return ReturnValue;
        }

        public override object VisitEmptyTransition(QactorProgParser.EmptyTransitionContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.state() != null)
            {
                foreach (var item in context.state())
                {
                    this.VisitState(item);
                }
            }
            if (context.anyAction() != null)
            {
                this.VisitAnyAction(context.anyAction());
            }
            return ReturnValue;
        }

        public override object VisitNonEmptyTransition(QactorProgParser.NonEmptyTransitionContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.emptyTransition() != null)
            {
                this.VisitEmptyTransition(context.emptyTransition());
            }
            if (context.timeout() != null)
            {
                this.VisitTimeout(context.timeout());
            }
            if (context.inputTransition() != null)
            {
                foreach (var item in context.inputTransition())
                {
                    this.VisitInputTransition(item);
                }
            }
            return ReturnValue;
        }

        public override object VisitTimeout(QactorProgParser.TimeoutContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.timeoutInt() != null)
            {
                this.VisitTimeoutInt(context.timeoutInt());
            }
            if (context.timeoutVar() != null)
            {
                this.VisitTimeoutVar(context.timeoutVar());
            }
            if (context.timeoutVarRef() != null)
            {
                this.VisitTimeoutVarRef(context.timeoutVarRef());
            }
            if (context.timeoutSol() != null)
            {
                this.VisitTimeoutSol(context.timeoutSol());
            }
            return ReturnValue;
        }

        public override object VisitTimeoutInt(QactorProgParser.TimeoutIntContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.state() != null)
            {
                this.VisitState(context.state());
            }
            return ReturnValue;
        }

        public override object VisitTimeoutVar(QactorProgParser.TimeoutVarContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.state() != null)
            {
                this.VisitState(context.state());
            }
            if (context.variableX() != null)
            {
                this.VisitVariableX(context.variableX());
            }
            return ReturnValue;
        }

        public override object VisitTimeoutVarRef(QactorProgParser.TimeoutVarRefContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.state() != null)
            {
                this.VisitState(context.state());
            }
            if (context.varRef() != null)
            {
                this.VisitVarRef(context.varRef());
            }
            return ReturnValue;
        }

        public override object VisitTimeoutSol(QactorProgParser.TimeoutSolContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.state() != null)
            {
                this.VisitState(context.state());
            }
            if (context.varSolRef() != null)
            {
                this.VisitVarSolRef(context.varSolRef());
            }
            return ReturnValue;
        }

        public override object VisitInputTransition(QactorProgParser.InputTransitionContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.eventTransSwitch() != null)
            {
                this.VisitEventTransSwitch(context.eventTransSwitch());
            }
            if (context.msgTransSwitch() != null)
            {
                this.VisitMsgTransSwitch(context.msgTransSwitch());
            }
            if (context.requestTransSwitch() != null)
            {
                this.VisitRequestTransSwitch(context.requestTransSwitch());
            }
            if (context.replyTransSwitch() != null)
            {
                this.VisitReplyTransSwitch(context.replyTransSwitch());
            }
            return ReturnValue;
        }

        public override object VisitEventTransSwitch(QactorProgParser.EventTransSwitchContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.@event() != null)
            {
                this.VisitEvent(context.@event());
            }
            if (context.state() != null)
            {
                this.VisitState(context.state());
            }
            if (context.anyAction() != null)
            {
                this.VisitAnyAction(context.anyAction());
            }
            return ReturnValue;
        }

        public override object VisitMsgTransSwitch(QactorProgParser.MsgTransSwitchContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.dispatch() != null)
            {
                this.VisitDispatch(context.dispatch());
            }
            if (context.state() != null)
            {
                this.VisitState(context.state());
            }
            if (context.anyAction() != null)
            {
                this.VisitAnyAction(context.anyAction());
            }
            return ReturnValue;
        }

        public override object VisitRequestTransSwitch(QactorProgParser.RequestTransSwitchContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.request() != null)
            {
                this.VisitRequest(context.request());
            }
            if (context.state() != null)
            {
                this.VisitState(context.state());
            }
            if (context.anyAction() != null)
            {
                this.VisitAnyAction(context.anyAction());
            }
            return ReturnValue;
        }

        public override object VisitReplyTransSwitch(QactorProgParser.ReplyTransSwitchContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.reply() != null)
            {
                this.VisitReply(context.reply());
            }
            if (context.state() != null)
            {
                this.VisitState(context.state());
            }
            if (context.anyAction() != null)
            {
                this.VisitAnyAction(context.anyAction());
            }
            return ReturnValue;
        }

        public override object VisitPHead(QactorProgParser.PHeadContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.pAtom() != null)
            {
                this.VisitPAtom(context.pAtom());
            }
            if (context.pStructRef() != null)
            {
                this.VisitPStructRef(context.pStructRef());
            }
            if (context.pStruct() != null)
            {
                this.VisitPStruct(context.pStruct());
            }
            return ReturnValue;
        }

        public override object VisitPAtom(QactorProgParser.PAtomContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());

            if (context.pAtomString() != null)
            {
                this.VisitPAtomString(context.pAtomString());
            }
            if (context.pAtomic() != null)
            {
                this.VisitPAtomic(context.pAtomic());
            }
            if (context.pAtomNum() != null)
            {
                this.VisitPAtomNum(context.pAtomNum());
            }
            if (context.variableX() != null)
            {
                this.VisitVariableX(context.variableX());
            }
            if (context.varRef() != null)
            {
                this.VisitVarRef(context.varRef());
            }
            if (context.varRefInStr() != null)
            {
                this.VisitVarRefInStr(context.varRefInStr());
            }
            if (context.varSolRef() != null)
            {
                this.VisitVarSolRef(context.varSolRef());
            }
            return ReturnValue;
        }

        public override object VisitPStructRef(QactorProgParser.PStructRefContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.pStruct() != null)
            {
                this.VisitPStruct(context.pStruct());
            }
            return ReturnValue;
        }

        public override object VisitPStruct(QactorProgParser.PStructContext context)
        {
            List<String> ReturnValue = new List<String>(); Trace(context.Depth());
            if (context.pHead() != null)
            {
                foreach (var item in context.pHead())
                {
                    this.VisitPHead(item);
                }
            }
            return ReturnValue;
        }
    }
}