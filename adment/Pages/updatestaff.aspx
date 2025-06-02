<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="updatestaff.aspx.cs" Inherits="adment.Pages.updatestaff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">

        <div class="col-lg-4">


   <asp:Panel ID="Panel1" runat="server">
    <h2>Update Staff Data</h2>

    <!-- Username (Read-Only) -->
    <div class="form-group">
        <label for="txtUpdateUsername">Username:</label>
<asp:DropDownList ID="DropDownList1" runat="server"  OnSelectedIndexChanged="adddatatofield" CssClass="form-control">
        </asp:DropDownList>    </div>

    <!-- Staff ID Dropdown -->
    <div class="form-group">
        <label for="ddlUpdateStaffID">Select Staff ID:</label>
             <asp:TextBox ID="updateStaffid" runat="server" CssClass="form-control" MaxLength="255" Enabled="false"></asp:TextBox>

    </div>

<div class="form-group">
    <label for="txtUpdateAllocatedSchool">Allocated School:</label>
    <asp:TextBox ID="txtUpdateAllocatedSchool"  runat="server" CssClass="form-control" MaxLength="955" Enabled="false"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvUpdateAllocatedSchool" runat="server" ControlToValidate="txtUpdateAllocatedSchool"
        InitialValue="" ErrorMessage="Allocated School is required." ForeColor="Red" Display="Dynamic" ValidationGroup="UpdateStaffData"></asp:RequiredFieldValidator>

