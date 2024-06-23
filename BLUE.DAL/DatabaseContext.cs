using BLUE.SHARED.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Threading.Tasks;

namespace BLUE.DAL
{
    public class DatabaseContext : DbContext
    {
        DbSet<DayZServer> DayZServers { get; set; }

        public DatabaseContext() : base("name=DBConnectionString")
        {
            this.Database.Delete();
            this.Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DayZServer>()
                .ToTable("DayZServers")
                .HasKey(s => s.SteamId);
        }

        public async Task<bool> IsServerExist(ulong steamId)
        {
            bool serverExist = await DayZServers.AsNoTracking().AnyAsync(s => s.SteamId.Equals((long)steamId));

            return serverExist;
        }

        public void AddOrUpdateServer(DayZServer server)
        {
            if(server.SteamId < 1)
            {
                throw new InvalidDataException("Invalid server id!");
            }

            DayZServers.AddOrUpdate(server);
        }
    }
}
