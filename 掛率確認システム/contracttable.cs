namespace 掛率確認システム
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("contracttable")]
    public partial class contracttable
    {
        public int id { get; set; }


        [StringLength(10)]
        public string customercode { get; set; }

        [StringLength(30)]
        [Key]
        public string importcode { get; set; }

        [StringLength(30)]
        public string productcode { get; set; }

        [StringLength(10)]
        public string revisiondate { get; set; }

        public int contractprice { get; set; }

        public int price { get; set; }

        [StringLength(50)]
        public string nonyuritu { get; set; }

        [ForeignKey("productcode")]
        public virtual goodstable goodstable { get; set; }

        [ForeignKey("customercode")]
        public virtual customertable customertable { get; set; }

        public virtual ICollection<markuptable> markuptables { get; set; }
    }
}