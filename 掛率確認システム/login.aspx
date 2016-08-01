<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="掛率確認システム.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>掛率確認システム(仮)ログイン</title>
</head>
<body style="background-image:url(http://www.keramogranit.info/wp-content/uploads/2016/05/light-wood-background-with-wood-background.jpg)">
    <h2>掛率確認システム(仮)</h2>

    <div style="position:absolute;top:0;right:0;">
        ver1.0

    </div>



    
    <form id="form1" runat="server">
        <div class="container" style="width:300px;margin:10px auto;">

                <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" BackColor="#EFF3FB" BorderColor="#B5C7DE" BorderPadding="6" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="1.0em" ForeColor="#333333" Height="202px" Width="332px" RememberMeText="資格情報を記憶する" FailureText="IDまたはパスワードが間違っています">
                    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                    <LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                    <TextBoxStyle Font-Size="1.0em" />
                    <TitleTextStyle BackColor="#507CD1" Font-Bold="True" Font-Size="1.0em" ForeColor="White" />
                </asp:Login>

        </div>
    </form>
    
</body>
</html>
