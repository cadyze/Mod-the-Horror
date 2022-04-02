using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod_the_Horror.Classes
{
    public class InvestigationTurn
    {
        public string longProgressNum;


        public string progressNum;
        public string LongProgressNum {
            get { return longProgressNum; }
            set { longProgressNum = value; }
        }

        public string location = "";
        public string precedingText = "";
        public string forcedEvent = "";

        public InvestigationTurn(string longProgressNum) {
            this.longProgressNum = longProgressNum;
            progressNum = longProgressNum.Substring(0, 3).ToLower();
        }
    }
}
