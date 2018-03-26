using System.Collections.Generic;

namespace lib
{
    public class Data
    {
        public static IDictionary<string, string> Storage;

        static Data()
        {
            Storage = new Dictionary<string, string>();
        }
        
        
        public static void Set(string key, string value)
        {
            Storage[key] = value;
        }

        public static string Get(string key)
        {
            if (Storage.ContainsKey(key)) {
                return Storage[key];
            } else {
                return "";
            }
        }
    }
}