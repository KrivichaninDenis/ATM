using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using log4net;
using Newtonsoft.Json;


namespace Bank
{
    public class LoadCassete
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof(LoadCassete));
        public StateOpeartion State=StateOpeartion.CasseteProblem;

        public List<Money> TxtReader(List<Money> allMoney, StateOpeartion stateBank,string path)
        {
            State = stateBank;
            var file = new StreamReader(path);

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
                State = StateOpeartion.CasseteProblem;               
            }
           
            
            return allMoney;
        }

        public List<Money> CsvReader(List<Money> allMoney, StateOpeartion stateBank, string path)
        {
            State = stateBank;
            var file = new StreamReader(path);

            try
            {
                string element;
                while ((element = file.ReadLine()) != null)
                {

                    var split = element.Split(',', '\t');

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
                State = StateOpeartion.CasseteProblem;
            }


            return allMoney;
        }

        public List<Money> XmlReader(List<Money> allMoney, StateOpeartion stateBank, string path)
        {
            State = stateBank;                                 
            try
            {
               using (var reader = new StreamReader(path))
                {
                    var deserializer = new XmlSerializer(typeof(List<Money>), new XmlRootAttribute("xml"));
                    allMoney = (List<Money>)deserializer.Deserialize(reader);
                    
                }                
               allMoney.Sort((a, b) => b.MoneyValue.CompareTo(a.MoneyValue));               
            }
            catch
            {
                State = StateOpeartion.CasseteProblem;               
            }
            return allMoney;
        }

        public List<Money> JsonReader(List<Money> allMoney, StateOpeartion stateBank, string path)
        {
            State = stateBank;
            try
            {
                string json = File.ReadAllText(path);
                allMoney = JsonConvert.DeserializeObject<List<Money>>(json);               
            }
            
            catch
            {               
                State = StateOpeartion.CasseteProblem;
            }
            return allMoney;
        }

        public StateOpeartion CheckState()
        {
            return State;
        }
    }
}
    
