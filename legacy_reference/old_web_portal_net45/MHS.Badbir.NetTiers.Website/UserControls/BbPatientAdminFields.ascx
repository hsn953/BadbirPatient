<%@ Control Language="C#" ClassName="BbPatientFields" %>

<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
        <fieldset>
            <legend>Patient</legend>
            <ol>
                <li><span class="literal">
                    <asp:Label ID="lbldataFirststudyno" runat="server" Text="Temporary study number:"
                        AssociatedControlID="dataFirststudyno" /></span> <span>
                            <asp:TextBox runat="server" ID="dataFirststudyno" Text='<%# Bind("Firststudyno") %>'></asp:TextBox><asp:RangeValidator
                                ID="RangeVal_dataFirststudyno" runat="server" Display="Dynamic" ControlToValidate="dataFirststudyno"
                                ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="0"
                                Type="Integer"></asp:RangeValidator>
                        </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataStudyidlastfive" runat="server" Text="Studyidlastfive:" AssociatedControlID="dataStudyidlastfive" /></span>
                    <span>
                        <asp:TextBox runat="server" ID="dataStudyidlastfive" Text='<%# Bind("Studyidlastfive") %>'></asp:TextBox><asp:RangeValidator
                            ID="RangeVal_dataStudyidlastfive" runat="server" Display="Dynamic" ControlToValidate="dataStudyidlastfive"
                            ErrorMessage="Invalid value" MaximumValue="99999" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataDateconsented" runat="server" Text="Date consented:" AssociatedControlID="dataDateconsented" /></span>
                    <span>
                        <asp:TextBox runat="server" ID="dataDateconsented" Text='<%# Bind("Dateconsented", "{0:d}") %>'
                            MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataDateconsented" runat="server"
                                SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
                        <asp:CompareValidator runat="server" ID="valdataDateconsented" ControlToValidate="dataDateconsented"
                            Operator="DataTypeCheck" Type="Date" ErrorMessage="Date must be in dd/mm/yyyy format" />
                </li>
                <li><span class="literal">
                    <asp:Label ID="lbldataConsentformreceived" runat="server" Text="Consent form received:"
                        AssociatedControlID="dataConsentformreceived" /></span> <span>
                            <asp:TextBox runat="server" ID="dataConsentformreceived" Text='<%# Bind("Consentformreceived", "{0:d}") %>'
                                MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataConsentformreceived" runat="server"
                                    SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
                            <asp:CompareValidator runat="server" ID="valdataConsentformreceived" ControlToValidate="dataConsentformreceived"
                                Operator="DataTypeCheck" Type="Date" ErrorMessage="Date must be in dd/mm/yyyy format" />
                        </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataGenderid" runat="server" Text="Gender:" AssociatedControlID="dataGenderid" /></span>
                    <span>
                        <data:EntityDropDownList runat="server" ID="dataGenderid" DataSourceID="GenderidBbGenderlkpDataSource"
                            DataTextField="Gender" DataValueField="Genderid" SelectedValue='<%# Bind("Genderid") %>'
                            AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
                        <data:BbGenderlkpDataSource ID="GenderidBbGenderlkpDataSource" runat="server" SelectMethod="GetAll" />
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataDateofbirth" runat="server" Text="Date of birth:" AssociatedControlID="dataDateofbirth" /></span>
                    <span>
                        <asp:TextBox runat="server" ID="dataDateofbirth" Text='<%# Bind("Dateofbirth", "{0:d}") %>'
                            MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataDateofbirth" runat="server"
                                SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
                        <asp:CompareValidator runat="server" ID="valdataDateofbirth" ControlToValidate="dataDateofbirth"
                            Operator="DataTypeCheck" Type="Date" ErrorMessage="Date must be in dd/mm/yyyy format" />
                    </span></li>
                <li runat="server" id="liDateOfDeath" visible='<%# Eval("DeathDate") == null ? false : true %>'><span class="literal">
                    <asp:Label ID="Label1" runat="server" Text="Date of death:" AssociatedControlID="dataDateofdeath" /></span>
                    <span>
                        <asp:Label runat="server" ID="dataDateofdeath" Text='<%# Bind("DeathDate", "{0:d}") %>'/>
                    </span></li>                    
                <li><span class="literal">
                    <asp:Label ID="lbldataStatusid" runat="server" Text="Status:" AssociatedControlID="dataStatusid" /></span>
                    <span>
                        <data:EntityDropDownList runat="server" ID="dataStatusid" DataSourceID="StatusidBbPatientStatuslkpDataSource"
                            DataTextField="Pstatus" DataValueField="Pstatusid" SelectedValue='<%# Bind("Statusid") %>'
                            AppendNullItem="false" Required="true" />
  
                        <data:BbPatientStatuslkpDataSource ID="StatusidBbPatientStatuslkpDataSource" runat="server"
                            SelectMethod="GetAll" />
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataStatusDetailid" runat="server" Text="Status detail:" AssociatedControlID="dataStatusDetailid" /></span>
                    <span>
                        <data:EntityDropDownList runat="server" ID="dataStatusDetailid" DataSourceID="statusdetailds"
                            DataTextField="Pstatusdetail" DataValueField="Pstatusdetailid" SelectedValue='<%# Bind("Statusdetailid") %>'
                            AppendNullItem="false" Required="true"  />
                        <data:BbPatientStatusDetaillkpDataSource ID="statusdetailds" runat="server" SelectMethod="GetAll" />
                    </span></li>
            </ol>
        </fieldset>
    </ItemTemplate>
</asp:FormView>
