using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _46_TestAnny
{
    static class Helper
    {
        public static IDictionary<string, string> Clone(this IDictionary<string, string> data) {
            IDictionary<string, string> cloneData = new Dictionary<string, string>();
            foreach (var d in data) {
                cloneData[d.Key] = d.Value;
            }
            return cloneData;
        }
    }
}