</div>



    <!-- Password -->
    <div class="form-group">
        <label for="txtUpdatePassword">Password:</label>
        <asp:TextBox ID="txtUpdatePassword" runat="server" TextMode="Password" CssClass="form-control" MaxLength="255" Enabled="false"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUpdatePassword" runat="server" ControlToValidate="txtUpdatePassword"
            InitialValue="" ErrorMessage="Password is required." ForeColor="Red" Display="Dynamic" ValidationGroup="UpdateStaffData"></asp:RequiredFieldValidator>
    </div>

    <!-- Confirm Password -->
    <div class="form-group">
        <label for="txtUpdateConfirmPassword">Confirm Password:</label>
        <asp:TextBox ID="txtUpdateConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" MaxLength="255" Enabled="false"></asp:TextBox>
        <asp:CompareValidator ID="cvUpdatePasswordMatch" runat="server" ControlToValidate="txtUpdateConfirmPassword"
            ControlToCompare="txtUpdatePassword" Operator="Equal" Type="String" ErrorMessage="Passwords must match." ForeColor="Red"
            Display="Dynamic" ValidationGroup="UpdateStaffData"></asp:CompareValidator>
    </div>

    <!-- Name -->
    <div class="form-group">
        <label for="txtUpdateName">Name:</label>
        <asp:TextBox ID="txtUpdateName" runat="server" CssClass="form-control" MaxLength="255" Required="true" ValidationGroup="UpdateStaffData" Enabled="false"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUpdateName" runat="server" ControlToValidate="txtUpdateName" InitialValue="" ErrorMessage="Name is required." ForeColor="Red" Display="Dynamic" ValidationGroup="UpdateStaffData"></asp:RequiredFieldValidator>
    </div>

    <!-- Staff Department -->
    <div class="form-group">
        <label for="ddlUpdateStaffDept">Staff Department:</label>
        <asp:DropDownList ID="ddlUpdateStaffDept" runat="server" CssClass="form-control" Required="true" ValidationGroup="UpdateStaffData" Enabled="false">
            <asp:ListItem Text="Select Department" Value="" />
            <asp:ListItem Text="Computer" Value="Computer" />
            <asp:ListItem Text="Electrical" Value="Electrical" />
            <asp:ListItem Text="Mechanical" Value="Mechanical" />
            <asp:ListItem Text="Civil" Value="Civil" />
            <asp:ListItem Text="Electronics" Value="Electronics" />
            <asp:ListItem Text="Fashion" Value="Fashion" />
            <asp:ListItem Text="Interior" Value="Interior" />
            <asp:ListItem Text="Other" Value="Other" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvUpdateStaffDept" runat="server" ControlToValidate="ddlUpdateStaffDept" InitialValue="" ErrorMessage="Staff Department is required." ForeColor="Red" Display="Dynamic" ValidationGroup="UpdateStaffData"></asp:RequiredFieldValidator>
    </div>

    <!-- Staff Designation -->
    <div class="form-group">
        <label for="ddlUpdateStaffDesignation">Staff Designation:</label>
        <asp:DropDownList ID="ddlUpdateStaffDesignation" runat="server" CssClass="form-control" Required="true" ValidationGroup="UpdateStaffData" Enabled="false">
            <asp:ListItem Text="Select Designation" Value="" />
            <asp:ListItem Text="Teaching Staff" Value="Teaching Staff" />
            <asp:ListItem Text="Non-Teaching Staff" Value="Non-Teaching Staff" />
            <asp:ListItem Text="HOD" Value="HOD" />
            <asp:ListItem Text="Other" Value="Other" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvUpdateStaffDesignation" runat="server" ControlToValidate="ddlUpdateStaffDesignation" InitialValue="" ErrorMessage="Staff Designation is required." ForeColor="Red" Display="Dynamic" ValidationGroup="UpdateStaffData"></asp:RequiredFieldValidator>
    </div>

    <!-- Gender -->
    <div class="form-group">
        <label for="ddlUpdateGender">Gender:</label>
        <asp:DropDownList ID="ddlUpdateGender" runat="server" CssClass="form-control" Required="true" ValidationGroup="UpdateStaffData" Enabled="false">
            <asp:ListItem Text="Select Gender" Value="" />
            <asp:ListItem Text="Male" Value="Male" />
            <asp:ListItem Text="Female" Value="Female" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvUpdateGender" runat="server" ControlToValidate="ddlUpdateGender" InitialValue="" ErrorMessage="Gender is required." ForeColor="Red" Display="Dynamic" ValidationGroup="UpdateStaffData"></asp:RequiredFieldValidator>
    </div>

    <!-- Mobile Number -->
    <div class="form-group">
        <label for="txtUpdateMobileNumber">Mobile Number:</label>
        <asp:TextBox ID="txtUpdateMobileNumber" runat="server" CssClass="form-control" MaxLength="20" Required="true" ValidationGroup="UpdateStaffData" Enabled="false"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUpdateMobileNumber" runat="server" ControlToValidate="txtUpdateMobileNumber" InitialValue="" ErrorMessage="Mobile Number is required." ForeColor="Red" Display="Dynamic" ValidationGroup="UpdateStaffData"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revUpdateMobileNumber" runat="server" ControlToValidate="txtUpdateMobileNumber" ValidationExpression="^[0-9]*$" ErrorMessage="Mobile Number must contain only digits." ForeColor="Red" Display="Dynamic" ValidationGroup="UpdateStaffData"></asp:RegularExpressionValidator>
    </div>

    <!-- Date of Birth -->
    <div class="form-group">
        <label for="txtUpdateDOB">Date of Birth:</label>
        <asp:TextBox ID="txtUpdateDOB" runat="server" CssClass="form-control" TextMode="Date" Required="true" ValidationGroup="UpdateStaffData" Enabled="false"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUpdateDOB" runat="server" ControlToValidate="txtUpdateDOB" InitialValue="" ErrorMessage="Date of Birth is required." ForeColor="Red" Display="Dynamic" ValidationGroup="UpdateStaffData"></asp:RequiredFieldValidator>
    </div>

    <!-- Enable/Disable Fields Button -->
    <asp:Button ID="btnEnableFields" runat="server" Text="Enable Fields" CssClass="btn btn-warning" OnClick="btnEnableFields_Click" />
    <!-- Update Button -->
    <asp:Button ID="btnUpdateStaff" OnClick="pushdata" runat="server" Text="Update Staff Data" CssClass="btn btn-success" ValidationGroup="UpdateStaffData" Enabled="false" />
