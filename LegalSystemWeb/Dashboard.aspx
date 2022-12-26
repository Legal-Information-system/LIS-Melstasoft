<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="LegalSystemWeb.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="mt-4">Dashboard</h1>
    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">In progress cases</li>
    </ol>

    <div class="row">

        <asp:Literal ID="ltCompanyStatus" runat="server">  </asp:Literal>

    </div>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Daily added cases</li>
    </ol>

    <div class="row">
        <div class="col-xl-6">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-chart-area me-1"></i>
                    Progress for the Month
                </div>
                <div class="card-body">
                    <canvas id="progressChart" width="100%" height="40"></canvas>
                </div>
            </div>
        </div>
        <div class="col-xl-6">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-chart-bar me-1"></i>
                    Claim amount limit exceeding Cases
                </div>
                <div class="card-body">
                    <canvas id="amountChart" width="100%" height="40"></canvas>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
