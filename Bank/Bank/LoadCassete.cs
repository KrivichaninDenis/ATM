using System.Collections.Generic;
using System.IO;
using log4net;

namespace Bank
{
    public class LoadCassete
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof(LoadCassete));
        StateOpeartion _state;
        public List<Money> TxtReader(List<Money> allMoney, StateOpeartion stateBank)
        {
            _state = stateBank;
            var file = new StreamReader("C:/Users/Кривичанин/Documents/Visual Studio 2012/Projects/Bank/MoneyFile.txt");

            try
            {
                string element;
                while ((element = file.ReadLine()) != null)
                {

                    var split = element.Split(' ', '\t');

                    var m1 = new Money
                    {
                        MoneyValue = int.Parse(split[0]),
                        MoneyCount = int.Parse(split[1])
                    };


                    allMoney.Add(m1);
                }
                allMoney.Sort((a, b) => b.MoneyValue.CompareTo(a.MoneyValue));
            }
            catch
            {
                _state = StateOpeartion.CasseteProblem;               
            }
           
            
            return allMoney;
        }

       
        public StateOpeartion CheckState()
        {
            return _state;
        }
    }
}
    
