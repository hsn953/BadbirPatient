<%@ Control Language="C#" ClassName="BbFupAdditionalFields" %>
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
            <legend>Additional Info</legend>
            <ol>
                <asp:Panel runat="server" Visible="false" Enabled="false" ID="Under16heightPanel">
                    <li>
                        <asp:Label ID="lbldataHeight" runat="server" AssociatedControlID="dataHeight" >Height (cm): <i>(Collected at follow-up for patients under 16)</i> </asp:Label>
                        <asp:TextBox runat="server" ID="dataHeight" Text='<%# Bind("Height") %>' onchange="javascript:updateBMI()"></asp:TextBox><asp:RangeValidator
                            ID="RangeVal_dataHeight" runat="server" Display="Dynamic" ControlToValidate="dataHeight"
                            ErrorMessage=" Allowed range is between 60cm and 300cm" MaximumValue="300" MinimumValue="60"
                            Type="Double"></asp:RangeValidator>
                    </li>
                </asp:Panel>
                <li>
                    <asp:CheckBox runat="server" ID="dataWeightMissing" Checked='<%# Bind("WeightMissing") %>' CssClass="bigCheck" Text=" Weight unavailable?" 
                        ToolTip="If this information is unavailable, tick this box so we don't raise an outstanding data query."/>
                    <br />
                    <asp:Label ID="lbldataWeight" runat="server" Text="Weight (kg):" AssociatedControlID="dataWeight" />
                    <asp:TextBox runat="server" ID="dataWeight" Text='<%# Bind("Weight") %>'></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataWeight" runat="server" Display="Dynamic" ControlToValidate="dataWeight"
                        ErrorMessage=" Allowed range is between 20Kg and 400Kg" MaximumValue="400" MinimumValue="20"
                        Type="Double"></asp:RangeValidator>
                </li>
                <li>
                    <asp:CheckBox runat="server" ID="dataWaistMissing" Checked='<%# Bind("WaistMissing") %>' CssClass="bigCheck" Text=" Waist unavailable?" 
                        ToolTip="If this information is unavailable, tick this box so we don't raise an outstanding data query."/>
                    <br />
                    <asp:Label ID="lbldataWaist" runat="server" Text="Waist (cm):" AssociatedControlID="dataWaist" />
                    <asp:TextBox runat="server" ID="dataWaist" Text='<%# Bind("Waist") %>'></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataWaist" runat="server" Display="Dynamic" ControlToValidate="dataWaist"
                        ErrorMessage=" Allowed range is between 20cm and 200cm" MaximumValue="200" MinimumValue="20"
                        Type="Double"></asp:RangeValidator>
                </li>
            </ol>
        </fieldset>
    </ItemTemplate>
</asp:FormView>
