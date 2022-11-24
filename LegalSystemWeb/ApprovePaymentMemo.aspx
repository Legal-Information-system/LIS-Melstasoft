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
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="card" style="width: 500px; margin-right: auto">
                <div class="card-body" style="padding-left: 30px;">
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
        <div class="table-responsive" style="width: 100%; padding-left: 40px; padding-right: 40px;">
            <asp:GridView Style="margin-top: 30px;" ID="gvPaymentDetails" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                <Columns>
                    <asp:BoundField DataField="" HeaderText="Judge Name" />
                    <asp:BoundField DataField="" HeaderText="Company Representer" />
                    <asp:BoundField DataField="" HeaderText="Action Taken" />
                    <asp:BoundField DataField="" HeaderText="Next Date" />
                    <asp:BoundField DataField="" HeaderText="Next Action" />
                    <asp:BoundField DataField="" HeaderText="Remarks" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-sm-6" style="text-align: center; padding-top: 20px;">
            <label style="">Please add Remark :</label>
        </div>
        <div class="col-sm-6" style="text-align: left">
            <asp:TextBox ID="txtPStatus" Style="width: 77%; margin-top: 20px;" runat="server"></asp:TextBox>
        </div>
        <div style="text-align: center; margin-top: 30px;">
            <asp:Button ID="Button1" runat="server" Text="Approve" Style="width: 250px; margin-right: 10px; height: 40px;" />
            <asp:Button ID="Button2" runat="server" Text="Reject" Style="width: 250px; margin-left: 10px; height: 40px;" />
        </div>
    </div>
</asp:Content>
