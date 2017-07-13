using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.CacheManager
{
    public class MyService : IMyService
    {
        public List<string> GetIds()
        {
            return new List<string>() { "1", "2", "3", "4" };
        }
        public List<string> GetIds(List<int> ids)
        {
            return new List<string>() { "5", "6", "7", "8" };
        }

        public int GetIds(List<int> ids, bool isTrue)
        {
            return 0;
        }

        public User GetUser(int id)
        {
            return new User() { Id = 123 };
        }
    }
}
