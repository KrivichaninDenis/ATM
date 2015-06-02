using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class ConvertMoney
    {
        public string  ConvertToString(List<Money> List)
        {
            var returnstring = string.Empty;
            returnstring += "\n";
            foreach (var m in List)
            {
                if (m.MoneyCount != 0)
                {
                    returnstring += "Money Value: " + m.MoneyValue + " Money Count: " + m.MoneyCount + "\n";
                }
            }
            return returnstring;
        }
    }
}
