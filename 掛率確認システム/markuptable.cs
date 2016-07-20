namespace 掛率確認システム
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("markuptable")]
    public partial class markuptable
    {
        [Key]
        public int id { get; set; }

        [StringLength(10)]
        public string customergroup { get; set; }

        [StringLength(30)]
        public string importcode { get; set; }

        [StringLength(50)]
        public string importname { get; set; }

        [StringLength(50)]
        public string nonyuritu { get; set; }

        [StringLength(50)]
        public string parts { get; set; }

        [StringLength(50)]
        public string repair { get; set; }

        [StringLength(100)]
        public string remarks { get; set; }

        [ForeignKey("importcode")]
        public virtual goodstable goodstable { get; set;}

        [ForeignKey("customergroup")]
        public virtual customertable customertable { get; set; }

    }

    public partial class tablejoin
    {
        public int id { get; set; }

        public customertable customertable { get; set; }

        public goodstable goodstable { get; set; }

        [StringLength(10)]
        public string customergroup { get; set; }

        [StringLength(50)]
        public string customergroupname { get; set; }

        [StringLength(10)]
        public string customercode { get; set; }

        [StringLength(50)]
        public string customername { get; set; }

        [StringLength(30)]
        public string importcode { get; set; }

        [StringLength(50)]
        public string importname { get; set; }

        [StringLength(30)]
        public string productcode { get; set; }

        [StringLength(60)]
        public string productname { get; set; }

        [StringLength(50)]
        public string nonyuritu { get; set; }

        [StringLength(50)]
        public string parts { get; set; }

        [StringLength(50)]
        public string repair { get; set; }

        [StringLength(100)]
        public string remarks { get; set; }

        public float? cost { get; set; }

        public float? price { get; set; }

        [StringLength(10)]
        public string importnonyuritu { get; set; }
    }




}