<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="FossLock.Web.Admin.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Products</h2>
    <asp:GridView runat="server" AutoGenerateColumns="false" SelectMethod="GetProducts" ItemType="FossLock.Model.Product">
        <RowStyle BackColor="White" />
        <AlternatingRowStyle BackColor="#99CCFF" />
        <Columns>
            <asp:BoundField HeaderText="Name" DataField="Name" />
            <asp:BoundField HeaderText="Notes" DataField="Notes" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="AddEditProduct.aspx?id=<%#: Item.Id %>">View/Edit</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <a href="AddEditProduct.aspx">Add Product</a> <br />
    <input type="text" name="alertMessage"></input>
    <asp:Button ID="alertButton" runat="server" Text="Test Alert" />

</asp:Content>
