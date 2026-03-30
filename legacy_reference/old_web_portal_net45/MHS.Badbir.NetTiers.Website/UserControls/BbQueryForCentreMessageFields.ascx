<%@ Control Language="C#" ClassName="BbQueryForCentreMessageFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
	    <h4>Reply</h4>
        <span>Enter your response below, then click send. </span>
        <br />
	    <asp:CheckBox runat="server" ID="rac" Text="&nbsp;&nbsp; Save as clinician" TextAlign="Right" Font-Italic="true" Font-Bold="false" Checked="false" Visible="true"
	    ToolTip="For Admins Only: You can save this comment on behalf of a clinician. This will save the comment and mark it as unread for admins. " />
	    <br />
        
		<asp:TextBox runat="server" ID="dataMessage" Text='<%# Bind("Message") %>'  TextMode="MultiLine" MaxLength="3000"  Width="500px" Rows="4"></asp:TextBox>
		<asp:RegularExpressionValidator ID="regexTextBox1" ControlToValidate="dataMessage" runat="server" 
		    ValidationExpression="^[\s\S]{0,3000}$" Text="Maximum message length is 3000 characters" />
        <br />
        <asp:TextBox Visible="false"  runat="server" ID="QidTextBox" Text='<%# Bind("Qid") %>' ></asp:TextBox>
        <br />
	</ItemTemplate>
</asp:FormView>


