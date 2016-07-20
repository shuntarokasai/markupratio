<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Markup.aspx.cs" Inherits="掛率確認システム.Markup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="width:1600px;margin:10px auto;">
            <asp:TextBox ID="test1" runat="server"></asp:TextBox>
            <asp:TextBox ID="test2" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" />
        <asp:GridView ID="GridView1" runat="server" ItemType="掛率確認システム.tablejoin" SelectMethod="GridView1_GetData" AllowPaging="True" PageSize="100" AllowSorting="True" CellPadding="10" AutoGenerateColumns="False" Width="1600px" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:BoundField DataField="importcode" HeaderText="メーカーコード" SortExpression="importcode"/>
                <asp:BoundField DataField="importname" HeaderText="メーカー名" SortExpression="importname" />
                <asp:BoundField DataField="productcode" HeaderText="商品コード" SortExpression="productcode"  />
                <asp:BoundField DataField="productname" HeaderText="商品名" SortExpression="productname" />
                <asp:BoundField DataField="cost" HeaderText="NET" SortExpression="cost"  />
                <asp:BoundField DataField="price" HeaderText="上代" SortExpression="price" />
                <asp:BoundField DataField="importnonyuritu" HeaderText="仕入掛率" SortExpression="importnonyuritu" />
                <asp:BoundField DataField="nonyuritu" HeaderText="得意先掛率" SortExpression="nonyuritu" />
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
        <asp:Label ID="Label1" runat="server"></asp:Label>
    <div>
    
    </div>
    </form>
</body>
</html>
