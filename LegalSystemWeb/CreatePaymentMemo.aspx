<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="CreatePaymentMemo.aspx.cs" Inherits="LegalSystemWeb.CreatePaymentMemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mw-100 my-4">
        <div class="row justify-content-center">

            <div class="card o-hidden border-0 shadow-lg p-0" style="padding-left: unset; padding-right: unset; width: 1000px; top: 43px; left: 236px;">
                <div class="card-header d-flex align-items-center justify-content-center" style="background-color: #212529; height: 50px">
                    <h3 class="text-light text-center bg-dark">Create Payment Invoice</h3>
                </div>
                <div class="card-body m-5">
                    <form class="user">
                        <div class="row mb-3">
                            <div class="col-3 align-middle" style="display: flex; flex-direction: row">

                                <asp:Literal ID="Literal1" runat="server" Text="Case No"></asp:Literal>

                            </div>
                            <div class="col-sm-6" style="display: flex; flex-direction: row">

                                <asp:DropDownList ID="ddlCaseNo" runat="server" CssClass="btn btn-primary dropdown-toggle w-100"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCaseNo" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>

                            </div>


                        </div>
                        <div class="row mb-3 mt-5">
                            <div class="col-3 align-middle" style="display: flex; flex-direction: row">

                                <asp:Literal ID="Literal2" runat="server" Text="Lawyer Name"></asp:Literal>

                            </div>
                            <div class="col-sm-6" style="display: flex; flex-direction: row">

                                <asp:DropDownList ID="ddlLawyerName" runat="server" CssClass="btn btn-primary dropdown-toggle w-100"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlLawyerName" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>

                            </div>

                        </div>

                        <div class="row mb-3 mt-5">
                            <div class="col-3 align-middle" style="display: flex; flex-direction: row">

                                <asp:Literal ID="Literal5" runat="server" Text="Activities"></asp:Literal>

                            </div>
                            <div class="col-sm-6" style="display: flex; flex-direction: column">
                                <div class="checkbox checkboxlist">
                                    <asp:CheckBoxList ID="cblActivity" CellPadding="5" CellSpacing="5" RepeatColumns="2" RepeatDirection="vertical" TextAlign="right" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>

                        <div class="row mb-3 mt-5">
                            <div class="col-3 align-middle d-flex">

                                <asp:Literal ID="Literal3" runat="server" Text="Total Payable Amount"></asp:Literal>

                            </div>
                            <div class="col-sm-6 d-flex">

                                <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtTotalPayableAmount" TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTotalPayableAmount" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>

                            </div>

                        </div>

                        <div class="row mb-3 mt-5">
                            <div class="col-3 align-middle d-flex">

                                <asp:Literal ID="Literal6" runat="server" Text="Remarks"></asp:Literal>

                            </div>
                            <div class="col-sm-6 d-flex">

                                <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtRemarks"></asp:TextBox>

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
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="1" />
                            </div>

                        </div>
                    </form>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
