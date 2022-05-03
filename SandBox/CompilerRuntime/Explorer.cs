using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace CompilerRuntime
{
    public class Explorer
    {
        private int _Deep = 0;
        private const int CHARFORTAB = 4;

        public String GetCurrentIdent()
        {
            return GetCurrentIdent(_Deep);
        }

        public String GetCurrentIdent(int deep)
        {
            return new String(' ', deep*CHARFORTAB);
        }

        public void AddIdent()
        {
            _Deep +=1;
        }

        public void SubIdent()
        {
            _Deep -=1; ;
        }

        public void Next(IParseTreeVisitor<object> visitor, ParserRuleContext context)
        {
            if(context != null)
            {
                for (int i = 0; i < context.children.Count; i++)
                {
                    this.AddIdent();
                    visitor.Visit(context.children[i]);
                    this.SubIdent();
                }
            }
        }
    }
}