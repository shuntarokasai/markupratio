<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Markup.aspx.cs" Inherits="掛率確認システム.Markup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>掛率確認システム(仮)</title>
</head>
<body style="background-image:url(http://www.keramogranit.info/wp-content/uploads/2016/05/light-wood-background-with-wood-background.jpg)">
    <h2>掛率確認システム(仮)</h2>

    <div style="position:absolute;top:0;right:0;">
        ver1.0

    </div>


    <form id="form1" runat="server">
        <div class="container" style="width:1800px;margin:10px auto;">

            <div class="container" style="width:1800px; margin:10px auto; background-color:Highlight; border:solid;text-align:center;">
                <br />
                

                <asp:Label ID="Label1" runat="server" Text="*メーカーコード："></asp:Label>
                <asp:TextBox ID="importsearch" runat="server"></asp:TextBox>

                <asp:Label ID="Label2" runat="server" Text="*得意先コード："></asp:Label>
                <asp:TextBox ID="ccsearch" runat="server"></asp:TextBox>

                <asp:Label ID="Label3" runat="server" Text="得意先名："></asp:Label>
                <asp:TextBox ID="cnsearch" runat="server"></asp:TextBox>

                <asp:Label ID="Label4" runat="server" Text="*商品コード："></asp:Label>
                <asp:TextBox ID="pcsearch" runat="server"></asp:TextBox>

                <asp:Button ID="Button1" runat="server" Text="検索" />
                <br /><br />
                <asp:Label ID="errmsg" runat="server" Visible="false" ForeColor="OrangeRed"></asp:Label>
            </div>

            
            <asp:GridView ID="GridView1" runat="server" ItemType="掛率確認システム.tablejoin" SelectMethod="GridView1_GetData" UpdateMethod="GridView1_UpdateItem"  AllowPaging="True" PageSize="150" AllowSorting="True" CellPadding="10" AutoGenerateColumns="False" Width="1800px" ForeColor="#333333" GridLines="Both">
            <Columns>


                <asp:BoundField DataField="customercode" HeaderText="得意先コード" SortExpression="customercode" />
                <asp:BoundField DataField="customername" HeaderText="得意先名" SortExpression="customername" />
                <asp:BoundField DataField="importcode" HeaderText="メーカーコード" SortExpression="importcode"/>
                <asp:BoundField DataField="importname" HeaderText="メーカー名" SortExpression="importname" />
                <asp:BoundField DataField="productcode" HeaderText="商品コード" SortExpression="productcode"  />
                <asp:BoundField DataField="productname" HeaderText="商品名" SortExpression="productname" />
                <asp:BoundField DataField="cost" HeaderText="NET" SortExpression="cost"  />
                <asp:BoundField DataField="contractprice" HeaderText="契約単価" SortExpression="contractprice" />
                <asp:BoundField DataField="price" HeaderText="得意先上代" SortExpression="price" />
                <asp:BoundField DataField="masterprice" HeaderText="マスタ上代" SortExpression="price" />
                <asp:BoundField DataField="nonyuritu1" HeaderText="得意先掛率" SortExpression="nonyuritu1" />
                <asp:BoundField DataField="parts" HeaderText="部品" SortExpression="parts" />
                <asp:BoundField DataField="repair" HeaderText="修理" SortExpression="repair" />
                <asp:BoundField DataField="remarks" HeaderText="備考" SortExpression="remarks" />
                

            </Columns>


            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
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
