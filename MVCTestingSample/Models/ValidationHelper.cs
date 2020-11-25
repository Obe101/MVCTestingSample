using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVCTestingSample.Models
{
    public static class ValidationHelper
    {
        public static bool IsValidPrice(string price)
        {
            if (price == "")
            {
                return false;
            }
            Regex pattern = new Regex(@"^\$?\d{00,}\.?(\d{1,})?$");
            return pattern.IsMatch(price);
            //try
            //{
            //    Convert.ToDouble(price);
            //    return true;
            //}
            //catch (FormatException)
            //{

            //    return false;
            //}
        }
    }
}
