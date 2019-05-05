using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared
{
    public interface IRepository
    {
        Task<Dictionary<string, string>> Insert(Dictionary<string,string> data);
        Task<IEnumerable<Dictionary<string, string>>> Read(Dictionary<string, string> data);
        Task<Dictionary<string, string>> Update(Dictionary<string, string> data);
    }
}
