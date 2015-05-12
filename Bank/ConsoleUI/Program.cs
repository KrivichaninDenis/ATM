using System;
using Bank;
using log4net;
using log4net.Config;

namespace ConsoleUI
{
    public class Program
    {

        public static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        public static void Main()
        {
            XmlConfigurator.Configure();
            Log.Debug("Start Bank Process");

            var atm = new Bank.Bank();
            var loadCassete = new LoadCassete();
            var algToGiveMoney = new Algorithm();
            var state = StateOpeartion.AllOk;
            atm.ReadCassete(loadCassete, state);

            state = loadCassete.CheckState();
            if (state == StateOpeartion.AllOk)
            {
                foreach (var m1 in atm.AllCassete)
                {
                    Console.WriteLine("Money Value: " + m1.MoneyValue + " Money Count: " + m1.MoneyCount + "\n");
                }
                while (algToGiveMoney.MaxMoney(atm.AllCassete) != 0)
                {
                    try
                    {
                        Console.WriteLine(@"Input money: ");
                        var s = Console.ReadLine();
                        if (string.IsNullOrEmpty(s)) continue;
                        var inputMoney = int.Parse(s);
                        var copyIputMoney = inputMoney;
                        if (atm.GiveClientMoney(algToGiveMoney, out state, inputMoney) == 0)
                        {
                            state = StateOpeartion.AllOk;
                            Console.WriteLine(state + " Successfully issued: " + copyIputMoney);
                            Log.Info("Successfully issued: "+copyIputMoney);
                        }
                        else
                        {
                            if (state == StateOpeartion.WantMoreThanHave)
                            {                                
                                Console.WriteLine("Error: " + state+" ("+copyIputMoney+")");
                                Log.Error(" Error: " + state + " (" + copyIputMoney + ")");
                            }
                            else
                            {
                                state = StateOpeartion.CanNotGiveThisCombination;
                                Console.WriteLine("Error: " + state + " (" + copyIputMoney + ")");
                                Log.Error(" Error: " + state + " (" + copyIputMoney + ")");
                            }                            
                        }
                    }
                    catch
                    {
                        state = StateOpeartion.IncorrectInput;
                        Console.WriteLine(@"Exception " + state);
                        Log.Error("Exception " + state + "\n");
                    }
                }
                state = StateOpeartion.NoMoneyToGive;
                Console.WriteLine(@"Exception " + state);
                Log.Error("Exception " + state + "\n");
            }
            else
            {
                state = StateOpeartion.CasseteProblem;
                Console.WriteLine(@"Exception " + state);
                Log.Error("Exception " + state + "\n");
            }
            Log.Debug("End Bank Process");
        }
    }
}
