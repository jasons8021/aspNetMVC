using iTed2.Models;
using iTed2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using PagedList.Mvc;

namespace iTed2.Controllers
{
    [Authorize]
    public class MyFavoriteController : Controller
    {
        // GET: MyFavorite
        public ActionResult Index()
        {
            iTed2Service service = new iTed2Service();

            var aspId = User.Identity.GetUserId();
            iMember member = service.GetMemberByAspId(aspId);
            ViewBag.APValue = service.GetMemberByAspId(User.Identity.GetUserId()).APValue;
            ViewBag.userName = member.Name;
            return View(service.GetMyFavoriteList(member.Id).ToLookup(m => m.Video.Category));
        }
    }
}