<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DailyCases.aspx.cs" Inherits="LegalSystemWeb.DailyCases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="row justify-content-center">

            <div class="card o-hidden border-0 shadow-lg p-0 my-3" style="padding-left: unset; padding-right: unset; width: 1000px;">
                <div class="card-header d-flex align-items-center justify-content-center" style="background-color: #212529; height: 50px">
                    <h3 class="text-light text-center bg-dark">Daily Case List Report</h3>
                </div>
                <div class="card-body m-5">

                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <%--<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCompany" EventName="SelectedIndexChanged" />
                            </Triggers>--%>
                        <ContentTemplate>
                            <div class="row mb-3">
                                <div class="col-3 align-middle" style="display: flex; flex-direction: row">

                                    <asp:Literal ID="Literal2" runat="server" Text="Company Name"></asp:Literal>

                                </div>
                                <div class="col-sm-6" style="display: flex; flex-direction: row">

                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="btn btn-outline-dark dropdown-toggle" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCompany" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>

                                </div>


                            </div>
                            <div class="row mb-3">
                                <div class="col-3 align-middle" style="display: flex; flex-direction: row">

                                    <asp:Literal ID="Literal3" runat="server" Text="Company Unit"></asp:Literal>

                                </div>
                                <div class="col-sm-6" style="display: flex; flex-direction: row">

                                    <asp:DropDownList ID="ddlCompanyUnit" runat="server" CssClass="btn btn-outline-dark dropdown-toggle" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyUnit_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCompanyUnit" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>

                                </div>


                            </div>
                            <%--<div class="row mb-3">
                                <div class="col-3 align-middle" style="display: flex; flex-direction: row">

                                    <asp:Literal ID="Literal1" runat="server" Text="Case No"></asp:Literal>

                                </div>
                                <div class="col-sm-6" style="display: flex; flex-direction: row">

                                    <asp:DropDownList ID="ddlCaseNo" runat="server" CssClass="btn btn-outline-dark dropdown-toggle"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCaseNo" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>

                                </div>


                            </div>--%>
                            <asp:Literal ID="ltDetails" runat="server"></asp:Literal>
                            <%--<div class="card">
                                <div class="card-body" style="padding-left: 30px;">
                                    <div class="row mb-1">
                                        <div class="col-sm-6">
                                            <p>Company</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblCompany" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mb-1">
                                        <div class="col-sm-6">
                                            <p>Claim Amount</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblClaimAmount" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mb-1">
                                        <div class="col-sm-6">
                                            <p>Created Date</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblCreateDate" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <p>Company Side</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblPlaintiff" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                    </div>
                                    <asp:Literal ID="ltPlaintifParty" runat="server">  </asp:Literal>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <p>Court</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblCourt" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <p>Court Location</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblLocationi" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <p>Case Status</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblStatus" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mb-1">
                                        <div class="col-sm-6">
                                            <p>Created User</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblUser" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <p>Judgement Type</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblJudgement" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mb-1">
                                        <div class="col-sm-6">
                                            <p>Case Outcome</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblCloseOutcome" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mb-1">
                                        <div class="col-sm-6">
                                            <p>Closed Date</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblCloseDate" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
</asp:Content>
