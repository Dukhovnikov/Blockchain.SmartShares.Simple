<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Blockchain.WebApplication.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <div class="text-center">
            <asp:Label ID="Label3" runat="server"
                Text="User's configuration" Font-Size="18pt" Font-Bold="True"
                Font="Roboto" ForeColor="Purple"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Position="Right"
                Text="Choose blockchain"
                Style="text-align: center; font-size: 16pt;" Height="30px"></asp:Label>
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server" WWidth="50px" Width="351px"></asp:DropDownList>
            <br />
            <asp:LinkButton ID="LinkButtonUploadMiningUser" runat="server" Style="text-align: center; font-size: x-large;" OnClick="LinkButtonUploadMiningUser_Click">Start maining</asp:LinkButton>
        </div>
    </div>
    <div style="text-align: center">
        <asp:Label ID="Label4" runat="server" Text="Results" Font-Size="18pt" Font-Bold="True" Font="Roboto" ForeColor="Purple"></asp:Label>
    </div>
    <div style="text-align: justify-all"><textarea id="TextArea1" rows="1020" runat="server" name="S1" role="textbox" style="position: static; width: 1020px; height: 420px; left: 15px;" disabled="True"></textarea></div>
    <hr />
    <asp:Label ID="Label5" runat="server" Style="word-wrap: normal" Text="© 2017, Dukhovnikov, Nikulina (students of group 3302)" Font-Size="8pt"></asp:Label>
</asp:Content>
