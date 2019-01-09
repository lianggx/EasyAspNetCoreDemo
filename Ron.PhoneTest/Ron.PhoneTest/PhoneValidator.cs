using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ron.PhoneTest
{
    public class PhoneValidator
    {
        private static readonly Regex checktor = new Regex(@"^1\d{10}$");
        public IDictionary segment = null;

        public PhoneValidator(IDictionary segment)
        {
            this.segment = segment;
        }

        public bool IsPhone(ref string tel)
        {
            if (string.IsNullOrEmpty(tel))
            {
                return false;
            }

            tel = tel.Replace("+86-", "").Replace("+86", "").Replace("86-", "").Replace("-", "");
            if (!checktor.IsMatch(tel))
            {
                return false;
            }
            string s = tel.Substring(0, 3);
            if (segment.Count > 0 && !segment.Contains(s))
            {
                return false;
            }

            return true;
        }
    }
}
