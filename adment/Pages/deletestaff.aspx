<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="deletestaff.aspx.cs" Inherits="adment.Pages.deletestaff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Red danger theme with animations and transitions */
        .red-danger-theme {
            background-color: #ff6b6b; /* Red background color */
            padding: 20px;
            border-radius: 10px;
            text-align: center;
        }

        .warning-message {
            color: #fff; /* White text color */
            font-size: 18px;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            margin: 10px 0;
            border: 2px solid #ff6b6b; /* Red border color */
            border-radius: 5px;
            background-color: #fff; /* White background color */
        }

        .btn-delete {
            background-color: #ff6b6b; /* Red background color */
            color: #fff; /* White text color */
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            transition: background-color 0.3s;
        }

        .btn-delete:hover {
            background-color: #ff4b4b; /* Darker red on hover */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="red-danger-theme">
        <h2>Delete Staff Member</h2>
        <p class="warning-message">By deleting this staff member, you will lose all data and changes made by the staff. There is no backup!</p>

        <!-- Dropdown list to select the staff member to delete -->
<asp:DropDownList ID="ddlStaffToDelete" runat="server" CssClass="form-control" AutoPostBack="true">
     
        </asp:DropDownList>

        <!-- Button to trigger the deletion -->
        <asp:Button ID="btnDeleteStaff" OnClick="deletestaffrecord" runat="server" Text="Delete Staff" CssClass="btn   btn-success"  />
    </div>

        <asp:LinkButton cssClass="btn btn-success  text-white" OnClick="LinkButton1_Click" ID="LinkButton1" runat="server"> Go  to Previous  Page </asp:LinkButton>

</asp:Content>
