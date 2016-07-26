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
using System.Windows;


namespace 掛率確認システム
{
    public partial class Markup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //テキストボックスに何も値が入っていない場合に，すべてのデータを非表示にするプロパティ　初期状態
            GridView1.ShowHeaderWhenEmpty = true;
            errmsg.Visible = false;


            //検索条件に何もヒットしなかった場合は，その旨のメッセージを表示する
            if (string.IsNullOrEmpty(importsearch.Text) && string.IsNullOrEmpty(pcsearch.Text) && string.IsNullOrEmpty(ccsearch.Text) && string.IsNullOrEmpty(cnsearch.Text))
            { }
            else
            {
                GridView1.DataBind();
                if(GridView1.Rows.Count == 0)
                {
                    errmsg.Visible = true;
                    errmsg.Text ="データがありませんでした";
                }
            }

                //string content = "";

                //var context = new markupmodel();

                //var querys = from x in context.markuptables
                //             join y in context.customertables on x.customergroup equals y.customergroup
                //             join z in context.goodstables on x.importcode equals z.importcode
                //             where x.importcode == "004" && x.customergroup == "19710"
                //             orderby x.customergroup
                //             select new tablejoin()
                //             {

                //                 id = x.id,
                //                 customergroup = x.customergroup,
                //                 customergroupname = y.customergroupname,
                //                 customercode = y.customercode,
                //                 customername = y.customername,
                //                 importcode = x.importcode,
                //                 importname = z.importname,
                //                 productcode = z.productcode,
                //                 productname = z.productname,
                //                 nonyuritu = x.nonyuritu,
                //                 parts = x.parts,
                //                 repair = x.repair,
                //                 remarks = x.remarks,
                //                 cost = z.cost,
                //                 price = z.price,
                //                 importnonyuritu = z.importnonyuritu
                //             };



                //    //foreach(var list in querys)
                //    //{
                //    //    content += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}</br>",  list.customergroup,list.customergroupname,list.customercode,list.customername,list.importcode,list.importname,list.productcode,list.productname,list.nonyuritu,list.parts,list.repair,list.cost,list.price,list.remarks);
                //    //}
                //    //Label1.Text = content;


            }

        // 戻り値の型は IEnumerable に変更できますが、// のページングと
        //並べ替えをサポートするには、次のパラメーターを追加する必要があります:
//     int maximumRows
//     int startRowIndex
//     out int totalRowCount
//     string sortByExpression
    public IQueryable<掛率確認システム.tablejoin> GridView1_GetData([Control("importsearch")] string importsearch, [Control("ccsearch")] string ccsearch,[Control("cnsearch")] string cnsearch,[Control("pcsearch")] string pcsearch )
        {
            var context = new markupmodel();

            var querys = from x in context.markuptables
                         join y in context.customertables on x.customergroup equals y.customergroup
                         join z in context.goodstables on x.importcode equals z.importcode
                         join a in context.contracttables on new { x.importcode,y.customercode,z.productcode} equals new {a.importcode,a.customercode,a.productcode} 
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
            if((string.IsNullOrEmpty(ccsearch) || string.IsNullOrEmpty(cnsearch)) && string.IsNullOrEmpty(importsearch) && string.IsNullOrEmpty(pcsearch))
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
            if(string.IsNullOrEmpty(ccsearch) || string.IsNullOrEmpty(cnsearch))
            {
                if(string.IsNullOrEmpty(cnsearch))
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
            if(string.IsNullOrEmpty(importsearch))
            {
                return querys.Where(x => x.customercode.Contains(ccsearch) && x.customername.Contains(cnsearch) && x.productcode.Contains(pcsearch));
            }
            else

            //商品コードのみ空欄
            if(string.IsNullOrEmpty(pcsearch))
            {
                return querys.Where(x => x.importcode.Contains(importsearch) && x.customercode.Contains(ccsearch) && x.customername.Contains(cnsearch));
            }
            else
            //全てに入力が入っている場合
            {
                return querys.Where(x => x.importcode.Contains(importsearch) && x.productcode.Contains(pcsearch) && x.customercode.Contains(ccsearch) && x.customername.Contains(cnsearch));
            }

        }

    }
}