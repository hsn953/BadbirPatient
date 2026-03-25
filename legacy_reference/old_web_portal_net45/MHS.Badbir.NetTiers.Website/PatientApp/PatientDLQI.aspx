<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientDLQI.aspx.cs" Inherits="PatientApp_PatientDLQI" MasterPageFile="~/MasterPages/bbDefault.master" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/bootstrap_theme.css" rel="stylesheet" />
    <script runat="server">

        protected void dataDlqiMissing_CheckedChanged(object sender, EventArgs e)
        {
            // If the missing data flag is ticked, disable the validators to allow the user to save the page without entering any information
            if (((CheckBox)sender).Checked)
            {
                // Loop through and disable the validators
                foreach (BaseValidator val in Page.Validators)
                    val.Enabled = false;
            }
            else
            {
                // Loop through and enable the validators
                foreach (BaseValidator val in Page.Validators)
                    val.Enabled = true;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <h1>Complete the DLQI Form</h1>

    
    <asp:FormView ID="FormView1"  DataKeyNames="FupId" runat="server"
        DataSourceID="bbpappdlqids" OnItemCreated="FormView1_ItemCreated" OnItemInserted="FormView1_ItemInserted">
        <InsertItemTemplate>
        





            <fieldset runat="server" class="fFupId" style="display: none;">
                <legend>FupId</legend>
                <ol>
                    <li>
                        <asp:Label ID="lbldataFupId" runat="server" Text="FupId:" AssociatedControlID="dataPappFupId" />
                        <asp:TextBox ID="dataPappFupId" runat="server" Text='<%# Bind("pappFupId") %>' Enabled="false" CssClass="disabled"></asp:TextBox>

                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("skipBreakup") %>' Enabled="false" CssClass="disabled"></asp:TextBox>

                    </li>
                </ol>
            </fieldset>

            <fieldset runat="server">
                <legend>DLQI</legend>
                <ol>
                    <li>
                        <asp:CheckBox runat="server" ID="dataDlqiMissing" Checked='<%# Bind("DlqiMissing") %>' CssClass="bigCheck" Text=" DLQI unavailable?"
                            ToolTip="If this information is unavailable, tick this box so we don't raise an outstanding data query."
                            OnCheckedChanged="dataDlqiMissing_CheckedChanged" OnLoad="dataDlqiMissing_CheckedChanged" AutoPostBack="true" />
                    </li>
                    <li>
                        <asp:Label ID="lbldataItchsoreScore" runat="server" Text="1. How itchy, sore, painful or stinging has your skin been?:" AssociatedControlID="dataItchsoreScore" />
                        <asp:DropDownList runat="server" ID="dataItchsoreScore" SelectedValue='<%# Bind("ItchsoreScore") %>'>
                            <asp:ListItem Value="" Text="<Please Choose>" Selected="True" />
                            <asp:ListItem Value="0" Text="Very much"></asp:ListItem>
                            <asp:ListItem Value="1" Text="A lot"></asp:ListItem>
                            <asp:ListItem Value="2" Text="A little"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Not at all"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RF1" ControlToValidate="dataItchsoreScore" ErrorMessage="* Required" />
                    </li>
                    <li>


                        <asp:Label ID="lbldataEmbscScore" runat="server" Text="2. How embarrassed or self conscious have you been because of your skin?:" AssociatedControlID="dataEmbscScore" />
                        <asp:DropDownList runat="server" ID="dataEmbscScore" SelectedValue='<%# Bind("EmbscScore") %>'>
                            <asp:ListItem Value="" Text="<Please Choose>" Selected="True" />
                            <asp:ListItem Value="0" Text="Very much"></asp:ListItem>
                            <asp:ListItem Value="1" Text="A lot"></asp:ListItem>
                            <asp:ListItem Value="2" Text="A little"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Not at all"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RF2" ControlToValidate="dataEmbscScore" ErrorMessage="* Required" />


                    </li>
                    <li>


                        <asp:Label ID="lbldataShophgScore" runat="server" Text="3. How much has your skin interfered with you going shopping or looking after your home or garden?:" AssociatedControlID="dataShophgScore" />
                        <asp:DropDownList runat="server" ID="dataShophgScore" SelectedValue='<%# Bind("ShophgScore") %>'>
                            <asp:ListItem Value="" Text="<Please Choose>" Selected="True" />
                            <asp:ListItem Value="0" Text="Very much"></asp:ListItem>
                            <asp:ListItem Value="1" Text="A lot"></asp:ListItem>
                            <asp:ListItem Value="2" Text="A little"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Not at all"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Not relevant"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RF3" ControlToValidate="dataShophgScore" ErrorMessage="* Required" />


                    </li>
                    <li>


                        <asp:Label ID="lbldataClothesScore" runat="server" Text="4. How much has your skin influenced the clothes you wear?:" AssociatedControlID="dataClothesScore" />
                        <asp:DropDownList runat="server" ID="dataClothesScore" SelectedValue='<%# Bind("ClothesScore") %>'>
                            <asp:ListItem Value="" Text="<Please Choose>" Selected="True" />
                            <asp:ListItem Value="0" Text="Very much"></asp:ListItem>
                            <asp:ListItem Value="1" Text="A lot"></asp:ListItem>
                            <asp:ListItem Value="2" Text="A little"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Not at all"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Not relevant"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RF4" ControlToValidate="dataClothesScore" ErrorMessage="* Required" />

                    </li>
                    <li>


                        <asp:Label ID="lbldataSocleisScore" runat="server" Text="5. How much has your skin affected any social or leisure activities:" AssociatedControlID="dataSocleisScore" />
                        <asp:DropDownList runat="server" ID="dataSocleisScore" SelectedValue='<%# Bind("SocleisScore") %>'>
                            <asp:ListItem Value="" Text="<Please Choose>" Selected="True" />
                            <asp:ListItem Value="0" Text="Very much"></asp:ListItem>
                            <asp:ListItem Value="1" Text="A lot"></asp:ListItem>
                            <asp:ListItem Value="2" Text="A little"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Not at all"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Not relevant"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RF5" ControlToValidate="dataSocleisScore" ErrorMessage="* Required" />


                    </li>
                    <li>

                        <asp:Label ID="lbldataSportScore" runat="server" Text="6. How much has your skin made it difficult for you to do any sport?:" AssociatedControlID="dataSportScore" />
                        <asp:DropDownList runat="server" ID="dataSportScore" SelectedValue='<%# Bind("SportScore") %>'>
                            <asp:ListItem Value="" Text="<Please Choose>" Selected="True" />
                            <asp:ListItem Value="0" Text="Very much"></asp:ListItem>
                            <asp:ListItem Value="1" Text="A lot"></asp:ListItem>
                            <asp:ListItem Value="2" Text="A little"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Not at all"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Not relevant"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RF6" ControlToValidate="dataSportScore" ErrorMessage="* Required" />


                    </li>
                    <li>


                        <asp:Label ID="lbldataWorkstudScore" runat="server" Text="7. Has your skin prevented you from working or studying?:" AssociatedControlID="dataWorkstudScore" />
                        <asp:DropDownList runat="server" ID="dataWorkstudScore" SelectedValue='<%# Bind("WorkstudScore") %>'>
                            <asp:ListItem Value="" Text="<Please Choose>" Selected="True" />
                            <asp:ListItem Value="5" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="6" Text="No"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Not relevant"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RF7a" ControlToValidate="dataWorkstudScore" ErrorMessage="* Required" />

                    </li>
                    <li>


                        <asp:Label ID="lbldataWorkstudnoScore" runat="server" Text="7a. If No, how much has your skin been a problem at work or studying?:" AssociatedControlID="dataWorkstudnoScore" />
                        <asp:DropDownList runat="server" ID="dataWorkstudnoScore" SelectedValue='<%# Bind("WorkstudnoScore") %>'>
                            <asp:ListItem Value="" Text="<Please Choose>" Selected="True" />
                            <asp:ListItem Value="1" Text="A lot"></asp:ListItem>
                            <asp:ListItem Value="2" Text="A little"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Not at all"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RF7b" ControlToValidate="dataWorkstudnoScore" ErrorMessage="* Required" />

                    </li>
                    <li>


                        <asp:Label ID="lbldataPartcrfScore" runat="server" Text="8. How much has your skin created problems with your partner or any of you close friends or relatives?:" AssociatedControlID="dataPartcrfScore" />
                        <asp:DropDownList runat="server" ID="dataPartcrfScore" SelectedValue='<%# Bind("PartcrfScore") %>'>
                            <asp:ListItem Value="" Text="<Please Choose>" Selected="True" />
                            <asp:ListItem Value="0" Text="Very much"></asp:ListItem>
                            <asp:ListItem Value="1" Text="A lot"></asp:ListItem>
                            <asp:ListItem Value="2" Text="A little"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Not at all"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Not relevant"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RF8" ControlToValidate="dataPartcrfScore" ErrorMessage="* Required" />

                    </li>
                    <li>


                        <asp:Label ID="lbldataSexdifScore" runat="server" Text="9. How much has your skin caused any sexual difficulties?:" AssociatedControlID="dataSexdifScore" />
                        <asp:DropDownList runat="server" ID="dataSexdifScore" SelectedValue='<%# Bind("SexdifScore") %>'>
                            <asp:ListItem Value="" Text="<Please Choose>" Selected="True" />
                            <asp:ListItem Value="0" Text="Very much"></asp:ListItem>
                            <asp:ListItem Value="1" Text="A lot"></asp:ListItem>
                            <asp:ListItem Value="2" Text="A little"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Not at all"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Not relevant"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RF9" ControlToValidate="dataSexdifScore" ErrorMessage="* Required" />

                    </li>
                    <li>


                        <asp:Label ID="lbldataTreatmentScore" runat="server" Text="10. How much of a problem has the treatment for your skin been, e.g. by making your home messy, or by taking up time?:" AssociatedControlID="dataTreatmentScore" />
                        <asp:DropDownList runat="server" ID="dataTreatmentScore" SelectedValue='<%# Bind("TreatmentScore") %>'>
                            <asp:ListItem Value="" Text="<Please Choose>" Selected="True" />
                            <asp:ListItem Value="0" Text="Very much"></asp:ListItem>
                            <asp:ListItem Value="1" Text="A lot"></asp:ListItem>
                            <asp:ListItem Value="2" Text="A little"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Not at all"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Not relevant"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RF10" ControlToValidate="dataTreatmentScore" ErrorMessage="* Required" />

                    </li>


                    <li>
                        <asp:Label ID="lblDLQI" runat="server" Text="DLQI Score (will be calculated when you save the page):" AssociatedControlID="totalScore" />
                        <asp:TextBox runat="server" ID="totalScore" ReadOnly="true"
                            Text='<%# Bind("totalScore", "{0:d}") %>' MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator runat="server" ID="totalScoreValidator" ControlToValidate="totalScore" Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Please enter a whole number" />
                        <asp:DropDownList ID="ddCalcScore" runat="server" Visible="false" SelectedValue='<%# Bind("SkipBreakup") %>'>
                            <asp:ListItem Enabled="true" Text="Calculate Total Score Automatically" Value="0" />
                            <asp:ListItem Text="Do Not Calculate Total Score Automatically" Value="1" />
                        </asp:DropDownList>



                    </li>


                    <li>


                        <asp:Label ID="lbldataDatecomp" runat="server" Text="Date completed:" AssociatedControlID="dataDatecomp" />
                        <asp:TextBox runat="server" ID="dataDatecomp" Text='<%# Bind("Datecomp", "{0:d}") %>' MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator runat="server" ID="valdataDatecomp" ControlToValidate="dataDatecomp" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ErrorMessage="* For 'vague' dates, enter 15/mm/yyyy or 01/07/yyyy or 9/9/2999." />
                        <asp:RequiredFieldValidator runat="server" ID="RFDate" ControlToValidate="dataDatecomp" ErrorMessage="* Required" />

                    </li>
                </ol>
            </fieldset>




                </InsertItemTemplate>
        <FooterTemplate>
                        <asp:Button CssClass="btn btn-sm btn-secondary" ID="InsertButton" runat="server" CausesValidation="True"
                CommandName="Insert" Text="Save and submit" />

        </FooterTemplate>
    </asp:FormView>



    <data:BbPappPatientDlqiDataSource ID="bbpappdlqids" runat="server" SelectMethod="GetByFormId">
    </data:BbPappPatientDlqiDataSource>

</asp:Content>
