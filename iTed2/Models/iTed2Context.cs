using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace iTed2.Models
{
    public class iTed2Context : DbContext
    {
        public iTed2Context()
            : base("name=paas17context")
        { }

        public virtual DbSet<iMember> iMembers { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<MyFavorite> MyFavorites { get; set; }
        public virtual DbSet<VideoDescription> VideoDescriptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}