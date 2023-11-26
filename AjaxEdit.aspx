<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AjaxEdit.aspx.cs" Inherits="Ediable_Repeater.AjaxEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Scripts/AjaxEdit.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater runat="server" ID="Repeater2">
        <HeaderTemplate>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th></th>
                    <th>First Name</th>
                    <th>Last Name</th>
                </tr>            
        </HeaderTemplate>
        <ItemTemplate>
                <tr id='<%# Eval("ID") %>'>
                    <td>
                        <image id="edit" src="Images/EditDocument.png" class="imgButton" />
                        <image id="delete" src="Images/Delete_black_32x32.png" class="imgButton"/>
                    </td>
                    <td>
                        <span ID="firstName"><%# Eval("FirstName") %></span>
                    </td>
                    <td>
                        <span ID="lastName"><%# Eval("LastName") %></span>
                    </td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
                <tr>
                    <td>
                        <image id="add" src="Images/112_Plus_Blue_32x32_72.png" class="imgButton" />
                    </td>
                    <td><input type="text" ID="newFirstName" /></td>
                    <td><input type="text" ID="newLastName" /></td>
                </tr>
        </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
