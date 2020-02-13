using Kubex.DAL.Configurations;
using Kubex.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kubex.DAL
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<CheckpointType> CheckpointTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DailyActivityReport> DailyActivityReports { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryType> EntryTypes { get; set; }
        public DbSet<Landmap> Landmaps { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<LicenseType> LicenseTypes { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<MissionType> MissionTypes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<RoundStatus> RoundStatuses { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }
        public DbSet<ZIP> ZIPCodes { get; set; }

        public DataContext(DbContextOptions options) : base (options) { }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserPostConfiguration());
            builder.ApplyConfiguration(new MissionConfiguration());
        }
    }
}