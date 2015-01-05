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
    [Table("Video")]
    public class Video
    {
        public Video()
        {
            MyFavorites = new HashSet<MyFavorite>();
        }

        [Key]
        [Column("Id")]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required]
        [Column("CategoryId")]
        [DisplayName("CategoryId")]
        public int CategoryId { get; set; }

        [Column("Title")]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Column("VideoUrl")]
        [DisplayName("VideoUrl")]
        public string VideoUrl { get; set; }

        [Column("Views")]
        [DisplayName("Views")]
        public int Views { get; set; }

        [Column("DownloadTimes")]
        [DisplayName("DownloadTimes")]
        public int DownloadTimes { get; set; }

        [Column("Description")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Column("ComsumeAP")]
        [DisplayName("ComsumeAP")]
        public string ComsumeAP { get; set; }

        [Column("ThumbnailUrl")]
        [DisplayName("ThumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        public virtual Category Category { get; set; }

        [JsonIgnore]
        public virtual ICollection<MyFavorite> MyFavorites { get; set; }
        /*
         * 用來取得將這影片設為最愛的成員列表
         * 參考MyLikeController裡面的_TopLikeMembers跟_TopLikeMembers.cshtml寫法
         * @foreach (var item in Model)
         * item 是指MyLike
         * Model是List<MyLike>
         */
    }
}