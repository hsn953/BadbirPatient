<%@ Control Language="C#" ClassName="BbQueryFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
        <fieldset>
            <ol>
                <li runat="server" id="li1"><span class="literal">
                    <asp:Label ID="lbldataQueryStatusId" runat="server" Text="Query Status:" AssociatedControlID="dataQueryStatusId" /></span>
                    <span>
                    <asp:Label ID="ValStatus" runat="server"/>

                        <data:EntityDropDownList runat="server" ID="dataQueryStatusId" DataSourceID="QueryStatusIdBbQueryStatuslkpDataSource"
                            DataTextField="QueryStatus" DataValueField="QueryStatusId" SelectedValue='<%# Bind("QueryStatusId") %>'
                            AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required"
                            Visible="false" />
                        <data:BbQueryStatuslkpDataSource ID="QueryStatusIdBbQueryStatuslkpDataSource" runat="server"
                            SelectMethod="GetAll" />
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataCentreid" runat="server" Text="Centre:" AssociatedControlID="dataCentres" /></span>
                    <span>
                    <asp:Label ID="ValCentre" runat="server" />
                        <data:EntityDropDownList runat="server" ID="dataCentres" DataSourceID="centreDS"
                            DataTextField="CentreName" DataValueField="CentreID" SelectedValue='<%# Bind("Centreid") %>'
                            AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" Visible="false" />
                        <data:BbCentreDataSource ID="centreDS" runat="server" SelectMethod="GetAll" />
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataChid" runat="server" Text="Study ID:" AssociatedControlID="dataChid" /></span>
                    <span>
                        <asp:Label ID="ValStudyno" runat="server" />
                        <data:EntityDropDownList runat="server" ID="dataChid" DataSourceID="Chidds" DataTextField="Studyno" Enabled=false
                            DataValueField="Chid" SelectedValue='<%# Bind("Chid") %>' AppendNullItem="true"
                            Required="true" ErrorText="Required" Visible="false" />
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataFupnumber" runat="server" Text="Follow Up Number:" AssociatedControlID="dataFupNumberDD" /></span>
                    <span>
                        <asp:Label runat="server" ID="dataFupNumberDD" Text='<%# Bind("Fupnumber")%>'/>
                        <asp:Label runat="server" ID="Label3" Text='Baseline' Visible="false"/>
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataQueryTypeId" runat="server" Text="Query Type:" AssociatedControlID="dataQueryTypeId" /></span>
                    <span>
                    <asp:Label ID="ValType" runat="server" />
                    
                        <data:EntityDropDownList runat="server" ID="dataQueryTypeId" DataSourceID="QueryTypeIdBbQueryTypelkpDataSource"
                            Enabled="false" DataTextField="QueryType" DataValueField="QueryTypeId" SelectedValue='<%# Bind("QueryTypeId") %>'
                            AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" Visible="false"/>
                        <data:BbQueryTypelkpDataSource ID="QueryTypeIdBbQueryTypelkpDataSource" runat="server"
                            SelectMethod="GetAll" EnableCaching="true" />
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="Label1" runat="server" Text="Description:" AssociatedControlID="dataQueryTypeId" /></span>
                    <span>
                        <asp:TextBox runat="server" ID="TitleBox" Text='<%#Bind("Subject") %>' MaxLength="250"
                            Width="400" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqQueryDescription" ControlToValidate="TitleBox" runat="server" Text="Please enter the details."></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexQueryDescription" ControlToValidate="TitleBox" runat="server"
                            ValidationExpression="^[\s\S]{5,500}$" Text="Feedback must be between 5 and 500 characters long." />
                    </span></li>
                <li runat="server" visible="false"><span class="literal">
                    <asp:Label ID="lbldataInclude" runat="server" Text="Include:" AssociatedControlID="dataInclude" /></span>
                    <span>
                        <asp:RadioButtonList runat="server" ID="dataInclude" SelectedValue='<%# Bind("Include") %>'>
                            <asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="False" Text="No"></asp:ListItem>
                        </asp:RadioButtonList>
                    </span></li>
                                    <li id="Li2" runat="server"  visible="false"><span class="literal">
                    <asp:Label ID="Label5" runat="server" Text="Is this a second request?: " AssociatedControlID="RadioButtonList1" /></span>
                    <span>
                        <asp:RadioButtonList runat="server" ID="RadioButtonList1" SelectedValue='<%# Bind("SecondRequest") %>'>
                            <asp:ListItem Value="True" Text="Yes" ></asp:ListItem>
                            <asp:ListItem Value="False" Text="No" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </span></li>

                <li><span class="literal">
                    <asp:Label ID="Label2" runat="server" Font-Italic="true" Visible='<%# Eval("Createdbyname") == null ? false : true %>'>This query was generated on <%# Eval("Createddate","{0:dd MMMM yyyy HH:mm}")%>
                    by <%# Eval("Createdbyname")%>
                    </asp:Label>
                    <asp:TextBox ID="unread" runat="server" Text='<%#Bind("Adminunread")%>' Visible="false" />
                </span></li>
            </ol>
        </fieldset>
    </ItemTemplate>
</asp:FormView>
