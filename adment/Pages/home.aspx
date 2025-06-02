<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="adment.Pages.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            display: flex;
            justify-content: space-between;
            margin-top: 20px;
            width: 30%;
        }

        .img {
            height: 200px;
            width: 300px;
        }

        .card {
            transition: transform 0.2s ease-in-out;
            cursor: pointer;
        }

        .card:hover {
            transform: scale(1.05);
        }

        .card-header {
            background-color: #007BFF;
            color: #fff;
        }

        .card-body1 {
            background-color: #fff;
            transition: background-color 0.5s ease-in-out;
        }

        .card-body1:hover {
            background-color: #f5f5f5;
        }

        .btn-success {
            background-color: #28a745;
            color: #fff;
        }

        .btn-success:hover {
            background-color: #218838;
        }

        .btn-success:focus {
            background-color: #1e7e34;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <div class="card bg-danger text-white mb-4" onmouseover="blurOtherCard(this)">
                    <div class="card-header">Institute Login</div>
                    <div class="card-body card-body1">
                        <img src="../imgdata/header/admin.png" class="img-fluid mb-1 img" alt="Admin Image" />
                        <hr />
                        <asp:Button runat="server" Text="Click Here" class="btn btn-success" OnClick="redirectadmin" />
                    </div>
                </div>
            </div>
            <div class="col-md-6 ">
                <div class="card bg-primary text-white mb-4" onmouseover="blurOtherCard(this)">
                    <div class="card-header">Staff Login</div>
                    <div class="card-body card-body1">
                        <img src="../imgdata/header/admission.png" class="img-fluid mb-1  img" alt="Staff Image" />
                        <hr />
                        <asp:Button runat="server" Text="Click Here" class="btn btn-success" OnClick="redirectstaff" />
                    </div>
                </div>
            </div>
        </div>
    </div>

   
</asp:Content>
