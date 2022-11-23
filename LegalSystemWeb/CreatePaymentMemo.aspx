<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreatePaymentMemo.aspx.cs" Inherits="LegalSystemWeb.CreatePaymentMemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container mw-100 my-4">
                <div class="row justify-content-center">

                    <div class="card o-hidden border-0 shadow-lg p-0" style="padding-left: unset; padding-right: unset; width: 1000px; top: 36px; left: 229px;">
                        <div class="card-header d-flex align-items-center justify-content-center" style="background-color: #212529; height: 50px">
                            <h3 class="text-light text-center bg-dark">Create Payment Invoice</h3>
                        </div>
                        <div class="card-body m-5">
                            <form class="user" action="">
                                <div class="row mb-3">
                                    <div class="col-3 align-middle" style="display: flex; flex-direction: row">

                                        <asp:Literal ID="Literal1" runat="server" Text="Case No"></asp:Literal>

                                    </div>
                                    <div class="col-sm-6" style="display: flex; flex-direction: row">

                                        <asp:DropDownList ID="ddlCaseNo" runat="server" CssClass="btn btn-primary dropdown-toggle w-100"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCaseNo" ErrorMessage="Required">*</asp:RequiredFieldValidator>

                                    </div>


                                </div>
                                <div class="row mb-3 mt-5">
                                    <div class="col-3 align-middle" style="display: flex; flex-direction: row">

                                        <asp:Literal ID="Literal2" runat="server" Text="Lawyer Name"></asp:Literal>

                                    </div>
                                    <div class="col-sm-6" style="display: flex; flex-direction: row">

                                        <asp:DropDownList ID="ddlLawyerName" runat="server" CssClass="btn btn-primary dropdown-toggle w-100"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlLawyerName" ErrorMessage="Required">*</asp:RequiredFieldValidator>

                                    </div>

                                </div>

                                <div class="row mb-3 mt-5">
                                    <div class="col-3 align-middle" style="display: flex; flex-direction: row">

                                        <asp:Literal ID="Literal5" runat="server" Text="Activities"></asp:Literal>

                                    </div>
                                    <div class="col-sm-6" style="display: flex; flex-direction: column">
                                        <div class="checkbox checkboxlist">
                                            <asp:CheckBoxList ID="cblActivity" runat="server" RepeatDirection="Vertical">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>

                                    <div class="row mb-3 mt-5">
                                        <div class="col-3 align-middle d-flex">

                                            <asp:Literal ID="Literal3" runat="server" Text="Total Payable Amount"></asp:Literal>

                                        </div>
                                        <div class="col-sm-6 d-flex">

                                            <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtTotalPayableAmount"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTotalPayableAmount" ErrorMessage="Required">*</asp:RequiredFieldValidator>

                                        </div>

                                    </div>

                                    <div class="row mb-6 mt-5 ">
                                        <div class="col-3 align-middle d-flex">

                                            <asp:Literal ID="Literal4" runat="server" Text="Upload Documents / Slips"></asp:Literal>

                                        </div>
                                        <div class="col-sm-6" style="display: flex; flex-direction: row">
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload Document" CssClass="btn btn-primary btn-user btn-block" />
                                        </div>

                                    </div>

                                    <div class="row mb-2 mt-4 ">
                                        <div class="d-flex justify-content-start ">

                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary btn-user btn-block m-2" BackColor="#212529" BorderColor="#212529" OnClick="btnReset_Click" />
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-user btn-block m-2" OnClick="btnSave_Click" />

                                        </div>

                                    </div>
                            </form>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
