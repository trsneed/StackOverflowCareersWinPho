using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowCareers.Model
{
    public class JoelTestResult
    {
        public JoelTestResult(KeyValuePair<string, bool> resPair)
        {
            Name = resPair.Key;
            Checked = resPair.Value;
        }
       public string Name { get; private set; }
       public bool Checked { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
