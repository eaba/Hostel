using System.Collections.Generic;

namespace Shared.Repository
{
    public interface IRepository<in IDbProperties>
    {
        IEnumerable<Dictionary<string, string>> Read(IDbProperties properties);
        int Update(IEnumerable<IDbProperties> properties);
    }
}
