<%@ Page Title="Login" MasterPageFile="~/MasterPages/bbDefault.master"
    Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ MasterType VirtualPath="~/MasterPages/bbDefault.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


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







    <script type="text/javascript">

        var disableTimeout = true;




        function getPassword() {

            ctl00_MainContent_LoginControl_Password.value = "A@_" + ctl00_MainContent_LoginControl_dob.value
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">



    <h1 class="text-center">BADBIR Patient Portal</h1>

    <asp:Login ID="LoginControl" runat="server" UserNameLabelText=" NHS Number"
        PasswordLabelText=" Date of birth" UserNameRequiredErrorMessage="Enter your NHS Number"
        PasswordRequiredErrorMessage="Enter your date of birth (dd/mm/yyyy)"
        LoginButtonText="Log in" FailureTextStyle-BackColor="LightPink" FailureTextStyle-ForeColor="Red"
        OnLoggedIn="LoginControl_OnPatientLoggedIn" FailureTextStyle-CssClass="alert-danger"
        DestinationPageUrl="~/PatientApp/Default.aspx" DisplayRememberMe="false" CreateUserUrl="~/Register.aspx" CreateUserText="If this is the first time you are accessing your account, please activate your account here">
        <LayoutTemplate>

            <div class="login-form" runat="server">

                <form runat="server">
                    <h3 class="text-center">Login</h3>
                    <div class="form-group" runat="server">
                        <asp:Literal runat="server" ID="FailureText" EnableViewState="false" ></asp:Literal>
                        <asp:RequiredFieldValidator CssClass="alert-danger" runat="server" ControlToValidate="UserName" ErrorMessage="Enter your NHS Number" ValidationGroup="LoginControl" ToolTip="Enter your NHS Number" Display="None" ID="UserNameRequired">Enter your NHS Number</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="alert-danger" runat="server" ControlToValidate="UserName" ErrorMessage="NHS Number is a 10 digit number" ValidationGroup="LoginControl" Display="None" ValidationExpression="([0-9]{10})">Please enter 10 digit number</asp:RegularExpressionValidator>

                        <asp:RequiredFieldValidator CssClass="alert-danger"  runat="server" ControlToValidate="dob" ErrorMessage="Enter your date of birth (dd/mm/yyyy)" ValidationGroup="LoginControl" ToolTip="Enter your date of birth (dd/mm/yyyy)" ID="PasswordRequired" Display="None">Date of birth required</asp:RequiredFieldValidator>
                        <asp:RangeValidator CssClass="alert-danger" runat="server" ControlToValidate="dob" ErrorMessage="Incorrect date of birth" Type="Date" ValidationGroup="LoginControl" MinimumValue="01/01/1910" MaximumValue='<%# System.DateTime.Now.ToString("dd/MM/yyyy") %>' Display="None">Incorrect Date of birth</asp:RangeValidator>

                    </div>

                    <div class="form-group">

                        <label for="UserName">NHS Number</label>
                        <asp:TextBox ID="UserName" CssClass="form-control" runat="server" required="required" placeholder="" type="number" MaxLength="10"
                            AutoCompleteType="None"  />

                    </div>
                    <div class="form-group">
                        <label for="dob">Date of birth</label>
                        <asp:TextBox runat="server" ID="dob" placeholder="dd/mm/yyyy" type="date" required="required" CssClass="form-control" />

                        <asp:TextBox runat="server" ID="Password" style="display:none;"></asp:TextBox>

                    </div>
                    <div class="form-group">

                        <asp:Button runat="server" CommandName="Login" OnClientClick="getPassword();" CausesValidation="true"
                            Text="Log in" ValidationGroup="LoginControl" ID="LoginButton"
                            CssClass="btn btn-primary btn-block"></asp:Button>

                    </div>

                </form>

                <p class="text-center">First time logging in? <a href="Register.aspx">Activate your account</a></p>
            </div>

        </LayoutTemplate>
    </asp:Login>



<script type="text/jscript">

    console.log("ready");

    document.getElementsByClassName("textbox")[0].className = "form-control";
    document.getElementsByClassName("textbox")[0].className = "form-control";

</script>



</asp:Content>
