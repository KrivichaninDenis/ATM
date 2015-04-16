using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;


namespace Bank
{
   public class Program
   {

       public static readonly ILog log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Debug("Start Bank Process");
            
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
            log.Debug("End Bank Process");
        }
    } 
}
