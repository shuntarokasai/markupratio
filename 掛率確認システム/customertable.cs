namespace 掛率確認システム
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("customertable")]
    public partial class customertable
    {
        public int id { get; set; }

        
        [StringLength(10)]
        public string customercode { get; set; }

        [StringLength(50)]
        public string customername { get; set; }

        [StringLength(10)]
        [Key]
        public string customergroup { get; set; }

        [StringLength(50)]
        public string customergroupname { get; set; }

        [StringLength(10)]
        public string departmentcode { get; set; }

        [StringLength(20)]
        public string departmentname { get; set; }

        [StringLength(10)]
        public string salescode { get; set; }

        [StringLength(20)]
        public string salesname { get; set; }

        public virtual ICollection<markuptable> markuptables { get; set; }
    }
}
