<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditCustomer.aspx.cs" Inherits="FossLock.Web.Admin.AddEditCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <asp:Label AssociatedControlID="NameTbx" Text="Name" runat="server" />
        <asp:TextBox ID="NameTbx" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="ContactFNameTbx" Text="Contact First Name" runat="server" />
        <asp:TextBox ID="ContactFNameTbx" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="ContactLNameTbx" Text="Contact Last Name" runat="server" />
        <asp:TextBox ID="ContactLNameTbx" runat="server" />
    </div>

    <!-- can license prereleases -->

    <div>
        <asp:Label AssociatedControlID="Address1Tbx" Text="Address 1" runat="server" />
        <asp:TextBox ID="Address1Tbx" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="Address2Tbx" Text="Address 2" runat="server" />
        <asp:TextBox ID="Address2Tbx" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="CityTbx" Text="City/State" runat="server" />
        <asp:TextBox ID="CityTbx" runat="server" /> <!-- city then state tbx -->
        <asp:TextBox ID="StateTbx" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="PostalCodeTbx" Text="Postal Code" runat="server" />
        <asp:TextBox ID="PostalCodeTbx" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="CountryTbx" Text="Country" runat="server" />
        <asp:TextBox ID="CountryTbx" MaxLength="3" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="Phone1Tbx" Text="Phone 1" runat="server" />
        <asp:TextBox ID="Phone1Tbx" TextMode="Phone" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="Phone2Tbx" Text="Phone 2" runat="server" />
        <asp:TextBox ID="Phone2Tbx" TextMode="Phone" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="FaxTbx" Text="Fax" runat="server" />
        <asp:TextBox ID="FaxTbx" TextMode="Phone" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="EmailTbx" Text="E-mail" runat="server" />
        <asp:TextBox ID="EmailTbx" TextMode="Email" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="NotesTbx" Text="Notes" runat="server" />
        <asp:TextBox ID="NotesTbx" TextMode="MultiLine" runat="server" />
    </div>

    <!-- Licenses -->

</asp:Content>
