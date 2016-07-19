namespace 掛率確認システム
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("importtable")]
    public partial class importtable
    {
        public int id { get; set; }

        [StringLength(5)]
        public string importcode { get; set; }

        [StringLength(50)]
        public string importname { get; set; }
    }
}
