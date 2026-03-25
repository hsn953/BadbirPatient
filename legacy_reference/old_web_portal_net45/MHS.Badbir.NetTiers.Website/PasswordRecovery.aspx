<%@ Page Title="Password recovery" Language="C#" MasterPageFile="~/MasterPages/bbDefault.master"
    AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="PasswordRecovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .dada
        {
            width: 19em;
            padding: 1em;
            margin-left: auto;
            margin-right: auto;
            margin-top: 2em;
            margin-bottom:2em;
            font-family: Tahoma, Sans-Serif;
            font-size: medium;
            border: #d5d5d5 10px solid;
            border-radius: 30px;

            /* Transitions added 25/11/2019 */
            -webkit-transition: all 4.0s ease;
            -moz-transition: all 4.0s ease;
            -o-transition: all 4.0s ease;
            transition: all 4.0s ease;
        }
        .dada div
        {
        	margin-bottom:1em;
        }
        .AspNet-PasswordRecovery-UserName-TitlePanel, .AspNet-PasswordRecovery-Question-TitlePanel
        {
            text-align: center;
            color: #1c94c4;
            font-size: 120%;
            padding: 5px 0px 12px 0px;
        }
        .AspNet-PasswordRecovery-UserName-UserPanel input
        {
            width: 15em;
            height: 1.5em;
            vertical-align: middle;
            border-color: #b3b3b3;
            border-style: solid;
            border-width: 1px;
            border-radius: 3px;
            padding: 3px;
            padding-right: 0px;
            margin-right: 0px;
            margin-top: 5px;
        }
        .AspNet-PasswordRecovery-QuestionPanel label, .AspNet-PasswordRecovery-AnswerPanel label
        {
        	display:none;
        }
       .AspNet-PasswordRecovery-Question-UserPanel label,.AspNet-PasswordRecovery-UserName-UserPanel label
        {
        	width:25em;
        	margin-right:20em;
        }
        .AspNet-PasswordRecovery-Question-UserPanel input
        {
        	width:15em;
            border-color: #b3b3b3;
            border-style: dotted;
            border-width: 1px;
            border-radius: 3px;
            background: none;
            padding: 3px;
            padding-right: 0px;
            margin-right: 0px;
        }
        .AspNet-PasswordRecovery-QuestionPanel input
        {
        	width:25em;
            border-style: none;
            background: none;
            padding: 3px;
            padding-right: 0px;
            margin-right: 0px;
            margin-bottom:-1em;
        }
        .AspNet-PasswordRecovery-AnswerPanel input
        {
        	width:18em;
            border-color: #b3b3b3;
            border-style: solid;
            border-width: 1px;
            border-radius: 3px;
            background: none;
            padding: 3px;
            padding-right: 0px;
            margin-right: 0px;
            margin-bottom:-1em;
        }
        
        

        .AspNet-PasswordRecovery-FailurePanel {
            border: 1px solid #F00;
            border-radius: 10px;
            background-color: #FDD;
            box-shadow: 3px 3px 6px #cccccc;
            padding: 8px;
        }
          
        #ctl00_MainContent_PasswordRecovery1_UserNameContainerID_SubmitButton {

        }

    </style>

    <script type="text/javascript">

        var disableTimeout = true;
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="dada">
        <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" UserNameLabelText=" Username: "
            SubmitButtonStyle-CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all fas iconbtn"
            SubmitButtonText="&#xf2ea; Reset password"
            UserNameFailureText="We couldn't find that username, please ensure you have entered it correctly."
            UserNameInstructionText="Please enter your username to reset your password."
            QuestionInstructionText="Answer the following security question to receive your password."
            OnUserLookupError="PasswordRecovery1_UserLookupError" OnLoad="PasswordRecovery1_Load"
            OnSendingMail="PasswordRecovery1_OnSendingMail"  QuestionFailureText="Sorry, that's not the answer to your security question. Please try again.">
            <SuccessTemplate>
                <div class="message-notification" style="text-align: center;">
                    Your password has been sent to you in an email.
                </div>
            </SuccessTemplate>
        </asp:PasswordRecovery>
        


    </div>
</asp:Content>
