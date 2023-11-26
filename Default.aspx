<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Ediable_Repeater._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function OnSave(obj)
        {
            // Find the row this button is in
            var tr = $(obj).closest("tr");
            // Get the value from the edit control
            var firstNameEdit = tr.find("[id*='firstNameEdit']").val();
            // assign value to hidden input
            tr.find("[id*='firstNameHidden']").val(firstNameEdit);
            var lastNameEdit = tr.find("[id*='lastNameEdit']").val();
            tr.find("[id*='lastNameHidden']").val(lastNameEdit);
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Repeater runat="server" ID="Repeater1" OnItemCommand="OnItemCommand" OnItemDataBound="OnItemDataBound">
                <HeaderTemplate>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th></th>
                            <th>First Name</th>
                            <th>Last Name</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:ImageButton ID="Edit" ImageUrl="~/Images/EditDocument.png" runat="server" CommandName="edit" />
                            <asp:ImageButton ID="Delete" ImageUrl="~/Images/Delete_black_32x32.png" runat="server"
                                CommandName="delete" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="firstName"><%# Eval("FirstName") %></asp:Label>
                            <asp:PlaceHolder runat="server" ID="firstNameEditPlaceholder" />
                            <input type="hidden" runat="server" id="firstNameHidden" visible="false" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lastName"><%# Eval("LastName") %></asp:Label>
                            <asp:PlaceHolder runat="server" ID="lastNameEditPlaceholder" />
                            <input type="hidden" runat="server" id="lastNameHidden" visible="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td>
                            <asp:ImageButton ID="Delete" ImageUrl="~/Images/112_Plus_Blue_32x32_72.png" runat="server"
                                OnClick="OnAddRecord" />
                        </td>
                        <td><asp:TextBox runat="server" ID="NewFirstName" /></td>
                        <td><asp:TextBox runat="server" ID="NewLastName" /></td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
