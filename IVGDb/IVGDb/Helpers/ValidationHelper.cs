using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVGDb.Helpers
{
    public class ValidationHelper
    {
        public static bool PasswordHashesEqual(string p1, string p2)
        {
            if (p1.Equals(p2))
                return true;
            if (p1 == null || p2 == null)
                return false;
            if (p1.Length != p2.Length)
                return false;
            for (int i = 0; i < p1.Length; i++)
                if (p1[i] != p2[i])
                    return false;

            return true;
        }
    }
}