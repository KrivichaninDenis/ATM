using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
   public class Algorithm
    {
        public  void GiveMoney(List<Money> AllMoney, StateOpeartion State)
       {

         
               while (MaxMoney(AllMoney) != 0)
               {
                   int InputMoney=0;
                   Console.WriteLine("Input money: ");

                  try
                  {
                      InputMoney = int.Parse(Console.ReadLine());                       
                  }
                   catch
                  {
                       State = StateOpeartion.IncorrectInput;
                        Console.WriteLine("Exception "+State.ToString()+"\n");
                        break;
                  }

                  if (InputMoney > MaxMoney(AllMoney))
                  {
                      State = StateOpeartion.WantMoreThanHave;
               Console.WriteLine("Exception " + State.ToString() + "\n");
                      
                  }
                   else
                   {
                       List<int> AllgiveMoney = new List<int>();


                       int CountOfmoney = 0;
                       int sum = 0;
                       for (int i = 0; i < AllMoney.Count; i++)
                       {
                           while (
                                   InputMoney >= AllMoney[i].MoneyValue &&
                                   (InputMoney - AllMoney[i].MoneyValue == 0 || InputMoney - AllMoney[i].MoneyValue >= AllMoney[AllMoney.Count - 1].MoneyValue)
                                  )
                           {
                               if (AllMoney[i].MoneyCount > 0)
                               {
                                   AllMoney[i].MoneyCount--;
                                   AllgiveMoney.Add(AllMoney[i].MoneyValue);
                                   CountOfmoney++;
                                   InputMoney -= AllMoney[i].MoneyValue;
                                   sum += AllMoney[i].MoneyValue;

                               }
                               else { break; }
                           }
                       }


                       if (InputMoney == 0 /*&& CountOfmoney <= 15*/)
                       {
                           State = StateOpeartion.AllOk;
                           Console.WriteLine(State.ToString()+":Successfully issued:)\n");

                       }
                       else
                       {
                           State = StateOpeartion.CanNotGiveThisCombination;
                           Console.WriteLine("Exception " + State.ToString() + "\n");
                           ReturnMoney(AllgiveMoney, AllMoney);
                       }
                   }
               }                     
               State = StateOpeartion.NoMoneyToGive;              
               Console.WriteLine("Exception "+State.ToString()+"\n");                       
        }
            
        public  void ReturnMoney(List<int> ReturnList,List<Money> AllMoney)
        {
            foreach (int Value in ReturnList)
            {
                int index = AllMoney.FindIndex(m => m.MoneyValue == Value);
                AllMoney[index].MoneyCount++;
            }
        }

        public  int MaxMoney(List<Money> AllMoney)
        {
            int MaxMoney = 0;
            foreach (Money M1 in AllMoney)
            {
                MaxMoney += M1.MoneyValue * M1.MoneyCount;
            }
            //Console.WriteLine("\nMaxMoney: " + MaxMoney + "\n");
            return MaxMoney;
        }

}
}
