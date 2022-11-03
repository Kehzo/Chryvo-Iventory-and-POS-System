using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YvonnieStore_Beta_2_
{
    class checking
    {
        public static Boolean checkinput_number(String input)
        {
            if (!input.All(Char.IsDigit) || input == "")
            {
                return false;
            }
            return true;
        }

        public static Boolean checkinput_string(string input)
        {
            if (!input.All(Char.IsLetter) || input == "")
            {
                return false;
            }
            return true;
        }
    }
}
