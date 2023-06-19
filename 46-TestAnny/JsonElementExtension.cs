using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Json
{
    static class JsonElementExtension
    {
        public static string GetVaueForConfig(this JsonElement jsonEl) {

            if (jsonEl.ValueKind == JsonValueKind.String) {
                return jsonEl.GetString();
            } else if(jsonEl.ValueKind== JsonValueKind.Null || jsonEl.ValueKind==JsonValueKind.Undefined){
                return null;
            }
            else {
                return jsonEl.GetRawText();
            }
        
        }

    }
}
