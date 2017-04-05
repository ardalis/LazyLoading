using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LazyLoading.Models;
using Microsoft.Extensions.Logging;

namespace LazyLoading.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ILoggerFactory _loggerFactory;

        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            ConfigureSpeakerSessions(builder);
            ConfigureSessionTags(builder);
        }

        private static void ConfigureSpeakerSessions(ModelBuilder builder)
        {
            builder.Entity<SpeakerSession>()
                .HasKey(t => new { t.SpeakerId, t.SessionId });

            builder.Entity<SpeakerSession>()
                .HasOne(s => s.Session)
                .WithMany(sp => sp.SpeakerSessions)
                .HasForeignKey(s => s.SessionId);

            builder.Entity<SpeakerSession>()
                .HasOne(s => s.Speaker)
                .WithMany(sp => sp.SpeakerSessions)
                .HasForeignKey(s => s.SpeakerId);
        }
        private static void ConfigureSessionTags(ModelBuilder builder)
        {
            builder.Entity<SessionTag>()
                .HasKey(t => new { t.SessionId, t.TagId });

            builder.Entity<SessionTag>()
                .HasOne(s => s.Session)
                .WithMany(sp => sp.SessionTags)
                .HasForeignKey(s => s.SessionId);

            builder.Entity<SessionTag>()
                .HasOne(s => s.Tag)
                .WithMany(sp => sp.SessionTags)
                .HasForeignKey(s => s.TagId);
        }
    }
}
