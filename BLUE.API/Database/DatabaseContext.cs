using BLUE.SHARED.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BLUE.API
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DayZServer> DayZServers { get; set; }

        public DatabaseContext() : base("name=DBConnectionString")
        {
            //this.Database.Delete();
            this.Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DayZServer>()
                .ToTable("DayZServers")
                .HasKey(s => s.SteamId);
        }

        public IQueryable<DayZServer> GetServersQuery()
        {
            var servers = DayZServers.AsQueryable().AsNoTracking();

            var a = servers.ToList();

            return servers;
        }

        public bool IsServerExist(ulong steamId)
        {
            bool serverExist = DayZServers.AsNoTracking().Any(s => s.SteamId.Equals((long)steamId));

            return serverExist;
        }

        public void AddOrUpdateServer(DayZServer server)
        {
            if(server.SteamId < 1)
            {
                throw new InvalidDataException("Invalid server id!");
            }

            DayZServers.AddOrUpdate(server);
            SaveChangesAsync();
        }
    }
}
