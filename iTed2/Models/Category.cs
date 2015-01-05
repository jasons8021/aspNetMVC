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
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            Videos = new HashSet<Video>();
        }

        [Key]
        [Column("Id")]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required]
        [Column("CategoryName")]
        [DisplayName("搜尋的關鍵字")]
        public string CategoryName { get; set; }

        [Column("Views")]
        [DisplayName("Views")]
        public int Views { get; set; }

        [JsonIgnore]
        public virtual ICollection<Video> Videos { get; set; }
    }
}