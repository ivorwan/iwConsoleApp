using System.Collections.Generic;

namespace iwConsoleApp.CacheManager
{
    public interface IMyService
    {
        List<string> GetIds();
        List<string> GetIds(List<int> ids);
        int GetIds(List<int> ids, bool isTrue);
        User GetUser(int id);
        
    }
}