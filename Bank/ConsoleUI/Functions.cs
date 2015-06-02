using System;
using System.Collections.Generic;
using Bank;
using log4net;

namespace ConsoleUI
{
    public class Functions
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof (Functions));
        public static Bank.Bank Atm = new Bank.Bank();
        public static LoadCassete LoadCassete = new LoadCassete();
        public static Algorithm AlgToGiveMoney = new Algorithm();
        public static StateOpeartion State = StateOpeartion.AllOk;
        public static OutputInformation OutInf = new OutputInformation();
        public  static ConvertMoney Convertret=new ConvertMoney();

        public static void OutMoneyOnScreen(List<Money> list)
        {
            if (list.Count == 0)
            {
                State = StateOpeartion.NoMoneyToGive;
                Console.WriteLine(@"Exception: No money in ATM");
                Log.Error("Exception " + State + "\n");
            }
            else
            {
                foreach (var m1 in list)
                {
                    Console.WriteLine("Money Value: " + m1.MoneyValue + " Money Count: " + m1.MoneyCount + "\n");
                }
            }
        }


        public static void MakeFunction(string com)
        {
            if (com == "Help")
            {
                Log.Info("Use HELP");
                Console.ForegroundColor=ConsoleColor.Red;                
                Console.WriteLine("-------All Functions: \n"); 
                Console.ForegroundColor = ConsoleColor.Yellow;                
                Console.WriteLine(" Help- See program function\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" Load- Load cassette\n");
                Console.WriteLine(" Clear- Clear cassette\n");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" Show- Show money list\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" Give- To give money\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Exit- Close program\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (com == "Exit")
            {
                Log.Info("End Bank Process");
                Environment.Exit(0);                
            }
            if  (com == "Give")
            {
                
                Console.WriteLine(@"Input money: ");
                var s = Console.ReadLine();
                try
                {
                    if (s != null)
                    {
                        var inputMoney=Int32.Parse(s) ;
                        var copy=inputMoney ;                    
                        if (AlgToGiveMoney.MaxMoney(Atm.AllCassete) == 0)
                        {
                            State = StateOpeartion.NoMoneyToGive;
                            Console.WriteLine(@"Exception " + State);
                            Log.Error("Exception " + State + "\n");
                        }
                        else
                        {
                            if (AlgToGiveMoney.MaxMoney(Atm.GiveClientMoney(AlgToGiveMoney, out State, inputMoney, OutInf)) == copy)
                            {
                                State = StateOpeartion.AllOk;
                                Console.WriteLine(State + " Successfully issued: " + copy);
                                Log.Info("Successfully issued: " + copy);
                                Console.WriteLine(Convertret.ConvertToString(AlgToGiveMoney.AllgiveMoney));
                                AlgToGiveMoney.AllgiveMoney.Clear();
                            }
                            else
                            {
                                if (State == StateOpeartion.WantMoreThanHave)
                                {
                                    Console.WriteLine("Error: " + State + " (" + copy + ")");
                                    Log.Error(" Error: " + State + " (" + copy + ")");
                                    AlgToGiveMoney.AllgiveMoney.Clear();
                                }
                                else
                                {
                                    State = StateOpeartion.CanNotGiveThisCombination;
                                    Console.WriteLine("Error: " + State + " (" + copy + ")");
                                    Log.Error(" Error: " + State + " (" + copy + ")");
                                    AlgToGiveMoney.AllgiveMoney.Clear();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    State = StateOpeartion.IncorrectInput;
                    Console.WriteLine(@"Exception " + State);
                    Log.Error("Exception " + State + "\n");
                    AlgToGiveMoney.AllgiveMoney.Clear();
                }                
            
}

            if (com == "Show")
            {
                OutMoneyOnScreen(Atm.AllCassete);
            }
            if (com == "Clear")
            {
                Atm.AllCassete.Clear();
                Console.WriteLine(@"Cassette successfully clear  ");
                State = StateOpeartion.AllOk;
                Log.Info("Cassettes successfully cleared");
            }
            if (com == "Load")
            {
                Log.Debug("Start Bank process");
                Atm.AllCassete.Clear();
                Console.WriteLine("Input Path: ");
                var path = Console.ReadLine();

                try
                {
                    Atm.ReadCassete(LoadCassete, State, path);
                    Console.WriteLine("Cassette successfully read:)");
                }
                catch
                {
                    State = StateOpeartion.CasseteProblem;
                    Console.WriteLine(State+ " :(");
                    Log.Error("Exception " + State + "\n");
                }
            }           
        }
    }
    }


        
    

