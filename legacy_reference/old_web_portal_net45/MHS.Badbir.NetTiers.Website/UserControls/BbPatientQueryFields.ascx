<%@ Control Language="C#" ClassName="BbPatientQueryFields" %>

<asp:FormView ID="FormView1" runat="server">
     <ItemTemplate>
     
     
     
     
        <fieldset>
            <ol>
                <li><span class="literal">
                    <asp:Label runat="server" ID="lblDataQueryStatus" class="queryFieldsLabel" Text="Query Status:" AssociatedControlID="dataQueryStatusId" CssClass="queryFieldsLabel" />
                    <asp:RadioButtonList runat="server" ID="dataQueryStatusId" SelectedValue='<%# Bind("QueryStatusId") %>'> 
                        <asp:ListItem Value="3" Text='<i class="fas fa-check-circle fa-fw"></i> Solved'></asp:ListItem>
                        <asp:ListItem Value="5" Text='<i class="fas fa-folder-open fa-fw"></i> Open' ></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <asp:Label ID="lbldataCentreid" runat="server" Text="Centre:" AssociatedControlID="selectedCenter" CssClass="queryFieldsLabel"/>
                    
                    <select id="centredd" onchange="setCentre(this.value,0)"> <option value="">Select Centre</option> </select>
                    <asp:TextBox ID="selectedCenter" runat="server" Text='<%# Bind("Centreid") %>' style="display:none;" />
                    
                    <asp:CompareValidator runat="server" ID="centrecheck" ControlToValidate="selectedCenter" Operator="GreaterThan" 
                                        ValueToCompare="0" ErrorMessage="Center is required" Display="Dynamic" />
                    <asp:RequiredFieldValidator runat="server" ID="rcehck2" ControlToValidate="selectedCenter" 
                                        ErrorMessage="Center is required" Display="Dynamic" />
                                        
                </li>
                <li>
                    <asp:Label ID="lbldataChid" runat="server" Text="Study ID:" AssociatedControlID="selectedChid" CssClass="queryFieldsLabel"/>
                    <select id="studynodd" onchange="setStudyid(this.value,0)"> <option value="">Select Centre First</option></select>
                    <asp:TextBox ID="selectedChid" runat="server" Text='<%# Bind("Chid") %>' style="display:none;"/>


                    <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="selectedChid" Operator="GreaterThan" 
                    ValueToCompare="0" ErrorMessage="Study ID is required" Display="Dynamic" />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="selectedChid" 
                                        ErrorMessage="Study ID is required" Display="Dynamic" />
                </li>
                <li><span class="literal">
                    <asp:Label ID="lbldataFupnumber" runat="server" Text="Follow Up Number:" AssociatedControlID="selectedFup" CssClass="queryFieldsLabel" /></span>
                    <span>
                    <select id="fupdd" onchange="setFup(this.value,0)"><option value="">Select Centre & Study No First</option> </select>                    
                    <asp:TextBox ID="selectedFup" runat="server" Text='<%# Bind("Fupnumber") %>' style="display:none;" />
                    
                    
                    </li>
                <li><span class="literal">
                    <asp:Label ID="lbldataQueryTypeId" runat="server" Text="Query Type:" AssociatedControlID="dataQueryTypeId" CssClass="queryFieldsLabel"/></span>
                    <span>
                        <data:EntityDropDownList runat="server" ID="dataQueryTypeId" DataSourceID="QueryTypeIdBbQueryTypelkpDataSource"
                            DataTextField="QueryType" DataValueField="QueryTypeId" SelectedValue='<%# Bind("QueryTypeId") %>'
                            Enabled="false" />
                        <!-- QueryTypeId is locked to 'Direct patient feedback' (ID #43) -->
                        <data:BbQueryTypelkpDataSource ID="QueryTypeIdBbQueryTypelkpDataSource" runat="server"
                            SelectMethod="GetByQueryTypeId">
                            <Parameters>
                                <asp:Parameter Name="QueryTypeId" Type="String" DefaultValue="43" />
                            </Parameters>
                        </data:BbQueryTypelkpDataSource>
                    </span>
                </li>
                <li><span class="literal">
                    <asp:Label ID="Label1" runat="server" Text="Description:" AssociatedControlID="dataQueryTypeId" CssClass="queryFieldsLabel queryFieldsLabelTA"/></span>
                    <span>
                        <asp:TextBox runat="server" ID="TitleBox" Text='<%#Bind("Subject") %>' MaxLength="4000"
                            Width="500" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="reqQueryDescription" ControlToValidate="TitleBox" runat="server" Text="Please enter a description."></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexTextBox1" ControlToValidate="TitleBox" runat="server"
                            ValidationExpression="^[\s\S]{5,4000}$" Text="Description must be between 5 and 4000 characters long." />
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataInclude" runat="server" Text="Valid:" AssociatedControlID="dataInclude" CssClass="queryFieldsLabel"/>
                    <span>
                        <asp:RadioButtonList runat="server" ID="dataInclude" SelectedValue='<%# Bind("Include") %>' RepeatDirection="Horizontal">
                            <asp:ListItem Value="True" Text='<i class="fas fa-check fa-fw"></i> Yes' Selected="True"></asp:ListItem>
                            <asp:ListItem Value="False" Text='<i class="fas fa-times fa-fw"></i> No'></asp:ListItem>
                        </asp:RadioButtonList>
                    </span>
                </li>
                <li><span class="literal">
                    <asp:Label ID="Label3" runat="server" Text="Priority:" AssociatedControlID="RadioButtonList1" CssClass="queryFieldsLabel"/></span>
                    <asp:RadioButtonList runat="server" ID="RadioButtonList1" SelectedValue='<%# Bind("SecondRequest") %>'> 
                        <asp:ListItem Value="False" Text='<i class="fas fa-minus fa-fw"></i> Normal' Selected="True"></asp:ListItem>
                        <asp:ListItem Value="True" Text='<i class="fas fa-exclamation-triangle fa-fw"></i> High' ></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li><span class="literal">
                    <asp:Label ID="Label2" runat="server" Font-Italic="true">This query was generated on <%# Eval("Createddate","{0:dd MMMM yyyy HH:mm}")%>
                    by <%# Eval("Createdbyname")%>
                    </asp:Label>
                    <asp:TextBox ID="unread" runat="server" Text='<%#Bind("Clinicianunread")%>' Visible="false" />
                </span></li>
            </ol>
        </fieldset>
    </ItemTemplate>
</asp:FormView>
