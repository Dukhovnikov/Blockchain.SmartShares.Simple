<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Blockchain.WebApplication.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <asp:Label ID="Label2" runat="server" Text="Выберете пользователя"></asp:Label>
        <b />
        <asp:DropDownList ID="DropDownList1" runat="server" Width="276px"></asp:DropDownList>
        <b />
        <asp:LinkButton ID="LinkButtonUploadMiningUser" runat="server" OnClick="LinkButtonUploadMiningUser_Click">Майнить</asp:LinkButton>
        <hr />
    </div>
    <div class="jumbotron">
        <asp:Label ID="LabelMiningStatus" runat="server" Text="Результат майнинга:"></asp:Label>
        <b />
        <asp:Label ID="Label1" runat="server" Text="" Style="word-wrap: normal" Width="100" Height="100" Font-Size="10"></asp:Label>
    </div>
</asp:Content>
