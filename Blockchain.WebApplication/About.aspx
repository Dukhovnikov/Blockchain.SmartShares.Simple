<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Blockchain.WebApplication.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron" style="text-align: justify-all">
        <asp:Label ID="Label2" runat="server" Width="800px" Height="50px" Text="Информация о блокчейне" Font-Size="18pt" Font-Bold="True" Font="Roboto" ForeColor="Purple" Style="text-align: center"></asp:Label>
        <asp:Label ID="Label1" runat="server" Font="Roboto" Font-Size="14pt"
            Style="word-wrap: initial" Width="950px" Height="614px"></asp:Label>
    </div>
        <hr/>
        <asp:Label ID="Label3" runat="server" Style="word-wrap: normal" Text="© 2017, Духовников Д.С., Никулина В.С. (студенты гр. 3302)" Font-Size="8pt"></asp:Label>
    </p>
</asp:Content>
