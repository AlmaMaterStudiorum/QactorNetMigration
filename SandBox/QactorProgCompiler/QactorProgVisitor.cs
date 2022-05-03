using CompilerRuntime;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace QactorProgCompiler
{
    public partial class QactorProgVisitor : QactorProgParserBaseVisitor<object>
    {
        public Explorer _Explorer;
        private StringBuilder _sb = new StringBuilder();
        public QactorProgVisitor(Explorer explorer)
        {
            _Explorer = explorer;
        }
        private String GetCurrentIdent()
        {
            return this._Explorer.GetCurrentIdent();
        }

        private String GetCurrentIdent(int depth)
        {
            return this._Explorer.GetCurrentIdent(depth);
        }

        private void Trace( String variableName , String content, [CallerMemberName] String member = "")
        {
            Debug.WriteLine($"{this.GetCurrentIdent()}{member} : {variableName}={content}");
        }

        private void Trace(String variableName, String content,int depth, [CallerMemberName] String member = "")
        {
            Debug.WriteLine($"{this.GetCurrentIdent(depth)}{member} : {variableName}={content}");
        }

        private void Trace( int depth, [CallerMemberName] String member = "")
        {
            String message = $"{this.GetCurrentIdent(depth)}{member}";


            Debug.WriteLine(message);


            _sb.AppendLine($"System.Console.WriteLine(\"{message}\");");


        }

        public String CodeToExecute
        {
            get { return _sb.ToString(); }
        }

#if false
        public override object VisitQactorSystemSpec(QactorProgParser.QactorSystemSpecContext context)
        {
            List<String> ReturnValue = new List<String>();
            _Explorer.AddIdent();
            /*
                systemSpec: SYSTEMSTRING name = ID;
                brokerSpec: MqttBrokerSTRING brokerHost = STRING DuePuntiSTRING brokerPort = INT WS;
                messageSpec: message+=messageP+ ;
                contexSpec: context+=contextP+ ;
                actorSpec: actor+=qActorDeclaration+ ;
            */
            this.VisitSystemSpec(context.systemSpec());

            QactorProgParser.BrokerSpecContext broker = context.brokerSpec();
            if (broker != null)
            {
                this.VisitBrokerSpec(broker);
            }
           
            QactorProgParser.MessageSpecContext[] messages = context.messageSpec();

            foreach (var item in messages)
            {
                this.VisitMessageSpec(item);
            }

            QactorProgParser.ContexSpecContext[] contexts = context.contexSpec();

            foreach (var item in contexts)
            {
                this.VisitContexSpec(item);
            }

            QactorProgParser.ActorSpecContext[] actors = context.actorSpec();

            foreach (var item in actors)
            {
                this.VisitActorSpec(item);
            }

            _Explorer.SubIdent();
            return ReturnValue;
        }

        public override object VisitSystemSpec(QactorProgParser.SystemSpecContext context)
        {
            List<String> ReturnValue = new List<String>();


            Trace("name",context.name.Text);


            return ReturnValue;
        }

        public override object VisitBrokerSpec(QactorProgParser.BrokerSpecContext context)
        {
            List<String> ReturnValue = new List<String>();


            Trace("brokerHost", context.brokerHost.Text);
            Trace("brokerPort", context.brokerPort.Text);


            return ReturnValue;
        }

        public override object VisitMessageSpec(QactorProgParser.MessageSpecContext context)
        {
            List<String> ReturnValue = new List<String>();


            //Trace("brokerHost", context.brokerHost.Text);
            //Trace("brokerPort", context.brokerPort.Text);

            this.VisitMessageP(context.messageP());

            return ReturnValue;
        }

        public override object VisitMessageP(QactorProgParser.MessagePContext context)
        {
            List<String> ReturnValue = new List<String>();

            this.VisitOutOnlyMessage(context.outOnlyMessage());

            this.VisitOutInMessage(context.outInMessage());

            return ReturnValue;
        }

        public override object VisitOutOnlyMessage(QactorProgParser.OutOnlyMessageContext context)
        {
            List<String> ReturnValue = new List<String>();

            this.VisitDispatch(context.dispatch());

            this.VisitEvent(context.@event());

            this.VisitSignal(context.signal());

            this.VisitToken(context.token());


            return ReturnValue;
        }

        public override object VisitOutInMessage(QactorProgParser.OutInMessageContext context)
        {
            List<String> ReturnValue = new List<String>();

            this.VisitRequest(context.request());

            this.VisitReply(context.reply());

            this.VisitInvitation(context.invitation());

            return ReturnValue;
        }

        public override object VisitRequest(QactorProgParser.RequestContext context)
        {
            List<String> ReturnValue = new List<String>();

            Trace("name", context.name.Text);

            this.VisitPHead(context.pHead());

            return ReturnValue;
        }

        public override object VisitPHead(QactorProgParser.PHeadContext context)
        {
            List<String> ReturnValue = new List<String>();

            //Trace("name", context.pAtom.Text);

            //this.VisitPHead(context.pHead());

            return ReturnValue;
        }





        //public override object VisitPVariabile(QactorProgParser.PVariabileContext context)
        //{
        //    List<String> ReturnValue = new List<String>();

        //    Debug.WriteLine($"{this.GetCurrentIdent()}{MethodBase.GetCurrentMethod().Name} : {context.ID().GetText()}  ");
        //    Debug.WriteLine($"{this.GetCurrentIdent()}{MethodBase.GetCurrentMethod().Name} : {context.VARID().GetText()}  ");

        //    //_Explorer.Next(this, context);

        //    return ReturnValue;
        //}
#endif

    }
}