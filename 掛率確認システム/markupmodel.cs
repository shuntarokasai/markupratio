namespace 掛率確認システム
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class markupmodel : DbContext
    {
        public markupmodel()
            : base("name=markupmodel")
        {
        }

        public virtual DbSet<customertable> customertables { get; set; }
        public virtual DbSet<goodstable> goodstables { get; set; }
        public virtual DbSet<markuptable> markuptables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
