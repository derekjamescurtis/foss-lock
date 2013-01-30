<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="FossLock.Web.Admin.AddEditUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <div>
        <asp:Label AssociatedControlID="UsernameTbx" Text="Username" runat="server" />
        <asp:TextBox ID="UsernameTbx" ReadOnly="true" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="EmailTbx" Text="Email" runat="server" />
        <asp:TextBox ID="EmailTbx" TextMode="Email" runat="server" />
    </div>

    <%-- show if user is locked out --%>
    <% if (this.MembershipUser.IsLockedOut) 
       { %>
    <div>
        <asp:Label AssociatedControlID="" Text="User Is Locked Out!" runat="server" />
        <%-- only show the unlock button to administrators --%>
        <% if (User.IsInRole(FossLock.Web.Account.RoleNames.ADMIN_ROLE)) { %>
        <asp:LinkButton ID="UnlockUserLnk" Text="Click to unlock user" />
        <% } %>
    </div>
    <% } %>

    <%-- password --%>
    <div>
        <asp:Label AssociatedControlID="PasswordTbx" Text="Password" runat="server" />
        <asp:TextBox ID="PasswordTbx" TextMode="Password" runat="server" />
        <br />

        <asp:Label AssociatedControlID="PasswordVerifyTbx" Text="Verify" runat="server" />
        <asp:TextBox ID="PasswordVerifyTbx" TextMode="Password" runat="server" />
    </div>

    <%-- pw question + answer --%>
    <div>
        <asp:Label AssociatedControlID="SecretQuestionTbx" Text="Secret Question" runat="server" />
        <asp:TextBox ID="SecretQuestionTbx" runat="server" />

        <br />
        <asp:Label AssociatedControlID="" Text="Answer" runat="server" />
        <asp:TextBox ID="SecretQuestionAnswerTbx" runat="server" />
    </div>

    <%-- notes --%>
    <div>
        <asp:Label AssociatedControlID="CommentTbx" Text="Notes" runat="server" />
        <asp:TextBox ID="CommentTbx" TextMode="MultiLine" runat="server" />
    </div>
    
    <div>
        <asp:Button ID="SaveButton" Text="Save Changes" runat="server" /><br />
        <asp:Button ID="DeleteButton" Text="Delete" runat="server" /> <%-- postback url is set in form_load event --%>
    </div>

</asp:Content>
