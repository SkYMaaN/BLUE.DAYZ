using BLUE.API.Abstractions;
using BLUE.SHARED.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BLUE.API.Repositories
{
    public class DayZServerRepository : IDayZServerRepository
    {
        private readonly DatabaseContext _dbContext;

        public DayZServerRepository(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<List<DayZServer>> GetAllServersAsync()
        {
            var servers = await GetServerListAsync(true, true);

            return servers;
        }

        public async Task<List<DayZServer>> GetServersWithoutHiddenAndBlockedAsync()
        {
            var servers = await GetServerListAsync(false, false);

            return servers;
        }

        private async Task<List<DayZServer>> GetServerListAsync(bool includeBlocked, bool includeHidden)
        {
            var query = _dbContext.GetServersQuery();

            if (!includeBlocked)
            {
                query = query.Where(s => !s.IsBlocked);
            }

            if (!includeHidden)
            {
                query = query.Where(s => !s.IsHidden);
            }

            query = query.OrderBy(s => s.IsAdvertised).ThenBy(s => s.AdRating);

            var servers = await query.ToListAsync();

            return servers;
        }
    }
}
