using System;
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

            using (var context = new markupmodel())
            {
                var lists = context.markuptables;

                foreach(var list in lists)
                {
                    content += string.Format("{0},{1},{2},{3},{4},{5},{6}</br>",  list.customergroup,list.customertable.customergroupname,list.importcode,list.goodstable.importname,list.nonyuritu,list.parts,list.repair);
                }
                Label1.Text = content;
            }

        }
    }
}