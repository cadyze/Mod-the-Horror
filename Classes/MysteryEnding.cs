using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod_the_Horror.Classes
{
    public class MysteryEnding
    {
        string endingLetter = "";

        public string EndingLetter
        {
            get { return endingLetter; }
            set { endingLetter = value; }
        }

        public string pathToImage = "";
        public string endingTitle = "";
        public string pageA = "";
        public string pageB = "";
        public string pageC = "";
        public string pageD = "";

        public MysteryEnding(char endingLetter) {
            EndingLetter = endingLetter.ToString();
        }
    }
}
