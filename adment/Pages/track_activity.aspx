<%@ Page Title="" Language="C#" MasterPageFile="~/Source/adment.Master" AutoEventWireup="true" CodeBehind="track_activity.aspx.cs" Inherits="adment.Pages.track_activity" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">

<div class="col-lg-4">



      <div class="form-group">
    <label for="dropdown1">Select Staff:</label>
    <asp:DropDownList ID="dropdown1" runat="server" CssClass="form-control" OnSelectedIndexChanged="Dropdown1_SelectedIndexChanged" AutoPostBack="true" Required="true">
        <asp:ListItem Text="Select Staff" Value="" />
    </asp:DropDownList>
</div>
<div class="form-group">
    <label for="dropdown2">Select School:</label>
    <asp:DropDownList ID="dropdown2" runat="server" CssClass="form-control" Required="true">
        <asp:ListItem Text="Select School" Value="" />
    </asp:DropDownList>
</div>
<asp:Button ID="SubmitButton" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="SubmitButton_Click" />
                    <asp:Button ID="LinkButton1" OnClick="PrintDataLink_Click" runat="server" Text="Print Data" CssClass="btn btn-success"  />

</div>

        <div class="col-lg-8">
            <!-- Right Side Content -->

<asp:Label ID="Label1" runat="server" Text="Your Staff Not filled the School profile / or not started any data feeding" CssClass="alert alert-danger" Visible="false"></asp:Label>
<div id="progressChart" style="width: 100%; height: 300px;"></div>
            <div class="text-center mt-3">
   <div>
    

</div>
                <div>
    <label id="progressLabel"></label>
    <div id="progressDiv" class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
</div>



            </div>

        </div>
    </div>

    <asp:Label ID="Label2" CssClass="alert alert-success" role="alert" runat="server" Text="Select Staff and School"></asp:Label>

    <br />    <br />
    <br />
        <asp:LinkButton cssClass="btn btn-success  text-white" OnClick="LinkButton2_Click" ID="LinkButton2" runat="server"> Go  to Previous  Page </asp:LinkButton>

</asp:Content>

