using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace iwConsoleApp.CacheManager
{
    public class CacheManager
    {
        public static T GetFromCache<T>(Func<T> function)
        {
            string key = "k";
            return GetFromCache(key, function);
        }

        public static T GetFromCache<T>(string key, Func<T> function)
        {
            var result = HttpRuntime.Cache[key];
            if (result == null)
            {
                result = function();
                HttpRuntime.Cache[key] = result;
            }
            return (T)result;
        }

        public static T Runner<T>(Func<T> funcToRun)
        {
            //Do stuff before running function as normal
            return funcToRun();
        }
    }
}
