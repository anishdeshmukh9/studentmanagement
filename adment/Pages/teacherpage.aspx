<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="teacherpage.aspx.cs" Inherits="adment.Pages.teacherpage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
    // Replace 'DropDownList1' with the actual ClientID of your DropDownList if needed
    var dropdown = document.getElementById('<%= DropDownList1.ClientID %>');

    // Set the default text (placeholder)
    var defaultText = "Select an Option";

    // Create an option element for the default text
    var defaultOption = document.createElement("option");
    defaultOption.text = defaultText;
    defaultOption.value = ""; // You can set this to an empty value or any other value

    // Add the default option as the first child of the dropdown
    dropdown.insertBefore(defaultOption, dropdown.options[0]);

    // Set the default text as selected
    dropdown.selectedIndex = 0;

    // Handle the change event to prevent selecting the default option
    dropdown.addEventListener("change", function () {
        if (dropdown.selectedIndex === 0) {
            // Do not allow selecting the default option
            // You can add additional handling here if needed
            dropdown.selectedIndex = -1;
        }
    });
    </script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
  
        <div class="row">
            <div class="col-lg-4">
                <div class="profile-card">
                  <div class="row"><div class="profile-image" style="margin-left:20%">
        <center>   <asp:Image ID="imgProfile" runat="server" ImageUrl="~/imgdata/staff/teacher.png" AlternateText="please refresh" CssClass="img-fluid" /></center> 
        </div>
   <hr />
        <asp:FileUpload ID="fileUpload"  runat="server" CssClass=" mt-2 btn btn-warning  form-control-file" />
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-danger"></asp:Label>
        <asp:Button ID="btnUpload" runat="server" Text="Change Profile Image" OnClientClick="return validateFile()" OnClick="btnUpload_Click" CssClass="btn btn-primary mt-2" />
 
 
        
  </div>

<script>
    function validateFile() {
        var fileUpload = document.getElementById('<%= fileUpload.ClientID %>');
        var lblMessage = document.getElementById('<%= lblMessage.ClientID %>');

        // Check if a file is selected
        if (fileUpload.value === "") {
            lblMessage.innerHTML = "Please select an image to upload.";
            return false;
        }

        // Check if the selected file is an image
        var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
        if (!allowedExtensions.exec(fileUpload.value)) {
            lblMessage.innerHTML = "Please upload a valid image file (jpg, jpeg, png, gif).";
            return false;
        }

        // Reset error message if validation passes
        lblMessage.innerHTML = "";
        return true;
    }
</script>

                    <div class="profile-details">
    <h4><asp:Label ID="lblTeacherName" runat="server" Font-Bold="True"></asp:Label></h4>
    <p>Staff ID: <asp:Label ID="lblStaffID" runat="server" Font-Bold="True"></asp:Label></p>
    <p>Department: <asp:Label ID="lblStaffDept" runat="server" Font-Bold="True"></asp:Label></p>
    <p>Designation: <asp:Label ID="lblStaffDesignation" runat="server" Font-Bold="True"></asp:Label></p>
    <p>Gender: <asp:Label ID="lblStaffGender" runat="server" Font-Bold="True"></asp:Label></p>
    <p>Mobile Number: <asp:Label ID="lblStaffMobileNumber" runat="server" Font-Bold="True"></asp:Label></p>
    <p>Date of Birth: <asp:Label ID="lblStaffDOB" runat="server" Font-Bold="True"></asp:Label></p>
    <p>Allocated School: <asp:Label ID="lblAllocatedSchool" runat="server" Font-Bold="True"></asp:Label></p>
</div>

                </div>
            </div>

           <div class="col-lg-8">
    <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Select Your Allotment</h3>
        </div>
        <div class="panel-body">
           <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="createtableforSchool "  Required="true" >
                 
                             

</asp:DropDownList>

     <asp:RequiredFieldValidator ID="rfvStaffDept" runat="server" ControlToValidate="DropDownList1" InitialValue="" ErrorMessage="required." ForeColor="Red" Display="Dynamic" ></asp:RequiredFieldValidator>

            <br /> 
            <div id="progressChart">

            </div>

                            <asp:Label ID="Label1123" CssClass="alert alert-danger" role="alert" runat="server" Text="before feeding data you shoud complet the school form given below !"></asp:Label>

            <br />
            <br />
            <br />

            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="redirectFields" CssClass="btn btn-danger">Feed Data</asp:LinkButton>
        </div>
        <br /><hr />
    </asp:Panel>
              <asp:Panel ID="Panel2" runat="server">
    <div class="container">
        <h2>School Profile Form</h2>
        <hr />

        <div class="form-group">
            <label for="txtSchoolName">School Name:</label>
            <asp:TextBox ID="txtSchoolName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtAddAddress">Address:</label>
            <asp:TextBox ID="txtAddAddress" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="ddlGrade">Grade:</label>
            <asp:DropDownList ID="ddlGrade" runat="server"  CssClass="form-control">

                <asp:ListItem Text="10th" Value="10th" />
                <asp:ListItem Text="12th" Value="12th" />
                <asp:ListItem Text="12th - Science" Value="12th - Science" />
                <asp:ListItem Text="12th - Arts" Value="12th - Arts" />
                <asp:ListItem Text="12th - commerce" Value="12th - commerce" />
                <asp:ListItem Text="12th - others" Value="12th - others" />

                <asp:ListItem Text="ITI" Value="ITI" />

            </asp:DropDownList>
        </div>

        <div class="form-group"> 
            <label for="txtSchoolContact">School Contact:</label>
            <asp:TextBox ID="txtSchoolContact" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtTotalStudents">Total Students:</label>
            <asp:TextBox ID="txtTotalStudents" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="btnSubmit"  OnClick="update_the_data" runat="server" Text="SUBMIT" CssClass="btn btn-success" />
         <asp:Button ID="btnUpdate" OnClick="enableFields" runat="server"  Text="enable editing" CssClass="btn btn-warning"  />
    </div>
</asp:Panel>

</div>

        </div>
    </div>
</asp:Content>
