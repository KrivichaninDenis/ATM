using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
   public class Program
    {       
       public static List<Money> AllMoney = new List<Money>();

       public static void ReadAllMoney()
       {

           System.IO.StreamReader file = new System.IO.StreamReader("C:/Users/Кривичанин/Documents/Visual Studio 2012/Projects/Bank/MoneyFile.txt");

           string Element = string.Empty;
           while ((Element = file.ReadLine()) != null)
           {

               string[] split = Element.Split(new Char[] { ' ', '\t' });

               Money M1 = new Money();

               M1.MoneyValue =int.Parse(split[0]);
               M1.MoneyCount = int.Parse(split[1]);

               AllMoney.Add(M1);
               Element = string.Empty;
           }
           AllMoney.Sort((A, B) => B.MoneyValue.CompareTo(A.MoneyValue));
       }

       public static int MaxMoney()
       {
           int MaxMoney = 0;
           foreach (Money M1 in AllMoney)
           {
               MaxMoney += M1.MoneyValue * M1.MoneyCount;
           }
           Console.WriteLine("\nMaxMoney: " + MaxMoney + "\n");
           return MaxMoney;
       }

       public static void ReturnMoney(List<int> ReturnList)
       {
           foreach (int Value in ReturnList)
           {
              int index= AllMoney.FindIndex(m => m.MoneyValue == Value);
              AllMoney[index].MoneyCount++;
           }
       }
       
       public static void Output()
        {        
            foreach (Money M1 in AllMoney)
            {
            
                Console.WriteLine("Money Value: "+M1.MoneyValue+" Money Count: "+M1.MoneyCount+"\n");
            }
        MaxMoney();
        }

       public static void GiveMoney()
       {
           int InputMoney;
           Console.WriteLine("Input money: ");
           InputMoney = int.Parse(Console.ReadLine());
           if (InputMoney > MaxMoney()) { Console.WriteLine("\nНет такой большой суммы!!!\n "); }
           else
           {
               List<int> AllgiveMoney = new List<int>();

               int CountOfmoney = 0;

               foreach (Money m1 in AllMoney)
               {
                   while (InputMoney >= m1.MoneyValue && ((InputMoney - m1.MoneyValue) >= AllMoney[AllMoney.Count - 1].MoneyValue) || InputMoney - m1.MoneyValue == 0)
                   {
                       if (m1.MoneyCount > 0)
                       {
                           m1.MoneyCount--;
                           AllgiveMoney.Add(m1.MoneyValue);
                           CountOfmoney++;
                           InputMoney -= m1.MoneyValue;

                       }
                       else { break; }
                   }
               }
               if (InputMoney == 0 && CountOfmoney <= 15)
               {
                   Console.WriteLine("Можем\n");
               }
               else
               {
                   Console.Write(" Не Можем\n");
                   ReturnMoney(AllgiveMoney);
               }
           }
       }
       
       static void Main(string[] args)
           {
               ReadAllMoney();
               Output();
               GiveMoney();
               Output();
           }
     }
    
}
