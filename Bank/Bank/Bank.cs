using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
   public class Bank
    {
       List<Money> AllCassete = new List<Money>();

       
       
       public void ReadCassete(LoadCassete LoadCs, StateOpeartion State)
       {           
           this.AllCassete = LoadCs.ReadAllMoney(this.AllCassete, State);
           
       }

       public void GiveClientMoney(Algorithm AlgToGive,StateOpeartion State)
       {
           AlgToGive.GiveMoney(this.AllCassete, State);

       }
    }
}
