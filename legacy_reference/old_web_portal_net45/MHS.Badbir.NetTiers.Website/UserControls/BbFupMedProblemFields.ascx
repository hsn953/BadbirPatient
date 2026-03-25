<%@ Control Language="C#" ClassName="BbPatientMedProblemFupFields" %>
<script runat="server">

    protected void dataMedprobMissing_CheckedChanged(object sender, EventArgs e)
    {
        // If the missing data flag is ticked, disable the validators to allow the user to save the page without entering any information
        if(((CheckBox)sender).Checked)
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


<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>

        <fieldset style="display: none">
            <legend>FUP id</legend>
            <ol>
                <li>
                    <span class="literal">
                        <asp:Label ID="lbldataFupId" runat="server" Text="Follow up Id:" AssociatedControlID="dataFupId" /></span>
                    <span>
                        <asp:TextBox runat="server" ID="dataFupId" Text='<%# Bind("FupId") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataFupId" runat="server" Display="Dynamic" ControlToValidate="dataFupId" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataFupId" runat="server" Display="Dynamic" ControlToValidate="dataFupId" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer" CssClass="disabled"></asp:RangeValidator>
                    </span>
                </li>
            </ol>
        </fieldset>

        <fieldset>
            <legend>Patient Medical Problems</legend>
            <ol>
                <li>
                    <asp:CheckBox runat="server" ID="dataMedprobMissing" Checked='<%# Bind("MedprobMissing") %>' CssClass="bigCheck" Text=" Medical problems information unavailable?" 
                        ToolTip="If this information is unavailable, tick this box so we don't raise an outstanding data query." 
                        OnCheckedChanged="dataMedprobMissing_CheckedChanged" OnLoad="dataMedprobMissing_CheckedChanged" AutoPostBack="true"/>
                </li>
                <li>
                    <asp:Label ID="lbldataHospitaladmissions" runat="server" Text="Patient admitted to hospital:" AssociatedControlID="dataHospitaladmissions" />
                    <asp:DropDownList runat="server" ID="dataHospitaladmissions" SelectedValue='<%# Bind("Hospitaladmissions") %>'>
                        <asp:ListItem Value="" Text="< Please Select >" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="0" Text="None"></asp:ListItem>
                        <asp:ListItem Value="1" Text="One"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Two"></asp:ListItem>
                        <asp:ListItem Value="3" Text="More than two"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="* Required" ControlToValidate="dataHospitaladmissions" />
                </li>
                <li>
                    <asp:Label ID="lbldataNewdrugs" runat="server" Text="Patient prescribed new drugs:" AssociatedControlID="dataNewdrugs" />
                    <asp:DropDownList runat="server" ID="dataNewdrugs" SelectedValue='<%# Bind("Newdrugs") %>'>
                        <asp:ListItem Value="" Text="< Please Select >" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="0" Text="None"></asp:ListItem>
                        <asp:ListItem Value="1" Text="One"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Two"></asp:ListItem>
                        <asp:ListItem Value="3" Text="More than two"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="* Required" ControlToValidate="dataNewdrugs" />
                </li>
                <li>
                    <asp:Label ID="lbldataNewclinics" runat="server" Text="Patient referred to new clinics:" AssociatedControlID="dataNewclinics" />
                    <asp:DropDownList runat="server" ID="dataNewclinics" SelectedValue='<%# Bind("Newclinics") %>'>
                        <asp:ListItem Value="" Text="< Please Select >" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="0" Text="None"></asp:ListItem>
                        <asp:ListItem Value="1" Text="One"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Two"></asp:ListItem>
                        <asp:ListItem Value="3" Text="More than two"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Missing"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="* Required" ControlToValidate="dataNewclinics" />
                </li>
                <li>
                    <span class="literal">
                        <asp:Label ID="lbldataOccupation" runat="server" Text="Patient occupation:" AssociatedControlID="dataOccupation" /></span>
                    <span>
                        <asp:TextBox runat="server" ID="dataOccupation" Text='<%# Bind("Occupation") %>' MaxLength="50"></asp:TextBox>
                    </span>
                </li>
                <li>
                    <asp:Label ID="lbldataEmploymentstatus" runat="server" Text="Employment status:" AssociatedControlID="dataEmploymentstatus" />
                    <data:EntityDropDownList runat="server" ID="dataEmploymentstatus" DataSourceID="workStatusDS"
                        DataTextField="workstatus" DataValueField="worstatuskid" SelectedValue='<%# Bind("Employmentstatus") %>'
                        AppendNullItem="true" NullItemText="< Please Choose ...>" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="* Required" ControlToValidate="dataEmploymentstatus" />

                    <data:BbWorkStatuslkpDataSource runat="server" ID="workStatusDS" SelectMethod="GetAll" />
                </li>
            </ol>
        </fieldset>

    </ItemTemplate>
</asp:FormView>


