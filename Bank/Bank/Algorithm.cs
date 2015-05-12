using System.Collections.Generic;
using System.Linq;
using log4net;

namespace Bank
{
   public class Algorithm
    {
       public static readonly ILog Log = LogManager.GetLogger(typeof(Algorithm));
       public    List<int> AllgiveMoney = new List<int>();
        public  int GiveMoney(List<Money> allMoney,out StateOpeartion state,int inputMoney)
       {
            state=StateOpeartion.AllOk;
            var countOfmoney=0;            

            if (inputMoney > MaxMoney(allMoney) )
                  {
                        state = StateOpeartion.WantMoreThanHave;                                                
                  }
                   else
                   {
                       foreach (Money oneElement in allMoney)
                       {
                           while (
                               inputMoney >= oneElement.MoneyValue &&
                               (inputMoney - oneElement.MoneyValue == 0 || inputMoney - oneElement.MoneyValue >= allMoney[allMoney.Count - 1].MoneyValue)
                               )
                           {
                               if (oneElement.MoneyCount > 0)
                               {
                                   oneElement.MoneyCount--;
                                   AllgiveMoney.Add(oneElement.MoneyValue);
                                   countOfmoney++;
                                   inputMoney -= oneElement.MoneyValue;
                               }
                               else { break; }
                           }
                       }
                   }

            return inputMoney;
       }
            
        public  void ReturnMoney(List<int> returnList,List<Money> allMoney)
        {
            foreach (int value in returnList)
            {
                int index = allMoney.FindIndex(m => m.MoneyValue == value);
                allMoney[index].MoneyCount++;
            }
        }

        public  int MaxMoney(List<Money> allMoney)
        {
            return allMoney.Sum(m1 => m1.MoneyValue*m1.MoneyCount);
        }
    }
}
