<%@ Control Language="C#" ClassName="BbQueryFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
        <fieldset>
            <ol>
                <li runat="server" id="li1"><span class="literal">
                    <asp:Label ID="lbldataQueryStatusId" runat="server" Text="Query Status:" AssociatedControlID="dataQueryStatusId" /></span>
                    <span>
                        <data:EntityDropDownList runat="server" ID="dataQueryStatusId" DataSourceID="QueryStatusIdBbQueryStatuslkpDataSource"
                            DataTextField="QueryStatus" DataValueField="QueryStatusId" SelectedValue='<%# Bind("QueryStatusId") %>'
                            AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required"
                            Enabled="false" />
                        <data:BbQueryStatuslkpDataSource ID="QueryStatusIdBbQueryStatuslkpDataSource" runat="server"
                            SelectMethod="GetAll" />
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataCentreid" runat="server" Text="Centreid:" AssociatedControlID="dataCentres" /></span>
                    <span>
                        <data:EntityDropDownList runat="server" ID="dataCentres" DataSourceID="centreDS"
                            DataTextField="CentreName" DataValueField="CentreID" SelectedValue='<%# Bind("Centreid") %>'
                            AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" AutoPostBack="true"
                            />
                        <data:BbCentreDataSource ID="centreDS" runat="server" SelectMethod="GetAll" />
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataChid" runat="server" Text="Study ID:" AssociatedControlID="dataChid" /></span>
                    <span>
                        <data:EntityDropDownList runat="server" ID="dataChid" DataSourceID="Chidds" DataTextField="Studyno"
                            DataValueField="Chid" SelectedValue='<%# Bind("Chid") %>' AppendNullItem="true"
                            Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />

                        <data:BbPatientCohortHistoryDataSource ID="Chidds" runat="server" SelectMethod="GetByRegcentreid"
                            EnableCaching="false" EnableSorting="true" Sort="studyno">
                            <Parameters>
                                <asp:SessionParameter Name="RegCentreID" SessionField="UserCentreid" />
                            </Parameters>
                        </data:BbPatientCohortHistoryDataSource>


                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataFupnumber" runat="server" Text="Follow Up Number:" AssociatedControlID="dataFupNumberDD" /></span>
                    <span>
                        <asp:DropDownList runat="server" ID="dataFupNumberDD" SelectedValue='<%# Bind("Fupnumber")%>'>
                            <asp:ListItem Value="" Text="Please Choose .." />
                            <asp:ListItem Value="0" Text="Baseline" />
                            <asp:ListItem Value="1" Text="1" />
                            <asp:ListItem Value="2" Text="2" />
                            <asp:ListItem Value="3" Text="3" />
                            <asp:ListItem Value="4" Text="4" />
                            <asp:ListItem Value="5" Text="5" />
                            <asp:ListItem Value="6" Text="6" />
                            <asp:ListItem Value="7" Text="7" />
                            <asp:ListItem Value="8" Text="8" />
                        </asp:DropDownList>
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataQueryTypeId" runat="server" Text="Query Type:" AssociatedControlID="dataQueryTypeId" /></span>
                    <span>
                        <data:EntityDropDownList runat="server" ID="dataQueryTypeId" DataSourceID="QueryTypeIdBbQueryTypelkpDataSource"
                            Enabled="false" DataTextField="QueryType" DataValueField="QueryTypeId" SelectedValue='<%# Bind("QueryTypeId") %>'
                            AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
                        <data:BbQueryTypelkpDataSource ID="QueryTypeIdBbQueryTypelkpDataSource" runat="server"
                            SelectMethod="QueryTypelkp_GetAllForClinician" />
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="Label1" runat="server" Text="Description:" AssociatedControlID="dataQueryTypeId" /></span>
                    <span>
                        <asp:TextBox runat="server" ID="TitleBox" Text='<%#Bind("Subject") %>' MaxLength="1000"
                            Width="400" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqQueryDescription" ControlToValidate="TitleBox" runat="server" Text="Please enter a description."></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexTextBox1" ControlToValidate="TitleBox" runat="server"
                            ValidationExpression="^[\s\S]{5,1000}$" Text="Description must be between 5 and 1000 characters long." />
                    </span></li>
                    
                                    <li runat="server"  visible="false"><span class="literal">
                    <asp:Label ID="Label3" runat="server" Text="Is this a second request?: " AssociatedControlID="RadioButtonList1" /></span>
                    <span>
                        <asp:RadioButtonList runat="server" ID="RadioButtonList1" SelectedValue='<%# Bind("SecondRequest") %>'>
                            <asp:ListItem Value="True" Text="Yes" ></asp:ListItem>
                            <asp:ListItem Value="False" Text="No" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </span></li>
                    
                <li runat="server" visible="false"><span class="literal">
                    <asp:Label ID="lbldataInclude" runat="server" Text="Include:" AssociatedControlID="dataInclude" /></span>
                    <span>
                        <asp:RadioButtonList runat="server" ID="dataInclude" SelectedValue='<%# Bind("Include") %>'>
                            <asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="False" Text="No"></asp:ListItem>
                        </asp:RadioButtonList>
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="Label2" runat="server" Font-Italic="true">This query was generated on <%# Eval("Createddate","{0:dd MMMM yyyy HH:mm}")%>
                    by <%# Eval("Createdbyname")%>
                    </asp:Label>
                    <asp:TextBox ID="unread" runat="server" Text='<%#Bind("Adminunread")%>' Visible="false" />
                </span></li>
            </ol>
        </fieldset>
    </ItemTemplate>
</asp:FormView>
