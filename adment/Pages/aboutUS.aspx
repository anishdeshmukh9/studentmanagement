<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="aboutUS.aspx.cs" Inherits="adment.Pages.aboutUS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <style>
        /* Add your custom CSS styles here */
        .advertisement-banner {
            background: linear-gradient(to right, #ff6a00, #ee0979);
            color: white;
            padding: 20px;
            text-align: center;
        }
        .section-heading {
            font-size: 24px;
            font-weight: bold;
        }
        .project-guide {
            font-size: 18px;
        }
        .development-card {
            border: 1px solid #ccc;
            margin: 10px;
            padding: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="advertisement-banner">
        <h1 class="display-4">ADMENT</h1>
        <p class="lead">Your Ultimate Admission Management Solution</p>
        <p class="project-guide">  🧑‍💻 Developed By  Computer Department  of @mitpoly yeola ✨</p>
    </div>
    <div class="container mt-4">
        <h1 class="section-heading">Development  Team</h1>

        <div class="row">
            <!-- Development Card 1 -->
            <div class="col-md-3">
                <div class="development-card">
                    <h4 class=" text-info" >Anish Deshmukh</h4>
                    <p>student - TY CO 2023</p>
                    <p>MIT POLY YEOLA </p>
                </div>
            </div>
            <!-- Development Card 2 -->
            <div class="col-md-3">
                <div class="development-card">
                    <h4 class=" text-info" >Sahil Abak </h4>
                    <p>student - TY CO 2023</p>
                    <p>MIT POLY YEOLA </p>
                </div>
            </div>
            <!-- Development Card 3 -->
            <div class="col-md-3">
                <div class="development-card">
                    <h4 class="text-info" >Satish Tribhuwan</h4>
                    <p>student - TY CO 2023</p>
                    <p>MIT POLY YEOLA </p>
                </div>
            </div>
            <!-- Development Card 4 -->
            <div class="col-md-3">
                <div class="development-card">
                    <h4 class=" text-info" >Kalyani Sawkar</h4>
                    <p>student - TY CO 2023</p>
                    <p>MIT POLY YEOLA </p>
                </div>
            </div>
        </div>
    </div>


    <div class="container mt-4">
        <h1 class="section-heading"> Support & Guidance</h1>

        <div class="row">
            <!-- Development Card 1 -->
            <div class="col-md-6">
                <div class="development-card">
                    <h4 class=" text-success" >Mr. Ghorpade M.S</h4>
                    <b><p class="text-danger ">HOD</p></b>
                    <p>MIT POLY YEOLA </p>
                </div>
            </div>
            <!-- Development Card 2 -->
            <div class="col-md-6">
                <div class="development-card">
                    <h4 class=" text-success">Miss. Ghodke R.B</h4>
                    <b><p class="text-danger"> Project Guide</p></b>
                    <p>MIT POLY YEOLA </p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
