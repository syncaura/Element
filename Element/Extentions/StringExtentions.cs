using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Element.Extentions {
    public static class StringExtentions {

        /// <summary>
        /// Check to see if a string a number
        /// </summary>
        /// <param name="val">The value that we want to check to see if it's numeric</param>
        /// <returns>True: Is Numberic, False: Is not numeric</returns>
        public static bool IsNumeric(this string val) {
            double number;
            return double.TryParse(val, out number);
        }
    }

}
