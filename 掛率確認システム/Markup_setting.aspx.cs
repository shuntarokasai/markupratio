using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Text;
using System.Xml.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;


namespace 掛率確認システム
{
    public partial class Markup_setting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //markuptableにデータを挿入するイベント
        protected void registration(object sender, EventArgs e)
        {
            int lastid = 0;
            var context = new markupmodel();

            var items = context.markuptables;

            foreach (var item in items)
            {
                lastid = item.id;　//現在登録されているdbの一番最後のidを取得
            }

            DataTable dt = new DataTable(); //登録するためのデータテーブルを作成
            DataRow dr = dt.NewRow();　//挿入するデータの行を作成


            string[] regiarray = new string[7];　//dtのヘッダを定義

            regiarray[0] = "id";
            regiarray[1] = "customergroup";
            regiarray[2] = "importcode";
            regiarray[3] = "nonyuritu";
            regiarray[4] = "parts";
            regiarray[5] = "repair";
            regiarray[6] = "remarks";

            for (int i=0; i<7; i++)
            {
                dt.Columns.Add(regiarray[i]);
            }


            //テキストボックスより受け取った，データを新規行に挿入する
            dr[0] = (lastid + 1).ToString();
            dr[1] = incustomer.Text;
            dr[2] = inimport.Text;
            dr[3] = innonyuritu.Text;
            dr[4] = inparts.Text;
            dr[5] = inrepair.Text;
            dr[6] = inremarks.Text;

            dt.Rows.Add(dr);
            

            string conString = ConfigurationManager.ConnectionStrings["markupmodel"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "dbo.markuptable";
                    con.Open();

                    sqlBulkCopy.WriteToServer(dt);

                    con.Close();

                }
            }

            
            

        }
    }
}