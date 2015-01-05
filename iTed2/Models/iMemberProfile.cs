using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTed2.Models
{
    public class iMemberProfile
    {
        [Display(Name = "序號")]
        public int MemberId { get; set; }

        [Display(Name = "照片")]
        public string Picture { get; set; }

        [Display(Name = "使用者")]
        public string Name { get; set; }

        [Display(Name = "權限")]
        public string Role { get; set; }

        public string AspNetUsersId { get; set; }
    }
}