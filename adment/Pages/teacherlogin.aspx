<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="teacherlogin.aspx.cs" Inherits="adment.Pages.teacherlogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container mt-4">
        <div class="row">
            <div class="col-md-8 offset-md-2">
                <div class="card">
                    <div class="card-header bg-success text-white">
                        <h4 class="mb-0">Teacher/Staff LOGIN...........</h4>
                    </div>
                    <div class="card-body">

                         <div class="form-group">
                               <asp:Label ID="lblAdminUsername" runat="server" Text="Master Username : "></asp:Label>
<asp:TextBox ID="txtAdminUsername" runat="server" CssClass="form-control" Required="true"></asp:TextBox>

                            </div>
                           
                            <!-- Username -->
                            <div class="form-group">
                               <asp:Label ID="lblUsername" runat="server" Text="Username : "></asp:Label>
<asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Required="true"></asp:TextBox>

                            </div>

                            <!-- Set Password -->
                            <div class="form-group">
                               <asp:Label ID="lblPassword" runat="server" Text="Password : "></asp:Label>
<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true"></asp:TextBox>
                            </div>

                           
                       
                           
                            <!-- Submit Button -->
<asp:Button ID="btnRegister" runat="server" Text="Submit" OnClick="logTeacher" CssClass="btn btn-danger" ></asp:Button>
                      
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
