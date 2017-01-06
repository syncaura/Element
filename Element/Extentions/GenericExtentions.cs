using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Element.Extentions {
    public static class GenericExtentions {
        /// <summary>
        /// Tries to parse an object as another data type
        /// </summary>
        /// <typeparam name="T">The object type we want to parse the object to</typeparam>
        /// <param name="val">Value that we want to try and parse</param>
        /// <returns>Value parsed as T OR the default of T if there was an error</returns>
        public static T ParseAs<T>(this object val) {
            try {
                return (T)Convert.ChangeType(val, typeof(T));
            } catch {
                // TODO: Possibly make this so it throws an error :) ~Cal 20167/01/06
                return default(T);
            }
        }
    }
}
