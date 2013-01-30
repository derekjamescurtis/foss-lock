<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="FossLock.Web.Admin.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <div>
        <asp:Label AssociatedControlID="UsernameTbx" Text="Username" runat="server" />
        <asp:TextBox ID="UsernameTbx" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="EmailTbx" Text="Email" runat="server" />
        <asp:TextBox ID="TextBox1" runat="server" />
    </div>
    
    <div>
        
        <asp:Label ID="Label2" AssociatedControlID="UsernameTbx" Text="Username" runat="server" />
        <asp:TextBox ID="TextBox2" runat="server" />
    </div>

    <div>
        <asp:Label ID="Label3" AssociatedControlID="UsernameTbx" Text="Username" runat="server" />
        <asp:TextBox ID="TextBox3" runat="server" />
    </div>


    <div>
        <asp:Label AssociatedControlID="GroupsDdl" Text="Group" runat="server" />
        <asp:DropDownList ID="GroupsDdl" runat="server">
            <asp:ListItem>User</asp:ListItem>
            <asp:ListItem>Manager</asp:ListItem>
            <asp:ListItem>Administrator</asp:ListItem>
        </asp:DropDownList>
    </div>

    <%-- add  --%>

</asp:Content>
