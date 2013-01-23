<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditProduct.aspx.cs" Inherits="FossLock.Web.Admin.AddEditProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <label for="nameTbx">Name</label>
    <input type="text" name="nameTbx" value="<%#: this.Product.Name %>" />

    <label for="releaseDateTbx">Release Date</label>
    <input type="date" name="releaseDateTbx" value="<%#: this.Product.ReleaseDate %>" />


    <label for="lockPropsDiv">Default Lock Properties</label>
    <div id="lockPropsDiv">
        <input type="checkbox" name="lockPropCpuChx" <% this.Product.DefaultLockProperties & FossLock.Core. %>
    </div>

    <!-- Available Features -->

    <!-- Versions -->




</asp:Content>
