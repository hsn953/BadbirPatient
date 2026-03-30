<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="PatientApp_Default" MasterPageFile="~/MasterPages/bbDefault.master" %>


    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/bootstrap_theme.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="card">
  <div class="card-header bg-primary">
    Fill a questtionaire
  </div>
  <div class="card-body">
    <h5 class="card-title">DLQI</h5>
    <p class="card-text">If you have been asked by your healthcare provider to complete a DLQI form for BADBIR for your follow up, please select this option.</p>
    <a href="PatientDLQI.aspx" class="btn btn-primary">Complete DLQI Form</a>
  </div>
  <div class="card-body">
    <h5 class="card-title">Lifestyle Factors</h5>
    <p class="card-text">If you have been asked by your healthcare provider to complete a Lifestyle Factors form for BADBIR for your follow up, please select this option.</p>
    <a href="PatientLifestyle.aspx" class="btn btn-primary">Complete Lifestyle Factors Form</a>
  </div>
 <div class="card-body">
    <h5 class="card-title">Medical Problems</h5>
    <p class="card-text">If you have been asked by your healthcare provider to complete a Medical Problems form for BADBIR for your follow up, please select this option.</p>
    <a href="Patient.aspx" class="btn btn-primary">Complete Medical Problems Form</a>
  </div>
</div>





    <div class="card">
  <div class="card-header bg-primary">
    Contact
  </div>
  <div class="card-body ">
    <h5 class="card-title">Send clinical information to BADBIR</h5>
    <p class="card-text">If you want to send any other clinical or health relateed information to BADBIR, please use this option.</p>
    <a href="#" class="btn btn-primary">Send other information</a>
  </div>
</div>

</asp:Content>