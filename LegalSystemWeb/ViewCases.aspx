<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCases.aspx.cs" Inherits="LegalSystemWeb.ViewCases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px;">
    </div>
    <div class="card mb-4">
        <div class="card-header">
            <i class="fas fa-table me-1"></i>
            Case Table
        </div>
        <div class="card-body table-responsive">
            <asp:GridView ID="datatablesSimple" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-responsive table-hover"
                CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                <HeaderStyle BackColor="#212529" ForeColor="white" HorizontalAlign="center" />

                <Columns>
                    <asp:BoundField DataField="CaseNumber" HeaderText="Case Number" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField DataField="CompanyId" HeaderText="Company Name" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField DataField="CompanyUnitId" HeaderText="Company Unit" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField DataField="CaseNatureId" HeaderText="Case Nature" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField DataField="CaseDescription" HeaderText="Case Description" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField DataField="ClaimAmount" HeaderText="Claim Amount" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField DataField="IsPlentif" HeaderText="IsPlentif" ItemStyle-HorizontalAlign="center" />
                    <asp:TemplateField HeaderText="View Details">
                        <ItemTemplate>
                            <asp:LinkButton ID="btndelete" runat="server">View Details</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>


    </div>

</asp:Content>
