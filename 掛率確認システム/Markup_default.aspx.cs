using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Entity;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;

namespace 掛率確認システム
{
    public partial class Markup_default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //クッキーの有効期限が切れていたらログインページに遷移する
            if (Request.Cookies["Username"] == null)
            {
                Response.Redirect("~/login.aspx");
            }


            //テキストボックスに何も値が入っていない場合に，すべてのデータを非表示にするプロパティ　初期状態
            GridView1.ShowHeaderWhenEmpty = true;
            errmsg.Visible = false;


            //検索条件に何もヒットしなかった場合は，その旨のメッセージを表示する
            if (string.IsNullOrEmpty(importsearch.Text) && string.IsNullOrEmpty(pcsearch.Text) && string.IsNullOrEmpty(ccsearch.Text) && string.IsNullOrEmpty(cnsearch.Text))
            { }
            else
            {
                GridView1.DataBind();
                if (GridView1.Rows.Count == 0)
                {
                    errmsg.Visible = true;
                    errmsg.Text = "データがありませんでした";
                }
            }

        }

        // 戻り値の型は IEnumerable に変更できますが、// のページングと
        //並べ替えをサポートするには、次のパラメーターを追加する必要があります:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<掛率確認システム.tablejoin> GridView1_GetData([Control("importsearch")] string importsearch, [Control("ccsearch")] string ccsearch, [Control("cnsearch")] string cnsearch, [Control("pcsearch")] string pcsearch)
        {
            var context = new markupmodel();

            var querys = from x in context.markuptables
                         join y in context.customertables on x.customergroup equals y.customergroup
                         join z in context.goodstables on x.importcode equals z.importcode
                         join a in context.contracttables on new { x.importcode, y.customercode, z.productcode } equals new { a.importcode, a.customercode, a.productcode }
                         orderby y.customercode
                         select new tablejoin()
                         {

                             id = x.id,
                             customergroup = x.customergroup,
                             customergroupname = y.customergroupname,
                             customercode = y.customercode,
                             customername = y.customername,
                             importcode = x.importcode,
                             importname = z.importname,
                             productcode = a.productcode,
                             productname = z.productname,
                             contractprice = a.contractprice,
                             price = a.price,
                             revisiondate = a.revisiondate,
                             nonyuritu1 = a.nonyuritu,
                             nonyuritu2 = x.nonyuritu,
                             parts = x.parts,
                             repair = x.repair,
                             remarks = x.remarks,
                             cost = z.cost,
                             masterprice = z.price,
                             importnonyuritu = z.importnonyuritu
                         };
            if (string.IsNullOrEmpty(importsearch) && string.IsNullOrEmpty(ccsearch) && string.IsNullOrEmpty(cnsearch) && string.IsNullOrEmpty(pcsearch))
            {
                errmsg.Visible = false;

                //初期状態において，データをすべて非表示にするため，あえて誤った抽出条件を記載しています
                return querys.Where(x => x.importcode.Contains(pcsearch));

            }
            else

            //得意先コード・得意先名が空欄，仕入先または商品名のどちらかが空欄の場合
            if ((string.IsNullOrEmpty(importsearch) || string.IsNullOrEmpty(pcsearch)) && string.IsNullOrEmpty(ccsearch) && string.IsNullOrEmpty(cnsearch))
            {
                errmsg.Visible = true;
                errmsg.Text = "メーカーコードまたは商品コードと得意先コードは必須入力です";

                //データをすべて非表示にするため，あえて誤った抽出条件を記載しています
                return querys.Where(x => x.importcode.Contains(pcsearch));
            }
            else

            //仕入先コード・商品名が空欄，得意先コードまたは得意先名のどちらかが空欄の場合
            if ((string.IsNullOrEmpty(ccsearch) || string.IsNullOrEmpty(cnsearch)) && string.IsNullOrEmpty(importsearch) && string.IsNullOrEmpty(pcsearch))
            {
                errmsg.Visible = true;
                errmsg.Text = "メーカーコードまたは商品コードと得意先コードは必須入力です";
                //データをすべて非表示にするため，あえて誤った抽出条件を記載しています
                return querys.Where(x => x.customercode.Contains(cnsearch));
            }
            else

            //商品コード空欄かつ得意先のどちらかが空欄
            if ((string.IsNullOrEmpty(pcsearch)) && (string.IsNullOrEmpty(ccsearch) || string.IsNullOrEmpty(cnsearch)))
            {
                if (string.IsNullOrEmpty(cnsearch))
                {
                    //得意先名が空欄
                    return querys.Where(x => x.importcode.Contains(importsearch) && x.customercode.Contains(ccsearch));
                }
                else
                {
                    //得意先コードが空欄
                    return querys.Where(x => x.importcode.Contains(importsearch) && x.customername.Contains(cnsearch));
                }
            }
            else

            //仕入先コード空欄かつ得意先のどちらかが空欄
            if (string.IsNullOrEmpty(importsearch) && (string.IsNullOrEmpty(ccsearch) || string.IsNullOrEmpty(cnsearch)))
            {
                //得意先名が空欄
                if (string.IsNullOrEmpty(cnsearch))
                {
                    return querys.Where(x => x.productcode.Contains(pcsearch) && x.customercode.Contains(ccsearch));
                }
                //得意先コードが空欄
                else
                {
                    return querys.Where(x => x.productcode.Contains(pcsearch) && x.customercode.Contains(cnsearch));
                }
            }
            else

            //仕入先コード・商品コードは入力されていて，得意先のどちらかが空欄
            if (string.IsNullOrEmpty(ccsearch) || string.IsNullOrEmpty(cnsearch))
            {
                if (string.IsNullOrEmpty(cnsearch))
                {
                    return querys.Where(x => x.importcode.Contains(importsearch) && x.productcode.Contains(pcsearch) && x.customercode.Contains(ccsearch));
                }
                else
                {
                    return querys.Where(x => x.importcode.Contains(importsearch) && x.productcode.Contains(pcsearch) && x.customername.Contains(cnsearch));
                }

            }
            else

            //仕入先コードのみ空欄
            if (string.IsNullOrEmpty(importsearch))
            {
                return querys.Where(x => x.customercode.Contains(ccsearch) && x.customername.Contains(cnsearch) && x.productcode.Contains(pcsearch));
            }
            else

            //商品コードのみ空欄
            if (string.IsNullOrEmpty(pcsearch))
            {
                return querys.Where(x => x.importcode.Contains(importsearch) && x.customercode.Contains(ccsearch) && x.customername.Contains(cnsearch));
            }
            else
            //全てに入力が入っている場合
            {
                return querys.Where(x => x.importcode.Contains(importsearch) && x.productcode.Contains(pcsearch) && x.customercode.Contains(ccsearch) && x.customername.Contains(cnsearch));
            }

        }


        //CSVダウンロード処理
        protected void csvdownload(object sender, EventArgs e)
        {
            var dt = DateTime.Now;

            Response.Clear();
            Response.Buffer = true;
            Response.ContentEncoding = Encoding.GetEncoding("Shift-jis");
            Response.AddHeader("content-disposition", "attachment;filename=掛率表" + dt.ToString("yyyyMMddHHmmss") + ".csv");
            Response.ContentType = "application/text";
            GridView1.AllowPaging = false;
            GridView1.DataBind();

            StringBuilder sbuilder = new StringBuilder();

            for (int i = 0; i < GridView1.Columns.Count; i++)
            {
                sbuilder.Append(GridView1.Columns[i].HeaderText + ",");

            }
            sbuilder.Append("\r\n");

            for (int r = 0; r < GridView1.Rows.Count; r++)
            {
                for (int c = 0; c < GridView1.Columns.Count; c++)
                {
                    if (GridView1.Rows[r].Cells[c].Text == "&nbsp;")
                    {
                        GridView1.Rows[r].Cells[c].Text = "";
                    }
                    sbuilder.Append(GridView1.Rows[r].Cells[c].Text + ",");
                }
                sbuilder.Append("\r\n");
            }
            Response.Output.Write(sbuilder.ToString());
            Response.Flush();
            Response.End();



        }


        //sqlserverに格納されている各テーブルを結合し，selectにて取得し，csvダウンロードを行う
        protected void csvdownload_all(object sender, EventArgs e)
        {
            DateTime dtime = DateTime.Now;

            DataTable dt = new DataTable();

            string conString = ConfigurationManager.ConnectionStrings["markupmodel"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("", con))
                {
                    //sqlDataAdapter:sqlcommandにて実行したSQL文をdatatableやdatasetに格納するプロパティ
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        con.Open();

                        command.CommandTimeout = 300;
                        command.CommandText = "SELECT customertable.customercode,customertable.customername,markuptable.importcode,goodstable.importname,contracttable.productcode,goodstable.productname,goodstable.cost,contracttable.contractprice,contracttable.price,goodstable.price,contracttable.nonyuritu,markuptable.parts,markuptable.repair,markuptable.remarks,contracttable.revisiondate FROM markuptable" +
                                              " INNER JOIN goodstable" +
                                              " ON markuptable.importcode = goodstable.importcode" +
                                              " INNER JOIN customertable" +
                                              " ON markuptable.customergroup = customertable.customergroup" +
                                              " INNER JOIN contracttable" +
                                              " ON customertable.customercode = contracttable.customercode and markuptable.importcode = contracttable.importcode and goodstable.productcode = contracttable.productcode";

                        //commandtextにて取得した結果をadapterに格納する
                        adapter.SelectCommand = command;

                        //fillにて自動でopen,closeを行う・またdtにその値を格納している
                        adapter.Fill(dt);

                        con.Close();
                    }


                }
            }

            Response.Clear();
            Response.Buffer = true;
            Response.ContentEncoding = Encoding.GetEncoding("Shift-jis");
            Response.AddHeader("content-disposition", "attachment;filename=掛率表all" + dtime.ToString("yyyyMMddHHmmss") + ".csv");
            Response.ContentType = "application/text";

            StringBuilder sbuilder = new StringBuilder();

            dt.Columns[0].ColumnName = "得意先コード";
            dt.Columns[1].ColumnName = "得意先名";
            dt.Columns[2].ColumnName = "メーカーコード";
            dt.Columns[3].ColumnName = "メーカー名";
            dt.Columns[4].ColumnName = "商品コード";
            dt.Columns[5].ColumnName = "商品名";
            dt.Columns[6].ColumnName = "NET";
            dt.Columns[7].ColumnName = "下代";
            dt.Columns[8].ColumnName = "上代";
            dt.Columns[9].ColumnName = "マスタ上代";
            dt.Columns[10].ColumnName = "掛率";
            dt.Columns[11].ColumnName = "部品(%)";
            dt.Columns[12].ColumnName = "修理(%)";
            dt.Columns[13].ColumnName = "備考";
            dt.Columns[14].ColumnName = "最終単価改定日";


            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sbuilder.Append(dt.Columns[i] + ",");

            }
            sbuilder.Append("\r\n");

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (dt.Rows[r][c].ToString() == "&nbsp;")
                    {
                        dt.Rows[r][c] = "";
                    }
                    sbuilder.Append(dt.Rows[r][c] + ",");
                }
                sbuilder.Append("\r\n");
            }
            Response.Output.Write(sbuilder.ToString());
            Response.Flush();
            Response.End();




        }

        //ログアウト処理
        protected void logout_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["UserName"] != null)
            {
                HttpCookie delcokkie = new HttpCookie("UserName");
                delcokkie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(delcokkie);
            }

            Response.Redirect("~/login.aspx");
        }
    }
}