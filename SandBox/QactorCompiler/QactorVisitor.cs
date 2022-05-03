using CompilerRuntime;
using CreateQactor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QactorCompiler
{
    public class QactorVisitor : QactorParserBaseVisitor<object>
    {
        public Explorer _Explorer;

        public QactorVisitor(Explorer explorer)
        {
            _Explorer = explorer;
        }

        private String GetCurrentIdent()
        {
            return this._Explorer.GetCurrentIdent();
        }

        public override object VisitQactorSystemSpec(QactorParser.QactorSystemSpecContext context)
        {
            List<String> ReturnValue = new List<String>();


#if false

            this.Visit(context.systemSpec());

            if(context.brokerSpec() != null)
            {
                this.Visit(context.brokerSpec());
            }


            QactorParser.MessageSpecContext[] mscs = context.messageSpec();

            foreach (var item in mscs)
            {
                this.Visit(item);
            }

            this.VisitMessageSpec(context.messageSpec());
                
            QactorParser.ContexSpecContext[] cscs = context.contexSpec();

            foreach (var item in cscs)
            {
                this.Visit(item);
            }

            QactorParser.ActorSpecContext[] ascs = context.actorSpec();

            foreach (var item in ascs)
            {
                this.Visit(item);
            }
#endif           

            //_Explorer.Next(this,context);

            this.VisitChildren(context);


            return ReturnValue;
        }

        public override object VisitSystemSpec(QactorParser.SystemSpecContext context)
        {
            List<String> ReturnValue = new List<String>();



            Debug.WriteLine($"{this.GetCurrentIdent()}{MethodBase.GetCurrentMethod().Name} : {context.name}");
            //_Explorer.Next(this, context);

            return ReturnValue;
        }

        public override object VisitBrokerSpec(QactorParser.BrokerSpecContext context)
        {
            List<String> ReturnValue = new List<String>();


            Debug.WriteLine($"{this.GetCurrentIdent()}{MethodBase.GetCurrentMethod().Name} : {context.brokerHost}  {context.brokerPort} ");

            return ReturnValue;
        }

        public object VisitMessageSpec(QactorParser.MessageSpecContext[] contexts)
        {
            List<String> ReturnValue = new List<String>();


            foreach (var item in contexts)
            {
                VisitMessageSpec(item);
            }

            return ReturnValue;
        }

        public override object VisitMessageSpec(QactorParser.MessageSpecContext context)
        {
            List<String> ReturnValue = new List<String>();


            Debug.WriteLine($"{this.GetCurrentIdent()}{MethodBase.GetCurrentMethod().Name} : Empty");

            //this._Explorer.Next(this,context);

            return ReturnValue;
        }

        public override object VisitMessageP(QactorParser.MessagePContext context)
        {
            List<String> ReturnValue = new List<String>();


            Debug.WriteLine($"{this.GetCurrentIdent()}{MethodBase.GetCurrentMethod().Name} : Empty");


            this.VisitOutOnlyMessage(context.outOnlyMessage());
            this.VisitOutInMessage(context.outInMessage());



            return ReturnValue;
        }
        


        public override object VisitOutOnlyMessage(QactorParser.OutOnlyMessageContext context)
        {
            List<String> ReturnValue = new List<String>();


            Debug.WriteLine($"{this.GetCurrentIdent()}{MethodBase.GetCurrentMethod().Name} : Empty");

            //this._Explorer.Next(this, context);

            this.Visit(context.dispatch());

            return ReturnValue;
        }

        public override object VisitOutInMessage(QactorParser.OutInMessageContext context)
        {
            List<String> ReturnValue = new List<String>();


            Debug.WriteLine($"{this.GetCurrentIdent()}{MethodBase.GetCurrentMethod().Name} : Empty");

            //this._Explorer.Next(this, context);

            return ReturnValue;
        }

        public override object VisitDispatch(QactorParser.DispatchContext context)
        {
            List<String> ReturnValue = new List<String>();


            Debug.WriteLine($"{this.GetCurrentIdent()}{MethodBase.GetCurrentMethod().Name} : {context.name}  {context.msg}");

            return ReturnValue;
        }
    }
}
