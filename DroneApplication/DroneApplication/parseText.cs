using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DroneApplication
{
   public class parseText
    {
       string dataToParse;

       public parseText(string vectorList)
       {
        dataToParse = vectorList.Substring(2, vectorList.Length - 2);
       }

       public List<string> removeUselessCrap()
       {
           List<string> usefulInfor =
               Regex.Matches(this.dataToParse.Replace(Environment.NewLine, string.Empty), @"\[([^]]*)\]")
               .Cast<Match>().Select(x => x.Groups[1].Value).ToList();
           return usefulInfor;
       }
    }
}
