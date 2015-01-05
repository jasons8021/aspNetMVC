using Google.Apis.Services;
using iTed2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTed2.Service
{
    public class iTed2Service
    {
        private iTed2Context db = new iTed2Context();
        private ApplicationDbContext AspNetDb = new ApplicationDbContext();
        private const int videoAP = 10;

        public List<Google.Apis.Customsearch.v1.Data.Result> GetGoogleList(string keyword)
        {
            string apiKey = "AIzaSyB3jHA9NMxFR_lQhn135ClHDezKBOjvaok";
            string cx = "000050275881028591178:gk3kd31s42i";

            var svc = new Google.Apis.Customsearch.v1.CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });

            var listRequest = svc.Cse.List(keyword);
            listRequest.Cx = cx;
            listRequest.Num = 5;

            var search = listRequest.Execute();

            if (search.Items != null)
            {
                //foreach (var result in search.Items)
                //{
                //    System.Diagnostics.Debug.WriteLine("****************************************************************************************************************");
                //    System.Diagnostics.Debug.WriteLine("Snippet : " + result.Snippet);
                //    System.Diagnostics.Debug.WriteLine("Title : " + result.Title);
                //    System.Diagnostics.Debug.WriteLine("Pagemap[\"cse_image\"] : " + result.Pagemap["cse_image"][0]["src"]);
                //    System.Diagnostics.Debug.WriteLine("Link : " + result.Link);
                //    System.Diagnostics.Debug.WriteLine("****************************************************************************************************************");
                //}

                var tedList = search.Items
                        .Where(item => !item.Title.Contains("transcript"))
                        .Where(item => !item.Link.Contains("https"))
                        .Where(item => !item.Link.Contains("=ja"))
                        .Select(item =>
                        {
                            if (item.Title.Contains("|"))
                                item.Title = item.Title.Substring(0, item.Title.IndexOf('|'));
                            if (item.Title.Contains("-"))
                                item.Title = item.Title.Substring(0, item.Title.IndexOf('-'));
                            if (item.Link.Contains("?language="))
                                item.Link = item.Link.Substring(0, item.Link.IndexOf('?'));
                            item.Link = item.Link.ToString().Replace("http://www.ted.com/talks/", "http://embed.ted.com/talks/lang/zh-tw/");
                            item.Link = item.Link.ToString().Replace("/transcript", "");
                            item.Link += ".html";
                            if (item.Pagemap.ContainsKey("cse_image"))
                            {
                                item.Pagemap["cse_image"][0]["src"] = item.Pagemap["cse_image"][0]["src"].ToString().Substring(0, item.Pagemap["cse_image"][0]["src"].ToString().Length-4);
                                item.Pagemap["cse_image"][0]["src"] += "134";
                            }
                            //System.Diagnostics.Debug.WriteLine("Pagemap[\"cse_image\"] : " + item.Pagemap["cse_image"][0]["src"]);
                            return item;
                        })
                        .ToList();
                return tedList;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No Find!");
                return null;
            }
        }

        public Category SetCategory(string keyword, int views = 1)
        {
            var linq = from cate in db.Categorys
                       where cate.CategoryName == keyword
                       select cate;

            var category = linq.FirstOrDefault();

            if (category == null)
            {
                category = new Category()
                {
                    CategoryName = keyword,
                    Views = views
                };
                db.Categorys.Add(category);
            }
            else
            {
                category.Views += views;
            }
            db.SaveChanges();

            return category;
        }

        public Category GetCategory(Category category)
        {
            if (category.Id == 0)
            {
                var linq = from item in db.Categorys
                           where item.CategoryName == category.CategoryName
                           select item;
                category = linq.FirstOrDefault();
            }
            else
            {
                category = db.Categorys.Find(category.Id);
            }

            return category;
        }

        public Video SetVideo(Video video)
        {
            if (video.Id == 0)
            {
                var linq = from item in db.Videos
                           where item.CategoryId == video.CategoryId && item.Title == video.Title && item.VideoUrl == video.VideoUrl
                           select item;

                var oldVideo = linq.FirstOrDefault();
                if (oldVideo == null)
                {
                    video = new Video
                    {
                        CategoryId = video.CategoryId,
                        Title = video.Title,
                        VideoUrl = video.VideoUrl,
                        Views = 0,
                        DownloadTimes = 0,
                        Description = video.Description,
                        ThumbnailUrl = video.ThumbnailUrl
                    };
                    db.Videos.Add(video);
                }
                else
                {
                    video = oldVideo;
                }
            }
            else
            {
                video = db.Videos.Find(video.Id);
            }

            video.Views++;
            db.SaveChanges();

            return video;
        }

        public void SetMember(iMember member)
        {
            db.iMembers.Add(member);
            db.SaveChanges();
        }

        public iMember GetMember(int memberId)
        {
            return db.iMembers.Find(memberId);
        }

        public iMember GetMemberByAspId(string aspNetId)
        {
            var linq = from m in db.iMembers
                       where m.AspNetUsersId == aspNetId
                       select m;
            return linq.FirstOrDefault();
        }

        public iMember GetMemberByEmail(string email)
        {
            var linq = from m in db.iMembers
                       where m.Email == email
                       select m;
            return linq.FirstOrDefault();
        }
        public void AddOneAp(iMember member)
        {
            if (member.APValue < member.MaxAP)
            {
                member.APValue++;
                db.SaveChanges();
            }
        }

        public void ConsumeAp(iMember member)
        {
            if (member.APValue >= videoAP)
            {
                //System.Diagnostics.Debug.WriteLine("ConsumeAp : " + videoAP);
                member.APValue -= videoAP;
                db.SaveChanges();
            }
        }

        public void SetVideoDescription(VideoDescription videoDescription)
        {
            db.VideoDescriptions.Add(videoDescription);
            db.SaveChanges();
        }

        public List<VideoDescription> GetVideoDescriptionList(int videoId)
        {
            var linq = from description in db.VideoDescriptions
                       where description.VideoId == videoId
                       select description;
            return linq.ToList();
        }

        public void SetFavorite(MyFavorite myFavorite)
        {
            var oldFavorite = db.MyFavorites.Find(myFavorite.iMemberId, myFavorite.VideoId);
            if (oldFavorite == null)
            {
                oldFavorite = new MyFavorite
                {
                    iMemberId = myFavorite.iMemberId,
                    VideoId = myFavorite.VideoId
                };

                db.MyFavorites.Add(oldFavorite);
                db.SaveChanges();
            }
        }

        public void deleteFavorite(MyFavorite myFavorite)
        {
            var oldFavorite = db.MyFavorites.Find(myFavorite.iMemberId, myFavorite.VideoId);
            if (oldFavorite != null)
            {
                db.MyFavorites.Remove(oldFavorite);
                db.SaveChanges();
            }
        }
        public List<MyFavorite> GetMyFavoriteList(int memberId)
        {
            return db.iMembers.Find(memberId).MyFavorites.ToList();
        }

        public bool CheckIsFavorite(int memberId, int videoId)
        {
            var favorite = db.MyFavorites.Find(memberId, videoId);
            if (favorite != null)
                return true;
            else
                return false;
        }

        public List<Video> getTopThreeVideo()
        {
            var list = (from video in db.Videos
                        orderby video.Views descending
                        select video).Take(3).ToList();

            foreach (Video video in list)
            {
                video.ThumbnailUrl = video.ThumbnailUrl.Substring(0, video.ThumbnailUrl.Length - 3);
                video.ThumbnailUrl += "355";
            
            }
            return list;
        }
        public List<iMemberProfile> GetMemberProfileList(List<iMember> memberList)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(AspNetDb));
            List<iMemberProfile> MemberRolesViewModel = new List<iMemberProfile>();

            foreach (var member in memberList)
            {
                iMemberProfile memberRole = new iMemberProfile();
                memberRole.MemberId = member.Id;
                memberRole.Name = member.Name;
                memberRole.Picture = member.Picture;
                memberRole.AspNetUsersId = member.AspNetUsersId;
                memberRole.Role = UserManager.GetRoles(member.AspNetUsersId).First();

                if (memberRole.Role == "Supervisor")
                    continue;
                MemberRolesViewModel.Add(memberRole);
            }

            MemberRolesViewModel = MemberRolesViewModel.OrderByDescending(x => x.MemberId).ToList();

            return MemberRolesViewModel;
        }

        public List<iMember> GetMemberList()
        {
            return db.iMembers.ToList();
        }

        public List<String> GetRoleList()
        {
            var linq = from roles in AspNetDb.Roles
                       where roles.Name != "Supervisor"
                       select roles.Name;
            return linq.ToList();
        }
    }
}