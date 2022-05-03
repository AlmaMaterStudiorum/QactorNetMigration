using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using QactorCompiler;

namespace AntlrCSharp
{
    class Program
    {
        private static void Main(string[] args)
        {
            Qactor();
        }

        private void Speak()
        {
            try
            {
                string input = "";
                StringBuilder text = new StringBuilder();
                Console.WriteLine("Input the chat.");

                // to type the EOF character and end the input: use CTRL+D, then press <enter>
                while ((input = Console.ReadLine()) != "\u0004")
                {
                    text.AppendLine(input);
                }

                AntlrInputStream inputStream = new AntlrInputStream(text.ToString());
                SpeakLexer speakLexer = new SpeakLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
                SpeakParser speakParser = new SpeakParser(commonTokenStream);

                SpeakParser.ChatContext chatContext = speakParser.chat();
                BasicSpeakVisitor visitor = new BasicSpeakVisitor();
                visitor.Visit(chatContext);

                foreach (var line in visitor.Lines)
                {
                    Console.WriteLine("{0} has said {1}", line.Person, line.Text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
#if true
        static private void Qactor()
        {
            try
            {
                Console.WriteLine("Go Qactor");

                // to type the EOF character and end the input: use CTRL+D, then press <enter>

                String AllText = File.ReadAllText(@"D:\Sviluppo\Unibo\ISS\issLab2021\it.unibo.demoqak21\src\demo0.qak");

                //QactorInputStream 
                AntlrInputStream inputStream = new AntlrInputStream(AllText);
                QactorLexer qactorLexer = new QactorLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(qactorLexer);
                QactorParser qactorParser = new QactorParser(commonTokenStream);


                QactorParser.QactorSystemSpecContext qactorSystemSpecContext = qactorParser.qactorSystemSpec();

                QactorVisitor visitor = new QactorVisitor();
                visitor.VisitQactorSystemSpec(qactorSystemSpecContext);

                //foreach (var line in visitor.Lines)
                //{
                //    Console.WriteLine("{0} has said {1}", line.Person, line.Text);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
#endif
    }
}
