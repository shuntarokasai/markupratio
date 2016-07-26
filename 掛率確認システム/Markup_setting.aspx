<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Markup_setting.aspx.cs" Inherits="掛率確認システム.Markup_setting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="得意先コード"></asp:Label>
        <asp:TextBox ID="incustomer" runat="server"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="メーカーコード"></asp:Label>
        <asp:TextBox ID="inimport" runat="server"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Text="納入率"></asp:Label>
        <asp:TextBox ID="innonyuritu" runat="server"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="部品"></asp:Label>
        <asp:TextBox ID="inparts" runat="server"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Text="修理"></asp:Label>
        <asp:TextBox ID="inrepair" runat="server"></asp:TextBox>
        <asp:Label ID="Label6" runat="server" Text="備考"></asp:Label>
        <asp:TextBox ID="inremarks" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="登録" OnClick="registration" />
    <div>
    
    </div>
    </form>
</body>
</html>
