<%@ Page Title="Manage Account" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/bbDefault.master" 
    CodeFile="ManageAccount.aspx.cs" Inherits="ManageAccount" %>
<%@ MasterType VirtualPath="~/MasterPages/bbDefault.master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .hEdit {width:11em}
        
        ul.radiobuttonlist {
            margin: 0;
            padding: 0;
        }
        .radiobuttonlist label {margin-left:1em;}
        
        .AspNet-DetailsView-Name {font-weight:bold; width:200px; display:inline-table;}
        .AspNet-DetailsView-Value {padding-left:50px; display:inline-table}
        
        .Centres .AspNet-CheckBoxList-Selected
        {
            background-color:#90ee90;
        }
        #content div.AspNet-DetailsView-Data li {padding:5px;}
        
        .Email input {width:20em;}
        .Comment input {width:20em;}
        
        .Centres label {width:auto;}
    </style>
    
    
    <script type="text/javascript">
        var resp;
 
    
  
        function getLocation(ip) {
            try{
                $.getJSON('//freegeoip.net/json/'+ip, function(data) {
                    resp = data;
                    var inHtml = "<p>";
                    if (resp == null)
                        inHtml += "Unable to resolve IP address";
                    else {
                        inHtml += "IP: " + resp.ip + "<br/>Country: " + resp.country_name + "<br/>Region: " + resp.region_name + "<br/>City/Town: " + resp.city +"<br/>Time Zone: "+resp.time_zone+ "<br/><br/>Information from freegeoip.net";
                    }
                    inHtml += "</p>"

                    $("#myPopup").html(inHtml).dialog({ modal: false,
                        buttons: { Ok: function() { $(this).dialog("close"); } }
                    });
                });
                
            }catch(err){
            alert("Unable to get details");
            }
        }
    
    
    </script>
    
    <script type="text/javascript">

        function PrintElem(elem) {
            var mywindow = window.open('', 'PRINT', 'height=400,width=600');

            mywindow.document.write('<html><head><title>BADBIR OTT Codes Print</title>');
            mywindow.document.write('</head><body >');
            mywindow.document.write('<h1>BADBIR OTT Codes</h1>');
            mywindow.document.write(document.getElementById(elem).innerHTML);
            mywindow.document.write('<br/><br/>This is private information and must be kept safe.</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            mywindow.close();

            return true;
        }


        function downloadInnerHtml(filename, elId, mimeType) {
            var elHtml = document.getElementById(elId).innerHTML;
            var link = document.createElement('a');
            mimeType = mimeType || 'text/plain';

            link.setAttribute('download', filename);
            link.setAttribute('href', 'data:' + mimeType + ';charset=utf-8,' + encodeURIComponent(elHtml));
            link.click();
        }
    
    
    
    </script>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    

   <fieldset>
        <legend>Your Account</legend>    
    
<asp:DetailsView DataSourceID="AdditionalUserData" DataKeyNames="BadbiRuserid" AutoGenerateRows="False" ID="UserAdditionalInfo" runat="server"  >
                <Fields>
                    <asp:TemplateField HeaderText="Name"><ItemTemplate><%#Eval("Title")%> <%#Eval("FName")%> <%#Eval("LName")%></ItemTemplate></asp:TemplateField>
                    <asp:BoundField DataField="Position" HeaderText="Position"  />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" ReadOnly="true" />
                    <asp:BoundField DataField="Hospital" HeaderText="Hospital (At Registration)" ReadOnly="true" />
                    <asp:BoundField DataField="BaselineTrainingDate" HeaderText="Baseline Training Date" DataFormatString="{0:D}" ReadOnly="true" />
                    <asp:BoundField DataField="BaselineTrainingBy" HeaderText="Baseline Training By" ReadOnly="true" />
                    <asp:BoundField DataField="FupTrainingDate" HeaderText="FUP Training Date" DataFormatString="{0:D}" ReadOnly="true" />
                    <asp:BoundField DataField="FupTrainingBy" HeaderText="FUP Training By" ReadOnly="true" />
                    <asp:TemplateField HeaderText="Two Factor Authentication"><ItemTemplate> <%# (Eval("Require2Fa") == null || Eval("Require2Fa").Equals(false)) ? "<font color='red'>Disabled</font>" : "<font color='green'>Activated</font>"%> </ItemTemplate></asp:TemplateField>
                </Fields>
            </asp:DetailsView>
                        
            <asp:DetailsView AutoGenerateRows="False" DataSourceID="MemberData" ID="UserInfo" runat="server" >      
                <Fields>
	                <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-CssClass="Email"></asp:BoundField>
	                <asp:BoundField DataField="CreationDate" HeaderText="Creation Date" ReadOnly="True"></asp:BoundField>
	                <asp:BoundField DataField="LastLockoutDate" HeaderText="Last Lockout Date" ReadOnly="True"></asp:BoundField>
	                <asp:BoundField DataField="LastPasswordChangedDate" HeaderText="Last Password Changed Date" ReadOnly="True"></asp:BoundField>
               </Fields>
            </asp:DetailsView>
    

            
            <data:BbAdditionalUserDetailDataSource ID="AdditionalUserData" runat="server" SelectMethod="GetByUserid">               
            </data:BbAdditionalUserDetailDataSource>
            
            
            <asp:ObjectDataSource ID="MemberData" runat="server" DataObjectTypeName="System.Web.Security.MembershipUser" 
            SelectMethod="GetUser" UpdateMethod="UpdateUser" TypeName="System.Web.Security.Membership">
            </asp:ObjectDataSource>
    
    
    
           <br />
    
    <asp:HyperLink ID="HyperLinkChangePwd"  NavigateUrl ="ChangePassword.aspx" runat="server"> Change Password</asp:HyperLink>
    
           <br />           <br />

         
   </fieldset> 
   
   
    

    <fieldset runat="server" id="twoFA" visible="false">
        <legend>Manage Two Factor Authentication Settings</legend>
        Two factor authentication is active on your account. Upon login, you will be asked to confirm a code in addition to your password. 
        <br />Once completed, this authentication lasts for 24 hours on the web browser provided your computer configurations and network do not change. 
        <br />
            
        <h3>One Time Tokens</h3>
        <asp:Button ID="btn_NewTokens" runat="server" Text="Get New Tokens" OnClick="getOTT" /><br />
            If you are likely to have limited access to your registered phone, you can obtain authentication codes in advance. 
        <br />
        <div runat="server" ID="codeLiteral" style="font-family: monospace; border:green dotted 1px; margin:  5px 1px 5px 1px; padding:5px; max-width: 400px; font-size:medium"></div>

        <br />
        <h3>Deauthenticate</h3>
            <asp:LinkButton runat="server" ID="deactivateOne" Text="Deactivate All Authentications" OnClick="deauthenticate2FA"   /> 
            <br />
            Revoke two factor authentication for the current browser or all authenticated browsers immediately. This will not log you out but the next time you log in, you will require a new authentication.
        <br />
       
        <h3>Registered Numbers</h3>
        List, delete or add new numbers. (This funcitonality will be added soon. Please contact BADBIR to modify your contact details if required.)
 <br />
        <asp:Button ID="Button1" runat="server" Text="Get Existing Numbers" OnClick="getNumbersList" /><br />
        <div runat="server" ID="numbersLiteral" style="font-family: monospace; border:green dotted 1px; margin:  5px 1px 5px 1px; padding:5px; max-width: 400px; font-size:medium">
        <b>Saved Numbers</b><br/>The following numbers are added to the authentication system for 2FA code delivery<br/>
        <asp:GridView runat="server" ID="numberGridView" CssClass="mGrid" DataKeyNames="ID" OnSelectedIndexChanged="removeSelectedNumber" AutoGenerateColumns="false"  >
            <Columns>
                <asp:CommandField ButtonType="Button" SelectText="Remove" ShowSelectButton="true" />
                <asp:BoundField DataField="number" HeaderText="Phone Number" />
                <asp:BoundField DataField="commMedium" HeaderText="Method" />
                <asp:CheckBoxField DataField="isVerified" HeaderText="Verified?" />
                
            </Columns>
        </asp:GridView>
        </div>
        
        <br />
        <asp:Panel runat="server">
        <asp:DropDownList runat="server" ID="DDCommType"><asp:ListItem Selected="True" Value="1" Text="SMS" /><asp:ListItem Value="2" Text="Call" /></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="DDCommType" ErrorMessage="*" runat="server" Display="Dynamic" />
        <asp:DropDownList runat="server" ID="DDcountry"><asp:ListItem Selected="True" Value="44" Text="UK" /><asp:ListItem Value="353" Text="Eire" /></asp:DropDownList>
        <asp:RequiredFieldValidator ControlToValidate="DDcountry" ErrorMessage="*" runat="server" Display="Dynamic" />
        <asp:TextBox runat="server" ID="TextNewNumber" MaxLength="12"></asp:TextBox>
        <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "TextNewNumber" ID="RegularExpressionValidator3" 
        ValidationExpression = "^([\d]){9,11}$" runat="server" ErrorMessage="Only numbers allowed. Minimum 9 and Maximum 11 digits required. Ignore all special characters, spaces and 0 perfix"></asp:RegularExpressionValidator>
        <asp:Button ID="Button2" runat="server" Text="Add New Number" OnClick="addNumberToList" /><br />
        
 </asp:Panel>
    </fieldset>
    
    <br />
    
    
    
    
    
    
    <h3>Ten most recent logins for this account</h3> Please change your password or contact BADBIR if suspect any of these.
            <asp:GridView runat="server" ID="loginLogGrid" DataSourceID="LoginLogDS" AutoGenerateColumns="false" >
            <Columns>
                <asp:TemplateField HeaderText="IP Address"><ItemTemplate>
                    <%#Eval("Ip") %> &nbsp;&nbsp; <button onclick="getLocation('<%#Eval("Ip") %>');return false;">Details</button>
                
                </ItemTemplate></asp:TemplateField> 
                <asp:BoundField HeaderText="Session Start" DataField="LoginTime" DataFormatString="{0:U}" />
                <asp:BoundField HeaderText="Session End" DataField="LoginTime" DataFormatString="{0:U}"  />
                <asp:BoundField HeaderText="User Agent String" DataField="UserAgent" />
                
            </Columns>
            </asp:GridView>
            
        <data:BbLoginLogDataSource runat="server" ID="LoginLogDS" SelectMethod="LoginLog_getLastTenLoginsByUser" > 
        
        </data:BbLoginLogDataSource>
        
        
        
            <div id="myPopup" title="IP Lookup Details"></div>


    
</asp:Content>