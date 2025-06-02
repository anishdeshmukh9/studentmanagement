<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="adminportal.aspx.cs" Inherits="adment.Pages.adminportal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            margin-top: 60px;
        }
        .btn-primary {
            margin-bottom: 20px;
        }
        .card-title {
            font-weight: bold;
        }
        .profile-img {
            height: 300px;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .activity-img {
            height: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <!-- Welcome Message -->
        <div class="alert alert-success" role="alert">
            <center>Welcome back, <asp:Label CssClass="text-danger" ID="lblAdminNameWelcome" runat="server" Text=""></asp:Label> ☺️ </center>
        </div>

        <div class="text-center">
            <h1> <i> ADMIN PORTAL! take actions calmly 🤝 ..... </i></h1>
        </div>

        <center>
            <div class="row">
                <div class="col-md-12">
                    <!-- Display Admin Profile Here -->
                    <div class="card" id="adminProfile" style="display: none;">
                        <div class="card-body">
                            <!-- Admin Logo -->
                            <div class="profile-img">
                                <img src="../imgdata/header/admin.png" alt="Admin Logo" class="img-fluid" />
                            </div>
                            <!-- Admin Profile Information -->
                            <h5 class="card-title text-primary">Admin Profile</h5>
                            <p class="text-muted">Welcome, <asp:Label ID="lblAdminName" runat="server" Text=""></asp:Label>!</p>
                            <!-- Display other profile information here -->
                            <div class="row">
                                <div class="col-md-6">
                                    <p class="font-weight-bold">NAME:</p>
                                    <p><asp:Label ID="lblInstituteName" runat="server" Text=""></asp:Label></p>
                                </div>
                                <div class="col-md-6">
                                    <p class="font-weight-bold">College:</p>
                                    <p><asp:Label ID="lblCollegeName" runat="server" Text=""></asp:Label></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </center>

        <br />
        <br />

        <asp:Panel runat="server" ID="panelprof" Visible="false">
            <!-- Edit User Profile Form -->
            <div class="container mt-4">
                <h5>Edit User Profile</h5>
                <div class="form-group">
                    <label for="txtUsername">Username:</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtFirstName"> Name of Admin :</label>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtInstituteName"> Institute Name:</label>
                    <asp:TextBox ID="txtclgName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtMobile"> Mobile number :</label>
                    <asp:TextBox ID="txtmobno" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPassword" runat="server" Text="Set Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" required="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="txtPasswordv" runat="server" ControlToValidate="txtPassword"
                        InitialValue="" ErrorMessage=" Password is required" ForeColor="Red" Display="Dynamic"
                        EnableClientScript="false"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvUsernameLength" runat="server" ControlToValidate="txtPassword"
                        ClientValidationFunction="validateUsernameLength" ErrorMessage="Password must be 12 characters or longer."
                        ForeColor="Red" Display="Dynamic" EnableClientScript="true" ValidationGroup="UsernameValidationGroup"></asp:CustomValidator>
                </div>
                <!-- Confirm Password -->
                <div class="form-group">
                    <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"
                        required="true"></asp:TextBox>
                    <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                        ControlToCompare="txtPassword" Operator="Equal" Type="String" ErrorMessage="Passwords do not match."
                        Display="Dynamic" ForeColor="Red"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="txtConfirmPasswordv" runat="server" ControlToValidate="txtConfirmPassword"
                        InitialValue="" ErrorMessage="This field is required" ForeColor="Red" Display="Dynamic"
                        EnableClientScript="false"></asp:RequiredFieldValidator>
                </div>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="hidepanel" CssClass="btn btn-primary" />
            </div>
        </asp:Panel>

        <!-- Activity Cards Section -->
        <div class="row mt-4">
            <!-- Manage My Profile Card -->
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Edit Profile</h5>
                        <div class="profile-img">
                            <img height="300px" src="" />
                        </div>
                        <br />
                        <asp:Button ID="editprof" runat="server" Text="Update" OnClick="showpanel" CssClass="btn btn-danger" />
                    </div>
                </div>
            </div>

            <!-- Create / Add Staffs Card -->
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Create / Add Staffs</h5>
                        <div class="profile-img">
                            <img height="300px" src="" />
                        </div>
                        <p class="card-text">Add new staff members to your college.</p>
                        <asp:HyperLink ID="hlAddStaffs" runat="server" CssClass="btn btn-success" NavigateUrl="addStaff.aspx">Go to Staffs</asp:HyperLink>
                    </div>
                </div>
            </div>
            
            </div>
   <div class="row mt-4">

            <!-- Track Activity Card -->
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Track Activity</h5>
                        <div class="activity-img">
                            <img height="300px" src="" />
                        </div>
                        <p class="card-text">View and monitor activity logs.</p>
                        <asp:HyperLink ID="hlTrackActivity" runat="server" CssClass="btn btn-warning" NavigateUrl="track_activity.aspx">Go to Activity</asp:HyperLink>
                    </div>
                </div>
            </div>
       
    <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title"> print  all data </h5>
                        <div class="activity-img">
                        </div>
                        <p class="card-text"> print all data of all staf with current status of data is entered  </p>
                        <asp:Button ID="Button1" OnClick="Button1_Click" CssClass="btn btn-primary" runat="server" Text="Button" />
                    </div>
                </div>
            </div>
        </div>
        </div>
   
</asp:Content>
