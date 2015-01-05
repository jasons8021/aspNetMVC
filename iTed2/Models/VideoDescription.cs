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
    [Table("VideoDescription")]
    public class VideoDescription
    {
        [Key]
        [Column("Id")]
        [DisplayName("Id")]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int iMemberId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VideoId { get; set; }

        [Column("MemberDescription")]
        [DisplayName("MemberDescription")]
        public string MemberDescription { get; set; }

        [Column("Date")]
        [DisplayName("Date")]
        public DateTime Date { get; set; }

        public virtual iMember iMember { get; set; }

        public virtual Video Video { get; set; }
    }
}