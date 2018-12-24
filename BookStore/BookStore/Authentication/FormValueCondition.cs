using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace BookStore.Authentication
{
    public class FormValueCondition
    {
        public bool IsOnlyNumber(string str)
        {
            Regex regex = new Regex("^[0-9]*$");
            return regex.IsMatch(str);
        }

        public bool IsOnlyNumberAndEG(string str)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]+$");
            return regex.IsMatch(str);
        }
    }
}