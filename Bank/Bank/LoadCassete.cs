using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Bank
{
    public class LoadCassete
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(LoadCassete));
        StateOpeartion State;
        public List<Money> ReadAllMoney(List<Money> AllMoney, StateOpeartion StateBank)
        {
            State = StateBank;
            System.IO.StreamReader file = new System.IO.StreamReader("C:/Users/Кривичанин/Documents/Visual Studio 2012/Projects/Bank/MoneyFile.txt");

            string Element = string.Empty;
            try
            {
                
                while ((Element = file.ReadLine()) != null)
                {

                    string[] split = Element.Split(new Char[] { ' ', '\t' });

                    Money M1 = new Money();

                    M1.MoneyValue = int.Parse(split[0]);
                    M1.MoneyCount = int.Parse(split[1]);

                    AllMoney.Add(M1);
                    Element = string.Empty;
                }
                AllMoney.Sort((A, B) => B.MoneyValue.CompareTo(A.MoneyValue));
            }
            catch
            {
                State = StateOpeartion.CasseteProblem;
                Console.WriteLine("Exception " + State.ToString() + "\n");
                log.Fatal("Exception " + State.ToString() + "\n");
            }
            Output(AllMoney, State);
            
            return AllMoney;
        }

        public void Output(List<Money> AllMoney, StateOpeartion State)
        {
            if (CheckState()!=StateOpeartion.AllOk)
            {
                State = StateOpeartion.NoMoneyToShow;
                Console.WriteLine("Exception " + State.ToString() + "\n");
                log.Error("Exception " + State.ToString() + "\n");
            }
            else
                foreach (Money M1 in AllMoney) 
                {
                    Console.WriteLine("Money Value: " + M1.MoneyValue + " Money Count: " + M1.MoneyCount + "\n");
                }
        }
        public StateOpeartion CheckState()
        {
            return State;
        }
    }
}
    
