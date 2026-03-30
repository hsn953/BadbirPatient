<%@ Page Title="Register a new account" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/bbDefault.master"
    CodeFile="Register.aspx.cs" Inherits="Register" %>

<%@ MasterType VirtualPath="~/MasterPages/bbDefault.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
     <script type="text/javascript">

        var disableTimeout = true;

    </script>


    <style type="text/css">
        .login-form {
            width: 340px;
            margin: 50px auto;
            font-size: 15px;
        }

            .login-form form {
                margin-bottom: 15px;
                background: #f7f7f7;
                box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
                padding: 30px;
            }

            .login-form h2 {
                margin: 0 0 15px;
            }

        .form-control, .btn {
            min-height: 38px;
            border-radius: 2px;
        }

        .btn {
            font-size: 15px;
            font-weight: bold;
        }
    </style>







</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h1 class="text-center">BADBIR Patient Portal</h1>
    <div class="login-form" runat="server">

        <form runat="server">
            <h3 class="text-center">First Use Activation</h3>
            <div class="form-group" runat="server">
                <asp:Literal runat="server" ID="FailureText" EnableViewState="false"></asp:Literal>
                <asp:ValidationSummary runat="server" ValidationGroup="LoginControl" DisplayMode="List" ForeColor="Red" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="NHSNumber" ErrorMessage="Enter your NHS Number" ValidationGroup="LoginControl" ToolTip="Enter your NHS Number" Display="None" ID="RequiredFieldValidator3">Enter your NHS Number</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="NHSNumber" ErrorMessage="NHS Number is a 10 digit number" ValidationGroup="LoginControl" Display="None" ValidationExpression="([0-9]{10})">Please enter 10 digit number</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TbDOB" ErrorMessage="Enter your date of birth (dd/mm/yyyy)" ValidationGroup="LoginControl" ToolTip="Enter your date of birth (dd/mm/yyyy)" ID="RequiredFieldValidator4" Display="None">Date of birth required</asp:RequiredFieldValidator>
                <asp:RangeValidator runat="server" ControlToValidate="TbDOB" ErrorMessage="Incorrect date of birth" Type="Date" ValidationGroup="LoginControl" MinimumValue="01/01/1910" MaximumValue="01/01/2021" Display="None">Incorrect Date of birth</asp:RangeValidator>

            </div>

            <div class="form-group">
                <label class="col-form-label" for="NHSNumber">NHS Number</label>
                <asp:TextBox ID="NHSNumber" class="form-control col-sm-10" runat="server" required="required" placeholder="" type="number" MaxLength="10"
                    AutoCompleteType="None" />
            </div>
            <div class="form-group">
                <label for="dob">Date of birth</label>
                <asp:TextBox runat="server" ID="TbDOB" placeholder="dd/mm/yyyy" type="date"
                    required="required" CssClass="form-control" />
            </div>


            <div class="form-group">
                <label for="fNameInit">First Name Initial</label>
                <asp:TextBox runat="server" ID="fNameInit" MaxLength="1" Width="2em"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="fNameInit" ErrorMessage="Enter your first name initial" ValidationGroup="LoginControl" ID="RequiredFieldValidator1" Display="None">First name initial required</asp:RequiredFieldValidator>
            </div>


            <div class="form-group">
                <label for="lNameInit">Last Name Initial</label>
                <asp:TextBox runat="server" ID="lNameInit" MaxLength="1" Width="2em"></asp:TextBox>

                <asp:RequiredFieldValidator runat="server" ControlToValidate="lNameInit" ErrorMessage="Enter your last name initial" ValidationGroup="LoginControl" ID="RequiredFieldValidator2" Display="None">Last name initial required</asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-block" CausesValidation="true" ValidationGroup="LoginControl" runat="server" ID="register" Text="Activate Patient Account" OnClick="createPatientAccount" />

            </div>

        </form>
                        <p class="text-center">Already activated? <a href="Default.aspx">Login</a></p>


    </div>

    <script type="text/javascript">

        console.log("ready");

        document.getElementsByClassName("textbox")[0].className = "form-control";
        document.getElementsByClassName("textbox")[0].className = "form-control";
        document.getElementsByClassName("textbox")[0].className = "form-control";
        document.getElementsByClassName("textbox")[0].className = "form-control";
    
</script>



</asp:Content>
