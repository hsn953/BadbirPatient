<%@ Control Language="C#" ClassName="BbPatientLifestyleFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
        <fieldset class="fFupId" style="display: none">
            <legend>FupId</legend>
            <ol>
                <li>
                    <asp:Label ID="lbldataFupId" runat="server" Text="FupId:" AssociatedControlID="dataFupId" />
                    <asp:TextBox runat="server" ID="dataFupId" Text='<%# Bind("FupId") %>' Enabled="false"
                        CssClass="disabled" />
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Birthplace</legend>
            <ol>
                <li>
                    <asp:Label ID="lbldataBirthtown" runat="server" Text="Birth town:" AssociatedControlID="dataBirthtown" />
                    <asp:TextBox runat="server" ID="dataBirthtown" Text='<%# Bind("Birthtown") %>' MaxLength="50"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="lbldataBirthcountry" runat="server" Text="Birth country:" AssociatedControlID="dataBirthcountry" />
                    <asp:TextBox runat="server" ID="dataBirthcountry" Text='<%# Bind("Birthcountry") %>'
                        MaxLength="50"></asp:TextBox>
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Work</legend>
            <ol>
                <li>
                    <asp:Label ID="lbldataOccupation" runat="server" Text="Occupation:" AssociatedControlID="dataOccupation" />
                    <asp:TextBox runat="server" ID="dataOccupation" Text='<%# Bind("Occupation") %>'
                        MaxLength="50"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="lbldataWorkstatusid" runat="server" Text="Work status:" AssociatedControlID="dataWorkstatusid" />
                    <data:EntityDropDownList runat="server" ID="dataWorkstatusid" DataSourceID="WorkstatusidBbWorkStatuslkpDataSource"
                        DataTextField="Workstatus" DataValueField="Worstatuskid" SelectedValue='<%# Bind("Workstatusid") %>'
                        AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
                    <data:BbWorkStatuslkpDataSource ID="WorkstatusidBbWorkStatuslkpDataSource" runat="server"
                        SelectMethod="GetAll" />
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Ethnicity</legend>
            <ol>
                <li>
                    <asp:Label ID="lbldataEthnicityid" runat="server" Text="Ethnicity:" AssociatedControlID="dataEthnicityid" />
                    <data:EntityDropDownList runat="server" ID="dataEthnicityid" DataSourceID="EthnicityidBbEthnicitylkpDataSource"
                        DataTextField="Ethnicity" DataValueField="Ethnicityid" SelectedValue='<%# Bind("Ethnicityid") %>'
                        AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
                    <data:BbEthnicitylkpDataSource ID="EthnicityidBbEthnicitylkpDataSource" runat="server"
                        SelectMethod="GetAll" />
                </li>
                <li>
                    <asp:Label ID="lbldataOtherethnicity" runat="server" Text="Other ethnicity:" AssociatedControlID="dataOtherethnicity" />
                    <asp:TextBox runat="server" ID="dataOtherethnicity" Text='<%# Bind("Otherethnicity") %>'
                        MaxLength="50"></asp:TextBox>
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Climate</legend>
            <ol>
                <li>
                    <asp:Label ID="lbldataOutdooroccupation" runat="server" Text="Do you have an occupation or hobby which is mainly outdoors:" />
                    <asp:RadioButtonList runat="server" ID="dataOutdooroccupation" SelectedValue='<%# Bind("Outdooroccupation") %>'
                        RepeatDirection="Vertical">
                        <asp:ListItem Value="" Text="- not set -" style="display: none" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <asp:Label ID="lbldataLivetropical" runat="server" Text="Have you ever lived in a tropical/subtropical country:" />
                    <asp:RadioButtonList runat="server" ID="dataLivetropical" SelectedValue='<%# Bind("Livetropical") %>'
                        RepeatDirection="Vertical">
                        <asp:ListItem Value="" Text="- not set -" style="display: none" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Smoking</legend>
            <ol>
                <li>
                    <asp:CheckBox runat="server" ID="dataSmokingMissing" Checked='<%# Bind("SmokingMissing") %>' CssClass="bigCheck" Text=" Smoking information unavailable?" 
                        ToolTip="If this information is unavailable, tick this box so we don't raise an outstanding data query."/>
                </li>
                <li>
                    <asp:Label ID="lbldataEversmoked" runat="server" Text="Ever smoked more than one cigarette a day:" />
                    <asp:RadioButtonList runat="server" ID="dataEversmoked" SelectedValue='<%# Bind("Eversmoked") %>'
                        RepeatDirection="Vertical">
                        <asp:ListItem Value="" Text="- not set -" style="display: none" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No"></asp:ListItem>
                        <asp:ListItem Value="" Text="Don't know"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <asp:Label ID="lbldataEversmokednumbercigsperday" runat="server" Text="Average number of cigarettes/day:"
                        AssociatedControlID="dataEversmokednumbercigsperday" />
                    <asp:TextBox runat="server" ID="dataEversmokednumbercigsperday" Text='<%# Bind("Eversmokednumbercigsperday") %>'></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataEversmokednumbercigsperday" runat="server" Display="Dynamic"
                        ControlToValidate="dataEversmokednumbercigsperday" ErrorMessage="Allowed value is between 0 and 200 cigarettes."
                        MaximumValue="200" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                </li>
                <li>
                    <asp:Label ID="lbldataAgestart" runat="server" Text="Age started smoking:" AssociatedControlID="dataAgestart" />
                    <asp:TextBox runat="server" ID="dataAgestart" Text='<%# Bind("Agestart") %>'></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataAgestart" runat="server" Display="Dynamic" ControlToValidate="dataAgestart"
                        ErrorMessage="Invalid value" MaximumValue="120" MinimumValue="0"
                        Type="Integer"></asp:RangeValidator>
                </li>
                <li>
                    <asp:Label ID="lbldataAgestop" runat="server" Text="Age stopped smoking:" AssociatedControlID="dataAgestop" />
                    <asp:TextBox runat="server" ID="dataAgestop" Text='<%# Bind("Agestop") %>'></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataAgestop" runat="server" Display="Dynamic" ControlToValidate="dataAgestop"
                        ErrorMessage="Invalid value" MaximumValue="120" MinimumValue="0"
                        Type="Integer"></asp:RangeValidator>
                </li>
                <li>
                    <asp:Label ID="lbldataCurrentlysmoke" runat="server" Text="Currently smoke more than one cigarette a day:"/>
                    <asp:RadioButtonList runat="server" ID="dataCurrentlysmoke" SelectedValue='<%# Bind("Currentlysmoke") %>'
                        RepeatDirection="Vertical">
                        <asp:ListItem Value="" Text="- not set -" style="display: none" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No"></asp:ListItem>
                        <asp:ListItem Value="" Text="Don't know"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <asp:Label ID="lbldataCurrentlysmokenumbercigsperday" runat="server" Text="How many cigarettes/day:"
                        AssociatedControlID="dataCurrentlysmokenumbercigsperday" />
                    <asp:TextBox runat="server" ID="dataCurrentlysmokenumbercigsperday" Text='<%# Bind("Currentlysmokenumbercigsperday") %>'></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataCurrentlysmokenumbercigsperday" runat="server" Display="Dynamic"
                        ControlToValidate="dataCurrentlysmokenumbercigsperday" ErrorMessage="Allowed value is between 0 and 200 cigarettes."
                        MaximumValue="200" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                        <br />
                         For occasional smoking, please select "Yes" and enter 0 cigarettes.                
                         </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Drinking</legend>
            <ol>
                <li>
                    <asp:CheckBox runat="server" ID="dataDrinkingMissing" Checked='<%# Bind("DrinkingMissing") %>' CssClass="bigCheck" Text=" Drinking information unavailable?" 
                        ToolTip="If this information is unavailable, tick this box so we don't raise an outstanding data query."/>
                </li>
                <li>
                    <asp:Label ID="lbldataDrinkalcohol" runat="server" Text="Does the patient drink alcohol?" AssociatedControlID="dataDrinkalcohol" />
                    <asp:RadioButtonList runat="server" ID="dataDrinkalcohol" SelectedValue='<%# Bind("Drinkalcohol") %>'
                        RepeatDirection="Vertical">
                        <asp:ListItem Value="" Text="- not set -" style="display: none" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No"></asp:ListItem>
                        <asp:ListItem Value="" Text="Don't know"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <asp:Label ID="lbldataDrnkunitsavg" runat="server" Text="If yes, how many units does the patient drink in an average week?"
                         AssociatedControlID="dataDrnkunitsavg" />
                    <asp:TextBox runat="server" ID="dataDrnkunitsavg" Text='<%# Bind("Drnkunitsavg") %>'></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataDrnkunitsavg" runat="server" Display="Dynamic" ControlToValidate="dataDrnkunitsavg"
                         ErrorMessage="Allowed value is between 0 and 400 units of alcohol." MaximumValue="400" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                        <br />
                         For occasional drinking, please select "Yes" and enter 0 units.
                </li>
            </ol>
        </fieldset>
    </ItemTemplate>
</asp:FormView>
