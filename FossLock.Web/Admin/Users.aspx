<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="FossLock.Web.Admin.Users" %>
<%@ Import Namespace="System.Web.Security" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Users</h2>

    <asp:GridView ItemType="System.Web.Security.MembershipUser" runat="server" SelectMethod="GetAllUsers" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="Username" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="LastActivityDate" HeaderText="Last Activity" />
            <asp:BoundField DataField="Comment" HeaderText="Notes" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="EditUser.aspx?username=<%#: Item.UserName %>">View/Edit</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <%-- is lockedout?  unlock --%>
    </asp:GridView>

    <a href="AddUser.aspx">Add User</a>

</asp:Content>
