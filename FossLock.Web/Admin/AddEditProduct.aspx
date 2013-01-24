<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditProduct.aspx.cs" Inherits="FossLock.Web.Admin.AddEditProduct" %>
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
        <asp:Label AssociatedControlID="ReleaseDateTbx" Text="Release Date" runat="server" />
        <asp:TextBox ID="ReleaseDateTbx" TextMode="Date" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="LockPropertiesClbx" Text="Default Hardware Locks" runat="server" />
        <asp:CheckBoxList ID="LockPropertiesClbx" runat="server" BorderColor="#333333" BorderStyle="Solid" BorderWidth="3px" RepeatColumns="3" RepeatDirection="Horizontal" BackColor="White" Width="100%" />
    </div>

    <asp:Label AssociatedControlID="FailOnNullHardwareIdChx" Text="Fail On Null Hardware ID" runat="server" />
    <asp:CheckBox ID="FailOnNullHardwareIdChx" runat="server" />

    <asp:Label AssociatedControlID="PermittedActivationTypesClbx" Text="Permitted Activation Types" runat="server" />
    <asp:CheckBoxList ID="PermittedActivationTypesClbx" runat="server" BackColor="White" BorderColor="#333333" BorderStyle="Solid" BorderWidth="3px" RepeatColumns="3" Width="100%" />


    <asp:Label AssociatedControlID="PermittedExpirationTypesClbx" Text="Permitted Expiration Types" runat="server" />
    <asp:CheckBoxList ID="PermittedExpirationTypesClbx" runat="server" BackColor="White" BorderColor="#333333" BorderStyle="Solid" BorderWidth="3px" RepeatColumns="3" Width="100%" />


    <asp:Label AssociatedControlID="MaximumTrialDaysTbx" Text="Maximum Trial Days" runat="server" />
    <asp:TextBox ID="MaximumTrialDaysTbx" TextMode="Number" runat="server" />

    <asp:Label AssociatedControlID="VersionStyleCbx" Text="Version Style" runat="server" />
    <asp:DropDownList ID="VersionStyleCbx" runat="server" />

    <asp:Label AssociatedControlID="VersionLeewayCbx" Text="Version Leeway" runat="server"  />
    <asp:DropDownList ID="VersionLeewayCbx" runat="server" />


    <div>
        <asp:Label AssociatedControlID="NotesTbx" Text="Notes" runat="server" />
        <asp:TextBox ID="NotesTbx" TextMode="MultiLine" runat="server" />
    </div>

    <!-- Available Features -->

    <!-- Versions -->




</asp:Content>
