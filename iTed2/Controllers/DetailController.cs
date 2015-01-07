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
    public class DetailController : Controller
    {
        const int PageSize = 5;
        const int consumedAP = 10;
        // GET: Detail
        public ActionResult Index()
        {
            //ViewBag.title = "傑森．波廷: 科技真能解決大問題? ";
            //ViewBag.description = "1969年，Buzz Aldrin 在月球上歷史性的一小步使人類在科技所能帶來可能性的時代跨越了一大步。科技的驚人力量曾用來解決我們的大問題。反觀今日，究竟發生了 ...";
            //ViewBag.videoUrl = "http://embed.ted.com/talks/lang/zh-tw/jason_pontin_can_technology_solve_our_big_problems.html";
            return View();
        }

        [HttpGet]
        public ActionResult Index(Video video)
        {
            //iTed2Service service = new iTed2Service();

            //if (ModelState.IsValid)
            //{
            //    iMember member = service.GetMemberByAspId(User.Identity.GetUserId());
            //    int apValue = member.APValue;
            //    if (apValue >= consumedAP)
            //    {

            //        service.ConsumeAp(member);

            //        Video newVideo = new Video();
            //        newVideo = service.SetVideo(video);

            //        bool isFav = service.CheckIsFavorite(member.Id, 2);
            //        ViewBag.IsFavorite = isFav;

            //        ViewBag.videoId = newVideo.Id;
            //        ViewBag.title = newVideo.Title;
            //        ViewBag.description = newVideo.Description;
            //        ViewBag.videoUrl = newVideo.VideoUrl;
            //    }
            //    else
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            //    //System.Diagnostics.Debug.WriteLine("video.Title : " + video.Title);
            //    //System.Diagnostics.Debug.WriteLine("video.Description : " + video.Description);
            //    //System.Diagnostics.Debug.WriteLine("video.VideoUrl : " + video.VideoUrl);
            //}

            ViewBag.title = "傑森．波廷: 科技真能解決大問題? ";
            ViewBag.description = "1969年，Buzz Aldrin 在月球上歷史性的一小步使人類在科技所能帶來可能性的時代跨越了一大步。科技的驚人力量曾用來解決我們的大問題。反觀今日，究竟發生了 ...";
            ViewBag.videoUrl = "http://embed.ted.com/talks/lang/zh-tw/jason_pontin_can_technology_solve_our_big_problems.html";
            ViewBag.videoId = 2;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _DescriptionList(Video video, VideoDescription videoDescription, int page = 1)
        {
            iTed2Service service = new iTed2Service();

            videoDescription.iMemberId = service.GetMemberByAspId(User.Identity.GetUserId()).Id;
            videoDescription.VideoId = video.Id;
            videoDescription.Date = System.DateTime.Now;
            service.SetVideoDescription(videoDescription);

            var memberDescriptionList = service.GetVideoDescriptionList(video.Id);
            var pagedList = memberDescriptionList.OrderByDescending(x => x.Date).ToPagedList(page, PageSize);
            ViewBag.VideoId = video.Id;

            return PartialView(pagedList);
        }

        [HttpGet]
        public ActionResult _DescriptionList(int videoId, int page = 1)
        {
            iTed2Service service = new iTed2Service();

            var memberDescriptionList = service.GetVideoDescriptionList(videoId);
            var pagedList = memberDescriptionList.OrderByDescending(x => x.Date).ToPagedList(page, PageSize);
            ViewBag.VideoId = videoId;

            return PartialView(pagedList);
        }
    }
}