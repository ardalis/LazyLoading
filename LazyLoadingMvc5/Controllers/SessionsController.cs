using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LazyLoadingMvc5.Models;

namespace LazyLoadingMvc5.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var sessions = _db.Sessions;
            return View(sessions);
        }

        public ContentResult Seed()
        {
            string result = "No changes made.";
            if (_db.Sessions.Count() == 0)
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