namespace 掛率確認システム
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("goodstable")]
    public partial class goodstable
    {
        public int id { get; set; }

        [StringLength(30)]
        public string productcode { get; set; }

        [StringLength(60)]
        public string productname { get; set; }

        [StringLength(30)]
        public string JAN { get; set; }

        public float? cost { get; set; }

        public float? price { get; set; }

        [StringLength(30)]
        [Key]
        public string importcode { get; set; }

        [StringLength(60)]
        public string importname { get; set; }

        [StringLength(10)]
        public string importnonyuritu { get; set; }

        public virtual ICollection<markuptable> markuptables { get; set; }

    }
}
