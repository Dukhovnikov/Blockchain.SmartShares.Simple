<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Blockchain.WebApplication.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
    <div class="text-center">
        <asp:Label ID="Label3" runat="server" 
            Text="Пользовательские настройки" Font-Size="18pt" Font-Bold="True" 
            Font="Roboto" ForeColor="Purple"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Position="Right" 
            Text="Выберете пользователя" 
            style="text-align: center; font-size: 16pt;" Height="30px"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" WWidth="400px"></asp:DropDownList>
        <br />
        <asp:LinkButton ID="LinkButtonUploadMiningUser" runat="server" style="text-align: center; font-size: x-large;" OnClick="LinkButtonUploadMiningUser_Click">Майнить</asp:LinkButton>
    </div>
      </div>
    <div class="jumbotron">
    <div class="text-center">
        <asp:Label ID="Label4" runat="server" Text="Результат майнинга" Font-Size="18pt" Font-Bold="True" Font="Roboto" ForeColor="Purple"></asp:Label>
        <asp:Label ID="Label1" runat="server"  Style="word-wrap: normal; margin-top: 66px; font-size: medium;" Height="251px" Font-Size="10pt"></asp:Label>
    </div>
    </div>
    <hr/>
    <asp:Label ID="Label5" runat="server" Style="word-wrap: normal" Text="© 2017, Духовников Д.С., Никулина В.С. (студенты гр. 3302)" Font-Size="8pt"></asp:Label>
</asp:Content>
