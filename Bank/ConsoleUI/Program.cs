using System;
using System.Linq;
using Bank;
using log4net;
using log4net.Config;

namespace ConsoleUI
{
    public class Program
    {

        public static readonly ILog Log = LogManager.GetLogger(typeof(Program));
        public static bool  Flag=true;


        public static void Main()
        {
            XmlConfigurator.Configure();
           

            string[] comands = {"Help", "Load","Give", "Clear", "Out","Exit","Localization","Show"};

            
            string path = "C:/Users/Кривичанин/Documents/Visual Studio 2012/Projects/Bank/MoneyFile.txt";
          

            Functions.State =StateOpeartion.AllOk;// Functions.LoadCassete.CheckState();
            if (Functions.State == StateOpeartion.AllOk)
            {
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor=ConsoleColor.Blue;
                Console.WriteLine(@"-------Input Help to see functional-------");
                Console.BackgroundColor = ConsoleColor.Black;
                while (Flag)
                {
                    try
                    {
                        Console.WriteLine(@"");
                        var command = Console.ReadLine();
                        if (!comands.Contains(command))
                        {
                            Console.WriteLine("Bad request!!!");                            
                        }
                        else
                        {
                            Functions.MakeFunction(command);
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }

        /*
                        
                        
        }*/

        
    }
}
