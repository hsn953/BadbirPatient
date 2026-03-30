<%@ Control Language="C#" ClassName="BbPatientLifestyleFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
        <fieldset class="fFupId" style="display: none">

            <script type="text/javascript">

$(document).ready(function(){
    updateBMI();

});

function updateBMI(){
    if(!isNaN(parseFloat($("#ctl00_ctl00_MainContent_MainContent_FormView1_dataWeight").val())))
    {
        //console.log("weight is number")
     if(!isNaN(parseFloat($("#ctl00_ctl00_MainContent_MainContent_FormView1_dataHeight").val())))
            {
                //console.log("height is number")

                ans =  (10000* $("#ctl00_ctl00_MainContent_MainContent_FormView1_dataWeight").val()) / (  $("#ctl00_ctl00_MainContent_MainContent_FormView1_dataHeight").val() * $("#ctl00_ctl00_MainContent_MainContent_FormView1_dataHeight").val()  )  ;
                if(!isNaN(parseFloat(ans)))
                {
                        //console.log("bmi is number:" + ans)

                $("#ctl00_ctl00_MainContent_MainContent_FormView1_dataBMI").val( ans.toPrecision(3) )
                }
                else
                {
                    $("#ctl00_ctl00_MainContent_MainContent_FormView1_dataBMI").val( "" );
                }
                
            }
            else
            {
                $("#ctl00_ctl00_MainContent_MainContent_FormView1_dataBMI").val( "" );
            
            };
    
    }
    else
    {
        $("#ctl00_ctl00_MainContent_MainContent_FormView1_dataBMI").val( "" );
    
    }
    
    return false;
}


            </script>

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
                <li>
                    <asp:Label ID="lbldataSystolic" runat="server" Text="Systolic (mm):" AssociatedControlID="dataSystolic" />
                    <asp:TextBox runat="server" ID="dataSystolic" Text='<%# Bind("Systolic") %>'></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataSystolic" runat="server" Display="Dynamic" ControlToValidate="dataSystolic"
                        ErrorMessage=" Allowed range is between 20mm and 300mm" MaximumValue="300" MinimumValue="20"
                        Type="Integer"></asp:RangeValidator>
                </li>
                <li>
                    <asp:Label ID="lbldataDiastolic" runat="server" Text="Diastolic (mm):" AssociatedControlID="dataDiastolic" />
                    <asp:TextBox runat="server" ID="dataDiastolic" Text='<%# Bind("Diastolic") %>'></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataDiastolic" runat="server" Display="Dynamic" ControlToValidate="dataDiastolic"
                        ErrorMessage=" Allowed range is between 20mm and 300mm" MaximumValue="300" MinimumValue="20"
                        Type="Integer"></asp:RangeValidator>
                </li>
                <li>
                    <asp:Label ID="lbldataHeight" runat="server" Text="Height (cm):" AssociatedControlID="dataHeight" />
                    <asp:TextBox runat="server" ID="dataHeight" Text='<%# Bind("Height") %>' onchange="javascript:updateBMI()"></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataHeight" runat="server" Display="Dynamic" ControlToValidate="dataHeight"
                        ErrorMessage=" Allowed range is between 60cm and 300cm" MaximumValue="300" MinimumValue="60"
                        Type="Double"></asp:RangeValidator>
                </li>
                <li>
                    <asp:CheckBox runat="server" ID="dataWeightMissing" Checked='<%# Bind("WeightMissing") %>' CssClass="bigCheck" Text=" Weight unavailable?" 
                        ToolTip="If this information is unavailable, tick this box so we don't raise an outstanding data query."/>
                    <br />
                    <asp:Label ID="lbldataWeight" runat="server" Text="Weight (kg):" AssociatedControlID="dataWeight" />
                    <asp:TextBox runat="server" ID="dataWeight" Text='<%# Bind("Weight") %>' onchange="javascript:updateBMI()"></asp:TextBox><asp:RangeValidator
                        ID="RangeVal_dataWeight" runat="server" Display="Dynamic" ControlToValidate="dataWeight"
                        ErrorMessage=" Allowed range is between 20Kg and 400Kg" MaximumValue="400" MinimumValue="20"
                        Type="Double"></asp:RangeValidator>
                </li>
                <li>
                    <asp:Label ID="Label1" runat="server" Text="BMI (calculated):" AssociatedControlID="dataBMI" />
                    <asp:TextBox runat="server" ID="dataBMI" ReadOnly="true" TabIndex="-1"></asp:TextBox><asp:RangeValidator
                        ID="RangeValidator1" runat="server" Display="Dynamic" ControlToValidate="dataBMI"
                        ErrorMessage=" Allowed range is between 5 and 50. Please check that Weight and Height are entered correctly in the required units. Contact BADBIR if you still want to save this value"
                        MaximumValue="60" MinimumValue="6" Type="Double"></asp:RangeValidator>
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
