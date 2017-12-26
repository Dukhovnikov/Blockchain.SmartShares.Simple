<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Blockchain.WebApplication.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="Label2" runat="server" Width="1019px" Height="50px" Text="About blockchain" Font-Size="18pt" Font-Bold="True" Font="Roboto" ForeColor="Purple" Style="text-align: center"></asp:Label>
        <div style="text-align: justify-all">
            <textarea id="TextArea1" runat="server" name="S1" role="textbox" style="position: static; width: 1020px; height: 632px; left: 15px;" font-size="14pt" disabled="True"></textarea></div>
    </div>
        <hr />
        <asp:Label ID="Label3" runat="server" Style="word-wrap: normal" Text="© 2017, Dukhovnikov, Nikulina (students of group 3302)" Font-Size="8pt"></asp:Label>
</asp:Content>
