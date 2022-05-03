using Antlr4.Runtime;
using CreateQactor;
using QactorCompiler;
using QactorProgCompiler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToyCompiler;

namespace QactorSandBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnVisitQactorSystemSpec_Click(object sender, RoutedEventArgs e)
        {
            Qactor();
        }

        static private void Qactor()
        {
            try
            {
                Console.WriteLine("Go Qactor");

                // to type the EOF character and end the input: use CTRL+D, then press <enter>
                const String fullPath001 = @"D:\Sviluppo\Unibo\ISS\FinalTaskISS2021AutomatedCarParking\Sprint_2\Source\it.unibo.parkmanagerservice\src\parkManagerServiceModel.qak";
                const String fullPath002 = @"D:\Sviluppo\Unibo\ISS\issLab2021\it.unibo.demoqak21\src\demo0.qak";
                String AllText = File.ReadAllText(fullPath001);

                //QactorInputStream 
                AntlrInputStream inputStream = new AntlrInputStream(AllText);
                QactorLexer qactorLexer = new QactorLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(qactorLexer);
                QactorParser qactorParser = new QactorParser(commonTokenStream);


                QactorParser.QactorSystemSpecContext qactorSystemSpecContext = qactorParser.qactorSystemSpec();
                //QactorParser.BrokerSpecContext brokerSpecContextContext = qactorParser.brokerSpec();
             
                QactorVisitor visitor = new QactorVisitor(new CompilerRuntime.Explorer());

                visitor.Visit(qactorSystemSpecContext);

               
                //visitor.VisitBrokerSpec(brokerSpecContextContext);

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

        static private void Toy()
        {
            try
            {
                Console.WriteLine("Go Toy");

                // to type the EOF character and end the input: use CTRL+D, then press <enter>

                String AllText = "((1000) - (25/5)) + ((1 + 2) * (-3.14159265))";

                //QactorInputStream 
                AntlrInputStream inputStream = new AntlrInputStream(AllText);
                ToyLexer lexer = new ToyLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
                ToyParser parser = new ToyParser(commonTokenStream);
                //parser.BuildParseTree = true;

                
                ToyParser.ParseContext context = parser.parse();
                //QactorParser.BrokerSpecContext brokerSpecContextContext = qactorParser.brokerSpec();

                ToyVisitor visitor = new ToyVisitor();
                visitor.Visit(context);



                //visitor.VisitBrokerSpec(brokerSpecContextContext);

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

        static private void QactorProg()
        {
            try
            {
                Debug.WriteLine("Go QactorProg");

                // to type the EOF character and end the input: use CTRL+D, then press <enter>
                const String fullPath001 = @"D:\Sviluppo\Lab2\getting-started-with-antlr-in-csharp-master\Data\actortProg001.qak";
                const String fullPath002 = @"D:\Sviluppo\Unibo\ISS\FinalTaskISS2021AutomatedCarParking\Sprint_2\Source\it.unibo.parkmanagerservice\src\parkManagerServiceModel.qak";

                String AllText = File.ReadAllText(fullPath002);

                AntlrInputStream inputStream = new AntlrInputStream(AllText);
                QactorProgLexer lexer = new QactorProgLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
                QactorProgParser parser = new QactorProgParser(commonTokenStream);
                parser.BuildParseTree = true;
                QactorProgParser.ProgramContext  program = parser.program();


                QactorProgVisitor visitor = new QactorProgVisitor(new CompilerRuntime.Explorer());

                try
                {
                    visitor.Visit(program);
                }
                catch (Exception)
                {

                    String code = visitor.CodeToExecute;
                }

              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        private void btnToy_Click(object sender, RoutedEventArgs e)
        {
            Toy();
        }

        private void btnVisitQactorProgSystemSpec_Click(object sender, RoutedEventArgs e)
        {
            QactorProg();
        }

        private void CreateVisitor()
        {
            const String fullPath002 = @"D:\Sviluppo\Lab2\getting-started-with-antlr-in-csharp-master\QactorProgAntLr4\QactorProgParser.g4";
            String[] AllLine = File.ReadAllLines(fullPath002);

            Dictionary<String, String> MapRule = new Dictionary<String, String>();
            Dictionary<String, List<String>> AllRules = new Dictionary<String, List<String>>();
            HashSet<String> rules = new HashSet<String>();

            for (int iLine = 0; iLine < AllLine.Length; iLine++)
            {
                String line = AllLine[iLine];

                if (line.Contains(':') == true)
                {
                    String[] arr = line.Split(':');

                    MapRule.Add(arr[0].Trim(), arr[1]);
                }
            }

            foreach (KeyValuePair<String, String> source in MapRule)
            {
                String sourceRule = source.Key;
                String sourceRuleContent = source.Value;
                String sourceRuleContentTrim = sourceRuleContent.Trim();
                foreach (KeyValuePair<String, String> target in MapRule)
                {
                    String targetRule = target.Key;

                    if (sourceRuleContent.Contains(targetRule) == true)
                    {
                        if (AllRules.ContainsKey(sourceRule) == true)
                        {
                            // OK
                        }
                        else
                        {

  
                            AllRules.Add(sourceRule, new List<String>());
                        }
                        if (sourceRuleContentTrim.Contains("+=" + targetRule)== true)
                        {
                            AllRules[sourceRule].Add("+" + targetRule);
                        }
                        else
                        {
                            AllRules[sourceRule].Add(targetRule);
                        }
                        

                    }
                }
            }
            StringBuilder sb = new StringBuilder();

            //StringBuilder sb = new StringBuilder();
            //for (int iRight = 0; iRight < rule.Value.Count; iRight++)
            //{
            //    sb.AppendLine($"    {rule.Value[iRight]}");
            //}
            //Debug.WriteLine(rule.Key);
            //Debug.WriteLine(sb.ToString());

            Debug.WriteLine("using CompilerRuntime;");
            Debug.WriteLine("using System.Diagnostics;");
            Debug.WriteLine("using System.Reflection;");
            Debug.WriteLine("using System.Runtime.CompilerServices;");
            Debug.WriteLine("");
            Debug.WriteLine("namespace QactorProgCompiler");
            Debug.WriteLine("{");
            Debug.WriteLine("   public partial class QactorProgVisitor : QactorProgParserBaseVisitor<object>");
            Debug.WriteLine("   {");

            foreach (KeyValuePair<String, List<String>> rule in AllRules)
            {
                String sRule = UpperCaseFirstLetter(rule.Key);
                Debug.WriteLine($"      public override object Visit{sRule}(QactorProgParser.{sRule}Context context)");
                Debug.WriteLine("       {");
                Debug.WriteLine("           List<String> ReturnValue = new List<String>();");

                foreach (String child in rule.Value)
                {
                    String newChild;
                    if(child.StartsWith("+") ==true)
                    {
                        newChild = child.Substring(1);
                        String subRule = UpperCaseFirstLetter(newChild);
                        Debug.WriteLine($"          foreach (var item in context._{newChild}())");
                        Debug.WriteLine("           {");
                        Debug.WriteLine($"              this.Visit{subRule}(item);");
                        Debug.WriteLine("           }");
                    }
                    else
                    {
                        newChild = child;
                        String subRule = UpperCaseFirstLetter(newChild);
                        Debug.WriteLine($"          if (context.{newChild}() != null)");
                        Debug.WriteLine("           {");                           
                        Debug.WriteLine($"              this.Visit{subRule}(context.{newChild}());");
                        Debug.WriteLine("           }");
                    }
                }

                Debug.WriteLine("           return ReturnValue;");
                Debug.WriteLine("       }");
            }

            Debug.WriteLine("   }");

            Debug.WriteLine("}");

           
        }

        private String UpperCaseFirstLetter(String element)
        {
            return char.ToUpper(element[0]) + element.Substring(1);
        }

        private void btnCreateVisitor_Click(object sender, RoutedEventArgs e)
        {
            CreateVisitor();
        }
    }
}
