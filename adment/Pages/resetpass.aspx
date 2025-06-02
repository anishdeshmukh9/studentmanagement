<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="resetpass.aspx.cs" Inherits="adment.Pages.resetpass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1"  runat="server">
    <!-- Email Input Field -->
    <div class="form-group">
        <asp:Label ID="lblEmail" runat="server" Text="Email Address"></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
            InitialValue=""
            ErrorMessage="Email is required"
            ForeColor="Red"
            Display="Dynamic"
            EnableClientScript="false"
            ValidationGroup="group1"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            ErrorMessage="Invalid email address"
            ForeColor="Red"
            Display="Dynamic"
            ValidationGroup="group1"></asp:RegularExpressionValidator>
    </div>

    <!-- Submit Button -->
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="group1" OnClick="btnSubmit_Click"  />
    
    <!-- Success Message (Initially Hidden) -->
    <asp:Label ID="lblSuccess" runat="server" Text="Password reset instructions sent to your registered email." CssClass="text-success" Visible="false"></asp:Label>
    
    <!-- Error Message (Initially Hidden) -->
    <asp:Label ID="lblError" runat="server" Text="This email is not registered." CssClass="text-danger" Visible="false"></asp:Label>
</asp:Panel>

        <asp:LinkButton cssClass="btn btn-success  text-white" OnClick="LinkButton1_Click" ID="LinkButton1" runat="server"> Go  to Home  Page </asp:LinkButton>
    <br />
</asp:Content>
