<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovePaymentMemo.aspx.cs" Inherits="LegalSystemWeb.ApprovePaymentMemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px;"></div>
    <div class="row mb-5">
        <h2 style="text-align: center; margin-bottom: 40px;">Payment Case Details</h2>
        <div class="col-sm-6">
            <div class="card" style="width: 500px; margin-left: auto;">
                <div class="card-body" style="padding-left: 30px;">
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Case ID:</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCompany" runat="server" Text="Id"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Company :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDescription" runat="server" Text="Company Name"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Company Unit :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblPlaintiff" runat="server" Text="Unit Name"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Case Description :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label1" runat="server" Text="Description"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Nature Of Case :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label2" runat="server" Text="Nature Of Case"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Plentiff :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label3" runat="server" Text="Plentiff"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Defendant :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label4" runat="server" Text="Defendant"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="card" style="width: 500px; margin-right: auto">
                <div class="card-body" style="padding-left: 30px;">
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Judge Name :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label19" runat="server" Text="Judge Name"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Company Representator :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label20" runat="server" Text="Representive"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Action Taken :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label21" runat="server" Text="Action Taken"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Next Date :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label22" runat="server" Text="Next Date"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Next Action :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label23" runat="server" Text="Next Action"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Total Payable :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label24" runat="server" Text="Total Payable"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Remarks :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label25" runat="server" Text="Remarks"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="text-align: center; margin-top: 30px;">
            <asp:Button ID="Button1" runat="server" Text="Approve" Style="width: 250px; margin-right: 10px; height: 40px;" />
            <asp:Button ID="Button2" runat="server" Text="Reject" Style="width: 250px; margin-left: 10px; height: 40px;" />
        </div>
    </div>
</asp:Content>
