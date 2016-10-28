<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form1.aspx.cs" Inherits="OOP2Lab4.Form1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Laboratorinis darbas 4</title>
    <link href="StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" CssClass="panel" > 
            <p>Iveskite menesio numeri:</p>
            <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
            <asp:Button ID="Button1" runat="server" CssClass="button" OnClick="Button1_Click" Text="Rodyti Rezultatus" />
            <asp:Button ID="Button3" runat="server" CssClass="button" Text="Rodyti Pradinius Duomenis" OnClick="Button3_Click" />
            <br />
            <h1>Agentu kruvis nurodyto menesio metu:</h1>
            <asp:TextBox ID="TextBox2" runat="server" Enabled="False" TextMode="MultiLine" CssClass="textbox" Height="300px" Width="500px"></asp:TextBox>
            <br />
        </asp:Panel>
        <asp:Panel ID="Panel2" visible="false" runat="server" CssClass="panel">
            <p>Iveskite agento koda:</p>
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" CssClass="button" OnClick="Button2_Click" Text="Rodyti Rezultatus" />
            <br />
            <br />
            <h1>Agento nesiojama prenumerata nurodyto menesio metu:</h1>
            <asp:TextBox ID="TextBox4" runat="server" Enabled="False" TextMode="MultiLine" CssClass="textbox" Height="300px" Width="500px" ></asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="Panel3" Visible="false" runat="server" CssClass="panel">
            <asp:TextBox ID="TextBox5" runat="server" Enabled="False" TextMode="MultiLine" CssClass="textbox" Height="300px" Width="500px" ></asp:TextBox>
        </asp:Panel>
    </form>
</body>
</html>
