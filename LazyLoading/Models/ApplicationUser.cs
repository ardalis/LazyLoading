using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LazyLoading.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
    }

    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }

    public class Speaker : BaseEntity
    {
        public string Name { get; set; }

        public List<SpeakerSession> SpeakerSessions { get; set; } = new List<SpeakerSession>();

    }

    public class SpeakerSession
    {
        public int SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
    }

    public class Session : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SpeakerSession> SpeakerSessions { get; set; } = new List<SpeakerSession>();
        public List<SessionTag> SessionTags { get; set; } = new List<SessionTag>();
    }

    public class SessionTag
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public List<SessionTag> SessionTags { get; set; } = new List<SessionTag>();

    }
}
