using iTed2.Models;
using iTed2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace iTed2.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        const int PageSize = 5;

        [AllowAnonymous]
        public ActionResult Index()
        {
            iTed2Service service = new iTed2Service();

            ViewBag.SearchClick = false;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Category category)
        {
            iTed2Service service = new iTed2Service();
            List<Google.Apis.Customsearch.v1.Data.Result> result = new List<Google.Apis.Customsearch.v1.Data.Result>();
            Category newCategory = new Category();
            ViewBag.SearchClick = false;
            if (ModelState.IsValid)
            {
                ViewBag.SearchClick = true;

                ViewBag.Keyword = category.CategoryName;
                result = service.GetGoogleList(category.CategoryName);
                if (result != null)
                    newCategory = service.SetCategory(category.CategoryName, 1);
                ViewBag.categoryId = newCategory.Id;
                ViewBag.TedList = result;
            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult Click(Video video)
        {
            iTed2Service service = new iTed2Service();
            Category newCategory = new Category();

            if (ModelState.IsValid)
            {
                string Id = User.Identity.GetUserId();
                iMember member = service.GetMemberByAspId(Id);
                ViewBag.APValue = member.APValue - 10;
                System.Diagnostics.Debug.WriteLine(member.Name + " : " + member.APValue);
            }
            return RedirectToAction("Index", "Detail", new { title = video.Title, description = video.Description, videoUrl = video.VideoUrl, categoryId = video.CategoryId, thumbnailUrl = video.ThumbnailUrl });
        }
    }
}