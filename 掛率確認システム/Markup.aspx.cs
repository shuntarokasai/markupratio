﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 掛率確認システム
{
    public partial class Markup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string content = "";

            var context = new markupmodel();

            var querys = from x in context.markuptables
                         join y in context.customertables on x.customergroup equals y.customergroup
                         join z in context.goodstables on x.importcode equals z.importcode
                         where x.importcode == "004" && x.customergroup == "19710"
                         orderby x.customergroup
                         select new tablejoin()
                         {

                             id = x.id,
                             customergroup = x.customergroup,
                             customergroupname = y.customergroupname,
                             customercode = y.customercode,
                             customername = y.customername,
                             importcode = x.importcode,
                             importname = z.importname,
                             productcode = z.productcode,
                             productname = z.productname,
                             nonyuritu = x.nonyuritu,
                             parts = x.parts,
                             repair = x.repair,
                             remarks = x.remarks,
                             cost = z.cost,
                             price = z.price,
                             importnonyuritu = z.importnonyuritu
                         };



                foreach(var list in querys)
                {
                    content += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}</br>",  list.customergroup,list.customergroupname,list.customercode,list.customername,list.importcode,list.importname,list.productcode,list.productname,list.nonyuritu,list.parts,list.repair,list.cost,list.price,list.remarks);
                }
                Label1.Text = content;
            

        }
    }
}