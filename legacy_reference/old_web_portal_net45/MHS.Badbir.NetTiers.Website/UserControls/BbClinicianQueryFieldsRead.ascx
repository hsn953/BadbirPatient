<%@ Control Language="C#" ClassName="BbQueryFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
        <fieldset>
            <ol>
                <li><span class="literal">
                    <asp:Label runat="server" ID="lblDataQueryStatus" class="queryFieldsLabel" Text="Query Status:" AssociatedControlID="dataQueryStatusId" CssClass="queryFieldsLabel" />
                    <asp:RadioButtonList runat="server" ID="dataQueryStatusId" SelectedValue='<%# Eval("QueryStatusId") %>' Enabled="false">
                        <asp:ListItem Value="3" Text='<i class="fas fa-check-circle fa-fw"></i> Solved'></asp:ListItem>
                        <asp:ListItem Value="5" Text='<i class="fas fa-folder-open fa-fw"></i> Open'></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li><span class="literal">
                    <asp:Label ID="lbldataChid" runat="server" Text="Study ID:" AssociatedControlID="dataChid" CssClass="queryFieldsLabel" /></span>
                    <span>
                        <asp:Label ID="dataChid" runat="server" Text='<%#Bind("Chidsource.studyno") %>' />
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataFupnumber" runat="server" Text="Follow Up:" AssociatedControlID="dataFupNumberDD" CssClass="queryFieldsLabel" /></span>
                    <span>
                        <asp:Label ID="dataFupNumberDD" runat="server" Text='<%# (Eval("Fupnumber") != null && Eval("Fupnumber").ToString().Equals("0"))? "Baseline" : Eval("Fupnumber")%>' />
                    </span></li>
                <li><span class="literal">
                    <asp:Label ID="lbldataQueryTypeId" runat="server" Text="Query Type:" AssociatedControlID="dataQueryTypeId" CssClass="queryFieldsLabel" /></span>
                    <span>
                        <asp:Label ID="dataQueryTypeId" runat="server" Text='<%#Bind("QueryTypeIdsource.QueryType") %>' />
                    </span></li>
                <li>
                    <span class="literal">
                    <asp:Label ID="Label1" runat="server" Text="Description:" AssociatedControlID="dataQueryTypeId" CssClass="queryFieldsLabel queryFieldsLabelTA" /></span>
                    <span>
                        <asp:TextBox runat="server" ID="TitleBox" Text='<%#Bind("Subject")%>' ReadOnly="true" Width="500" Rows="5"
                            TextMode="MultiLine" BackColor="Transparent" BorderColor="Transparent" />
                    </span>
                </li>
                <li runat="server" id="secondreq">
                    <asp:Label ID="Label3" runat="server" Text="Priority:" AssociatedControlID="RadioButtonList1" CssClass="queryFieldsLabel"/></span>
                    <asp:RadioButtonList runat="server" ID="RadioButtonList1" SelectedValue='<%# Bind("SecondRequest") %>' Enabled="false"> 
                        <asp:ListItem Value="False" Text='<i class="fas fa-minus fa-fw"></i> Normal' Selected="True"></asp:ListItem>
                        <asp:ListItem Value="True" Text='<i class="fas fa-exclamation-triangle fa-fw"></i> High' ></asp:ListItem>
                    </asp:RadioButtonList>
                </li>

                <li><span class="literal">
                    <asp:Label ID="lbldataCreateddate" runat="server" Font-Italic="true">This query was generated on <%# Eval("Createddate","{0:dd MMMM yyyy HH:mm}")%>
                    by <%# Eval("Createdbyname")%>
                    </asp:Label>
                </span></li>
            </ol>
        </fieldset>
    </ItemTemplate>
</asp:FormView>
