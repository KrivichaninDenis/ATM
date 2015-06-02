using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank
{
    public class Statistics
    {
        public DateTime WhenLoad = new DateTime();
        public int MaxValue;       
        public List<StatsElement> TimeUsing = new List<StatsElement>();
        public static ConvertMoney Converter=new ConvertMoney(); 
        public Statistics AddStats(Algorithm algToGiveMoney, int money,Statistics stats)
        {
            StatsElement statEl = new StatsElement
            {
                Date = DateTime.UtcNow,
                MoneyCombination =Converter.ConvertToString(algToGiveMoney.AllgiveMoney),
                Sum = money
            };
            stats.TimeUsing.Add(statEl);
            return stats;
        }

        public override string ToString()
        {
            string result=string.Empty;
            result +=WhenLoad + "\nMax Value: "+MaxValue + "\n";           
            return TimeUsing.Aggregate(result, (current, stat) => current + ("\n"+stat.Date + "\nMoney: " + stat.Sum + " \nCombination: " + stat.MoneyCombination));
        }
    }
}
