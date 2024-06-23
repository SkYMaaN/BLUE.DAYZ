using BLUE.SHARED.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLUE.API.Abstractions
{
    public interface IDayZServerRepository
    {
        Task<List<DayZServer>> GetAllServersAsync();

        Task<List<DayZServer>> GetServersWithoutHiddenAndBlockedAsync();
    }
}
