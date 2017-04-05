using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LazyLoading.Data;
using LazyLoading.Models;
using Microsoft.EntityFrameworkCore;

namespace LazyLoading.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SessionsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var sessions = _db.Sessions
                .Include(s => s.SessionTags)
                .ThenInclude(st => st.Tag)
                .Include(s => s.SpeakerSessions)
                .ThenInclude(ss=>ss.Speaker);
            return View(sessions);
        }

        public IActionResult Seed()
        {
            string result = "No changes made.";
            if(_db.Sessions.Count() == 0)
            {
                var speakerSteve = new Speaker() { Name = "Steve Smith" };
                var speakerJulie = new Speaker() { Name = "Julie Lerman" };
                _db.Speakers.Add(speakerSteve);
                _db.Speakers.Add(speakerJulie);

                var tagCsharp = new Tag() { Name = "C#" };
                var tagEf = new Tag() { Name = "Entity Framework" };
                var tagAspNet = new Tag() { Name = "ASP.NET" };
                _db.Tags.Add(tagCsharp);
                _db.Tags.Add(tagEf);
                _db.Tags.Add(tagAspNet);

                var sessionImprovingCode = new Session() { Name = "Improving the Quality of Existing Software" };
                var sessionDesignPatterns = new Session() { Name = "Common Web Design Patterns" };
                var sessionDdd = new Session() { Name = "Implementing DDD with ASP.NET Core" };
                _db.Sessions.Add(sessionImprovingCode);
                _db.Sessions.Add(sessionDesignPatterns);
                _db.Sessions.Add(sessionDdd);

                int count = _db.SaveChanges();

                // assign speakers
                sessionImprovingCode.SpeakerSessions
                    .Add(new SpeakerSession()
                    {
                        SessionId = sessionImprovingCode.Id,
                        SpeakerId = speakerSteve.Id
                    });
                sessionDesignPatterns.SpeakerSessions
                    .Add(new SpeakerSession()
                    {
                        SessionId = sessionDesignPatterns.Id,
                        SpeakerId = speakerSteve.Id
                    });
                sessionDdd.SpeakerSessions
                    .Add(new SpeakerSession()
                    {
                        SessionId = sessionDdd.Id,
                        SpeakerId = speakerSteve.Id
                    });
                sessionDdd.SpeakerSessions
                    .Add(new SpeakerSession()
                    {
                        SessionId = sessionDdd.Id,
                        SpeakerId = speakerJulie.Id
                    });

                // assign tags
                sessionImprovingCode.SessionTags
                    .Add(new SessionTag()
                    {
                        SessionId = sessionImprovingCode.Id,
                        TagId = tagCsharp.Id
                    });
                sessionImprovingCode.SessionTags
                    .Add(new SessionTag()
                    {
                        SessionId = sessionImprovingCode.Id,
                        TagId = tagAspNet.Id
                    });

                count += _db.SaveChanges();
                result = $"{count} records updated.";
            }
            return Content(result);
        }
    }
}
