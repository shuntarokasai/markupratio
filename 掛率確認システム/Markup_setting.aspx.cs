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
            errmsg.Visible = false;
        }

        //markuptableにデータを挿入するイベント
        protected void registration(object sender, EventArgs e)
        {
            //CSV登録か，ウェブ画面での登録かをここで判断する
            if(csvselect.HasFile)
            {
                //CSVファイルかどうかの確認
                if(csvselect.PostedFile.ContentType == "application/vnd.ms-excel")
                {
                    var context = new markupmodel();

                    var idchecks = context.markuptables;

                    var cgchecks = context.customertables;

                    var icchecks = context.goodstables;

                    var file = csvselect.PostedFile;
                    string filepath = string.Empty;

                    file.SaveAs(Path.GetTempPath() + Path.GetFileName(file.FileName)); //ブラウザーの一時フォルダにフルパスで情報を保存する
                    filepath = Path.GetTempPath() + Path.GetFileName(file.FileName); //保存した情報をfilepath変数に格納する

                    DataTable dt = new DataTable();

                    string[] filecontent = File.ReadAllLines(filepath, Encoding.GetEncoding("Shift-jis"));
                    if (filecontent.Count() > 0)
                    {
                        //dtにヘッダーを定義する
                        string[] columns = filecontent[0].Split(',');
                        for (int c = 0; c < columns.Count(); c++)
                        {
                            dt.Columns.Add(columns[c]);
                        }

                        //dtにデータを格納する:CSVにてid行も定義

                        for (int r = 1; r < filecontent.Count(); r++)
                        {
                            string[] rowdata = filecontent[r].Split(',');
                            dt.Rows.Add(rowdata);
                        }

                    }

                    int lastid = 0;
                    //dtに重複しないidを調査し格納する
                    foreach (var idcheck in idchecks)
                    {
                        lastid = idcheck.id;
                    }


                    //dtのidフィールドに最新のidを格納する
                    int count = 0;
                    for (var i = lastid + 1; i < lastid + 1 + dt.Rows.Count; i++)
                    {
                        dt.Rows[count][0] = i;
                        count++;
                    }

                    //dtがcustomertable,goodstableに存在するかどうかを調査

                    int[] cgflagarray = new int[dt.Rows.Count];
                    int[] icflagarray = new int[dt.Rows.Count];


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //customergroupの存在チェック
                        foreach (var cgcheck in cgchecks)
                        {
                            if (dt.Rows[i][1].ToString() == cgcheck.customergroup)
                            {
                                //挿入するデータにたいして，1つずつcustomertableのcustomergroupの値をチェックする
                                cgflagarray[i] = 0;
                                break;
                            }
                            else
                            {
                                //なかった場合
                                cgflagarray[i] = 1;
                            }
                        }

                        //importcodeの存在チェック
                        foreach (var iccheck in icchecks)
                        {
                            if (dt.Rows[i][2].ToString() == iccheck.importcode)
                            {
                                icflagarray[i] = 0;
                                break;
                            }
                            else
                            {
                                icflagarray[i] = 1;
                            }
                        }
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //マスタにない場合
                        if (cgflagarray[i] != 0 || icflagarray[i] != 0)
                        {
                            errmsg.Visible = true;
                            errmsg.Text = i + 2 + "行目の得意先グループコードまたはメーカーコードはマスタに存在しません";
                            return;
                        }
                    }


                    //dtがmarkuptableにすでに存在するかのチェック

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        foreach (var check in idchecks)
                        {
                            if (dt.Rows[i][1].ToString() == check.customergroup && dt.Rows[i][2].ToString() == check.importcode)
                            {
                                errmsg.Visible = true;
                                errmsg.Text = i + 2 + "行目のデータはすでに存在しています";
                                return;
                            }
                        }
                    }


                    string consString = ConfigurationManager.ConnectionStrings["markupmodel"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(consString))
                    {
                        using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(con))
                        {
                            sqlbulkcopy.DestinationTableName = "dbo.markuptable";
                            con.Open();

                            sqlbulkcopy.WriteToServer(dt);

                            con.Close();

                        }
                    }

                    errmsg.Visible = true;
                    errmsg.Text = "登録が完了しました";

                }

                //CSVファイルではない場合
                else
                {
                    errmsg.Visible = true;
                    errmsg.Text = "CSVファイルを選択してください";
                    return;
                }


                



            }
            //ウェブ画面登録の処理
            else
            {
                int lastid = 0;
                var context = new markupmodel();

                //最終idの取得用
                var items = context.markuptables;

                //入力した得意先が存在するかチェックする用
                var pcchecks = context.customertables;

                //入力した仕入先が存在するかチェックする用
                var icchecks = context.goodstables;


                foreach (var item in items)
                {
                    lastid = item.id; //現在登録されているdbの一番最後のidを取得
                }

                DataTable dt = new DataTable(); //登録するためのデータテーブルを作成
                DataRow dr = dt.NewRow(); //挿入するデータの行を作成


                string[] regiarray = new string[7]; //dtのヘッダを定義

                regiarray[0] = "id";
                regiarray[1] = "customergroup";
                regiarray[2] = "importcode";
                regiarray[3] = "nonyuritu";
                regiarray[4] = "parts";
                regiarray[5] = "repair";
                regiarray[6] = "remarks";

                for (int i = 0; i < 7; i++)
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


                if (dr[1].ToString() == "")
                {
                    errmsg.Visible = true;
                    errmsg.Text = "得意先グループコードが入力されていません";
                    return;
                }
                else
                if (dr[2].ToString() == "")
                {
                    errmsg.Visible = true;
                    errmsg.Text = "メーカーコードが入力されていません";
                    return;
                }

                int pcnotfound = 0;
                int icnotfound = 0;

                //入力された得意先コードがマスタに存在するかチェック
                foreach (var pccheck in pcchecks)
                {
                    if (dr[1].ToString() != pccheck.customergroup)
                    {
                        pcnotfound = 1;
                    }
                    else
                    {
                        pcnotfound = 0;
                        break;
                    }
                }

                //入力された仕入先コードがマスタに存在するかチェック
                foreach (var iccheck in icchecks)
                {
                    if (dr[2].ToString() != iccheck.importcode)
                    {
                        icnotfound = 1;
                    }
                    else
                    {
                        icnotfound = 0;
                        break;
                    }
                }

                //得意先が見つからなかった場合はreturnする
                if (pcnotfound == 1)
                {
                    errmsg.Visible = true;
                    errmsg.Text = "得意先グループコードが見つかりませんでした。内容を確認してください";
                    return;
                }
                else
                if (icnotfound == 1)
                {
                    errmsg.Visible = true;
                    errmsg.Text = "メーカーコードが見つかりませんでした。内容を確認してください";
                    return;
                }
                else
                //既存メーカーまたは得意先がすでに登録されていた場合
                if (pcnotfound == 0 && icnotfound == 0)
                {
                    errmsg.Visible = true;
                    errmsg.Text = "すでにその得意先コードとメーカーコードにて登録があります";
                    return;
                }
                else
                {
                    errmsg.Visible = true;
                    errmsg.Text = "登録が完了しました";
                }


                //すべてのreturnをスルーした場合に初めてdtにdrをaddすることが可能
                dt.Rows.Add(dr);

                //準備したdtを用いて，sqlserverのテーブルへのデータ追加
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
















        //markuptable更新処理
        protected void update(object sender, EventArgs e)
        {


            //CSV更新か，画面更新かをここで判断する
            if(csvselect.HasFile)
                //ファイルが選択されていた場合
            {
                //CSVファイルかどうかの確認
                if(csvselect.PostedFile.ContentType == "application/vnd.ms-excel")
                {
                    var context = new markupmodel();

                    var checks = context.markuptables;



                    var file = csvselect.PostedFile;

                    string filepath = string.Empty;


                    file.SaveAs(Path.GetTempPath() + Path.GetFileName(file.FileName));

                    filepath = Path.GetTempPath() + Path.GetFileName(file.FileName);

                    DataTable dt = new DataTable();


                    string[] filecontent = File.ReadAllLines(filepath, Encoding.GetEncoding("Shift-jis"));

                    if (filecontent.Count() > 0)
                    {
                        //dtにヘッダーを定義
                        string[] columns = filecontent[0].Split(',');
                        for (int c = 0; c < columns.Count(); c++)
                        {
                            dt.Columns.Add(columns[c]);
                        }


                        //dtにデータを格納する：CSVにてid行も定義する
                        for (int r = 1; r < filecontent.Count(); r++)
                        {
                            string[] rowsdata = filecontent[r].Split(',');
                            dt.Rows.Add(rowsdata);
                        }

                    }

                    int[] flagarray = new int[dt.Rows.Count];


                    //dtのidに対して，markuptableのidを取得し代入する
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        foreach (var check in checks)
                        {
                            if (dt.Rows[i][1].ToString() == check.customergroup && dt.Rows[i][2].ToString() == check.importcode)
                            {
                                dt.Rows[i][0] = check.id.ToString();
                                flagarray[i] = 0;
                                break;
                            }
                            else
                            {
                                flagarray[i] = 1;
                            }
                        }

                    }



                    //markuptableに登録されていないデータを表示する
                    string errlist = "";
                    int checksum = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (flagarray[i] == 1)
                        {
                            errlist += string.Format("{0},", i + 2);
                            checksum++;
                        }

                        //flagarray配列に1つでもエラーがあれば，errmsg.textにエラーを表示する
                        if (i == dt.Rows.Count - 1 && checksum != 0)
                        {
                            errmsg.Visible = true;
                            errmsg.Text = errlist + "行目の得意先グループコードまたはメーカーコードはデータに登録されていません";
                            return;
                        }

                    }

                    //markuptableを更新する処理
                    string conString = ConfigurationManager.ConnectionStrings["markupmodel"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlCommand command = new SqlCommand("", con))
                        {
                            con.Open();

                            command.CommandTimeout = 300;
                            for (int r = 0; r < dt.Rows.Count; r++)
                            {
                                command.CommandText = "UPDATE markuptable SET customergroup ='" + dt.Rows[r][1] + "',importcode ='" + dt.Rows[r][2] + "',nonyuritu ='" + dt.Rows[r][3] + "',parts='" + dt.Rows[r][4] + "',repair='" + dt.Rows[r][5] + "',remarks ='" + dt.Rows[r][6] + "' WHERE id =" + dt.Rows[r][0];
                                //コマンドテキストを実行したあとに一回ずつexecutenonqueryを実行しなければならない
                                command.ExecuteNonQuery();
                            }


                            con.Close();

                        }
                    }

                    errmsg.Visible = true;
                    errmsg.Text = "更新完了しました";

                }
                else
                //CSVファイルではなかった場合
                {
                    errmsg.Visible = true;
                    errmsg.Text = "CSVファイルを選択してください";
                    return;
                }

                
                





            }

            //ウェブ画面処理の場合
            else
            {

                var context = new markupmodel();

                var checks = context.markuptables;

                DataTable dt = new DataTable();

                DataRow dr = dt.NewRow();


                string[] updatearray = new string[7]; //dtのヘッダを定義



                //得意先及び，メーカーコードが空欄だった場合は，全通り検索するのに時間を要するため，空欄チェックを事前に実施する
                if (incustomer.Text == "" || inimport.Text == "")
                {
                    errmsg.Visible = true;
                    errmsg.Text = "得意先グループコードまたはメーカーコードが空欄です";
                    return;
                }



                int notfound = 0;

                foreach (var check in checks)
                {
                    if (check.customergroup == incustomer.Text && check.importcode == inimport.Text)
                    {
                        updatearray[0] = check.id.ToString(); //markuptableから更新すべきフィールドのデータのidを取得する
                        notfound = 0;
                        break;
                    }
                    else
                    {
                        notfound = 1;
                    }
                }


                if (notfound == 1)
                {
                    errmsg.Visible = true;
                    errmsg.Text = "更新すべきデータがありません";
                    return;
                }


                //id以外の情報をarrayに入れ込む
                updatearray[1] = incustomer.Text;
                updatearray[2] = inimport.Text;
                updatearray[3] = innonyuritu.Text;
                updatearray[4] = inparts.Text;
                updatearray[5] = inrepair.Text;
                updatearray[6] = inremarks.Text;

                //markuptableを更新する処理
                string conString = ConfigurationManager.ConnectionStrings["markupmodel"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("", con))
                    {

                        con.Open();

                        //sqlcommandにて更新を行う　ダブルコーテで囲まれているため，シングルコーテや空白をしっかりと記述しなければならない
                        command.CommandTimeout = 300;
                        command.CommandText = "UPDATE markuptable SET customergroup ='" + updatearray[1] + "',importcode ='" + updatearray[2] + "',nonyuritu ='" + updatearray[3] + "',parts='" + updatearray[4] + "',repair='" + updatearray[5] + "',remarks ='" + updatearray[6] + "' WHERE id =" + updatearray[0];
                        command.ExecuteNonQuery();

                        con.Close();

                    }
                }

                errmsg.Visible = true;
                errmsg.Text = "更新完了しました";

            }


        }
    }
}