using iTed2.Models;
using iTed2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iTed2.Controllers
{
    [Authorize]
    public class WebApiController : ApiController
    {
        private iTed2Context db = new iTed2Context();

        [Route("api/iTed2/getUser")]
        [HttpGet]
        public List<String> GetUser(string id)
        {
            iTed2Service service = new iTed2Service();
            iMember member = new iMember();
            List<String> info = new List<string>();

            if(!String.IsNullOrEmpty(id))
            {
                member = service.GetMemberByAspId(id);
                info.Add(member.Name);
                info.Add(member.APValue.ToString());
                info.Add(member.MaxAP.ToString());
            }
            return info;
        }

        [Route("api/iTed2/addOneAP")]
        [HttpGet]
        public int addOneAP(string id)
        {
            iTed2Service service = new iTed2Service();
            iMember member = service.GetMemberByAspId(id);
            service.AddOneAp(member);

            System.Diagnostics.Debug.WriteLine("AP+1\n, member.APValue : " + member.APValue);

            return member.APValue;
        }

        [Route("api/iTed2/writeDescription")]
        [HttpGet]
        public void writeDescription(string aspId, int videoId, string memberDescription)
        {
            iTed2Service service = new iTed2Service();

            if (!String.IsNullOrEmpty(memberDescription))
            {
                VideoDescription videoDescription = new VideoDescription()
                {
                    iMemberId = service.GetMemberByAspId(aspId).Id,
                    VideoId = videoId,
                    MemberDescription = memberDescription,
                    Date = System.DateTime.Now
                };

                service.SetVideoDescription(videoDescription);
            }
        }

        [Route("api/iTed2/addFavorite")]
        [HttpGet]
        public void addFavorite(string aspId, int videoId)
        {
            iTed2Service service = new iTed2Service();

            if (!String.IsNullOrEmpty(aspId) && videoId != 0)
            {
                MyFavorite myFavorite = new MyFavorite()
                {
                    iMemberId = service.GetMemberByAspId(aspId).Id,
                    VideoId = videoId,
                };

                service.SetFavorite(myFavorite);
            }
        }
        
        [Route("api/iTed2/removeFavorite")]
        [HttpGet]
        public void removeFavorite(string aspId, int videoId)
        {
            iTed2Service service = new iTed2Service();

            if (!String.IsNullOrEmpty(aspId) && videoId != 0)
            {
                MyFavorite myFavorite = new MyFavorite()
                {
                    iMemberId = service.GetMemberByAspId(aspId).Id,
                    VideoId = videoId,
                };

                service.deleteFavorite(myFavorite);
            }
        }

        [Route("api/iTed2/checkFavorite")]
        [HttpGet]
        public bool checkFavorite(string aspId, int videoId)
        {
            iTed2Service service = new iTed2Service();

            return service.CheckIsFavorite(service.GetMemberByAspId(aspId).Id, videoId);
        }

        [Route("api/iTed2/getCurrentAP")]
        [HttpGet]
        public int getCurrentAP(string aspId)
        {
            iTed2Service service = new iTed2Service();

            return service.GetMemberByAspId(aspId).APValue;
        }

        [AllowAnonymous]
        [Route("api/iTed2/getIsAuth")]
        [HttpGet]
        public bool getIsAuth()
        {
            //System.Diagnostics.Debug.WriteLine(User.Identity.IsAuthenticated);
            bool isa = User.Identity.IsAuthenticated;
            return isa;
        }
    }
}
