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
            <legend>Smoking</legend>
            <ol>
                <li>
                    <asp:CheckBox runat="server" ID="dataSmokingMissing" Checked='<%# Bind("SmokingMissing") %>' CssClass="bigCheck" Text=" Smoking information unavailable?" 
                        ToolTip="If this information is unavailable, tick this box so we don't raise an outstanding data query."/>
                </li>
                <li>
                    <asp:Label ID="lbldataCurrentlysmoke" runat="server" Text="Currently a smoker:" AssociatedControlID="dataCurrentlysmoke" />
                    <asp:RadioButtonList runat="server" ID="dataCurrentlysmoke" SelectedValue='<%# Bind("Currentlysmoke") %>'
                        RepeatDirection="Vertical">
                        <asp:ListItem Value="" Text="- not set -" Selected="True" style="display: none;"></asp:ListItem>
                        <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                    <i class='fas fa-info-circle'></i> For occasional smoking, please select "Yes" and enter 0 cigarettes.
                    <br /><br />
                </li>
                <li>
                    <asp:Label ID="lbldataCurrentlysmokenumbercigsperday" runat="server" Text="Number of cigarettes per day:"
                        AssociatedControlID="dataCurrentlysmokenumbercigsperday" Width="175" />
                    <asp:TextBox runat="server" ID="dataCurrentlysmokenumbercigsperday" Text='<%# Bind("Currentlysmokenumbercigsperday") %>'></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataCurrentlysmokenumbercigsperday" runat="server" Display="Dynamic"
                        ControlToValidate="dataCurrentlysmokenumbercigsperday" ErrorMessage="Allowed value is between 0 and 200 cigarettes."
                        MaximumValue="200" MinimumValue="0" Type="Integer"></asp:RangeValidator>
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

                    <asp:Label ID="lbldataDrinkalcohol" runat="server" Text="Drinks alcohol:" AssociatedControlID="dataDrinkalcohol" />
                    <asp:RadioButtonList runat="server" ID="dataDrinkalcohol" SelectedValue='<%# Bind("Drinkalcohol") %>'
                        RepeatDirection="Vertical">
                        <asp:ListItem Value="" Text="- not set -" Selected="True" style="display: none;"></asp:ListItem>
                        <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                    <i class='fas fa-info-circle'></i> For occasional drinking, please select "Yes" and enter 0 units.
                    <br /><br />
                </li>
                <li>
                    <asp:Label ID="lbldataDrnkunitsavg" runat="server" Text="Units of alcohol:" AssociatedControlID="dataDrnkunitsavg" Width="100"/>
                    <asp:TextBox runat="server" ID="dataDrnkunitsavg" Text='<%# Bind("Drnkunitsavg") %>'></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataDrnkunitsavg" runat="server" Display="Dynamic" ControlToValidate="dataDrnkunitsavg"
                        ErrorMessage="Allowed value is between 0 and 400 units of alcohol." MaximumValue="400" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                </li>
            </ol>
        </fieldset>
    </ItemTemplate>
</asp:FormView>
