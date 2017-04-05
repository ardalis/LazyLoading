using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace LazyLoadingMvc5.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }

    public class Speaker : BaseEntity
    {
        public string Name { get; set; }

        public virtual List<SpeakerSession> SpeakerSessions { get; set; } = new List<SpeakerSession>();

    }

    public class SpeakerSession : BaseEntity
    {
        public int SpeakerId { get; set; }
        public virtual Speaker Speaker { get; set; }
        public int SessionId { get; set; }
        public virtual Session Session { get; set; }
    }

    public class Session : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<SpeakerSession> SpeakerSessions { get; set; } = new List<SpeakerSession>();
        public virtual List<SessionTag> SessionTags { get; set; } = new List<SessionTag>();
    }

    public class SessionTag: BaseEntity
    {
        public int SessionId { get; set; }
        public virtual Session Session { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<SessionTag> SessionTags { get; set; } = new List<SessionTag>();

    }
}