</asp:Panel>






        </div>

        <div class="col-lg-8">


 <div class="container-fluid">
                            <div class="col-12">
                                <h2>Staff List</h2>
                                <div class="table-responsive"  style="max-height: 70vh; overflow-y: auto;">
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:admentConnectionString %>" ProviderName="<%$ ConnectionStrings:admentConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [SAFF_DATA]"></asp:SqlDataSource>
                                    <asp:GridView ID="gvStaffList" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="SR. NO" />
                                            <asp:TemplateField HeaderText="Data_OF_Teacher">
                                                <ItemTemplate>
                                                    <div class="container-fluid">
                                                        <div class="col-11">
                                                            Staff ID&nbsp; :&nbsp;  <asp:Label CssClass=" text-danger" ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Small" Text='<%# Eval("STAFF_ID") %>'></asp:Label>
                                                            &nbsp;<br />
                                                        </div>

                                                        <div class="col-11">
                                                            NAME&nbsp;&nbsp; :
                                                            <asp:Label ID="Label2" CssClass ="btn btn-success"  runat="server" Text='<%# Eval("NAME") %>' Font-Bold="True"></asp:Label>
                                                        </div>

                                                        <div class="col-11">
                                                            Username :
                                                            <asp:Label ID="Label3" runat="server" CssClass=" text-success" Text='<%# Eval("USERNAME") %>' Font-Bold="True"></asp:Label>
                                                        </div>

                                                        <div class="col-11">
                                                            Password&nbsp; :<asp:Label ID="Label4" CssClass=" text-danger" runat="server" Text='<%# Eval("PASS") %>' Font-Bold="True"></asp:Label>
                                                        </div>

                                                        <div class="col-11">
                                                            Department :<asp:Label ID="Label5" runat="server" CssClass=" text-primary" Text='<%# Eval("STAFF_DEPT") %>' Font-Bold="True"></asp:Label>
                                                        </div>

                                                        <div class="col-12">
                                                            Designation :<asp:Label ID="Label6" CssClass=" text-warning" runat="server" Text='<%# Eval("SAFF_DESIG") %>' Font-Bold="True"></asp:Label>
                                                        </div>  

                                                         <div class="col-12">
                                                            MobNo :<asp:Label ID="Label7" runat="server" CssClass=" text-info" Text='<%# Eval("MOBNO") %>' Font-Bold="True"></asp:Label>
                                                        </div>
                                                        <div class="col-12">
                                                            Gender :<asp:Label ID="Label9" runat="server" CssClass=" text-info" Text='<%# Eval("GENDER") %>' Font-Bold="True"></asp:Label>
                                                        </div>  
                                                         <div class="col-12">
                                                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Allocated To : 👇👇
                                                             <asp:Label ID="Label8" runat="server" CssClass="mdb-color white-text" Text='<%# Eval("ALLOCATED_SCHOOL") %>' Font-Bold="True"></asp:Label>

                                                        </div>  


                                                    </div>
                                                </ItemTemplate>
                                              

                                                
                                            </asp:TemplateField>

  
                                            
                                            
                                         
                                        </Columns>
                                    </asp:GridView>


                                </div>

                                <br /><hr />
                                        <asp:ScriptManager runat="server"></asp:ScriptManager>

            <asp:UpdatePanel ID="deletePanel" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h2>Delete Allotment</h2>
                <hr />
                <h3 class="panel-title   alert alert-danger">Delete School can lose school data forever</h3>
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <label for="ddlStaffId">Select Staff ID:</label>
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlStaffId_SelectedIndexChanged" ID="ddlStaffId" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="ddlSchool">Select School:</label>
                    <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                </div>
                <asp:LinkButton ID="btnDeleteSchool"  OnClick="btnDeleteSchool_Click" runat="server" CssClass="btn btn-danger" Text="Delete School" />
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlStaffId" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>


                            </div>

                        </div>

        </div>

       


    </div>

        <asp:LinkButton cssClass="btn btn-success  text-white" OnClick="LinkButton1_Click" ID="LinkButton1" runat="server"> Go  to Previous  Page </asp:LinkButton>


</asp:Content>
