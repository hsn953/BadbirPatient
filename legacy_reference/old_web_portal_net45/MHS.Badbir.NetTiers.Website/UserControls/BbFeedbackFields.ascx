<%@ Control Language="C#" ClassName="BbFeedbackFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<fieldset>
			<legend>Bb Feedback</legend>
			<ol>
			<li>
        <span class="literal"><asp:Label ID="lbldataPatientid" runat="server" Text="Patientid:" AssociatedControlID="dataPatientid" /></span>
        <span>
					<data:EntityDropDownList runat="server" ID="dataPatientid" DataSourceID="PatientidBbPatientDataSource" DataTextField="Firststudyno" DataValueField="Patientid" SelectedValue='<%# Bind("Patientid") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:BbPatientDataSource ID="PatientidBbPatientDataSource" runat="server" SelectMethod="GetAll"  />
				</span>
			</li>
			<li>
        <span class="literal"><asp:Label ID="lbldataFeedback" runat="server" Text="Feedback:" AssociatedControlID="dataFeedback" /></span>
        <span>
					<asp:TextBox runat="server" ID="dataFeedback" Text='<%# Bind("Feedback") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</span>
			</li>
			<li>
        <span class="literal"><asp:Label ID="lbldataStatus" runat="server" Text="Status:" AssociatedControlID="dataStatus" /></span>
        <span>
					<asp:TextBox runat="server" ID="dataStatus" Text='<%# Bind("Status") %>' MaxLength="50"></asp:TextBox>
				</span>
			</li>
			<li>
        <span class="literal"><asp:Label ID="lbldataCreatedbyid" runat="server" Text="Created by (id):" AssociatedControlID="dataCreatedbyid" /></span>
        <span>
					<asp:Label class="audit-field" runat="server" id="dataCreatedbyid" Text='<%# Eval("Createdbyid") %>'></asp:Label>
				</span>
			</li>
			<li>
        <span class="literal"><asp:Label ID="lbldataCreatedbyname" runat="server" Text="Created by:" AssociatedControlID="dataCreatedbyname" /></span>
        <span>
					<asp:Label class="audit-field" runat="server" id="dataCreatedbyname" Text='<%# Eval("Createdbyname") %>'></asp:Label>
				</span>
			</li>
			<li>
        <span class="literal"><asp:Label ID="lbldataCreateddate" runat="server" Text="Date created:" AssociatedControlID="dataCreateddate" /></span>
        <span>
					<asp:Label class="audit-field" runat="server" id="dataCreateddate" Text='<%# Eval("Createddate") %>'></asp:Label>
				</span>
			</li>
			<li>
        <span class="literal"><asp:Label ID="lbldataLastupdatedbyid" runat="server" Text="Last updated by (id):" AssociatedControlID="dataLastupdatedbyid" /></span>
        <span>
					<asp:Label class="audit-field" runat="server" id="dataLastupdatedbyid" Text='<%# Eval("Lastupdatedbyid") %>'></asp:Label>
				</span>
			</li>
			<li>
        <span class="literal"><asp:Label ID="lbldataLastupdatedbyname" runat="server" Text="Last updated by:" AssociatedControlID="dataLastupdatedbyname" /></span>
        <span>
					<asp:Label class="audit-field" runat="server" id="dataLastupdatedbyname" Text='<%# Eval("Lastupdatedbyname") %>'></asp:Label>
				</span>
			</li>
			<li>
        <span class="literal"><asp:Label ID="lbldataLastupdateddate" runat="server" Text="Date last updated:" AssociatedControlID="dataLastupdateddate" /></span>
        <span>
					<asp:Label class="audit-field" runat="server" id="dataLastupdateddate" Text='<%# Eval("Lastupdateddate") %>'></asp:Label>
				</span>
			</li>
			</ol>
		</fieldset>

	</ItemTemplate>
</asp:FormView>


