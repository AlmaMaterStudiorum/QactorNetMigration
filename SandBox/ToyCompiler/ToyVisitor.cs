using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler
{
    public class ToyVisitor : ToyParserBaseVisitor<object>
    {

        private int _Deep = 0;
        private const int CHARFORTAB = 4;

        private String GetCurrentIdent()
        {
            return new String(' ', _Deep*CHARFORTAB);
        }

        private void AddIdent()
        {
            _Deep +=1;
        }

        private void SubIdent()
        {
            _Deep -=1; ;
        }

        private void Next(ParserRuleContext context)
        {
            for (int i = 0; i < context.children.Count; i++)
            {
                this.AddIdent();
                this.Visit(context.children[i]);
                this.SubIdent();
            }
        }
        public override object VisitExpr(ToyParser.ExprContext context)
        {
            List<String> ReturnValue = new List<String>();

            Debug.WriteLine($"{this.GetCurrentIdent()}ExprContext : {context.GetText()} ");

            return ReturnValue;
        }

        public override object VisitParse(ToyParser.ParseContext context)
        {
            List<String> ReturnValue = new List<String>();

            Debug.WriteLine($"{this.GetCurrentIdent()}ParseContext : {context.GetText()} ");

            Next(context);

            return ReturnValue;
        }
        public override object VisitNumberExpr( ToyParser.NumberExprContext context)
        { 
            List<String> ReturnValue = new List<String>();

            Debug.WriteLine($"{this.GetCurrentIdent()}NumberExpr : {context.GetText()} ");

            Next(context);

            return ReturnValue;
        }

        public override object VisitPlusExpr(ToyParser.PlusExprContext context)
        {
            List<String> ReturnValue = new List<String>();

            Debug.WriteLine($"{this.GetCurrentIdent()}PlusExpr : {context.GetText()} ");

            Next(context);

            return ReturnValue;
        }

        public override object VisitSubExpr(ToyParser.SubExprContext context)
        {
            List<String> ReturnValue = new List<String>();

            Debug.WriteLine($"{this.GetCurrentIdent()}SubExpr : {context.GetText()} ");

            Next(context);

            return ReturnValue;
        }

        public override object VisitMultExpr(ToyParser.MultExprContext context)
        {
            List<String> ReturnValue = new List<String>();

            Debug.WriteLine($"{this.GetCurrentIdent()}MultExpr : {context.GetText()} ");
            
            Next(context);

            return ReturnValue;
        }

        public override object VisitDivExpr(ToyParser.DivExprContext context)
        {
            List<String> ReturnValue = new List<String>();

            Debug.WriteLine($"{this.GetCurrentIdent()}DivExpr : {context.GetText()} ");

            Next(context);

            return ReturnValue;
        }

        public override object VisitNestedExpr(ToyParser.NestedExprContext context)
        {
            List<String> ReturnValue = new List<String>();

            Debug.WriteLine($"{this.GetCurrentIdent()}NestedExpr : {context.GetText()} ");

            Next(context);

            return ReturnValue;
        }

        public override object VisitUnaryExpr(ToyParser.UnaryExprContext context)
        {
            List<String> ReturnValue = new List<String>();

            Debug.WriteLine($"{this.GetCurrentIdent()}UnaryExpr : {context.GetText()} ");

            Next(context);

            return ReturnValue;
        }

    }
}
