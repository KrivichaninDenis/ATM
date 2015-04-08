using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
   public class Program
    {       
        static void Main(string[] args)
        {
            Bank ATM = new Bank();
            LoadCassete LoadCassete = new LoadCassete();
            Algorithm AlgToGiveMoney = new Algorithm();
            StateOpeartion State = StateOpeartion.AllOk ;
            ATM.ReadCassete(LoadCassete,State);
            
            State = LoadCassete.CheckState();
            if (State == StateOpeartion.AllOk)
            {
                ATM.GiveClientMoney(AlgToGiveMoney, State);
            }
        }
    } 
}
