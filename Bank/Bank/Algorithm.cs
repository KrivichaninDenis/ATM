using System.Collections.Generic;
using System.Linq;
using log4net;

namespace Bank
{
   public class Algorithm 
    {
       public static readonly ILog Log = LogManager.GetLogger(typeof(Algorithm));

       public List<Money> AllgiveMoney= new List<Money>();
       public OutputInformation Outi;

       public  List<Money> GiveMoney(List<Money> allMoney,out StateOpeartion state,int inputMoney, OutputInformation outInf)
        {
            Outi = outInf;
            
            state=StateOpeartion.AllOk;            
            AllgiveMoney.Clear();            
            foreach (Money t in allMoney)
            {
                Money m=new Money();
                m.MoneyValue = t.MoneyValue;
                m.MoneyCount = 0;
                AllgiveMoney.Add(m);
            }
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
                                   (inputMoney - oneElement.MoneyValue == 0 ||
                                    inputMoney - oneElement.MoneyValue >= allMoney[allMoney.Count - 1].MoneyValue)
                                   )
                               {
                                   if (oneElement.MoneyCount > 0)
                                   {
                                       oneElement.MoneyCount--;
                                   }
                                   else break;                                   
                                   var element = oneElement;
                                   foreach (var m in AllgiveMoney.Where(m => m.MoneyValue == element.MoneyValue))
                                       {
                                           m.MoneyCount++;
                                       }                                     
                                       //countOfmoney++;
                                       inputMoney -= oneElement.MoneyValue;                                                          
                           }                          
                       }
                //
                       int count = allMoney.Count - 1;
                       if (inputMoney > AllgiveMoney[count].MoneyValue)
                       {
                           if (AllgiveMoney[count - 1].MoneyValue + inputMoney == AllgiveMoney[count].MoneyValue * 4)
                           {
                               int i;
                               for (i = count - 1; i >= 0; i--)
                                   if (AllgiveMoney[i].MoneyCount > 0)
                                   {
                                       AllgiveMoney[i].MoneyCount--;
                                       allMoney[i].MoneyCount++;
                                       break;
                                   }
                               if (i < count - 1)
                               {
                                   for (int g = i + 1; g <= count - 1; g++)
                                   {
                                       AllgiveMoney[g].MoneyCount++;
                                       allMoney[g].MoneyCount--;
                                   }
                               }
                               AllgiveMoney[count].MoneyCount += 4;
                               allMoney[count].MoneyCount -= 4;
                           }
                       }
                   }

            return AllgiveMoney;
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
