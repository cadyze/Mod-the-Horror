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
        public int winPrizeAmt;
        public string failureText;
        public string failurePrize;
        public int losePrizeAmt;

        public EventOption(string optionDesc, string statTest, string successText, string successPrize, int winPrizeAmt,
            string failureText, string failurePrize, int losePrizeAmt) {

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
