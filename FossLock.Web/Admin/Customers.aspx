<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="FossLock.Web.Admin.Customers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    
    <h2>Customers</h2>
    <asp:GridView runat="server" AutoGenerateColumns="false" SelectMethod="GetCustomers" ItemType="FossLock.Model.Customer">
        <RowStyle BackColor="White" />
        <AlternatingRowStyle BackColor="#99CCFF" />
        <Columns>
            <asp:BoundField HeaderText="Name" DataField="Name" />
            <asp:BoundField HeaderText="Notes" DataField="Notes" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="AddEditCustomer.aspx?id=<%#: Item.Id %>">View/Edit</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <a href="AddEditCustomer.aspx">Add Customer</a> <br />

</asp:Content>
