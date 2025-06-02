<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="adminlogon.aspx.cs" Inherits="adment.Pages.adminlogon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-8 offset-md-2">
                <div class="card">
                    <div class="card-header bg-success text-white">
                        <h4 class="mb-0">Institute  LOGIN  </h4>
                    </div>
                    <div class="card-body bg-lite">
                        
                           
                        <asp:Label runat="server" Text="" ID="lblErrorMessage"></asp:Label>
                            <div class="form-group">
                               <asp:Label ID="lblUsername" runat="server" Text="Username : "></asp:Label>
<asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Required="true"></asp:TextBox>

                            </div>

                           
                            <div class="form-group">

                               <asp:Label ID="lblPassword" runat="server" Text="Password : "></asp:Label>
<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true"></asp:TextBox>
                            </div>

                       
                           
                           
                            <!-- Submit Button -->
                        <asp:Button runat="server" Text="Button" CssClass="btn btn-info"  ID="loginsuccess"  OnClick="redirectToPortal" Visible="false" />
                        <br />
<asp:Button ID="btnRegister" runat="server" Text="Submit" CssClass="btn btn-danger" OnClick="btnRegister_Click" ></asp:Button>
                      

                        <asp:LinkButton  ID="LinkButton1" runat="server" OnClick="redirectlink"   CssClass="btn btn-info" >Forget Password ? ..</asp:LinkButton>
                        <br />
                        <br />
                       

                    </div>
                </div>
            </div>
        </div>
            <asp:LinkButton cssClass="btn btn-success  text-white" OnClick="LinkButton4_Click" ID="LinkButton4" runat="server"> Go  to Previous  Page </asp:LinkButton>

    </div>
</asp:Content>
