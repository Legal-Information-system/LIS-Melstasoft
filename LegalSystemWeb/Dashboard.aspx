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


    <!-- Content Row -->
    <div class="row">

        <div class="col-lg-6 mb-4">
            <div class="card shadow mb-4">

                <div class="card-header py-3">
                    <p class="m-0">Daily Case List</p>
                </div>

                <div class="card-body">

                    <asp:Literal ID="ltDailyCase" runat="server"></asp:Literal>

                </div>

                <div class="card-footer py-3">
                    <div class="row">
                        <div class="col-8">
                            <h4 class="small">Total Daily Cases</h4>
                        </div>
                        <div class="col-4">
                            <h4 class="small d-flex flex-row-reverse"><%=DailyTotal %></h4>
                        </div>
                    </div>
                </div>

            </div>
        </div>


        <div class="col-lg-6 mb-4">
            <div class="card shadow mb-4">

                <div class="card-header py-3">
                    <p class="m-0">Monthly Case List</p>
                </div>

                <div class="card-body">

                    <asp:Literal ID="ltMonthlyCase" runat="server"></asp:Literal>

                </div>

                <div class="card-footer py-3">
                    <div class="row">
                        <div class="col-8">
                            <h4 class="small">Total Monthly Cases</h4>
                        </div>
                        <div class="col-4">
                            <h4 class="small d-flex flex-row-reverse"><%=MonthlyTotal %></h4>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </div>





</asp:Content>
