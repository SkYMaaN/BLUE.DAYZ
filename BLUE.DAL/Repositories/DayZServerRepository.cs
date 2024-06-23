using BLUE.DAL.Abstractions;
using BLUE.SHARED.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLUE.DAL.Repositories
{
    public class DayZServerRepository : IDayZServerRepository
    {
        private readonly DatabaseContext _databaseContext;

        public DayZServerRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<List<DayZServer>> GetServerList()
        {
            throw new System.NotImplementedException();
        }
    }
}
