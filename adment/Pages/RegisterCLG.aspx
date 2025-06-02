<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="RegisterCLG.aspx.cs" Inherits="adment.Pages.RegisterCLG" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validateUsernameLength(sender, args) {
            var username = args.Value;
            if (username.length >= 12) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-8 offset-md-2">
                <div class="card">
                    <div class="card-header bg-success text-white">
                        <h4 class="mb-0">Institute Registration</h4>
                    </div>
                    <div class="card-body">
                        <!-- Institute Name -->
                        <div class="form-group">
                            <asp:Label runat="server" for="txtInstituteName" Text="Name of Institute"></asp:Label>
                            <asp:TextBox runat="server" type="text" class="form-control" id="txtInstituteName" name="txtInstituteName"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInstituteName" runat="server" 
                                ControlToValidate="txtInstituteName"
                                InitialValue=""
                                ErrorMessage="Institute Name is required"
                                ForeColor="Red"
                                Display="Dynamic" 
                                EnableClientScript="false"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Admin Name -->
                        <div class="form-group">
                            <asp:Label ID="lblAdminName" runat="server" Text="Name of Admin"></asp:Label>
                            <asp:TextBox ID="txtAdminName" runat="server" CssClass="form-control" Required="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="txtAdminNamev" runat="server" 
                                ControlToValidate="txtAdminName"
                                InitialValue=""
                                ErrorMessage="ADMIN Name is required"
                                ForeColor="Red"
                                Display="Dynamic" 
                                EnableClientScript="false"></asp:RequiredFieldValidator>
                        </div>

                       

<!-- Create Username -->
<div class="form-group">
    <asp:LinkButton runat="server" ID="usernameerror" Visible="false">Username is in use</asp:LinkButton>
    <asp:Label ID="lblUsername" runat="server" Text="Create Username"></asp:Label>
    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Required="true"></asp:TextBox>
    <asp:Button ID="btnverify" runat="server" Text="Check username is available" CssClass="btn btn-danger" OnClick="registerverify" Visible="true" ValidationGroup="UsernameValidationGroup" />
    <asp:RegularExpressionValidator ID="revUsername" runat="server"
        ControlToValidate="txtUsername"
        ValidationExpression="^(?=.*[a-zA-Z])[a-zA-Z0-9]{12,}$"
        ErrorMessage="Username must contain at least one letter (a-z or A-Z) and be 12 characters or longer. Do not contain any special symbols."
        ForeColor="Red"
        Display="Dynamic"
        EnableClientScript="true"
        ValidationGroup="UsernameValidationGroup"></asp:RegularExpressionValidator>
    <asp:CustomValidator ID="cvUsernameLength" runat="server"
        ControlToValidate="txtUsername"
        ClientValidationFunction="validateUsernameLength"
        ErrorMessage="Username must be 12 characters or longer."
        ForeColor="Red"
        Display="Dynamic"
        EnableClientScript="true"
        ValidationGroup="UsernameValidationGroup"></asp:CustomValidator>
</div>

                        <!-- Email -->
                        <div class="form-group">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" 
                                InitialValue=""
                                ErrorMessage="Email is required"
                                ForeColor="Red"
                                Display="Dynamic"
                                EnableClientScript="false"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ErrorMessage="Invalid email address"
                                ForeColor="Red"
                                Display="Dynamic"></asp:RegularExpressionValidator>
                        </div>

                        <!-- Confirm Email -->
                        <div class="form-group">
                            <asp:Label ID="lblConfirmEmail" runat="server" Text="Confirm Email"></asp:Label>
                            <asp:TextBox ID="txtConfirmEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CompareValidator ID="cvConfirmEmail" runat="server" ControlToValidate="txtConfirmEmail" ControlToCompare="txtEmail" Operator="Equal" Type="String" ErrorMessage="Email addresses do not match." Display="Dynamic" ForeColor="Red"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="rfvConfirmEmail" runat="server" ControlToValidate="txtConfirmEmail"
                                InitialValue=""
                                ErrorMessage="Please confirm your email"
                                ForeColor="Red"
                                Display="Dynamic"
                                EnableClientScript="false"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Set Password -->
                        <div class="form-group">
                            <asp:Label ID="lblPassword" runat="server" Text="Set Password"></asp:Label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="txtPasswordv" runat="server" 
                                ControlToValidate="txtPassword"
                                InitialValue=""
                                ErrorMessage="Password is required"
                                ForeColor="Red"
                                Display="Dynamic" 
                                EnableClientScript="false"></asp:RequiredFieldValidator>

                             <asp:CustomValidator ID="CustomValidator1" runat="server"
        ControlToValidate="txtPassword"
        ClientValidationFunction="validateUsernameLength"
        ErrorMessage="Password must be 12 characters or longer."
        ForeColor="Red"
        Display="Dynamic"
        EnableClientScript="true"
        ValidationGroup="UsernameValidationGroup"></asp:CustomValidator>


                        </div>

                        <!-- Confirm Password -->
                        <div class="form-group">
                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" Operator="Equal" Type="String" ErrorMessage="Passwords do not match." Display="Dynamic" ForeColor="Red"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="txtConfirmPasswordv" runat="server" 
                                ControlToValidate="txtConfirmPassword"
                                InitialValue=""
                                ErrorMessage="This is required"
                                ForeColor="Red"
                                Display="Dynamic" 
                                EnableClientScript="false"></asp:RequiredFieldValidator>
                        </div>

                   <!-- Mobile Number -->
<asp:Panel runat="server" ValidationGroup="RegistrationValidationGroup">
    <div class="form-group">
        <asp:Label ID="lblMobileNumber" runat="server" Text="Mobile Number"></asp:Label>
        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RegularExpressionValidator ID="revMobileNumber" runat="server" ControlToValidate="txtMobileNumber" ValidationExpression="[0-9]{10}" ErrorMessage="Please enter a 10-digit mobile number." ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="txtMobileNumberv" runat="server" 
            ControlToValidate="txtMobileNumber"
            InitialValue=""
            ErrorMessage="This is required"
            ForeColor="Red"
            Display="Dynamic" 
            EnableClientScript="false"></asp:RequiredFieldValidator>      
    </div>
</asp:Panel>

              <!-- Activation Key -->
<div class="form-group">
    <asp:Label ID="lblActivationKey" runat="server" Text="Product Key / Activation Key"></asp:Label>
    <asp:TextBox ID="txtActivationKey" runat="server" CssClass="form-control"></asp:TextBox>
    <asp:LinkButton runat="server" ID="activationerror" Visible="false">Please enter a valid product key</asp:LinkButton>
    <asp:RequiredFieldValidator ID="txtActivationKeyv" runat="server" 
        ControlToValidate="txtActivationKey"
        InitialValue=""
        ErrorMessage="This is required"
        ForeColor="Red"
        Display="Dynamic" 
        EnableClientScript="false"></asp:RequiredFieldValidator>
</div>


                        <asp:CheckBox runat="server" Required="false" Text="Already read all the Terms & Conditions @ADMINT" /> 
                        <br />
                        <!-- Submit Button -->
                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-danger" OnClick="registerme" Visible="false"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
