using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod_the_Horror
{
    public class EventOption
    {
        public string optionDesc;
        public string statTest;
        public string successText;
        public string successPrize;
        public string winPrizeAmt;
        public string failureText;
        public string failurePrize;
        public string losePrizeAmt;

        public EventOption(string optionDesc, string statTest, string successText, string successPrize, string winPrizeAmt,
            string failureText, string failurePrize, string losePrizeAmt) {

            this.optionDesc = optionDesc;
            this.statTest = statTest;
            this.successText = successText;
            this.successPrize = successPrize;
            this.winPrizeAmt = winPrizeAmt;
            this.failureText = failureText;
            this.failurePrize = failurePrize;
            this.losePrizeAmt = losePrizeAmt;
        }
    }
}
