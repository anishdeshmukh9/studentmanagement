<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="feedData.aspx.cs" Inherits="adment.Pages.feedData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
    .file-upload-container {
        text-align: center;
    }

    .file-upload-container h2 {
        font-size: 24px;
    }

    .file-upload-container h2:last-child {
        margin-top: 20px;
    }

    .file-upload-container input[type="file"] {
        display: none;
    }

    .file-upload-button {
        display: inline-block;
        padding: 10px 20px;
        background-color: #4285F4;
        color: #fff;
        border: none;
        cursor: pointer;
        font-size: 16px;
    }

    .file-upload-button:hover {
        background-color: #357ABD;
    }

    .file-list {
        margin-top: 10px;
    }

    .file-link {
        display: block;
        font-size: 18px;
        text-decoration: none;
        color: #4285F4;
        margin-bottom: 5px;
    }

    .file-link:hover {
        text-decoration: underline;
    }
</style>
<script>
    function scrollToBottom() {
        var scrpanel = document.getElementById('scrpanel');
        scrpanel.scrollTop = scrpanel.scrollHeight;
    }

    // Call scrollToBottom() whenever new data is added to the div
    // For example, you can call it after appending new content to the div
    // For demonstration purposes, a setTimeout is used here, but you should call it where you actually add new content
    setTimeout(function () {
        // Add your code to append new content here

        // Call scrollToBottom to scroll to the bottom
        scrollToBottom();
    }, 0);
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <h1> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h1>
    
    <div class="row">

       <div class="col-4 ">

          
           <div class="container mt-3">
        <h4>Add Student Data</h4>
        <hr />

        <asp:TextBox required="true" ID="txtStudentID" runat="server" CssClass="form-control mb-2" placeholder="Student ID *"></asp:TextBox>
        <asp:TextBox required="true" ID="txtFullName" runat="server" CssClass="form-control mb-2" placeholder="Full Name *"></asp:TextBox>
        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control mb-2" placeholder="Address"></asp:TextBox>
        <asp:TextBox  ID="txtCasteCategory" runat="server" CssClass="form-control mb-2" placeholder="Caste / Category "></asp:TextBox>
                            <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" TextMode="Date"  ValidationGroup="AddStaffData"></asp:TextBox>

               <br />
     <asp:TextBox Required="true" ID="txtMobileNumber" runat="server" CssClass="form-control mb-2" placeholder="Mobile Number *"></asp:TextBox>
<asp:RegularExpressionValidator ID="revMobileNumber" runat="server" ControlToValidate="txtMobileNumber"
    ValidationExpression="^\d{10}$" ErrorMessage="Mobile Number must be 10 digits long." ForeColor="Red" Display="Dynamic" ValidationGroup="AddStaffData"></asp:RegularExpressionValidator>

<asp:TextBox   ID="txtWhatsAppNumber" runat="server" CssClass="form-control mb-2" placeholder="WhatsApp Number"></asp:TextBox>
<asp:RegularExpressionValidator ID="revWhatsAppNumber" runat="server" ControlToValidate="txtWhatsAppNumber"
    ValidationExpression="^\d{10}$" ErrorMessage="WhatsApp Number must be 10 digits long." ForeColor="Red" Display="Dynamic" ValidationGroup="AddStaffData"></asp:RegularExpressionValidator>


        <asp:Button ID="btnAddData" runat="server" Text="Add Data" OnClick="feedDatabtn" CssClass="btn btn-success mb-2"  />
    </div>

          

       </div>

       <div class="col-lg-8" onload="abc()" id="scrpanel" style="height: 50vh; overflow: scroll;">



           

            <br />
           <asp:GridView ID="GridView1"  runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="StudentID" HeaderText="Student ID" />
        <asp:BoundField DataField="FullName" HeaderText="Full Name" />
        <asp:BoundField DataField="Address" HeaderText="Address" />
        <asp:BoundField DataField="CasteCategory" HeaderText="Caste / Category" />
        <asp:BoundField DataField="DOB" HeaderText="Date of Birth" DataFormatString="{0:yyyy-MM-dd}" />
        <asp:BoundField DataField="MobileNumber" HeaderText="Mobile Number" />
        <asp:BoundField DataField="WhatsAppNumber" HeaderText="WhatsApp Number" />
    </Columns>
    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" />
    <PagerStyle CssClass="pager" />
</asp:GridView>
<asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
    <asp:ListItem Text="10" Value="10" />
    <asp:ListItem Text="20" Value="20" />
    <asp:ListItem Text="50" Value="50" />
    <asp:ListItem Text="All" Value="0" />
</asp:DropDownList>









        </div>


            <asp:LinkButton cssClass="btn btn-info  text-white" OnClick="LinkButton3_Click" ID="LinkButton3" runat="server"> Freze Data & go  to Previous  Page </asp:LinkButton>

     <br />   
       </div>

    <div class="row">

        <div class="col-5">

        </div>
        <div class="col-lg-7">
                                    <asp:LinkButton ID="LinkButton1" OnClick="ExportToPdf" CssClass="btn btn-danger" runat="server">Print To Pdf</asp:LinkButton>

        </div>

    </div>
     <div>
            <h2>File Upload</h2>
            <asp:FileUpload  CssClass="btn btn-success" ID="FileUploadControl" runat="server" />

         <asp:LinkButton ID="LinkButton2" OnClick="UploadButton_Click" runat="server">Upload</asp:LinkButton>

            <asp:ListView ID="FilesListView" runat="server">
                <LayoutTemplate>
                    <ul>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li>
                      <asp:LinkButton  Text='<%# Container.DataItem %>' CommandArgument='<%# Container.DataItem %>' OnCommand="DownloadFile" runat="server" />
<asp:Button CssClass="btn  btn-danger"  Text="Delete" CommandArgument='<%# Container.DataItem %>' OnCommand="DeleteFile" runat="server" />

                    </li>
                </ItemTemplate>
            </asp:ListView>
        </div>






  
</asp:Content>
