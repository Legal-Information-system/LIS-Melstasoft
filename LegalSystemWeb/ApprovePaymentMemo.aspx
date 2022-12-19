<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovePaymentMemo.aspx.cs" Inherits="LegalSystemWeb.ApprovePaymentMemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px;"></div>
    <div class="row mb-5">
        <h2 style="text-align: center; margin-bottom: 40px;">Payment Case Details</h2>
        <div class="col-sm-6 mb-3 ">
            <div class="card d-flex justify-content-end" style="margin-left: auto;">
                <div class="card-body" style="padding-left: 30px;">
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Payment Id :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblPaymentId" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Case Number :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCaseNumber" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Lawyer Name :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblLawyerName" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Requested Payment Amount :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblRequestedPaymentAmount" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-sm-6 mb-3 container-fluid">
            <div class="card d-flex justify-content-start" style="margin-right: auto">
                <div class="card-body" style="padding-left: 30px;">
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Remarks :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblRemarks" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Payment Status :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblPaymentStatus" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Created Date :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCreatedDate" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6">
                            <p>Created By :</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCreatedBy" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="d-flex justify-content-center">

            <div class="card mb-4 ">

                <div class="card-header">
                    Documents
                </div>

                <div class="card-body table-responsive">
                    <asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-responsive table-hover"
                        CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                        <HeaderStyle BackColor="#212529" ForeColor="white" HorizontalAlign="center" />
                        <Columns>
                            <asp:BoundField DataField="DocumentPaymentId" HeaderText="Id" ItemStyle-HorizontalAlign="center" />
                            <asp:BoundField DataField="DocumentName" HeaderText="Name" ItemStyle-HorizontalAlign="center" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="center" HeaderText="Download Link">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnView" CssClass="btn btn-info btn-user btn-block"
                                        AutoPostBack="true" runat="server" OnClick="btnView_Click">
                                    Download File
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
        <div id="UpdateStatus">
            <%if (lblPaymentStatus.Text == "Pending")
                { %>
            <%if (Session["User_Role_Id"].ToString() == "3")
                { %>
            <div class="row mb-5">
                <div class="col-sm-4" style="text-align: end; padding-top: 20px;">
                    <label style="">Please add Remark :</label>
                </div>
                <div class="col-sm-8" style="text-align: left">
                    <asp:TextBox ID="txtRemarks" Style="width: 65%; margin-left: auto; margin-top: 20px;" runat="server"></asp:TextBox>
                </div>
            </div>
            <div style="text-align: center; margin-top: 30px;">
                <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="Approve" Style="width: 250px; margin-right: 10px; height: 40px;" />
                <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" Text="Reject" Style="width: 250px; margin-left: 10px; height: 40px;" />
            </div>
            <div class="col-sm-6 m-3">
                <asp:Label ID="lblSuccessMsg" runat="server" Text="" ForeColor="#33cc33"></asp:Label>
            </div>

            <%} %>
            <%} %>
        </div>

    </div>
</asp:Content>
