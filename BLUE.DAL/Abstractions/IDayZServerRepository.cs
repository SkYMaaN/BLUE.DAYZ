using BLUE.SHARED.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLUE.DAL.Abstractions
{
    public interface IDayZServerRepository
    {
        Task<List<DayZServer>> GetServerList();
    }
}
