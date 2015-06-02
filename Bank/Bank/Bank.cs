using System.Collections.Generic;
using System.IO;

namespace Bank
{
   public class Bank
    {
      public List<Money> AllCassete = new List<Money>();

       
       
       public void ReadCassete(LoadCassete loadCs, StateOpeartion state,string path)
       {
           
           switch (Path.GetExtension(path))
           {
               case ".txt":
                   {
                       AllCassete = loadCs.TxtReader(AllCassete, state,path);   
                       break;                      
                   }
               case ".csv":
                   {
                       AllCassete = loadCs.CsvReader(AllCassete, state, path);
                       break;
                   }
               case ".xml":
                   {
                       AllCassete = loadCs.XmlReader(AllCassete, state, path);
                       break;
                   }
               case ".json":
                   {
                       AllCassete = loadCs.JsonReader(AllCassete, state, path);
                       break;
                   }
               default:
               {
                  // MessageBox.Show("Select another format");
                   state=StateOpeartion.CasseteProblem;                   
                   break;
               }
                
           }
                  
       }

       public List<Money> GiveClientMoney(Algorithm algToGive,out StateOpeartion state, int inputMoney, OutputInformation outInf)
       {
         return  algToGive.GiveMoney(AllCassete, out state,inputMoney,outInf);
       }
    }
}
