<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Markup_setting.aspx.cs" Inherits="掛率確認システム.Markup_setting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>掛率確認システム(仮)</title>
</head>
<body style="background-image:url(http://www.keramogranit.info/wp-content/uploads/2016/05/light-wood-background-with-wood-background.jpg)">
    <h2>掛率確認システム(仮)登録画面</h2>
    <h3><a href="Markup.aspx">TOPへ戻る</a></h3>


    <div style="position:absolute;top:0;right:0;">
        ver1.0

    </div>
    <form id="form1" runat="server">
        <div class="container" style="width:2000px;margin:10px auto;">

            <br /><br /><br />

            <div class="container" style="width:1800px; margin:10px auto; background-color:lightblue; border:solid;text-align:center;">

                <br />

                <asp:Label ID="errmsg" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                <br />

                <asp:Label ID="Label2" runat="server" Text="*メーカーコード："></asp:Label>
                <asp:TextBox ID="inimport" runat="server"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Text="*得意先グループコード：" ></asp:Label>
                <asp:TextBox ID="incustomer" runat="server"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Text="納入率："></asp:Label>
                <asp:TextBox ID="innonyuritu" runat="server"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Label ID="Label4" runat="server" Text="部品(%)："></asp:Label>
                <asp:TextBox ID="inparts" runat="server"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Label ID="Label5" runat="server" Text="修理(%)："></asp:Label>
                <asp:TextBox ID="inrepair" runat="server"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Label ID="Label6" runat="server" Text="備考："></asp:Label>
                <asp:TextBox ID="inremarks" runat="server"></asp:TextBox>
                &nbsp;&nbsp;
                <br /><br />
                <asp:Button ID="Button1" runat="server" Text="登録" OnClick="registration" OnClientClick="return confirm('登録してもよろしいですか?');" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" Text="更新" OnClick="update" OnClientClick="return confirm('更新してもよろしいですか?');" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="csvselect" runat="server" />
                <br /><br />

                <asp:Label ID="Label7" runat="server" Text="※得意先グループコードがない場合は得意先コードを入力してください" ForeColor="#009933"></asp:Label>
                

            </div>

        </div>
    <div>
    
    </div>
    </form>
</body>
</html>
