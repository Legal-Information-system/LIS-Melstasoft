<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewPaymentMemo.aspx.cs" Inherits="LegalSystemWeb.ViewPaymentMemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px;">
    </div>
    <div class="card mb-4">
        <div class="card-header">
            <i class="fas fa-table me-1"></i>
            Approve Payment Table
        </div>
        <div class="card-body table-responsive">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanged="GridView1_PageIndexChanged">
                <HeaderStyle BackColor="#212529" ForeColor="white" HorizontalAlign="center" />
                <Columns>
                    <asp:BoundField DataField="PaymentId" HeaderText="Payment Id" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField DataField="CaseNumber" HeaderText="Case Number" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField DataField="lawyer.LawyerName" HeaderText="Lawyer Name" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField DataField="Actions" HeaderText="Actions" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField DataField="Amount" HeaderText="Requested Payment Amount" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField DataField="caseMaster.payableAmount" HeaderText="Total Payable Amount" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField DataField="paymentStatus.StatusName" HeaderText="Status" ItemStyle-HorizontalAlign="center" />
                    <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnView" runat="server" OnClick="btnView_Click">View Details</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
