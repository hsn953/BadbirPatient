<%@Page Title="Stronger authentication"  
    Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="mfaDefault" %>


<head>
	<meta http-equiv="X-UA-Compatible" content="IE=10" />	<meta http-equiv="Content-Type" content="text/html;charset=ISO-8859-1" />    <meta http-equiv="PRAGMA" content="NO-CACHE" />
</head>
<body>





    <script type="text/javascript" src="https://authsys.azurewebsites.net/bootstrap.js" ></script>
   
    <div id="authSysWrapper" publickey="jalsdkjflkwejflkqwejf" userid="<%=Session["UserBADBIRuserid"] %>" submitURL="optional">
    </div>




</body>