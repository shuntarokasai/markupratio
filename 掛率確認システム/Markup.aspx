<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Markup.aspx.cs" Inherits="掛率確認システム.Markup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>掛率確認システム(仮)</title>
</head>
<body style="background-image:url(http://www.keramogranit.info/wp-content/uploads/2016/05/light-wood-background-with-wood-background.jpg)">
    <h2>掛率確認システム(仮)</h2>
    <h3><a href="Markup_setting.aspx">登録画面</a></h3>

    <div style="position:absolute;top:0;right:0;">
        ver1.0

    </div>


    <form id="form1" runat="server">
        <div style="position:absolute;top:10px;right:0;">
            <h3><asp:LinkButton ID="logout" runat="server" OnClick="logout_Click">ログアウト</asp:LinkButton></h3>

        </div>
        



        <div class="container" style="width:2000px;margin:10px auto;">

            <%--日付の表示--%>
            <div style="background-color:honeydew; border:solid 1px ; margin:10px auto; width:1800px; text-align:right;">

                <%
                    var dt = DateTime.Today;
                    var yesterdaydt = dt.AddDays(-1);
                    Response.Write("最終更新日：" +yesterdaydt.Month + "月" + yesterdaydt.Day + "日");
                %>
            </div>


            <div class="container" style="width:1800px; margin:10px auto; background-color:honeydew; border:solid 1px;text-align:center;">
                <br />
                

                <asp:Label ID="Label1" runat="server" Text="*メーカーコード："></asp:Label>
                <asp:TextBox ID="importsearch" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label2" runat="server" Text="*得意先コード："></asp:Label>
                <asp:TextBox ID="ccsearch" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Text="得意先名："></asp:Label>
                <asp:TextBox ID="cnsearch" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label4" runat="server" Text="*商品コード："></asp:Label>
                <asp:TextBox ID="pcsearch" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="検索" />
                <br /><br />
                <asp:Label ID="errmsg" runat="server" Visible="false" ForeColor="OrangeRed"></asp:Label>
                <br />
                <asp:Button ID="Button2" runat="server" Text="CSVダウンロード" OnClick="csvdownload" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button3" runat="server" Text="CSV全件出力" OnClick="csvdownload_all" />
                <br /><br />
            </div>

            
            <asp:GridView ID="GridView1" runat="server" ItemType="掛率確認システム.tablejoin" SelectMethod="GridView1_GetData" AllowPaging="True" PageSize="150" AllowSorting="True" CellPadding="10" AutoGenerateColumns="False" Width="2000px" ForeColor="#333333" GridLines="Both" RowStyle-Wrap="true">
            <Columns>


                <asp:BoundField DataField="customercode" HeaderText="得意先コード" SortExpression="customercode" ItemStyle-Width="150px" />
                <asp:BoundField DataField="customername" HeaderText="得意先名" SortExpression="customername" ItemStyle-Width="250px" />
                <asp:BoundField DataField="importcode" HeaderText="メーカーコード" SortExpression="importcode" ItemStyle-Width="200px"/>
                <asp:BoundField DataField="importname" HeaderText="メーカー名" SortExpression="importname" ItemStyle-Width="200px" />
                <asp:BoundField DataField="productcode" HeaderText="商品コード" SortExpression="productcode" ItemStyle-Width="100px"  />
                <asp:BoundField DataField="productname" HeaderText="商品名" SortExpression="productname" ItemStyle-Width="500px" />
                <asp:BoundField DataField="cost" HeaderText="NET" SortExpression="cost" ItemStyle-Width="25px"  />
                <asp:BoundField DataField="contractprice" HeaderText="下代" SortExpression="contractprice" ItemStyle-Width="25px" />
                <asp:BoundField DataField="price" HeaderText="上代" SortExpression="price" ItemStyle-Width="25px" />
                <asp:BoundField DataField="masterprice" HeaderText="マスタ上代" SortExpression="price" ItemStyle-Width="25px" />
                <asp:BoundField DataField="nonyuritu1" HeaderText="掛率" SortExpression="nonyuritu1" ItemStyle-Width="15px" />
                <asp:BoundField DataField="parts" HeaderText="部品(%)" SortExpression="parts" ItemStyle-Width="25px" />
                <asp:BoundField DataField="repair" HeaderText="修理(%)" SortExpression="repair" ItemStyle-Width="25px" />
                <asp:BoundField DataField="remarks" HeaderText="備考" SortExpression="remarks" ItemStyle-Width="630px" />
                <asp:BoundField DataField="revisiondate" HeaderText="最終単価改定日" SortExpression="remarks" ItemStyle-Width="100px" />
                

            </Columns>


            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Font-Size="Smaller" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
        </div>
    <div>
    
    </div>
    </form>
</body>
</html>
