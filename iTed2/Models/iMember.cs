using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iTed2.Models
{
    [Table("iMember")]
    public class iMember
    {
        public iMember()
        {
            MyFavorites = new HashSet<MyFavorite>();
            MyVideoDescriptions = new HashSet<VideoDescription>();
        }

        [Key]
        [Column("Id")]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Column("Picture")]
        [DisplayName("Picture")]
        public string Picture { get; set; }

        [Column("AspNetUsersId")]
        [DisplayName("AspNetUsersId")]
        public string AspNetUsersId { get; set; }

        [Column("StoneAmount")]
        [DisplayName("StoneAmount")]
        public int StoneAmount { get; set; }

        [Column("APValue")]
        [DisplayName("APValue")]
        public int APValue { get; set; }

        [Column("MaxAP")]
        [DisplayName("MaxAP")]
        public int MaxAP { get; set; }

        [Column("Email")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [JsonIgnore]
        public virtual ICollection<MyFavorite> MyFavorites { get; set; }
        /* 使用範例
         * Input : memberId和categoryId
         * Output : 取得某人對某種類最愛的影片
         * var linq = from member in db.MyFavorites
                       where member.MemberId == memberId && member.Video.categoryId == categoryId
                       select member;
            return linq.ToList();
         */

        [JsonIgnore]
        public virtual ICollection<VideoDescription> MyVideoDescriptions { get; set; }

    }
}