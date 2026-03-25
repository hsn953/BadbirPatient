<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/bbDefault.master" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>
<%@ MasterType VirtualPath="~/MasterPages/bbDefault.master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    <fieldset>
        <legend>Change Your Password</legend>
         <asp:ChangePassword
            ID="ChangePassword1"
            runat="server"
            PasswordLabelText="Existing Password"
            ChangePasswordTitleText=""
            InstructionText="Your new password must be at least 8 characters long, contain at least one number and one special character (a character that isn't a letter or number)."
            CancelDestinationPageUrl="ManageAccount.aspx"
            ContinueDestinationPageUrl="ManageAccount.aspx"
            >
            
         </asp:ChangePassword>
      
         
    </fieldset>
</asp:Content>