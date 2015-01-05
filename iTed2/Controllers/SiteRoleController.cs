using iTed2.Models;
using iTed2.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iTed2.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class SiteRoleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: SiteRole
        public ActionResult Index(int page = 1)
        {
            iTed2Service service = new iTed2Service();

            var memberList = service.GetMemberList();
            var memberProfileList = service.GetMemberProfileList(memberList);
            ViewBag.RoleCollection = service.GetRoleList();
            ViewBag.page = page;
            return View(memberProfileList);
        }

        public void ChangeRole(string role, string aspNetUserId)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            UserManager.RemoveFromRole(aspNetUserId, UserManager.GetRoles(aspNetUserId).First());
            UserManager.AddToRole(aspNetUserId, role);

            db.SaveChanges();
        }
    }
}