using System.Collections.Generic;

namespace Bank
{
   public class Bank
    {
      public List<Money> AllCassete = new List<Money>();

       
       
       public void ReadCassete(LoadCassete loadCs, StateOpeartion state)
       {           
           AllCassete = loadCs.TxtReader(AllCassete, state);           
       }

       public int GiveClientMoney(Algorithm algToGive,out StateOpeartion state, int inputMoney)
       {
         return  algToGive.GiveMoney(AllCassete, out state,inputMoney);

       }
    }
}
