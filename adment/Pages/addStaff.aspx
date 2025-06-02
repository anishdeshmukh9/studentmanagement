<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="addStaff.aspx.cs" Inherits="adment.Pages.addStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Custom CSS for profile card */
        .profile-card {
            background-color: #f8f9fa;
            border: 1px solid #d1d3e2;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
            max-width: 300px;
            margin-top: 20px;
        }

        .profile-image {
            text-align: center;
        }

        .profile-image img {
            max-width: 150px;
            border-radius: 50%;
            border: 5px solid #fff;
        }

        .profile-details {
            margin-top: 20px;
        }

        .profile-details h4 {
            margin-top: 0;
        }
    </style>
    <script type="text/javascript">
        function togglePanel() {
            var panel = document.getElementById('<%= pnlAddStaffData.ClientID %>');
            var gridPanel = document.getElementById('<%= gridpanel.ClientID %>');

            if (panel.style.display === 'none' || panel.style.display === '') {
                panel.style.display = 'block';
                gridPanel.style.display = 'none';
            } else {
                panel.style.display = 'none';
                gridPanel.style.display = 'block';
            }
        }
    </script>

    <script type="text/javascript">
        // Function to scroll GridView to the bottom
        function scrollGridViewToBottom() {
            var gridView = document.getElementById('<%= gvStaffList.ClientID %>');
            if (gridView) {
                // Scroll to the bottom
                gridView.scrollTop = gridView.scrollHeight;
            }
        }

        // Call the function when the page is loaded
        window.onload = function () {
            scrollGridViewToBottom();
        };
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />

    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-3">

                <div class="panel-heading">

                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnlAddStaffData" runat="server">
                        <h2>Add Staff Data</h2>

                        <!-- Username -->
                        <div class="form-group">
                            <label for="txtUsername">Username:</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" MaxLength="255" Required="true" ValidationGroup="AddStaffData"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" InitialValue="" ErrorMessage="Username is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddStaffData"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Password -->
                        <div class="form-group">
                            <label for="txtPassword">Password:</label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" MaxLength="255" Required="true" ValidationGroup="AddStaffData"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" InitialValue="" ErrorMessage="Password is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddStaffData"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Allocated School -->
                   <div class="form-group">
    <label class="bg-danger text-white" for="txtAllocatedSchool">Allocated School (Enter multiple schools separated by a comma):</label>
    <asp:TextBox ID="txtAllocatedSchool" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" MaxLength="1000"  ValidationGroup="AddStaffData"></asp:TextBox>

<asp:RegularExpressionValidator ID="regexAllocatedSchool" runat="server"
    ControlToValidate="txtAllocatedSchool"
    ValidationExpression="^[A-Za-z]+([,\r\n]+[A-Za-z]+)*$"
    ErrorMessage="Input must contain valid school names separated by commas or new lines, and last school must not end with a , "
    Display="Dynamic"
    ForeColor="Red"
    EnableClientScript="true"
    SetFocusOnError="true" />




