<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditProduct.aspx.cs" Inherits="FossLock.Web.Admin.AddEditProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        
    <asp:ValidationSummary ID="PageValidationSummary" runat="server" />

    <div>
        <asp:Label AssociatedControlID="NameTbx" Text="Name" runat="server" />
        <asp:TextBox ID="NameTbx" runat="server" />
        <asp:RequiredFieldValidator ControlToValidate="NameTbx" ErrorMessage="Name is a required field." Display="None" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="ReleaseDateTbx" Text="Release Date" runat="server" />
        <asp:TextBox ID="ReleaseDateTbx" runat="server" />
        <asp:RequiredFieldValidator ControlToValidate="ReleaseDateTbx" ErrorMessage="Release Date is a required field." Display="None" runat="server" />
        <asp:CompareValidator ControlToValidate="ReleaseDateTbx" ErrorMessage="Release Date is in an invalid format." Type="Date" Operator="DataTypeCheck" Display="None" runat="server" /> 
        <asp:RangeValidator ControlToValidate="ReleaseDateTbx" runat="server" Display="None" ID="DateRangeValidator" />  <!-- this is config'd in code behind -->
    </div>

    <div>
        <asp:Label AssociatedControlID="LockPropertiesClbx" Text="Default Hardware Locks" runat="server" />
        <asp:CheckBoxList ID="LockPropertiesClbx" runat="server" BorderColor="#333333" BorderStyle="Solid" BorderWidth="3px" RepeatColumns="3" RepeatDirection="Horizontal" BackColor="White" Width="100%" />
    </div>

    <div>
        <asp:Label AssociatedControlID="FailOnNullHardwareIdChx" Text="Fail On Null Hardware ID" runat="server" />
        <asp:CheckBox ID="FailOnNullHardwareIdChx" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="PermittedActivationTypesClbx" Text="Permitted Activation Types" runat="server" />
        <asp:CheckBoxList ID="PermittedActivationTypesClbx" runat="server" BackColor="White" BorderColor="#333333" BorderStyle="Solid" BorderWidth="3px" RepeatColumns="3" Width="100%" />
    </div>

    <div>
        <asp:Label AssociatedControlID="PermittedExpirationTypesClbx" Text="Permitted Expiration Types" runat="server" />
        <asp:CheckBoxList ID="PermittedExpirationTypesClbx" runat="server" BackColor="White" BorderColor="#333333" BorderStyle="Solid" BorderWidth="3px" RepeatColumns="3" Width="100%" />
    </div>

    <div>
        <asp:Label AssociatedControlID="MaximumTrialDaysTbx" Text="Maximum Trial Days" runat="server" />
        <asp:TextBox ID="MaximumTrialDaysTbx" TextMode="Number" runat="server" />
        <asp:RangeValidator MinimumValue="1" MaximumValue="365" ControlToValidate="MaximumTrialDaysTbx" ErrorMessage="Maximum Trial Days must be more than 0 and less than 365" Display="None" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="VersionStyleCbx" Text="Version Style" runat="server" />
        <asp:DropDownList ID="VersionStyleCbx" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="VersionLeewayCbx" Text="Version Leeway" runat="server"  />
        <asp:DropDownList ID="VersionLeewayCbx" runat="server" />
    </div>

    <div>
        <asp:Label AssociatedControlID="NotesTbx" Text="Notes" runat="server" />
        <asp:TextBox ID="NotesTbx" TextMode="MultiLine" runat="server" />
    </div>

    <!-- Available Features -->

    <!-- Versions -->

    <div>
        <asp:Button ID="SaveButton" Text="Save Changes" runat="server" /><br />
        <asp:Button ID="DeleteButton" Text="Delete" runat="server" OnClick="DeleteButton_Click" />
    </div>


</asp:Content>