</div>



                        <!-- School Address -->


                        <!-- Name -->
                        <div class="form-group">
                            <label for="txtName">Name:</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="255" Required="true" ValidationGroup="AddStaffData"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" InitialValue="" ErrorMessage="Name is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddStaffData"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Staff ID -->
                        <div class="form-group">
                            <label for="txtStaffID">Staff ID:</label>
                            <asp:TextBox ID="txtStaffID" runat="server" CssClass="form-control" TextMode="Number" Required="true" ValidationGroup="AddStaffData"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStaffID" runat="server" ControlToValidate="txtStaffID" InitialValue="" ErrorMessage="Staff ID is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddStaffData"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Staff Department -->
                        <div class="form-group">
                            <label for="ddlStaffDept">Staff Department:</label>
                            <asp:DropDownList ID="ddlStaffDept" runat="server" CssClass="form-control" Required="true" ValidationGroup="AddStaffData">
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
                            <asp:RequiredFieldValidator ID="rfvStaffDept" runat="server" ControlToValidate="ddlStaffDept" InitialValue="" ErrorMessage="Staff Department is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddStaffData"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Staff Designation -->
                        <div class="form-group">
                            <label for="ddlStaffDesignation">Staff Designation:</label>
                            <asp:DropDownList ID="ddlStaffDesignation" runat="server" CssClass="form-control" Required="true" ValidationGroup="AddStaffData">
                                <asp:ListItem Text="Select Designation" Value="" />
                                <asp:ListItem Text="Teaching Staff" Value="Teaching Staff" />
                                <asp:ListItem Text="Non-Teaching Staff" Value="Non-Teaching Staff" />
                                <asp:ListItem Text="HOD" Value="HOD" />
                                <asp:ListItem Text="Other" Value="Other" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvStaffDesignation" runat="server" ControlToValidate="ddlStaffDesignation" InitialValue="" ErrorMessage="Staff Designation is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddStaffData"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Gender -->
                        <div class="form-group">
                            <label for="ddlGender">Gender:</label>
                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" Required="true" ValidationGroup="AddStaffData">
                                <asp:ListItem Text="Select Gender" Value="" />
                                <asp:ListItem Text="Male" Value="Male" />
                                <asp:ListItem Text="Female" Value="Female" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="ddlGender" InitialValue="" ErrorMessage="Gender is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddStaffData"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Mobile Number -->
                        <div class="form-group">
                            <label for="txtMobileNumber">Mobile Number:</label>
                            <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" MaxLength="20" Required="true" ValidationGroup="AddStaffData"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMobileNumber" runat="server" ControlToValidate="txtMobileNumber" InitialValue="" ErrorMessage="Mobile Number is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddStaffData"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revMobileNumber" runat="server" ControlToValidate="txtMobileNumber" ValidationExpression="^[0-9]*$" ErrorMessage="Mobile Number must contain only digits." ForeColor="Red" Display="Dynamic" ValidationGroup="AddStaffData"></asp:RegularExpressionValidator>
                        </div>

                        <!-- Date of Birth -->
                        <div class="form-group">
                            <label for="txtDOB">Date of Birth:</label>
                            <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" TextMode="Date" Required="true" ValidationGroup="AddStaffData"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDOB" runat="server" ControlToValidate="txtDOB" InitialValue="" ErrorMessage="Date of Birth is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddStaffData"></asp:RequiredFieldValidator>
                        </div>
                        <!-- Submit Button -->
                        <asp:Button ID="btnAddStaffData"  OnClick="btnAddStaffData_Click" runat="server" Text="Add Staff Data" CssClass="btn btn-danger" ValidationGroup="AddStaffData" />
                    </asp:Panel>
                </div>
                 </div>

      <script type="text/javascript">
          function printGridViewToPDF() {
              var originalContents = document.body.innerHTML;
              var printReport = document.getElementById('1234').innerHTML;
              document.body.innerHTML = printReport;
              window.print();
              document.body.innerHTML = originalContents;
             
          }
      </script>





                <div class="col-lg-9">
                    <asp:Panel ID="gridpanel" runat="server" CssClass="container"  >
                        <div class="container-fluid">
                            <div class="col-12">
                                <h2>Staff List</h2>
                                <div class="table-responsive"  id="1234" style="max-height: 70vh; overflow-y: auto;">
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:admentConnectionString %>" ProviderName="<%$ ConnectionStrings:admentConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [SAFF_DATA]"></asp:SqlDataSource>
                                    <asp:GridView ID="gvStaffList" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="SR. NO" />
                                            <asp:TemplateField HeaderText="Data_OF_Teacher">
                                                <ItemTemplate>

                                                     <div class="container-fluid">
<div class="profile-image" style="margin-left:4%">
                        <asp:Image ID="imgProfile" runat="server" ImageUrl='<%# GetProfileImagePath(Eval("STAFF_ID")) %>' AlternateText="please refresh" CssClass="img-fluid" />
        </div>                                                        <div class="col-11">
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
                                                             <asp:Label ID="Label8" runat="server" CssClass=" mdb-color white-text" Text='<%# Eval("ALLOCATED_SCHOOL") %>' Font-Bold="True"></asp:Label>

                                                        </div>  


                                                    </div>
                                                </ItemTemplate>
                                              

                                                
                                            </asp:TemplateField>

  
                                            
                                            
                                         
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                            <br />
                            <br />
                            <a  onclick="printGridViewToPDF()"> <b style="background-color: hotpink; color:aliceblue;">Click to Print in PDF </b></a>

                        </div>


                        <br />
                        <br />
                        <br />
                        <br />
                        <br />

<div class="container mt-4">
    <div class="row">
        <div class="col-md-6">
            <div class="card">
               
                <img src="../imgdata/greentik.png" class="card-img-top" alt="Update Image">
                <div class="card-body">
                    <h5 class="card-title">Update Staff</h5>
                    <asp:LinkButton ID="LinkButton3" OnClick="gotoupdate" CssClass="btn btn-success" runat="server">Go to Update Staff</asp:LinkButton>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="card">
                <img src="../imgdata/blaktik.png"" class="card-img-top" alt="Delete Image">
                <div class="card-body">
                    <h5 class="card-title">Delete Staff</h5>
                    <asp:LinkButton ID="LinkButton4" OnClick="gotodelete" CssClass="btn btn-danger" runat="server">Go to Delete Staff</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</div>

                        <br />
                
                        


                    </asp:Panel>


                </div>



           


        </div>
    </div>

    <asp:LinkButton cssClass="btn btn-success  text-white" OnClick="LinkButton1_Click" ID="LinkButton1" runat="server"> Go  to Previous  Page </asp:LinkButton>
    <br /> 
</asp:Content>
